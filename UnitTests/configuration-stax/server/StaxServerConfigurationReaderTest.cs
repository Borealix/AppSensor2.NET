using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

/**
 * Test server xml configuration reader
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
//@RunWith(SpringJUnit4ClassRunner.class)
//@ContextConfiguration(locations={"classpath:base-context.xml"})

namespace org.owasp.appsensor.configuration.server {

    [TestClass]
    public class StaxServerConfigurationReaderTest {
        //	ServerConfiguration configuration;
        [Inject]
        [TestMethod]
            /// <exception cref="InvalidOperationException"></exception>
            public void testConfigLoad() {
		        ServerConfigurationReader reader = new StaxServerConfigurationReader();
		        ServerConfiguration configuration = reader.read();

                Assert.AreEqual(3, configuration.getCorrelationSets().Count);

                Assert.AreEqual("server1", configuration.getCorrelationSets().iterator().next().getClientApplications().iterator().next());

                //		Assert.AreEqual("org.owasp.appsensor.analysis.ReferenceEventAnalysisEngine", configuration.getEventAnalysisEngineImplementation());
                //		Assert.AreEqual("org.owasp.appsensor.analysis.ReferenceAttackAnalysisEngine", configuration.getAttackAnalysisEngineImplementation());
                //		Assert.AreEqual("org.owasp.appsensor.analysis.ReferenceResponseAnalysisEngine", configuration.getResponseAnalysisEngineImplementation());
                //		
                //		Assert.AreEqual("org.owasp.appsensor.storage.InMemoryEventStore", configuration.getEventStoreImplementation());
                //		Assert.AreEqual("org.owasp.appsensor.storage.InMemoryAttackStore", configuration.getAttackStoreImplementation());
                //		Assert.AreEqual("org.owasp.appsensor.storage.InMemoryResponseStore", configuration.getResponseStoreImplementation());
                //		
                //		Assert.AreEqual("org.owasp.appsensor.logging.Slf4jLogger", configuration.getLoggerImplementation());
                //		
                //		Assert.AreEqual("org.owasp.appsensor.accesscontrol.ReferenceAccessController", configuration.getAccessControllerImplementation());
                //		
                //		Assert.AreEqual(2, configuration.getEventStoreObserverImplementations().Count);
                //		Assert.AreEqual("org.owasp.appsensor.analysis.ReferenceEventAnalysisEngine", configuration.getEventStoreObserverImplementations().iterator().next());
                //		
                //		Assert.AreEqual(2, configuration.getAttackStoreObserverImplementations().Count);
                //		Assert.AreEqual("org.owasp.appsensor.analysis.ReferenceAttackAnalysisEngine", configuration.getAttackStoreObserverImplementations().iterator().next());
                //		
                //		Assert.AreEqual(2, configuration.getResponseStoreObserverImplementations().Count);
                //		Assert.AreEqual("org.owasp.appsensor.analysis.ReferenceResponseAnalysisEngine", configuration.getResponseStoreObserverImplementations().iterator().next());

                Assert.AreEqual(5, configuration.getDetectionPoints().Count);
                Assert.AreEqual("IE1", configuration.getDetectionPoints().iterator().next().getId());
                Assert.AreEqual(4, configuration.getDetectionPoints().iterator().next().getThreshold().getInterval().getDuration());
                Assert.AreEqual("minutes", configuration.getDetectionPoints().iterator().next().getThreshold().getInterval().getUnit());

                Assert.AreEqual(5, configuration.getDetectionPoints().iterator().next().getResponses().Count);
                Assert.AreEqual("log", configuration.getDetectionPoints().iterator().next().getResponses().iterator().next().getAction());
	        }
        }
    }