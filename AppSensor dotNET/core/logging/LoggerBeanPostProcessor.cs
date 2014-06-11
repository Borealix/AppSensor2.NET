/*using java.lang.reflect.Field;
using javax.inject.Named;
using org.slf4j.LoggerFactory;
using org.springframework.beans.BeansException;
using org.springframework.beans.factory.config.BeanPostProcessor;*/

using Ninject;
using System;
/**
 * This class is a Spring post-processor to use reflection to set
 * the logger fields of all classes marked as {@link Loggable}. 
 * The logger is marked with the appropriate class. This prevents us 
 * from having to set the class for the logger in each individual class.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.logging {
[Named("LoggerBeanPostProcessor")]
public class LoggerBeanPostProcessor : BeanPostProcessor {

    /// <exception cref="BeansException"></exception>
    public object postProcessAfterInitialization(object bean, string beanName) {
        //if "logger" field does not exist, exception simply logged
        if(bean.GetType().isAnnotationPresent(Loggable.GetType)) {
            try {
                Field field = bean.GetType().getDeclaredField("logger");
                field.setAccessible(true);
                field.set(bean, LoggerFactory.getLogger(bean.GetType()));
            } catch(Exception e) {
                //System.err.println("Error processing logger for " + bean.GetType().getCanonicalName() + " for bean " + beanName);
                Console.Error.WriteLine("Error processing logger for " + bean.GetType().getCanonicalName() + " for bean " + beanName);
                e.printStackTrace();
            }
        }

        return bean;
    }
    /// <exception cref="BeansException"></exception>
    public object postProcessBeforeInitialization(object bean, string beanName) {
        return bean;
    }
}
}