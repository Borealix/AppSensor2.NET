using org.owasp.appsensor;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.storage;

/**
 * The attack analysis engine is an implementation of the Observer pattern. 
 * 
 * In this case the analysis engines watches the {@link AttackStore} interface.
 * 
 * AnalysisEngine implementations are the components of AppSensor that 
 * constitute the "brain" of the system. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */

namespace org.owasp.appsensor.analysis {
    //@AttackStoreListener
    //public abstract class AttackAnalysisEngine : AttackListener {
    public abstract class AttackAnalysisEngine {

        public void onAdd(Attack attack) {
            analyze(attack);
        }

        public abstract void analyze(Attack attack);
    }
}