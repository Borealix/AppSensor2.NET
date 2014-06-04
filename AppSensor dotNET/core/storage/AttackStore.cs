/*using java.util.Collection;
using java.util.concurrent.CopyOnWriteArrayList;
using javax.inject.Inject;*/
using Ninject;
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.criteria;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.listener.AttackListener;
using System.Collections.ObjectModel;
/**
 * A store is an observable object. 
 * 
 * It is watched by implementations of the {@link AttackListener} interfaces. 
 * 
 * In this case the analysis engines watch the *Store interfaces of AppSensor.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
///*
namespace org.owasp.appsensor.storage{
    public abstract class AttackStore {
	
	    private static Collection<AttackListener> listeners = new CopyOnWriteArrayList<>();
        //private static System.Collections.Concurrent.Partitioner<AttackListener> listeners = new ();
	
	    /// <summary>
	    /// Add an attack to the AttackStore
	    /// </summary>
	    /// <param name="attack">the <seealso cref="org.owasp.appsensor.Attack"/> object to Add to the AttackStore</param>
        public abstract void addAttack(Attack attack);
	
	    /**
	     * Finder for attacks in the AttackStore. 
	     * 
	     * @param criteria the {@link org.owasp.appsensor.criteria.SearchCriteria} object to search by
	     * @return a {@link java.util.Collection} of {@link org.owasp.appsensor.Attack} objects matching the search criteria.
	     */
	    public abstract Collection<Attack> findAttacks(SearchCriteria criteria);
	
	    /**
	     * Register an {@link AttackListener} to notify when {@link Attack}s are added
	     * 
	     * @param listener the {@link AttackListener} to register
	     */
	    public void registerListener(AttackListener listener) {
		    if (! listeners.Contains(listener)) {
			    listeners.Add(listener);
		    }
	    }
	
	    /**
	     * Notify each {@link AttackListener} of the specified {@link Attack}
	     * 
	     * @param response the {@link Attack} to notify each {@link AttackListener} about
	     */
	    public void notifyListeners(Attack attack) {
		    foreach (AttackListener listener in listeners) {
			    listener.onAdd(attack);
		    }
	    }
	
	    /**
	     * Automatically inject any {@link @AttackStoreListener}s, which are implementations of 
	     * {@link AttackListener} so they can be notified of changes.
	     * 
	     * @param collection of {@link AttackListener}s that are injected to be 
	     * 			listeners on the {@link @AttackStore}
	     */
	    [Inject] //@AttackStoreListener
	    public void setListeners(Collection<AttackListener> listeners) {
		    foreach (AttackListener listener in listeners) {
			    registerListener(listener);	
		    }
	    }
	
    }
}