using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

//using org.junit.runner.RunWith;
//using org.springframework.test.context.ContextConfiguration;
//using org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

namespace org.owasp.appsensor {
    //@RunWith(SpringJUnit4ClassRunner.class)
    //@ContextConfiguration(locations={"classpath:base-context.xml"})
    [TestClass]
    public class AppSensorServerWiringTest {
        
        [Inject]
	    private AppSensorServer appSensorServer;

        [TestMethod]
        public void testInstantiation() {
		    Assert.IsNotNull(appSensorServer, "Server instance is null");
		    Assert.IsNotNull(appSensorServer.getEventStore(), "Event store cannot is null");
		    Console.Error.WriteLine(appSensorServer.getEventStore().GetType());
		    Assert.IsNotNull(appSensorServer.getAttackStore(), "Attack store cannot is null");
		    Assert.IsNotNull(appSensorServer.getResponseStore(), "Response store cannot is null");
		    Assert.IsNotNull(appSensorServer.getEventAnalysisEngine(), "EventAnalysisEngine store cannot is null");
		    Assert.IsNotNull(appSensorServer.getAttackAnalysisEngine(), "AttackAnalysisEngine store cannot is null");
		    Assert.IsNotNull(appSensorServer.getConfiguration().getClientApplicationIdentificationHeaderName(), "ClientApplicationIdentificationHeaderName cannot be null");
        }
    }
}