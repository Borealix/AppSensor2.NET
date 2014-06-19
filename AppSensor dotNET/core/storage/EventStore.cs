/*using java.util.Collection;
using java.util.concurrent.CopyOnWriteArrayList;

using javax.inject.Inject;
*/
using org.owasp.appsensor;
using System.Collections.ObjectModel;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.criteria;
using Ninject;
using System.Collections.Generic;

/**
 * A store is an observable object. 
 * 
 * It is watched by implementations of the {@link EventListener} interfaces. 
 * 
 * In this case the analysis engines watch the *Store interfaces of AppSensor.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage{
public abstract class EventStore {

	//private static Collection<EventListener> listeners = new CopyOnWriteArrayList<>();
    private static SynchronizedCollection<EventListener> listeners = new SynchronizedCollection<EventListener>();
	
	/**
	 * Add an {@link org.owasp.appsensor.Event} to the EventStore
	 * 
	 * @param event the {@link org.owasp.appsensor.Event} to Add to the EventStore
	 */
	public abstract void addEvent(Event Event);
	
	/**
	 * A finder for Event objects in the EventStore
	 * 
	 * @param criteria the {@link org.owasp.appsensor.criteria.SearchCriteria} object to search by
	 * @return a {@link java.util.Collection} of {@link org.owasp.appsensor.Event} objects matching the search criteria.
	 */
	public abstract Collection<Event> findEvents(SearchCriteria criteria);

	/**
	 * Register an {@link EventListener} to notify when {@link Event}s are added
	 * 
	 * @param listener the {@link EventListener} to register
	 */
	public void registerListener(EventListener listener) {
		if (! listeners.Contains(listener)) {
			listeners.Add(listener);
		}
	}
	
	/**
	 * Notify each {@link EventListener} of the specified {@link Event}
	 * 
	 * @param response the {@link Event} to notify each {@link EventListener} about
	 */
	public void notifyListeners(Event Event) {
		foreach (EventListener listener in listeners) {
			listener.OnAdd(Event);
		}
	}
	
	/**
	 * Automatically inject any {@link @EventStoreListener}s, which are implementations of 
	 * {@link EventListener} so they can be notified of changes.
	 * 
	 * @param collection of {@link EventListener}s that are injected to be 
	 * 			listeners on the {@link @EventStore}
	 */
    [Inject] //@EventStoreListener
	public void setListeners(Collection<EventListener> listeners) {
		foreach (EventListener listener in listeners) {
			registerListener(listener);	
		}
	}	
}
}