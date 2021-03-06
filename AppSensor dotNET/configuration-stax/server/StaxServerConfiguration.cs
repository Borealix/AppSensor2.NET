using Ninject;
using org.owasp.appsensor.exceptions;
using Tools.CopyProperties;
using System;
/**
 * Represents the configuration for server-side components. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.configuration.server {

// [Named("StaxServerConfiguration")]
public class StaxServerConfiguration : ServerConfiguration {

	public StaxServerConfiguration() {
		InitClass(true);
	}
	
	public StaxServerConfiguration(bool loadConfiguration) {
        InitClass(loadConfiguration);
	}

    private void InitClass(bool loadConfiguration) {
        if(loadConfiguration) {
            ServerConfiguration configuration = loadconfiguration(new StaxServerConfigurationReader());
            if(configuration != null) {
                //BeanUtils.copyProperties(configuration, this);
                ReflectionUtils.CopyProperties(configuration, this);
            }
        }
    }
	
	/**
	 * Bootstrap mechanism that loads the configuration for the server object based 
	 * on the specified configuration reading mechanism. 
	 * 
	 * The reference implementation of the configuration is XML-based, but this interface 
	 * allows for whatever mechanism is desired
	 * 
	 * @param configurationReader desired configuration reader 
	 */
	private ServerConfiguration loadconfiguration(ServerConfigurationReader configurationReader) {
		ServerConfiguration configuration = null;
		
		try {
			configuration = configurationReader.read();
		} catch(ConfigurationException pe) {
			throw new Exception(pe.Message);
		}
		
		return configuration;
	}
}
}