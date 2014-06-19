using org.owasp.appsensor;

/**
 * Annotation for Listener for {@link ResponseStore}. If applied 
 * to a class, then it is registered as a listener and notified 
 * if a new {@link Response} is added to the {@link ResponseStore}.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage {
/*@Qualifier
@Retention(RUNTIME)
@Target({TYPE, METHOD, FIELD, PARAMETER})
@Inherited*/
public interface ResponseStoreListener {

}
}