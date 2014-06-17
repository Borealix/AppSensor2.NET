/*
import java.util.List;
import java.util.Collection;
import java.util.concurrent.CopyOnWriteArrayList;
import org.joda.time.DateTime;
import org.slf4j.Logger;

import javax.inject.Named;
*/

using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.User;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener.EventListener;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.util.DateUtils;
using log4net;
using System.Collections.ObjectModel;
using org.owasp.appsensor.criteria;
using System;
using Ninject;
using System.Collections.Generic;
using org.owasp.appsensor.util;


/**
 * This is a reference implementation of the {@link EventStore}.
 * 
 * Implementations of the {@link EventListener} interface can register with 
 * this class and be notified when new {@link Event}s are added to the data store 
 * 
 * The implementation is trivial and simply stores the {@link Event} in an in-memory collection.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage{
//@Loggable
[Named("InMemoryEventStore")]
public class InMemoryEventStore : EventStore {
	
	private ILog Logger;
	
	/** maintain a collection of {@link Event}s as an in-memory list */
    private SynchronizedCollection<Event> events = new SynchronizedCollection<Event>();
	
	/**
	 * {@inheritDoc}
	 */
	public override void addEvent(Event Event) {
		Logger.Warn("Security event " + Event.GetDetectionPoint().getId() + " triggered by user: " + Event.GetUser().getUsername());
		
		events.Add(Event);
		
		//super.notifyListeners(Event);
        base.notifyListeners(Event);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Event> findEvents(SearchCriteria criteria) {
		if (criteria == null) {
			throw new ArgumentException("criteria must be non-null");
		}
		
		//Collection<Event> matches = new List<Event>();
        Collection<Event> matches = new Collection<Event>();
		
		User user = criteria.GetUser();
		DetectionPoint detectionPoint = criteria.GetDetectionPoint();
		//Collection<string> detectionSystemIds = criteria.getDetectionSystemIds(); 
        HashSet<string> detectionSystemIds = criteria.getDetectionSystemIds(); 
		DateTime? earliest = DateUtils.fromString(criteria.getEarliest());
		
		foreach (Event Event in events) {
			//check user match if user specified
			bool userMatch = (user != null) ? user.Equals(Event.GetUser()) : true;
			
			//check detection system match if detection systems specified
			bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.Count > 0) ? 
					detectionSystemIds.Contains(Event.GetDetectionSystemId()) : true;
			
			//check detection point match if detection point specified
			bool detectionPointMatch = (detectionPoint != null) ? 
					detectionPoint.getId().Equals(Event.GetDetectionPoint().getId()) : true;
			
			bool earliestMatch = (earliest != null) ? earliest < DateUtils.fromString(Event.GetTimestamp()) : true;
					
			if (userMatch && detectionSystemMatch && detectionPointMatch && earliestMatch) {
				matches.Add(Event);
			}
		}
		
		return matches;
	}
}
}