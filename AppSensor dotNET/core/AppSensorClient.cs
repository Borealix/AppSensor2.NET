using log4net;
using Ninject;
using org.owasp.appsensor.configuration.client;
/*
 * import javax.inject.Inject;
 * import javax.inject.Named;
 */
using org.owasp.appsensor.configuration.client.ClientConfiguration;
using org.owasp.appsensor.Events;
using org.owasp.appsensor.Events.EventManager;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.Responses;
using org.owasp.appsensor.response.UserManager;
using org.owasp.appsensor.response;

/**
 * AppSensor core class for accessing client-side components. Most components
 * are discoverd via DI. However, the configuration portions are setup in 
 * the appsensor-client-config.xml file.
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor{
[Named ("")]
//@Loggable
[Named("AppSensorClient")]
public class AppSensorClient {
	
	//@SuppressWarnings("unused")
	private ILog Logger;
	
	/** accessor for {@link org.owasp.appsensor.configuration.client.ClientConfiguration} */
	private ClientConfiguration configuration;
	
	/** accessor for {@link org.owasp.appsensor.event.EventManager} */
	private EventManager eventManager; 

	/** accessor for {@link org.owasp.appsensor.response.ResponseHandler} */
	private ResponseHandler responseHandler;
	
	/** accessor for {@link org.owasp.appsensor.response.UserManager} */
	private UserManager userManager;
	
	public AppSensorClient() { }
	
	public ClientConfiguration getConfiguration() {
		return configuration;
	}
    [Inject]
	public void setConfiguration(ClientConfiguration updatedConfiguration) {
		configuration = updatedConfiguration;
	}

	public EventManager getEventManager() {
		return eventManager;
	}
    [Inject]
	public void setEventManager(EventManager eventManager) {
		this.eventManager = eventManager;
	}

	public ResponseHandler getResponseHandler() {
		return responseHandler;
	}
    [Inject]
	public void setResponseHandler(ResponseHandler responseHandler) {
		this.responseHandler = responseHandler;
	}

	public UserManager getUserManager() {
		return userManager;
	}
    [Inject]
	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}
	
}
}