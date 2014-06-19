using org.owasp.appsensor;
using org.owasp.appsensor.handler;
using org.owasp.appsensor.logging;
using org.owasp.appsensor.util;
using System;
using System.Collections.ObjectModel;
using Ninject;
using log4net;
using org.owasp.appsensor.events;
/**
 * Local {@link EventManager} that is used when the application is configured
 * to run within the same JVM as the Analysis Engine.  
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.events {
//@Loggable
[Named("LocalEventManager")]
public class LocalEventManager : EventManager {
	
	//@SuppressWarnings("unused")
	private ILog Logger;
	
	[Inject]
	private LocalRequestHandler requestHandler;
	
	private DateTime responsesLastChecked = DateUtils.getCurrentTimestamp();
	
	/**
	 * {@inheritDoc}
	 */
	public override void addEvent(Event Event) {
		requestHandler.addEvent(Event);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override void addAttack(Attack attack) {
		requestHandler.addAttack(attack);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Response> getResponses() {
		Collection<Response> responses = requestHandler.getResponses(responsesLastChecked.ToString());
		
		//now update last checked
		responsesLastChecked = DateUtils.getCurrentTimestamp();
		
		return responses;
	}
	
}
}