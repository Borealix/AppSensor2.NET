/*
import javax.inject.Inject;
import javax.inject.Named;
import javax.ws.rs.container.ContainerRequestContext;
*/
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.ClientApplication;
using org.owasp.appsensor.RequestHandler;
using org.owasp.appsensor.accesscontrol.Action;
using org.owasp.appsensor.accesscontrol.Context;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using Ninject;

/**
 * This is a simple helper class for performing access control checks for REST requests.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.rest {
[Named ("AccessControlUtils")]
public class AccessControlUtils {

    [Inject]
	private AppSensorServer appSensorServer;
	
	/**
	 * Check authz before performing action.
	 * 
	 * @param action desired action
	 * @throws NotAuthorizedException thrown if user does not have role.
	 */
    /// <exception cref="NotAuthorizedException"></exception>
	public void checkAuthorization(Action action, ContainerRequestContext requestContext){
		string clientApplicationName = (string)requestContext.getProperty(RequestHandler.APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR);

		ClientApplication clientApplication = appSensorServer.getConfiguration().findClientApplication(clientApplicationName);
		
		appSensorServer.getAccessController().assertAuthorized(clientApplication, action, new Context());
	}
	
}
}