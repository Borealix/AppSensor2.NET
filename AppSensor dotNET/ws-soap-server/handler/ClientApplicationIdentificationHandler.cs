using Ninject;
/*
import java.util.List;
import java.util.Map;
import java.util.Set;

import javax.inject.Named;
import javax.xml.namespace.QName;
import javax.xml.ws.handler.MessageContext;
import javax.xml.ws.handler.soap.SOAPHandler;
import javax.xml.ws.handler.soap.SOAPMessageContext;

using org.springframework.stereotype.Service;
 */
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.exceptions.NotAuthenticatedException;
using System;
using System.Collections.Generic;

/**
 * This is the jax-ws soap handler that performs
 * authentication of the client applications. 
 * 
 * The authentication mechanism involves checking an HTTP request header 
 * for the username of the given client application. 
 * 
 * NOTE: This means that implementors must ensure that end users are not able 
 * to make direct requests to the service or it will be possible to masquerade 
 * as a valid client application. 
 * 
 * The intended deployment scenario is to use a standard reverse proxy setup 
 * whereby a web server or agent of some kind performs the authentication 
 * (SSO, HTTP Basic Auth, etc.) and then sets the request header key-value pair, 
 * and then forwards the request to the servlet container/app server where 
 * this soap handler then processes the request. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.handler {
[Named("ClientApplicationIdentificationHandler")]
//@Service
public class ClientApplicationIdentificationHandler : SOAPHandler<SOAPMessageContext> {
	
	/** default name for client application identification header */
	private static string HEADER_NAME = "X-Appsensor-Client-Application-Name";
	
	private static bool checkedConfigurationHeaderName = false;
	
	private static AppSensorServer appSensorServer;
	
	public override bool handleMessage(SOAPMessageContext context) {
		//should only run on first request
		if (! checkedConfigurationHeaderName) {
			updateHeaderFromConfiguration();
			checkedConfigurationHeaderName = true;
		}
				
	    //check inbound headers
		if (! (Boolean)context.get(MessageContext.MESSAGE_OUTBOUND_PROPERTY)) {
			
			@SuppressWarnings("unchecked")
			Map<string, List<string>> httpHeaders = (Map<string, List<string>>) context.get(MessageContext.HTTP_REQUEST_HEADERS);
			List<string> clientApplicationIdentifier = httpHeaders.get(HEADER_NAME);
			
			// Get the client application identifier passed in HTTP headers parameters
		    if (clientApplicationIdentifier == null || clientApplicationIdentifier.get(0) == null) {
		    	throw new NotAuthenticatedException("Access requires sending configured client application identification header.");
		    }
		    
		    //set the specific header for the client app
		    httpHeaders.put(ReferenceSoapRequestHandler.APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR, clientApplicationIdentifier);
		}

		return true;
	}

	private void updateHeaderFromConfiguration() {
		string configuredHeaderName = appSensorServer.getConfiguration().getClientApplicationIdentificationHeaderName();
		
		if (configuredHeaderName != null && configuredHeaderName.Trim().Length() > 0) {
			HEADER_NAME = configuredHeaderName;
		}
	}
	
	public override bool handleFault(SOAPMessageContext context) {
		return true;
	}

	public override void close(MessageContext context) {
		//
	}

	public override Set<QName> getHeaders() {
		return null;
	}
	
	//hack workaround b/c DI doesn't work for jax-ws handlers with base spring
	public static void setAppSensorServer(AppSensorServer appSensorServer) {
		ClientApplicationIdentificationHandler.appSensorServer = appSensorServer;
	}
	
}
}