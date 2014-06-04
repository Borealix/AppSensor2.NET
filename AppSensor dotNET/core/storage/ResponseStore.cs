/*import java.util.Collection;
import java.util.concurrent.CopyOnWriteArrayList;

import javax.inject.Inject;*/

using org.owasp.appsensor.Response;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener.ResponseListener;
using System.Collections.ObjectModel;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.criteria;
using Ninject;

/**
 * A store is an observable object. 
 * 
 * It is watched by implementations of the {@link ResponseListener} interfaces. 
 * 
 * In this case the analysis engines watch the *Store interfaces of AppSensor.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage {
public abstract class ResponseStore {
	
	private static Collection<ResponseListener> listeners = new CopyOnWriteArrayList<>();
	
	/**
	 * Add a response to the ResponseStore
	 * 
	 * @param response {@link org.owasp.appsensor.Response} to Add to the ResponseStore
	 */
	public abstract void addResponse(Response response);
	
	/**
	 * Finder for responses in the ResponseStore
	 * 
	 * @param criteria the {@link org.owasp.appsensor.criteria.SearchCriteria} object to search by
	 * @return a {@link java.util.Collection} of {@link org.owasp.appsensor.Response} objects matching the search criteria.
	 */
	public abstract Collection<Response> findResponses(SearchCriteria criteria);

	/**
	 * Register an {@link ResponseListener} to notify when {@link Response}s are added
	 * 
	 * @param listener the {@link ResponseListener} to register
	 */
	public void registerListener(ResponseListener listener) {
		if (! listeners.Contains(listener)) {
			listeners.Add(listener);
		}
	}
	
	/**
	 * Notify each {@link ResponseListener} of the specified {@link Response}
	 * 
	 * @param response the {@link Response} to notify each {@link ResponseListener} about
	 */
	public void notifyListeners(Response response) {
		foreach (ResponseListener listener in listeners) {
			listener.OnAdd(response);
		}
	}
	
	/**
	 * Automatically inject any {@link ResponseStoreListener}s, which are implementations of 
	 * {@link ResponseListener} so they can be notified of changes.
	 * 
	 * @param collection of {@link ResponseListener}s that are injected to be 
	 * 			listeners on the {@link ResponseStore}
	 */
	[Inject] //@ResponseStoreListener
	public void setListeners(Collection<ResponseListener> listeners) {
		foreach (ResponseListener listener in listeners) {
			registerListener(listener);	
		}
	}
}
}