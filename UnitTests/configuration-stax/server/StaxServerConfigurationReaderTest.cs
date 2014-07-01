using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using org.owasp.appsensor.correlation;
using System.Collections.Generic;

namespace org.owasp.appsensor.configuration.server {
    /**
     * Test server xml configuration reader
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     */
        //@RunWith(SpringJUnit4ClassRunner.class)
        //@ContextConfiguration(locations={"classpath:base-context.xml"})
    [TestClass]
    public class StaxServerConfigurationReaderTest {
        //	ServerConfiguration configuration;
        [Inject]
        [TestMethod]
            /// <exception cref="InvalidOperationException"></exception>
            public void testConfigLoad() {
		        ServerConfigurationReader reader = new StaxServerConfigurationReader();
		        ServerConfiguration configuration = reader.read();

                HashSet<CorrelationSet>.Enumerator valCorrelationSets = configuration.getCorrelationSets().GetEnumerator();
                HashSet<String>.Enumerator valClientApplications = configuration.getClientApplications().GetEnumerator();

                Assert.AreEqual(3, configuration.getCorrelationSets().Count);

                valCorrelationSets.MoveNext();
                valClientApplications.MoveNext();
                Assert.AreEqual("server1", valCorrelationSets.Current.getClientApplications().GetEnumerator);


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

                HashSet<DetectionPoint>.Enumerator valDetectionPoints = configuration.getDetectionPoints().GetEnumerator();
                
                Assert.AreEqual(5, configuration.getDetectionPoints().Count);
                valDetectionPoints.MoveNext();
                Assert.AreEqual("IE1", valDetectionPoints.Current.getId());
                valDetectionPoints.MoveNext();
                Assert.AreEqual(4, valDetectionPoints.Current.getThreshold().getInterval().getDuration());
                valDetectionPoints.MoveNext();
                Assert.AreEqual("minutes", valDetectionPoints.Current.getThreshold().getInterval().getUnit());

                valDetectionPoints.MoveNext();
                Assert.AreEqual(5, valDetectionPoints.Current.getResponses().Count);
                valDetectionPoints.MoveNext();
                Assert.AreEqual("log", valDetectionPoints.Current.getResponses().iterator().next().getAction());
	        }
        }
    }