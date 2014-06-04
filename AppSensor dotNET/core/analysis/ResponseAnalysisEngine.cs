using org.owasp.appsensor.Response;
using org.owasp.appsensor.listener.ResponseListener;
using org.owasp.appsensor.storage.ResponseStore;
using org.owasp.appsensor.storage.ResponseStoreListener;
using org.owasp.appsensor.listener;

/**
 * The response analysis engine is an implementation of the Observer pattern. 
 * 
 * In this case the analysis engines watches the {@link ResponseStore} interface.
 * 
 * AnalysisEngine implementations are the components of AppSensor that 
 * constitute the "brain" of the system. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */

namespace org.owasp.appsensor.analysis {
    //@ResponseStoreListener
    public abstract class ResponseAnalysisEngine :  ResponseListener {
        public void onAdd(Response response) {
            analyze( response); 
        }
        public abstract void analyze(Response response) {
        }
    }
}