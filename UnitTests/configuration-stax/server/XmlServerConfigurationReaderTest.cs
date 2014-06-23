using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace org.owasp.appsensor.configuration.server {
    /**
     * Test various configuration settings from the xml server configuration
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     */
    //@RunWith(SpringJUnit4ClassRunner.class)
    //@ContextConfiguration(locations={"classpath:base-context.xml"})
    [TestClass]
    public class XmlServerConfigurationReaderTest {
        [TestMethod]
        /// <exception cref="InvalidOperationException"></exception>
        public void testConfigLoad() {
            ServerConfigurationReader reader = new StaxServerConfigurationReader();
            ServerConfiguration configuration = reader.read("/appsensor-server-config.xml", "/appsensor_server_config_2.0.xsd");

            //Assert.IsTrue("org.owasp.appsensor.analysis.ReferenceEventAnalysisEngine".Equals(configuration.getEventAnalysisEngineImplementation()));
            Assert.AreEqual("X-Appsensor-Client-Application-Name2", configuration.getClientApplicationIdentificationHeaderName());
        }
    }
}