using org.owasp.appsensor;
/**
 * Role is the standard attribution of an access to be used by the {@link AccessController} 
 * to determine {@link clientApplication} access to the different pieces of functionality.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.accesscontrol {
    public enum Role {
        ADD_EVENT,
        ADD_ATTACK,
        GET_RESPONSES,
        EXECUTE_REPORT
    }
}