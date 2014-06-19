using org.owasp.appsensor;
using org.owasp.appsensor.storage;
/**
 * This interface is implemented by classes that want to be notified
 * when a new {@link Attack} is created and stored in the AppSensor system. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.listener {
    //@AttackStoreListener
    public interface AttackListener {

        /**
         * Listener method to handle when a new 
         * {@link Attack} is added to the {@link AttackStore}
         * 
         * @param attack {@link Attack} that is added to the {@link AttackStore}
         */
        //public void OnAdd(Response response);
        void OnAdd(Response response);
    }
}