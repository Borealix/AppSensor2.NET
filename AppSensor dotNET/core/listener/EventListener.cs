using org.owasp.appsensor.Event;
using org.owasp.appsensor.storage.EventStore;
using org.owasp.appsensor.storage.EventStoreListener;
/**
 * This interface is implemented by classes that want to be notified
 * when a new {@link Event} is created and stored in the AppSensor system. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Rapha�l Taban
 */
namespace org.owasp.appsensor.listener {
    //@EventStoreListener
    public interface EventListener {
	    /**
	     * Listener method to handle when a new 
	     * {@link Event} is added to the {@link EventStore}
	     * 
	     * @param event {@link Event} that is added to the {@link EventStore}
	     */
	    public void OnAdd(Event Event);
    }
}