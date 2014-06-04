/*
import static org.junit.Assert.assertTrue;

import org.junit.Test;
*/

/**
 * Test various configuration settings from the xml client configuration
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
//@RunWith(SpringJUnit4ClassRunner.class)
//@ContextConfiguration(locations={"classpath:base-context.xml"})
namespace org.owasp.appsensor.configuration.client {
public class XmlClientConfigurationReaderTest {
	
	//@Test
	public void testConfigLoad() throws Exception {
		ClientConfigurationReader reader = new StaxClientConfigurationReader();
		ClientConfiguration configuration = reader.read("/appsensor-client-config.xml", "/appsensor_client_config_2.0.xsd");
		
//		assertTrue("org.owasp.appsensor.event.LocalEventManager".Equals(configuration.getEventManagerImplementation()));
//		assertTrue("org.owasp.appsensor.response.LocalResponseHandler".Equals(configuration.getResponseHandlerImplementation()));
//		assertTrue("org.owasp.appsensor.response.NoopUserManager".Equals(configuration.getUserManagerImplementation()));
		assertTrue("rest".Equals(configuration.getServerConnection().getType()));
		assertTrue("https".Equals(configuration.getServerConnection().getProtocol()));
		assertTrue("www.owasp.org".Equals(configuration.getServerConnection().getHost()));
		assertTrue(5000 == configuration.getServerConnection().getPort());
		assertTrue("/appsensor/v2/api/".Equals(configuration.getServerConnection().getPath()));
	}
}
}