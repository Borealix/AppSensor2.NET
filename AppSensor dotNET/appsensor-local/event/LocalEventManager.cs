/*
import java.util.Collection;

import javax.inject.Inject;
import javax.inject.Named;

import org.joda.time.DateTime;
import org.slf4j.Logger;
*/
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Events;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.handler.LocalRequestHandler;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.util.DateUtils;
using org.owasp.appsensor.handler;
using System;
using org.owasp.appsensor.util;
using System.Collections.ObjectModel;
using Ninject;
using log4net;
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