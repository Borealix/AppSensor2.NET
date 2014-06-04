/*
import java.util.Collection;

import javax.inject.Inject;
import javax.inject.Named;
import javax.ws.rs.GET;
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
using org.owasp.appsensor.Response;
using org.owasp.appsensor.accesscontrol.Action;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using org.owasp.appsensor.rest.AccessControlUtils;
using org.owasp.appsensor.rest;
using System.Collections.ObjectModel;
using Ninject;
using org.owasp.appsensor.criteria;

/**
 * This is the restful endpoint that handles reporting requests on the server-side. 
 * 
 * This simple RESTful implementation queries the appropriate *Store implementations 
 * for matching entities.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.reporting {
//@Path("/api/v1.0/reports")
//@Produces(MediaType.APPLICATION_JSON)
[Named ("")]
[Named ("RestReportingEngine")]
public class RestReportingEngine : ReportingEngine {

    [Inject]
	private AppSensorServer appSensorServer;
	
	[Inject]
	private AccessControlUtils accessControlUtils;
	
	//@Context
	private ContainerRequestContext requestContext;
	
	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Event Event) { }

	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Attack attack) { }

	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Response response) { }
	
	/**
	 * {@inheritDoc}
	 */
	//@GET
	//@Path("/events")
    /// <exception cref="NotAuthorizedException"></exception>
	public override Collection<Event> findEvents(@QueryParam("earliest") string earliest) {
		accessControlUtils.checkAuthorization(Action.EXECUTE_REPORT, requestContext);
		
		SearchCriteria criteria = new SearchCriteria().setEarliest(earliest);
		
		return appSensorServer.getEventStore().findEvents(criteria);
	}
	
	/**
	 * {@inheritDoc}
	 */
	//@Override
	//@GET
	//@Path("/attacks")
	public override Collection<Attack> findAttacks(@QueryParam("earliest") string earliest) {
		accessControlUtils.checkAuthorization(Action.EXECUTE_REPORT, requestContext);
		
		SearchCriteria criteria = new SearchCriteria().setEarliest(earliest);
		
		return appSensorServer.getAttackStore().findAttacks(criteria);
	}
	
	/**
	 * {@inheritDoc}
	 */
	//@Override
	//@GET
	//@Path("/responses")
	public override Collection<Response> findResponses(@QueryParam("earliest") string earliest) {
		accessControlUtils.checkAuthorization(Action.EXECUTE_REPORT, requestContext);
		
		SearchCriteria criteria = new SearchCriteria().setEarliest(earliest);
		
		return appSensorServer.getResponseStore().findResponses(criteria);
	}
}
}