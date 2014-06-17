/*
import javax.inject.Named;

import org.slf4j.Logger;
 */
using System;
using Ninject;
using org.owasp.appsensor.ClientApplication;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.exceptions;
using log4net;

/**
 * This particular {@link AccessController} implementation simply checks the {@link ClientApplication}s 
 * role(s) to see if it matches the expected {@link Action}. If there is a match found, 
 * then the access is considered valid.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.accesscontrol {
//@Loggable
[Named("ReferenceAccessController")]
public class ReferenceAccessController : AccessController {
	
	//@SuppressWarnings("unused")
	private ILog Logger;
	
	/**
	 * {@inheritDoc}
	 */
	public override bool isAuthorized(ClientApplication ClientApplication, Action Action, Context Context) {
		bool authorized = false;

		//if (ClientApplication != null && Action != null && Action.ToString() != null && Action.ToString().Trim().Length() > 0) {
        if (ClientApplication != null && Action != null && Action.ToString() != null && Action.ToString().Trim().Length > 0) {
			foreach (Role role in ClientApplication.getRoles()) {
				
				//simple check to make sure that 
				//the value of the action matches the value of one of the roles (exact match)
				if (role != null && role.ToString() != null && 
						role.ToString().Equals(Action.ToString())) {
					authorized = true; 
					break;
				}
			}
		}
		
		return authorized;
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override void assertAuthorized(ClientApplication clientApplication, Action action, Context context) {
        //throws NotAuthorizedException {
        /// <exception cref="NotAuthorizedException"></exception>
		if (! isAuthorized(clientApplication, action, context)) {
            throw new NotAuthorizedException("Access is not allowed for client application: " + clientApplication + 
            //throw new AuthenticationException("Access is not allowed for client application: " + clientApplication + 
					" when trying to perform action : " + action + 
					" using context: " + context);
		}
	}
	
}
}