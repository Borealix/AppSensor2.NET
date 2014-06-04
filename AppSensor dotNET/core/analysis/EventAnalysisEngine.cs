using org.owasp.appsensor.Event;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.listener.EventListener;
using org.owasp.appsensor.storage.EventStore;
using org.owasp.appsensor.storage.EventStoreListener;

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