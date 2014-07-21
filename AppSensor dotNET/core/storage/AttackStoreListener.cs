/*
using static java.lang.annotation.ElementType.FIELD;
using static java.lang.annotation.ElementType.METHOD;
using static java.lang.annotation.ElementType.PARAMETER;
using static java.lang.annotation.ElementType.TYPE;
using static java.lang.annotation.RetentionPolicy.RUNTIME;
using java.lang.annotation.Inherited;
using java.lang.annotation.Retention;
using java.lang.annotation.Target;
 */

/**
 * Annotation for Listener for {@link AttackStore}. If applied 
 * to a class, then it is registered as a listener and notified 
 * if a new {@link Attack} is added to the {@link AttackStore}.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage{
    //@Qualifier
    //@Retention(RUNTIME)
    //@Target({TYPE, METHOD, FIELD, PARAMETER})
    //@Inherited
    //[AttributeUsage(Inherited = True)]
    public interface AttackStoreListener {
    }
}