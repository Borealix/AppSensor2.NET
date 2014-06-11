/*
import java.util.Collection;
import java.util.List;
import java.util.Map;

import javax.annotation.PostConstruct;
import javax.annotation.Resource;
import javax.inject.Inject;
import javax.inject.Named;
import javax.jws.HandlerChain;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.xml.ws.WebServiceContext;
import javax.xml.ws.handler.MessageContext;
*/
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.ClientApplication;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.accesscontrol.Action;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using org.owasp.appsensor.util.StringUtils;
using System.Collections.ObjectModel;
using Ninject;
using System.Collections.Generic;
using org.owasp.appsensor.accesscontrol;
using org.owasp.appsensor.criteria;
using org.owasp.appsensor.util;
using System.Web.Services;

/**
 * This is the soap endpoint that handles requests on the server-side. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.handler {
[WebService(
        portname = "SoapRequestHandlerPort",
        serviceName = "SoapRequestHandlerService",
        Namespace = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl",
        endpointInterface = "org.owasp.appsensor.handler.SoapRequestHandler"
        )]
@HandlerChain(file="handler-chain.xml")
[Named ("ReferenceSoapRequestHandler")]
public class ReferenceSoapRequestHandler : SoapRequestHandler  {//extends SpringBeanAutowiringSupport 
	
	//@Resource 
	private WebServiceContext wsContext;
	
	[Inject]
	private AppSensorServer appSensorServer;
	
	/**
	 * {@inheritDoc}
	 */
	//@WebMethod

    /// <exception cref="InvalidOperationException"></exception>
	public override void addEvent(Event Event) {
		checkAuthorization(Action.ADD_EVENT);
		
		Event.setDetectionSystemId(getClientApplicationName());
		
		appSensorServer.getEventStore().addEvent(Event);
	}

	/**
	 * {@inheritDoc}
	 */
	//@WebMethod
    /// <exception cref="NotAuthorizedException"></exception>
	public override void addAttack(Attack attack) {
		checkAuthorization(Action.ADD_ATTACK);
		
		attack.setDetectionSystemId(getClientApplicationName());
		
		appSensorServer.getAttackStore().addAttack(attack);
	}

	/**
	 * {@inheritDoc}
	 */
	//@WebMethod
	/// <exception cref="NotAuthorizedException"></exception>
	public override Collection<Response> getResponses(string earliest) {
		checkAuthorization(Action.GET_RESPONSES);
		
		SearchCriteria criteria = new SearchCriteria().
				setDetectionSystemIds(StringUtils.toCollection(getClientApplicationName())).
				setEarliest(earliest);
		
		return appSensorServer.getResponseStore().findResponses(criteria);
	}
	
	/**
	 * Check authz before performing action.
	 * @param action desired action
	 * @throws NotAuthorizedException thrown if user does not have role.
	 */
    /// <exception cref="NotAuthorizedException"></exception>
	private void checkAuthorization(Action action) {
		@SuppressWarnings("unchecked")
		Map<string, List<string>> httpHeaders = (Map<String, List<string>>) wsContext.getMessageContext().get(MessageContext.HTTP_REQUEST_HEADERS);
		string clientApplicationName = httpHeaders.get(APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR).get(0);

		ClientApplication clientApplication = appSensorServer.getConfiguration().findClientApplication(clientApplicationName);
		
		org.owasp.appsensor.accesscontrol.Context context = new org.owasp.appsensor.accesscontrol.Context();
		
		appSensorServer.getAccessController().assertAuthorized(clientApplication, action, context);
	}
	
	private string getClientApplicationName() {
		@SuppressWarnings("unchecked")
		Map<string, List<string>> httpHeaders = (Map<string, List<String>>) wsContext.getMessageContext().get(MessageContext.HTTP_REQUEST_HEADERS);
		
		string clientApplicationName = httpHeaders.get(APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR).get(0);
		
		return clientApplicationName;
	}

	//hack workaround b/c DI doesn't work for jax-ws handlers with base spring
	//@PostConstruct
	public void init() {
		ClientApplicationIdentificationHandler.setAppSensorServer(appSensorServer);
	}

}
}