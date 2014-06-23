using org.owasp.appsensor;
using org.owasp.appsensor.storage;
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
    //public abstract class ResponseAnalysisEngine :  ResponseListener {
    public abstract class ResponseAnalysisEngine {
        public void onAdd(Response response) {
            analyze( response); 
        }
        public void analyze(Response response) {
        }
    }
}