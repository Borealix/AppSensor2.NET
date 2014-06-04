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
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.User;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener.EventListener;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.util.DateUtils;
using org.owasp.appsensor.util.FileUtils;
using org.slf4j.Logger;
using log4net.Repository.Hierarchy;
using System.IO;
using System.Collections.ObjectModel;
using org.owasp.appsensor.criteria;
using System;
using Ninject;
using org.owasp.appsensor.util;

// import com.google.gson.Gson;

/**
 * This is a reference implementation of the {@link EventStore}.
 * 
 * Implementations of the {@link EventListener} interface can register with 
 * this class and be notified when new {@link Event}s are added to the data store 
 * 
 * This implementation is file-based and has the feature that it will load previous 
 * {@link Event}s if configured to do so.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage {
[Named ("")]
//@Loggable
[Named("")]
public class FileBasedEventStore : EventStore {
	
	private Logger logger;
	
	//@SuppressWarnings("unused")
	[Inject]
	private AppSensorServer appSensorServer;
	
	public static string DEFAULT_FILE_PATH = File.separator + "tmp";
	
	public static string DEFAULT_FILE_NAME = "events.txt";
	
	public static string FILE_PATH_CONFIGURATION_KEY = "filePath";
	
	public static string FILE_NAME_CONFIGURATION_KEY = "fileName";
	
	private Gson gson = new Gson();
	
	private Path path = null;
	
	/**
	 * {@inheritDoc}
	 */
	public override void addEvent(Event Event) {
		Logger.warn("Security event " + Event.GetDetectionPoint().getId() + " triggered by user: " + Event.GetUser().getUsername());
		
		writeEvent(Event);
		
		super.notifyListeners(Event);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Event> findEvents(SearchCriteria criteria) {
		if (criteria == null) {
			throw new IllegalArgumentException("criteria must be non-null");
		}
		
		Collection<Event> matches = new List<Event>();
		
		User user = criteria.GetUser();
		DetectionPoint detectionPoint = criteria.GetDetectionPoint();
		Collection<string> detectionSystemIds = criteria.getDetectionSystemIds(); 
		DateTime earliest = DateUtils.fromString(criteria.getEarliest());
		
		Collection<Event> events = loadEvents();
		
		foreach (Event Event in events) {
			//check user match if user specified
			bool userMatch = (user != null) ? user.Equals(Event.GetUser()) : true;
			
			//check detection system match if detection systems specified
			bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.size() > 0) ? 
					detectionSystemIds.Contains(Event.GetDetectionSystemId()) : true;
			
			//check detection point match if detection point specified
			bool detectionPointMatch = (detectionPoint != null) ? 
					detectionPoint.getId().Equals(Event.GetDetectionPoint().getId()) : true;
			
			bool earliestMatch = (earliest != null) ? earliest.isBefore(DateUtils.fromString(Event.GetTimestamp())) : true;
			
			if (userMatch && detectionSystemMatch && detectionPointMatch && earliestMatch) {
				matches.Add(Event);
			}
		}
		
		return matches;
	}
	
	protected void writeEvent(Event Event) {
		string json = gson.toJson(Event);
		
		try {
			Files.write(getPath(), Arrays.asList(json), StandardCharsets.UTF_8, StandardOpenOption.APPEND, StandardOpenOption.WRITE);
		} catch (IOException e) {
			Logger.error("Error occurred loading writing event file to path: " + getPath(), e);
		}
	}
	
	protected Collection<Event> loadEvents() {
		Collection<Event> events = new List<>();
		
		try {
			Collection<String> lines = Files.readAllLines(getPath());
			
			foreach (string line in lines) {
				Event Event = gson.fromJson(line, Event.Class);
				
				events.Add(Event);
			}
		} catch (IOException e) {
			Logger.error("Error occurred loading configured event file from path: " + getPath(), e);
		}
		
		return events;
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