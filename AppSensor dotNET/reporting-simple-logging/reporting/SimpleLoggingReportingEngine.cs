/*
import java.util.Collection;

import javax.inject.Named;

 */
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.storage.AttackStoreListener;
using org.owasp.appsensor.storage.EventStoreListener;
using org.owasp.appsensor.storage.ResponseStoreListener;
using log4net.Repository.Hierarchy;
using Ninject;
using System.Collections.ObjectModel;


/**
 * This is the reference reporting engine, and is an implementation of the observer pattern. 
 * 
 * It is notified with implementations of the *Listener interfaces and is 
 * passed the observed objects. In this case, we are concerned with {@link Event},
 *  {@link Attack} and {@link Response} implementations. 
 * 
 * The implementation simply logs the action. Other implementations are expected to create 
 * some manner of visualization.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.reporting {
/*@Named
@EventStoreListener
@AttackStoreListener
@ResponseStoreListener
@Loggable*/
[Named("SimpleLoggingReportingEngine")]
public class SimpleLoggingReportingEngine : ReportingEngine {
	
	private Logger logger;
	
	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Event Event) {
		Logger.Info("Reporter observed event by user [" + Event.GetUser().getUsername() + "]");
	}

	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Attack attack) {
		Logger.Info("Reporter observed attack by user [" + attack.GetUser().getUsername() + "]");
	}

	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Response response) {
		Logger.Info("Reporter observed response for user [" + response.GetUser().getUsername() + "]");
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Event> findEvents(string earliest) {
		throw new UnsupportedOperationException("This method is not implemented for local logging implementation");
	}

	/**
	 * {@inheritDoc}
	 */
	//@Override
	public Collection<Attack> findAttacks(string earliest) {
		throw new UnsupportedOperationException("This method is not implemented for local logging implementation");
	}

	/**
	 * {@inheritDoc}
	 */

	public override Collection<Response> findResponses(string earliest) {
		throw new UnsupportedOperationException("This method is not implemented for local logging implementation");
	}

}
}