using org.owasp.appsensor;
using org.owasp.appsensor.exceptions;
/**
 * This interface is meant to gate access to the different {@link Action} 
 * that can be performed to ensure a {@link clientApplication} has appropriate permissions.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.accesscontrol {

public interface AccessController {

    //public bool isAuthorized(clientApplication clientApplication, Action action, Context context);
    bool isAuthorized(ClientApplication clientApplication, Action action, Context context);
    /// <exception cref="NotAuthorizedException"></exception>
    //public void assertAuthorized(clientApplication clientApplication, Action action, Context context);
    void assertAuthorized(ClientApplication clientApplication, Action action, Context context);
    }
}