using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/**
 * Test various configuration settings from the xml client configuration
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
//@RunWith(SpringJUnit4ClassRunner.class)
//@ContextConfiguration(locations={"classpath:base-context.xml"})

namespace org.owasp.appsensor.configuration.client {
    [TestClass]
    public class XmlClientConfigurationReaderTest {
        [TestMethod]
        public void TestMethod1() {
            ClientConfigurationReader reader = new StaxClientConfigurationReader();
		    ClientConfiguration configuration = reader.read("/appsensor-client-config.xml", "/appsensor_client_config_2.0.xsd");
		
    //		Assert.IsTrue("org.owasp.appsensor.event.LocalEventManager".Equals(configuration.getEventManagerImplementation()));
    //		Assert.IsTrue("org.owasp.appsensor.response.LocalResponseHandler".Equals(configuration.getResponseHandlerImplementation()));
    //		Assert.IsTrue("org.owasp.appsensor.response.NoopUserManager".Equals(configuration.getUserManagerImplementation()));
		    Assert.IsTrue("rest".Equals(configuration.getServerConnection().getType()));
		    Assert.IsTrue("https".Equals(configuration.getServerConnection().getProtocol()));
		    Assert.IsTrue("www.owasp.org".Equals(configuration.getServerConnection().getHost()));
		    Assert.IsTrue(5000 == configuration.getServerConnection().getPort());
		    Assert.IsTrue("/appsensor/v2/api/".Equals(configuration.getServerConnection().getPath()));
            }
    }
}
