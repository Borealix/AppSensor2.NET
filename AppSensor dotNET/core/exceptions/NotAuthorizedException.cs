/**
 * This exception is thrown by the {@link org.owasp.appsensor.accesscontrol.AccessController}
 * when a {@link org.owasp.appsensor.ClientApplication} is not authorized to perform a 
 * specific action
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System;
namespace org.owasp.appsensor.exceptions {
    public class NotAuthorizedException : Exception {

	    //private static long serialVersionUID = 3914161530293443363L;

	    public NotAuthorizedException(string s) : base (){
		    //super(s);
	    }
	
	    public NotAuthorizedException(string s, Exception cause) : base () {
		    //super(s, cause);
	    }
    }
}