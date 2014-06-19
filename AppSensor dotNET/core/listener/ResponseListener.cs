using org.owasp.appsensor;
using org.owasp.appsensor.storage;
/**
 * This interface is implemented by classes that want to be notified
 * when a new {@link Response} is created and stored in the AppSensor system. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.listener {
    //@ResponseStoreListener
    public interface ResponseListener {

        /**
         * Listener method to handle when a new 
         * {@link Response} is added to the {@link ResponseStore}
         * 
         * @param attack {@link Response} that is added to the {@link ResponseStore}
         */
        //public void OnAdd(Response response);
        void OnAdd(Response response);
    }
}