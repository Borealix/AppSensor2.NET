/*
import javax.inject.Inject;
import javax.inject.Named;

import org.slf4j.Logger;
*/
using org.owasp.appsensor.Responses;
using org.owasp.appsensor.AppSensorClient;
using org.owasp.appsensor.logging.Loggable;
using Ninject;
using System;
using log4net;
/**
 * This class should only be used as the server-side response handler
 * if you are in local mode. Otherwise, use a NO-OP implementation 
 * on the server-side.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.response {
//@Loggable
[Named("LocalResponseHandler")]
public class LocalResponseHandler : ResponseHandler {

	/** Logger */
	private ILog Logger;
	
	[Inject]
	private AppSensorClient appSensorClient;
	
	/**
	 * {@inheritDoc}
	 */
	public override void handle(Response response) {

        if(org.owasp.appsensor.Responses.ResponseHandler.LOG.Equals(response.getAction())) {
			Logger.Error("Response executed for user:" + response.GetUser().getUsername() + 
					", Action: Increased Logging");
        } else if(org.owasp.appsensor.Responses.ResponseHandler.LOGOUT.Equals(response.getAction())) {
			Logger.Error("Response executed for user:" + response.GetUser().getUsername() + 
					", Action: Logging out malicious account");
			
			appSensorClient.getUserManager().logout(response.GetUser());
        } else if(org.owasp.appsensor.Responses.ResponseHandler.DISABLE_USER.Equals(response.getAction())) {
			Logger.Error("Response executed for user:" + response.GetUser().getUsername() + 
					", Action: Disabling malicious account");
			
			appSensorClient.getUserManager().logout(response.GetUser());
        } else if(org.owasp.appsensor.Responses.ResponseHandler.DISABLE_COMPONENT_FOR_SPECIFIC_USER.Equals(response.getAction())) {
			Logger.Error("Response executed for user:" + response.GetUser().getUsername() + 
					", Action: Disabling Component for Specific User");
			
			//TODO: fill in real code for disabling component for specific user
        } else if(org.owasp.appsensor.Responses.ResponseHandler.DISABLE_COMPONENT_FOR_ALL_USERS.Equals(response.getAction())) {
			Logger.Error("Response executed for user:" + response.GetUser().getUsername() + 
					", Action: Disabling Component for All Users");
			
			//TODO: fill in real code for disabling component for all users
		} else {
			//throw new IllegalArgumentException("There has been a request for an action " +
            throw new ArgumentException("There has been a request for an action " +
					"that is not supported by this response handler.  The requested action is: " + response.getAction());
		}
		
	}
}
}