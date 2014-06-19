using org.owasp.appsensor;
/**
 * Annotation for Listener for {@link EventStore}. If applied 
 * to a class, then it is registered as a listener and notified 
 * if a new {@link Event} is added to the {@link EventStore}.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage{


/*@Qualifier
@Retention(RUNTIME)
@Target({TYPE, METHOD, FIELD, PARAMETER})
@Inherited*/
public interface EventStoreListener {
}
}