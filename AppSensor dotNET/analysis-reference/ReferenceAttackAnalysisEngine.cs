/*
import java.util.List;
import java.util.Collection;
import javax.inject.Inject;
import javax.inject.Named;
import org.slf4j.Logger;
 */
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.Interval;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.storage.AttackStore;
using org.owasp.appsensor.storage.ResponseStore;
using org.owasp.appsensor.criteria;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Ninject;
using System;
using log4net;

/**
 * This is the reference {@link Attack} analysis engine, 
 * and is an implementation of the Observer pattern. 
 * 
 * It is notified with implementations of the {@link Attack} class. 
 * 
 * The implementation performs a simple analysis that checks the created attack against any created {@link Response}s. 
 * It then creates a {@link Response} and adds it to the {@link ResponseStore}. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.analysis {
//@Loggable
[Named("ReferenceAttackAnalysisEngine")]
public class ReferenceAttackAnalysisEngine : AttackAnalysisEngine {

    //private Logger logger;
    private ILog Logger;

	[Inject]
	private AppSensorServer appSensorServer;
	
	/**
	 * This method analyzes {@link Attack} objects that are added 
	 * to the system (either via direct addition or generated by the event analysis
	 * engine), generates an appropriate {@link Response} object, 
	 * and adds it to the configured {@link ResponseStore} 
	 * 
	 * @param event the {@link Attack} that was added to the {@link AttackStore}
	 */
	public override void analyze(Attack attack) {
		if (attack != null) {
			Response response = findAppropriateResponse(attack);
			
			if (response != null) {
                //Logger.Info("Response set for user <" + attack.GetUser().getUsername() + "> - storing response action " + response.getAction());
                Logger.Info ("Response set for user <" + attack.GetUser().getUsername() + "> - storing response action " + response.getAction());
				appSensorServer.getResponseStore().addResponse(response);
			}
		}
	}
	
	/**
	 * Find/generate {@link Response} appropriate for specified {@link Attack}.
	 * 
	 * @param attack {@link Attack} that is being analyzed
	 * @return {@link Response} to be executed for given {@link Attack}
	 */
	protected Response findAppropriateResponse(Attack attack) {
		DetectionPoint triggeringDetectionPoint = attack.GetDetectionPoint();
		
		SearchCriteria criteria = new SearchCriteria().
				setUser(attack.GetUser()).
				setDetectionPoint(triggeringDetectionPoint).
				setDetectionSystemIds(appSensorServer.getConfiguration().getRelatedDetectionSystems(attack.GetDetectionSystemId()));
		
		//grab any existing responses
		Collection<Response> existingResponses = appSensorServer.getResponseStore().findResponses(criteria);
		
		string responseAction = null;
		Interval interval = null;

        //Collection<Response> possibleResponses = findPossibleResponses(triggeringDetectionPoint);
        Collection<Response> possibleResponses = findPossibleResponses(triggeringDetectionPoint);

		//if (existingResponses == null || existingResponses.Size() == 0) {
        if(existingResponses == null || existingResponses.Count == 0) {
			//no responses yet, just grab first configured response from detection point
            // Response response = possibleResponses.iterator().next();
            IEnumerator <Response> enumerator = possibleResponses.GetEnumerator();
            enumerator.MoveNext();
            Response response = enumerator.Current;
			
			responseAction = response.getAction();
			interval = response.getInterval();
		} else {
			foreach (Response configuredResponse in possibleResponses) {
				responseAction = configuredResponse.getAction();
				interval = configuredResponse.getInterval();

				if (! isPreviousResponse(configuredResponse, existingResponses)) {
					//if we find that this response doesn't already exist, use it
					break;
				}
				
				//if we reach here, we will just use the last configured response (repeat last response)
			}
		}
		
		if(responseAction == null) {
            //throw new IllegalArgumentException("No appropriate response was configured for this detection point: " + triggeringDetectionPoint.getId());
            throw new ArgumentException("No appropriate response was configured for this detection point: " + triggeringDetectionPoint.getId());
		}
		
		Response responses = new Response();
		responses.setUser(attack.GetUser());
		responses.setTimestamp(attack.GetTimestamp());
		responses.setAction(responseAction);
		responses.setInterval(interval);
		responses.setDetectionSystemId(attack.GetDetectionSystemId());
		
		return responses;
	}
	
	/**
	 * Lookup configured {@link Response} objects for specified {@link DetectionPoint}
	 * 
	 * @param triggeringDetectionPoint {@link DetectionPoint} that triggered {@link Attack}
	 * @return collection of {@link Response} objects for given {@link DetectionPoint}
	 */
	//protected Collection<Response> findPossibleResponses(DetectionPoint triggeringDetectionPoint) {
    protected List<Response> findPossibleResponses(DetectionPoint triggeringDetectionPoint) {
		//Collection<Response> possibleResponses = new List<Response>();
        List<Response> possibleResponses = new List<Response>();
		
		foreach (DetectionPoint configuredDetectionPoint in appSensorServer.getConfiguration().getDetectionPoints()) {
			if (configuredDetectionPoint.getId().Equals(triggeringDetectionPoint.getId())) {
				possibleResponses = configuredDetectionPoint.getResponses();
				break;
			}
		}
		return possibleResponses;
	}
	/**
	 * Test a given {@link Response} to see if it's been executed before.
	 * 
	 * @param response {@link Response} to test to see if it's been executed before
	 * @param existingResponses set of previously executed {@link Response}s
	 * @return true if {@link Response} has been executed before
	 */
	protected bool isPreviousResponse(Response response, Collection<Response> existingResponses) {
		bool previousResponse = false;
		
		foreach (Response existingResponse in existingResponses) {
			if (response.getAction().Equals(existingResponse.getAction())) {
				previousResponse = true;
			}
		}	
		return previousResponse;
	}
}
}