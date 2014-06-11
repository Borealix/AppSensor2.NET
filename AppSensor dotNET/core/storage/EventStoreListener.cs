/*using static java.lang.annotation.ElementType.FIELD;
using static java.lang.annotation.ElementType.METHOD;
using static java.lang.annotation.ElementType.PARAMETER;
using static java.lang.annotation.ElementType.TYPE;
using static java.lang.annotation.RetentionPolicy.RUNTIME;

using java.lang.annotation.Inherited;
using java.lang.annotation.Retention;
using java.lang.annotation.Target;

using javax.inject.Qualifier;*/

using org.owasp.appsensor.Event;
/**
 * Annotation for Listener for {@link EventStore}. If applied 
 * to a class, then it is registered as a listener and notified 
 * if a new {@link Event} is added to the {@link EventStore}.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Rapha�l Taban
 */
namespace org.owasp.appsensor.storage{


/*@Qualifier
@Retention(RUNTIME)
@Target({TYPE, METHOD, FIELD, PARAMETER})
@Inherited*/
public interface EventStoreListener {
}
}