/**
 * This exception is for anytime the configuration for appsensor is invalid.
 * 
 * This is used by the {@link org.owasp.appsensor.configuration.client.ClientConfigurationReader}
 * and the {@link org.owasp.appsensor.configuration.server.ServerConfigurationReader}
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System;
namespace org.owasp.appsensor.exceptions{
    public class ConfigurationException : Exception {
        //private static long serialVersionUID = 538520201225584981L;
        public ConfigurationException(string s)
            : base()
        {
	        //super(s);
	    }
	
	    //public ConfigurationException(string s, Throwable cause)
        public ConfigurationException(string s, Exception cause) // Es Exception una equivalencia valida?
            : base ()
        {
		    // super(s, cause);
	    }
    }
}