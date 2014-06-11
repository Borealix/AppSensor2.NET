/*
import java.util.List;
import java.util.Collection;
import java.util.concurrent.CopyOnWriteArrayList;

import javax.inject.Named;

import org.joda.time.DateTime;
import org.slf4j.Logger;
*/
using org.owasp.appsensor.Response;
using org.owasp.appsensor.User;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener.ResponseListener;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.util.DateUtils;
using log4net;
using System.Collections.ObjectModel;
using org.owasp.appsensor.criteria;
using System;
using Ninject;
using org.owasp.appsensor.util;
using System.Collections.Generic;
/**
 * This is a reference implementation of the {@link ResponseStore}.
 * 
 * Implementations of the {@link ResponseListener} interface can register with 
 * this class and be notified when new {@link Response}s are added to the data store 
 * 
 * The implementation is trivial and simply stores the {@link Response} in an in-memory collection.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
//@Loggable
namespace org.owasp.appsensor.storage{
[Named("InMemoryResponseStore")]
public class InMemoryResponseStore : ResponseStore {

	private ILog Logger;

	/** maintain a collection of {@link Response}s as an in-memory list */
    private static SynchronizedCollection<Response> responses = new SynchronizedCollection<Response>();
	
	/**
	 * {@inheritDoc}
	 */
	//@Override
	public override void addResponse(Response response) {
		Logger.Warn("Security response " + response + " triggered for user: " + response.GetUser().getUsername());

		responses.Add(response);
		
		base.notifyListeners(response);
	}
	
	/**
	 * {@inheritDoc}
	 */
	//@Override
	public override Collection<Response> findResponses(SearchCriteria criteria) {
		if (criteria == null) {
			throw new ArgumentException("criteria must be non-null");
		}
		
		Collection<Response> matches = new Collection<Response>();
		
		User user = criteria.GetUser();
		Collection<string> detectionSystemIds = criteria.getDetectionSystemIds(); 
		DateTime? earliest = DateUtils.fromString(criteria.getEarliest());
		
		foreach (Response response in responses) {
			//check user match if user specified
			bool userMatch = (user != null) ? user.Equals(response.GetUser()) : true;
			
			//check detection system match if detection systems specified
			bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.Count > 0) ? 
					detectionSystemIds.Contains(response.GetDetectionSystemId()) : true;
			
			bool earliestMatch = (earliest != null) ? earliest < DateUtils.fromString(response.GetTimestamp()) : true;
					
			if (userMatch && detectionSystemMatch && earliestMatch) {
				matches.Add(response);
			}
		}
		
		return matches;
	}
	
}
}