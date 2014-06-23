using System.Collections.ObjectModel;
using System;
using org.owasp.appsensor.util;
using Ninject;
using org.owasp.appsensor;
using org.owasp.appsensor.logging;
using org.owasp.appsensor.storage;
using org.owasp.appsensor.criteria;
using log4net;
/**
 * This is a statistical {@link Event} analysis engine, 
 * and is an implementation of the Observer pattern. 
 * 
 * It is notified with implementations of the {@link Event} class.
 * 
 * The implementation performs a simple analysis that watches the configured {@link Threshold} and 
 * determines if it has been crossed. If so, an {@link Attack} is created and added to the 
 * {@link AttackStore}. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.analysis {
//@Loggable
// [Named("ReferenceEventAnalysisEngine")]
public class ReferenceEventAnalysisEngine : EventAnalysisEngine {

	private ILog Logger;
	
	[Inject]
	private AppSensorServer appSensorServer;
	
	/**
	 * This method analyzes statistical {@link Event}s that are added to the system and 
	 * detects if the configured {@link Threshold} has been crossed. If so, an {@link Attack} is 
	 * created and added to the system.
	 * 
	 * @param event the {@link Event} that was added to the {@link EventStore}
	 */
	//public override void analyze(Event Event) {
    public void analyze(Event Event) {
		SearchCriteria criteria = new SearchCriteria().
				setUser(Event.GetUser()).
				setDetectionPoint(Event.GetDetectionPoint()).
				setDetectionSystemIds(appSensorServer.getConfiguration().getRelatedDetectionSystems(Event.GetDetectionSystemId()));

		Collection<Event> existingEvents = appSensorServer.getEventStore().findEvents(criteria);

		DetectionPoint configuredDetectionPoint = appSensorServer.getConfiguration().findDetectionPoint(Event.GetDetectionPoint());
		
		int eventCount = countEvents(configuredDetectionPoint.getThreshold().getInterval().toMillis(), existingEvents, Event);

		//4 examples for the below code
		//1. count is 5, t.count is 10 (5%10 = 5, No Violation)
		//2. count is 45, t.count is 10 (45%10 = 5, No Violation) 
		//3. count is 10, t.count is 10 (10%10 = 0, Violation Observed)
		//4. count is 30, t.count is 10 (30%10 = 0, Violation Observed)

		int thresholdCount = configuredDetectionPoint.getThreshold().getCount();

		if (eventCount % thresholdCount == 0) {
			Logger.Info("Violation Observed for user <" + Event.GetUser().getUsername() + "> - storing attack");
			//have determined this event triggers attack
			appSensorServer.getAttackStore().addAttack(new Attack(Event));
		}
	}
	
	/**
	 * Count the number of {@link Event}s over a time {@link Interval} specified in milliseconds.
	 * 
	 * @param intervalInMillis {@link Interval} as measured in milliseconds
	 * @param existingEvents set of {@link Event}s matching triggering {@link Event} id/user pulled from {@link Event} storage
	 * @return number of {@link Event}s matching time {@link Interval}
	 */
	protected int countEvents(long intervalInMillis, Collection<Event> existingEvents, Event triggeringEvent) {
		int count = 0;
		
		//grab the startTime to begin counting from based on the current time - interval
        //DateTime startTime = DateUtils.getCurrentTimestamp().MinusMillis((int)intervalInMillis);
        DateTime startTime = DateUtils.getCurrentTimestamp().AddMilliseconds(-(intervalInMillis));
		//count events after most recent attack.
		DateTime? mostRecentAttackTime = findMostRecentAttackTime(triggeringEvent);
		
		foreach (Event Event in existingEvents) {
			DateTime? eventTimestamp = DateUtils.fromString(Event.GetTimestamp());
			//ensure only events that have occurred since the last attack are considered
			// if (eventTimestamp.isAfter(mostRecentAttackTime)) {
            if (eventTimestamp > mostRecentAttackTime) {
				if (intervalInMillis > 0) {
					// if (DateUtils.fromString(Event.GetTimestamp()).IsAfter(startTime)) {
                    if (DateUtils.fromString(Event.GetTimestamp()) > startTime) {
						//only increment when event occurs within specified interval
						count++;
					}
				} else {
					//no interval - all events considered
					count++;
				}
			}
		}
		
		return count;
	}
	
	/**
	 * Find most recent {@link Attack} matching the given {@link Event} ({@link User}, {@link DetectionPoint}, detection system)
	 * and find it's timestamp. 
	 * 
	 * The {@link Event} should only be counted if they've occurred after the most recent {@link Attack}.
	 * 
	 * @param event {@link Event} to use to find matching {@link Attack}s
	 * @return timestamp representing last matching {@link Attack}, or -1L if not found
	 */
	protected DateTime? findMostRecentAttackTime(Event Event) {
		DateTime? newest = DateUtils.epoch();
		
		SearchCriteria criteria = new SearchCriteria().
				setUser(Event.GetUser()).
				setDetectionPoint(Event.GetDetectionPoint()).
				setDetectionSystemIds(appSensorServer.getConfiguration().getRelatedDetectionSystems(Event.GetDetectionSystemId()));
		
		Collection<Attack> attacks = appSensorServer.getAttackStore().findAttacks(criteria);
		
		foreach (Attack attack in attacks) {
            // if (DateUtils.fromString(attack.GetTimestamp()).isafter(newest)) {
            if (DateUtils.fromString(attack.GetTimestamp())>newest) {
				newest = DateUtils.fromString(attack.GetTimestamp());
			}
		}
		return newest;
	}	
}
}