using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Spring.Context;
using Spring.Context.Support;

//using org.junit.runner.RunWith;
//using org.springframework.test.context.ContextConfiguration;
//using org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

namespace org.owasp.appsensor {
    //@RunWith(SpringJUnit4ClassRunner.class)
    //@ContextConfiguration(locations={"classpath:base-context.xml"})
    [TestClass]
    public class AppSensorServerWiringTest {
        
        // Error: No esta definido ningun objeto con el nombre AppSensorServer.
        [TestMethod]
        public void testInstantiation() {
            // ApplicationContext ctx = new FileSystemXmlApplicationContext("spring.xml");
            // MovieLister lister = (MovieLister)ctx.getBean("MovieLister");
            // Movie[] movies = lister.moviesDirectedBy("Sergio Leone");
            // assertEquals("Once Upon a Time in the West", movies[0].getTitle());

            IApplicationContext context = new XmlApplicationContext("base-context.xml");
            // , "appsensor-server-config.xml"
            AppSensorServer appSensorServer = (AppSensorServer)context.GetObject("AppSensorServer");

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