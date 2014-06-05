/*
import java.io.File;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.util.List;
import java.util.Arrays;
import java.util.Collection;

import javax.inject.Inject;
import javax.inject.Named;

import org.joda.time.DateTime;
 */
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.User;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener.ResponseListener;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.util.DateUtils;
using org.owasp.appsensor.util.FileUtils;

//import com.google.gson.Gson;

/**
 * This is a reference implementation of the {@link ResponseStore}.
 * 
 * Implementations of the {@link ResponseListener} interface can register with 
 * this class and be notified when new {@link Response}s are added to the data store 
 * 
 * This implementation is file-based and has the feature that it will load previous 
 * {@link Response}s if configured to do so.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
using org.owasp.appsensor.storage;
using log4net;
using org.owasp.appsensor;
using System.IO;
using System.Collections.ObjectModel;
using org.owasp.appsensor.criteria;
using System;
using Ninject;
using org.owasp.appsensor.util;
namespace org.owasp.appsensor.storage{
[Named ("")]
//@Loggable
[Named("FileBasedResponseStore")]
public class FileBasedResponseStore : ResponseStore {

	private ILog Logger;
	
	//@SuppressWarnings("unused")
	[Inject]
	private AppSensorServer appSensorServer;
	
	public static string DEFAULT_FILE_PATH = File.separator + "tmp";
	
	public static string DEFAULT_FILE_NAME = "responses.txt";
	
	public static string FILE_PATH_CONFIGURATION_KEY = "filePath";
	
	public static string FILE_NAME_CONFIGURATION_KEY = "fileName";
	
	private Gson gson = new Gson();
	
	private Path path = null;
	
	/**
	 * {@inheritDoc}
	 */
	public override void addResponse(Response response) {
		Logger.Warn("Security response " + response + " triggered for user: " + response.GetUser().getUsername());

		writeResponse(response);
		
		super.notifyListeners(response);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Response> findResponses(SearchCriteria criteria) {
		if (criteria == null) {
			throw new IllegalArgumentException("criteria must be non-null");
		}
		
		Collection<Response> matches = new List<Response>();
		
		User user = criteria.GetUser();
		Collection<string> detectionSystemIds = criteria.getDetectionSystemIds(); 
		DateTime earliest = DateUtils.fromString(criteria.getEarliest());
		
		Collection<Response> responses = loadResponses();
		
		foreach (Response response in responses) {
			//check user match if user specified
			bool userMatch = (user != null) ? user.Equals(response.GetUser()) : true;
			
			//check detection system match if detection systems specified
			bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.size() > 0) ? 
					detectionSystemIds.Contains(response.GetDetectionSystemId()) : true;
			
			bool earliestMatch = (earliest != null) ? earliest.isBefore(DateUtils.fromString(response.GetTimestamp())) : true;
					
			if (userMatch && detectionSystemMatch && earliestMatch) {
				matches.Add(response);
			}
		}
		
		return matches;
	}
	
	protected void writeResponse(Response response) {
		string json = gson.toJson(response);
		
		try {
			Files.write(getPath(), Arrays.asList(json), StandardCharsets.UTF_8, StandardOpenOption.APPEND, StandardOpenOption.WRITE);
		} catch (IOException e) {
			Logger.Error("Error occurred loading writing event file to path: " + getPath(), e);
		}
	}
	
	protected Collection<Response> loadResponses() {
		Collection<Response> responses = new List<>();
		
		try {
			Collection<String> lines = Files.readAllLines(getPath());
			
			foreach (string line in lines) {
				Response response = gson.fromJson(line, Response.Class);
				
				responses.Add(response);
			}
		} catch (IOException e) {
			Logger.Error("Error occurred loading configured event file from path: " + getPath(), e);
		}
		
		return responses;
	}
	
	protected Path getPath() {
		if (path != null && Files.exists(path)) {
			return path;
		}
		
		path = FileUtils.getOrCreateFile(lookupFilePath(), lookupFileName());
		
		return path;
	}
	
	protected string lookupFilePath() {
		return DEFAULT_FILE_PATH;
	}
	
	protected string lookupFileName() {
		return DEFAULT_FILE_NAME;
	}
	
}
}