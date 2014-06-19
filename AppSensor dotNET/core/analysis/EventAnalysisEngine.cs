using org.owasp.appsensor;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.storage;

/**
 * The event analysis engine is an implementation of the Observer pattern. 
 * 
 * In this case the analysis engines watches the {@link EventStore} interface.
 * 
 * AnalysisEngine implementations are the components of AppSensor that 
 * constitute the "brain" of the system. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */

namespace org.owasp.appsensor.analysis {
    //@EventStoreListener
    public abstract class EventAnalysisEngine :  EventListener{
        
        public abstract void analyze(Event Event) {
        }

        public void onAdd(Event Event) {
            analyze(Event);
        }
    }
}