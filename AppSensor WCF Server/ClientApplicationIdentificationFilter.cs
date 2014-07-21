using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace org.owasp.appsensor.wcf {
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
    //@Named
    //@Service
    public class ClientApplicationIdentificationFilter { //implements SOAPHandler<SOAPMessageContext> {
	
	    private static UnauthorizedAccessException unauthorized =
			       new UnauthorizedAccessException("Page requires sending configured client application identification header.");
	
	    /** default name for client application identification header */
	    private static String HEADER_NAME = "X-Appsensor-Client-Application-Name";
	
	    private static bool checkedConfigurationHeaderName = false;
	
	    //@Inject
        [Inject]
	    private AppSensorServer appSensorServer;
	
	    //@Override
        public void filter(IncomingWebRequestContext context) { //throws WebApplicationException
		    //should only run on first request
		    if (! checkedConfigurationHeaderName) {
			    updateHeaderFromConfiguration();
			    checkedConfigurationHeaderName = true;
		    }
		
	        // Get the client application identifier passed in HTTP headers parameters
	        String clientApplicationIdentifier = context.Headers.Get(HEADER_NAME);
	    
	        if (clientApplicationIdentifier == null) {
	    	    throw unauthorized;
	        }

            context.Headers.Set(RequestHandler.APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR, clientApplicationIdentifier);
	    }
	
	    private void updateHeaderFromConfiguration() {
		    String configuredHeaderName = appSensorServer.getConfiguration().getClientApplicationIdentificationHeaderName();
		
		    if (configuredHeaderName != null && configuredHeaderName.Trim().Length > 0) {
			    HEADER_NAME = configuredHeaderName;
		    }
	    }
    }
}