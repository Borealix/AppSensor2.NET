using log4net;
using Ninject;
using System;
using System.Reflection;
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
//public class LoggerBeanPostProcessor : BeanPostProcessor {
    public class LoggerBeanPostProcessor : TypeInfo {

    /// <exception cref="BeansException"></exception>
    public object postProcessAfterInitialization(object bean, string beanName) {
        //if "logger" field does not exist, exception simply logged
        if(bean.GetType().GetCustomAttribute(typeof (Loggable))!=null) {
            try {
                //Field field = bean.GetType().getDeclaredField("logger");
                FieldInfo field = bean.GetType().GetField("logger");
                //field.setAccessible(true);
                field.SetValue(bean, LogManager.GetLogger(bean.GetType()));
            } catch(Exception e) {
                Console.Error.WriteLine("Error processing logger for " + bean.GetType().FullName + " for bean " + beanName);
                Console.WriteLine(e.StackTrace);
                ;
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