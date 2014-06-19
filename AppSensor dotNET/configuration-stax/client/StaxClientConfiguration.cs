using Ninject;
using org.owasp.appsensor.exceptions;
using System;
using Tools.CopyProperties;
//using 
/**
 * Represents the configuration for client-side components. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.configuration.client {
[Named("StaxClientConfiguration")]
public class StaxClientConfiguration : ClientConfiguration {
    public StaxClientConfiguration() {
        InitClass(true);
    }

	public StaxClientConfiguration(bool loadConfiguration) {
        InitClass(loadConfiguration);
	}

    private void InitClass(bool loadConfiguration) {
        if(loadConfiguration) {
            ClientConfiguration configuration = loadconfiguration(new StaxClientConfigurationReader());
            if(configuration != null) {
                //BeanUtils.copyProperties(configuration, this);
                ReflectionUtils.CopyProperties(configuration, this);
            }
        }
    }


    /**
     * Bootstrap mechanism that loads the configuration for the client object based 
     * on the specified configuration reading mechanism. 
     * 
     * The reference implementation of the configuration is XML-based, but this interface 
     * allows for whatever mechanism is desired
     * 
     * @param configurationReader desired configuration reader 
     */
    private ClientConfiguration loadconfiguration(ClientConfigurationReader configurationReader) {
        ClientConfiguration configuration = null;

        try {
            configuration = configurationReader.read();
        } catch(ConfigurationException pe) {
            //throw new RuntimeException(pe);
            throw new SystemException(pe.Message);
        }

        return configuration;
    }
	
}
}