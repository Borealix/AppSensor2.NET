using Ninject;
/*
import javax.inject.Inject;
import javax.inject.Named;
import javax.ws.rs.WebApplicationException;
import javax.ws.rs.container.ContainerRequestContext;
import javax.ws.rs.container.ContainerRequestFilter;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;
import javax.ws.rs.ext.Provider;
*/
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.handler.RestRequestHandler;

/**
 * This is the jax-rs container request filter that performs
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
 * this container request filter then processes the request. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.rest.filter {
//@Provider
[Named ("ClientApplicationIdentificationFilter")]
public class ClientApplicationIdentificationFilter : ContainerRequestFilter {
    
	private static WebApplicationException unauthorized =
			   new WebApplicationException(
			       Response.status(Status.UNAUTHORIZED)
			               .entity("Page requires sending configured client application identification header.").build());
	
	/** default name for client application identification header */
	private static string HEADER_NAME = "X-Appsensor-Client-Application-Name";
	
	private static bool checkedConfigurationHeaderName = false;
	
	[Inject]
	private AppSensorServer appSensorServer;
	
    /// <exception cref="WebApplicationException"></exception>
	public override void filter(ContainerRequestContext context){
		//should only run on first request
		if (! checkedConfigurationHeaderName) {
			updateHeaderFromConfiguration();
			checkedConfigurationHeaderName = true;
		}
		
	    // Get the client application identifier passed in HTTP headers parameters
	    string clientApplicationIdentifier = context.getHeaderString(HEADER_NAME);
	    
	    if (clientApplicationIdentifier == null) {
	    	throw unauthorized;
	    }
	    
	    context.setProperty(RestRequestHandler.APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR, clientApplicationIdentifier);
	}
	
	private void updateHeaderFromConfiguration() {
		string configuredHeaderName = appSensorServer.getConfiguration().getClientApplicationIdentificationHeaderName();
		
		if (configuredHeaderName != null && configuredHeaderName.Trim().Length() > 0) {
			HEADER_NAME = configuredHeaderName;
		}
	}
}
}