/*
import java.util.Collection;

import javax.inject.Inject;
import javax.inject.Named;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.container.ContainerRequestContext;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
*/
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.RequestHandler;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.accesscontrol.Action;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using org.owasp.appsensor.rest.AccessControlUtils;
using org.owasp.appsensor.rest.filter.ClientApplicationIdentificationFilter;
using org.owasp.appsensor.util.StringUtils;
using org.owasp.appsensor.rest;
using System.Collections.ObjectModel;
using Ninject;
using org.owasp.appsensor.accesscontrol;
using org.owasp.appsensor.criteria;

/**
 * This is the restful endpoint that handles requests on the server-side. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.handler {
//@Path("/api/v1.0")
//@Produces(MediaType.APPLICATION_JSON)
//@Consumes(MediaType.APPLICATION_JSON)
[Named ("")]
[Named ("RestRequestHandler")]
public class RestRequestHandler : RequestHandler {

    [Inject]
	private AppSensorServer appSensorServer;
	
	[Inject]
	private AccessControlUtils accessControlUtils;
	
	//@Context
	private ContainerRequestContext requestContext;
	
	/**
	 * {@inheritDoc}
	 */
	//@Override
	//@POST
	//@Path("/events")
	/// <exception cref="NotAuthorizedException"></exception>
    public override void addEvent(Event Event){
		accessControlUtils.checkAuthorization(Action.ADD_EVENT, requestContext);
		
		Event.setDetectionSystemId(getClientApplicationName());
		
		appSensorServer.getEventStore().addEvent(Event);
	}

	/**
	 * {@inheritDoc}
	 */
	//@Override
	//@POST
	//@Path("/attacks")
    /// <exception cref="NotAuthorizedException"></exception>
	public override void addAttack(Attack attack) {
		accessControlUtils.checkAuthorization(Action.ADD_ATTACK, requestContext);
		
		attack.setDetectionSystemId(getClientApplicationName());
		
		appSensorServer.getAttackStore().addAttack(attack);
	}

	/**
	 * {@inheritDoc}
	 */
	//@Override
	//@GET
	//@Path("/responses")
	//@Produces(MediaType.APPLICATION_JSON)
    /// <exception cref="NotAuthorizedException"></exception>
	public override Collection<Response> getResponses(@QueryParam("earliest") string earliest) {
		accessControlUtils.checkAuthorization(Action.GET_RESPONSES, requestContext);

		SearchCriteria criteria = new SearchCriteria().
				setDetectionSystemIds(StringUtils.toCollection(getClientApplicationName())).
				setEarliest(earliest);

		return appSensorServer.getResponseStore().findResponses(criteria);
	}
	
	/**
	 * Helper method to retrieve client application name.
	 * This is set by the {@link ClientApplicationIdentificationFilter} 
	 * 
	 * @return client application name
	 */
	private string getClientApplicationName() {
		string clientApplicationName = (string)requestContext.getProperty(APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR);
		
		return clientApplicationName;
	}
}	
}