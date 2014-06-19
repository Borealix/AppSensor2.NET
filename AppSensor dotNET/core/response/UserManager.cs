using org.owasp.appsensor;
/**
 * The UserManager is used by the client application as an interface that must
 * be implemented to handle certain {@link org.owasp.appsensor.Response} actions. 
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.response {
    public interface UserManager {

        /**
         * Logout the {@link org.owasp.appsensor.User}
         * 
         * @param user User to logout
         */
        //public void logout(User user);
        void logout(User user);

        /**
         * Disable (lock) the {@link org.owasp.appsensor.User}
         * 
         * @param user User to disable (lock)
         */
        //public void disable(User user);
        void disable(User user);
    }
}