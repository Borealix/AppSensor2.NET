using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Ninject;
using Spring.Context;
using Spring.Context.Support;
using UnitTests.App_Start;
using Spring;

namespace org.owasp.appsensor {
    //@RunWith(SpringJUnit4ClassRunner.class)
    //@ContextConfiguration(locations={"classpath:base-context.xml"})
    [TestClass]
    public class AppSensorServerWiringTest {
        
        [Inject]
        private AppSensorServer appSensorServer;

        [TestMethod]
        public void testInstantiation() {
            //var kernel = NinjectWebCommon.CreatePublicKernel();
            //appSensorServer = kernel.Resolve<AppSensorServer>();

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

//[TestClass]
//public class TestControllersHomeController {
//    private HomeController _sut;

//    [TestInitialize]
//    public void MyTestInitialize() {
//        var kernel = NinjectWebCommon.CreatePublicKernel();
//        _sut = kernel.Resolve();
//    }

//    [TestMethod]
//    public void TestIndex() {
//        var result = _sut.Index();
//        Assert.IsNotNull(result);
//    }
//}