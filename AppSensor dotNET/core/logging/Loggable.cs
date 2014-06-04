/*
using java.lang.annotation.ElementType;
using java.lang.annotation.Retention;
using java.lang.annotation.RetentionPolicy;
using java.lang.annotation.Target;
 */

/**
 * This interface is meant to denote that a class uses a logger and 
 * that logger should be post-processed by the {@link LoggerBeanPostProcessor}.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.logging {
    //@Retention(RetentionPolicy.RUNTIME)
    //@Target(ElementType.TYPE)
    public interface Loggable {
    }
}
