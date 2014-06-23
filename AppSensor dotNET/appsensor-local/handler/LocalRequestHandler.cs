using Ninject;
using org.owasp.appsensor;
using org.owasp.appsensor.criteria;
using org.owasp.appsensor.exceptions;
using org.owasp.appsensor.logging;
using log4net;
using System.Collections.ObjectModel;
using org.owasp.appsensor.util;
/**
 * This is the local endpoint that handles requests on the server-side.
 * 
 * Since this is a local implementation, there is no need for access control.
 * There are no requests coming from anywhere other than self, so it's trusted. 
 * 
 * Additionally, client/server is actually just an API call in the same JVM instance, 
 * but is separated to maintain the architectural design. Simple delegation 
 * lets us use the same pattern here. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.handler {
//@Loggable
// [Named("LocalRequestHandler")]
public class LocalRequestHandler : RequestHandler {

	//@SuppressWarnings("unused")
	private ILog Logger;
	
	[Inject]
	private AppSensorServer appSensorServer;
	
	private static string detectionSystemId = null;	//start with blank
	
	/**
	 * {@inheritDoc}
	 */
	//public override void addEvent(Event Event) {
    public void addEvent(Event Event) {
        /// <exception cref="NotAuthorizedException"></exception>
		if (detectionSystemId == null) {
			detectionSystemId = Event.GetDetectionSystemId();
		}
		
		appSensorServer.getEventStore().addEvent(Event);
	}

	/**
	 * {@inheritDoc}
	 */
	//public override void addAttack(Attack attack) {
    public void addAttack(Attack attack) {
        /// <exception cref="NotAuthorizedException"></exception>
		if (detectionSystemId == null) {
			detectionSystemId = attack.GetDetectionSystemId();
		}
		
		appSensorServer.getAttackStore().addAttack(attack);
	}

	/**
	 * {@inheritDoc}
	 */
	//public override Collection<Response> getResponses(string earliest) {
    public Collection<Response> getResponses(string earliest) {
        /// <exception cref="NotAuthorizedException"></exception>
		SearchCriteria criteria = new SearchCriteria().
                setDetectionSystemIds(StringUtils.toCollection(detectionSystemId != null ? detectionSystemId : "")).
				setEarliest(earliest);
		
		return appSensorServer.getResponseStore().findResponses(criteria);
	}
}
}