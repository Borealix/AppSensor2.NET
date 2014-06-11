//using java.util.Collection;
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.ClientApplication;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using org.owasp.appsensor.listener.AttackListener;
using org.owasp.appsensor.listener.EventListener;
using org.owasp.appsensor.listener.ResponseListener;
using System.Collections.ObjectModel;
using org.owasp.appsensor.listener;
/**
 * A reporting engine is an implementation of the observer pattern and 
 * extends the *Listener interfaces. 
 * 
 * In this case the reporting engines watch the *Store interfaces of AppSensor.
 * 
 * The reporting engines are meant to provide simple access to get notified 
 * when the different components are added to the *Store's for reporting.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.reporting {
    public interface ReportingEngine : EventListener, AttackListener, ResponseListener {
	    /**
	     * Find {@link Event}s starting from specified time (unix timestamp)
	     * 
	     * @param earliest string representing start time to use to find {@link Event}s (RFC-3339)
	     * @return Collection of {@link Event}s from starting time
	     * @throws NotAuthorizedException thrown if {@link ClientApplication} is not authorized for reporting
	     */
        /// <exception cref="NotAuthorizedException"></exception>
	    public Collection<Event> findEvents(string earliest);
	    /**
	     * Find {@link Attack}s starting from specified time (unix timestamp)
	     * 
	     * @param earliest string representing start time to use to find {@link Attack}s (RFC-3339)
	     * @return Collection of {@link Attack}s from starting time
	     * @throws NotAuthorizedException thrown if {@link ClientApplication} is not authorized for reporting
	     */
	    /// <exception cref="NotAuthorizedException"></exception>
        public Collection<Attack> findAttacks(string earliest);
	    /**
	     * Find {@link Response}s starting from specified time (unix timestamp)
	     * 
	     * @param earliest string representing start time to use to find {@link Response}s (RFC-3339)
	     * @return Collection of {@link Response}s from starting time
	     * @throws NotAuthorizedException thrown if {@link ClientApplication} is not authorized for reporting
	     */
        /// <exception cref="NotAuthorizedException"></exception>
        public Collection<Response> findResponses(string earliest);
    }
}