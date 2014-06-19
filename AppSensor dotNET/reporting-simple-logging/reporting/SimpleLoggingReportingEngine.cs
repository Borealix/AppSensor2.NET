using org.owasp.appsensor;
using org.owasp.appsensor.logging;
using org.owasp.appsensor.storage;
using log4net;
using Ninject;
using System.Collections.ObjectModel;
using System;


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
	
	private ILog Logger;
	
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
		//throw new UnsupportedOperationException("This method is not implemented for local logging implementation");
        throw new NotSupportedException("This method is not implemented for local logging implementation");
	}

	/**
	 * {@inheritDoc}
	 */
	public override Collection<Attack> findAttacks(string earliest) {
        throw new NotSupportedException("This method is not implemented for local logging implementation");
	}

	/**
	 * {@inheritDoc}
	 */

	public override Collection<Response> findResponses(string earliest) {
        throw new NotSupportedException("This method is not implemented for local logging implementation");
	}

}
}