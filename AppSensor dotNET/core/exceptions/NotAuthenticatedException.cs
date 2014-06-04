/**
 * This exception is meant to be thrown by the {@link org.owasp.appsensor.RequestHandler}
 * when a {@link org.owasp.appsensor.ClientApplication} is not providing appropriate
 * authentication credentials
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System;
namespace org.owasp.appsensor.exceptions {
    public class NotAuthenticatedException  {
	    
        //private static long serialVersionUID = 538520201225584981L;
        
        public NotAuthenticatedException(string s): base () {
		    //super(s);
	    }
	
	    public NotAuthenticatedException(string s, Exception cause): base() {
		    //super(s, cause);
	    }
    }
}