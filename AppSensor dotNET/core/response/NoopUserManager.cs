//using javax.inject.Named;
//using org.slf4j.Logger;

using org.owasp.appsensor.User;
using org.owasp.appsensor.logging.Loggable;
using log4net;
using log4net.Config;
using Ninject;


/**
 * No-op user manager that is used most likely in test configurations. 
 * It is possible the response handler could handle these actions 
 * directly, but unlikely. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 *
 */
namespace org.owasp.appsensor.response {
    //@Loggable
    [Named("NoopUserManager")]
    public class NoopUserManager : UserManager {

        private ILog logger;

        /**
         * {@inheritDoc}
         */
        public void logout(User user) {
            logger.Info("The no-op user manager did not logout the user as requested.");
        }

        /**
         * {@inheritDoc}
         */
        public void disable(User user) {
            logger.Info("The no-op user manager did not disable the user as requested.");
        }
    }
}