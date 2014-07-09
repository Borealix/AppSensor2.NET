using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Ninject;
using org.owasp.appsensor.configuration.server;
using org.owasp.appsensor.criteria;
using System.Collections.Generic;
using Spring.Context;
using Spring.Context.Support;

namespace org.owasp.appsensor.analysis {
    /**
     * Test basic {@link Event} analysis engine. Add a number of {@link Event}s matching 
     * the known set of criteria and ensure the {@link Attack}s are triggered at 
     * the appropriate points.
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     * @author Raphaël Taban
     */
    //@RunWith(SpringJUnit4ClassRunner.class)
    //@ContextConfiguration(locations={"classpath:base-context.xml"})
    [TestClass]
    public class ReferenceStatisticalEventAnalysisEngineTest {

	    private static User bob = new User("bob");
	
	    private static DetectionPoint detectionPoint1 = new DetectionPoint();
	
	    private static HashSet<String> detectionSystems1 = new HashSet<String>();
	
	    private static String detectionSystem1 = "localhostme";

        // AppSensorServer appSensorServer;
	
	    // AppSensorClient appSensorClient;
	
	    //@BeforeClass
        [TestInitialize]
        public void doSetup() {
		    detectionPoint1.setId("IE1");
		
		    detectionSystems1.Add(detectionSystem1);
	    }

        /// <exception cref="InvalidOperationException"></exception>
	    [TestMethod]
        public void testAttackCreation() {
            IApplicationContext contextServer = new XmlApplicationContext("Resources/base-context.xml", "Resources/appsensor-server-config.xml");
            IApplicationContext contextClient = new XmlApplicationContext("Resources/base-context.xml", "Resources/appsensor-client-config.xml");
            
            AppSensorServer appSensorServer = (AppSensorServer)contextServer.GetObject("AppSensorServer");
            AppSensorClient appSensorClient = (AppSensorClient)contextClient.GetObject("AppSensorClient");

		    ServerConfiguration updatedConfiguration = appSensorServer.getConfiguration();
		    updatedConfiguration.setDetectionPoints(loadMockedDetectionPoints());
		    appSensorServer.setConfiguration(updatedConfiguration);

		    SearchCriteria criteria = new SearchCriteria().
				    setUser(bob).
				    setDetectionPoint(detectionPoint1).
				    setDetectionSystemIds(detectionSystems1);
		
		    Assert.AreEqual(0, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(0, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(1, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(0, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(2, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(0, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(3, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(1, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(4, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(1, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(5, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(1, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(6, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(2, appSensorServer.getAttackStore().findAttacks(criteria).Count);
		
		    appSensorClient.getEventManager().addEvent(new Event(bob, detectionPoint1, "localhostme"));
		
		    Assert.AreEqual(7, appSensorServer.getEventStore().findEvents(criteria).Count);
		    Assert.AreEqual(2, appSensorServer.getAttackStore().findAttacks(criteria).Count);
	    }


        //private Collection<DetectionPoint> loadMockedDetectionPoints() {
        private HashSet<DetectionPoint> loadMockedDetectionPoints() {
            //Collection<DetectionPoint> configuredDetectionPoints = new Collection<DetectionPoint>();
            HashSet<DetectionPoint> configuredDetectionPoints = new HashSet<DetectionPoint>();

            Interval minutes5 = new Interval(5, Interval.MINUTES);
            Interval minutes6 = new Interval(6, Interval.MINUTES);
            Interval minutes7 = new Interval(7, Interval.MINUTES);
            Interval minutes8 = new Interval(8, Interval.MINUTES);
            Interval minutes11 = new Interval(11, Interval.MINUTES);
            Interval minutes12 = new Interval(12, Interval.MINUTES);
            Interval minutes13 = new Interval(13, Interval.MINUTES);
            Interval minutes14 = new Interval(14, Interval.MINUTES);
            Interval minutes15 = new Interval(15, Interval.MINUTES);
            Interval minutes31 = new Interval(31, Interval.MINUTES);
            Interval minutes32 = new Interval(32, Interval.MINUTES);
            Interval minutes33 = new Interval(33, Interval.MINUTES);
            Interval minutes34 = new Interval(34, Interval.MINUTES);
            Interval minutes35 = new Interval(35, Interval.MINUTES);

            Threshold events3minutes5 = new Threshold(3, minutes5);
            Threshold events12minutes5 = new Threshold(12, minutes5);
            Threshold events13minutes6 = new Threshold(13, minutes6);
            Threshold events14minutes7 = new Threshold(14, minutes7);
            Threshold events15minutes8 = new Threshold(15, minutes8);

            Response log = new Response();
            log.setAction("log");

            Response logout = new Response();
            logout.setAction("logout");

            Response disableUser = new Response();
            disableUser.setAction("disableUser");

            Response disableComponentForSpecificUser31 = new Response();
            disableComponentForSpecificUser31.setAction("disableComponentForSpecificUser");
            disableComponentForSpecificUser31.setInterval(minutes31);

            Response disableComponentForSpecificUser32 = new Response();
            disableComponentForSpecificUser32.setAction("disableComponentForSpecificUser");
            disableComponentForSpecificUser32.setInterval(minutes32);

            Response disableComponentForSpecificUser33 = new Response();
            disableComponentForSpecificUser33.setAction("disableComponentForSpecificUser");
            disableComponentForSpecificUser33.setInterval(minutes33);

            Response disableComponentForSpecificUser34 = new Response();
            disableComponentForSpecificUser34.setAction("disableComponentForSpecificUser");
            disableComponentForSpecificUser34.setInterval(minutes34);

            Response disableComponentForSpecificUser35 = new Response();
            disableComponentForSpecificUser35.setAction("disableComponentForSpecificUser");
            disableComponentForSpecificUser35.setInterval(minutes35);

            Response disableComponentForAllUsers11 = new Response();
            disableComponentForAllUsers11.setAction("disableComponentForAllUsers");
            disableComponentForAllUsers11.setInterval(minutes11);

            Response disableComponentForAllUsers12 = new Response();
            disableComponentForAllUsers12.setAction("disableComponentForAllUsers");
            disableComponentForAllUsers12.setInterval(minutes12);

            Response disableComponentForAllUsers13 = new Response();
            disableComponentForAllUsers13.setAction("disableComponentForAllUsers");
            disableComponentForAllUsers13.setInterval(minutes13);

            Response disableComponentForAllUsers14 = new Response();
            disableComponentForAllUsers14.setAction("disableComponentForAllUsers");
            disableComponentForAllUsers14.setInterval(minutes14);

            Response disableComponentForAllUsers15 = new Response();
            disableComponentForAllUsers15.setAction("disableComponentForAllUsers");
            disableComponentForAllUsers15.setInterval(minutes15);

            Collection<Response> point1Responses = new Collection<Response>();
            point1Responses.Add(log);
            point1Responses.Add(logout);
            point1Responses.Add(disableUser);
            point1Responses.Add(disableComponentForSpecificUser31);
            point1Responses.Add(disableComponentForAllUsers11);

            DetectionPoint point1 = new DetectionPoint("IE1", events3minutes5, point1Responses);

            Collection<Response> point2Responses = new Collection<Response>();
            point2Responses.Add(log);
            point2Responses.Add(logout);
            point2Responses.Add(disableUser);
            point2Responses.Add(disableComponentForSpecificUser32);
            point2Responses.Add(disableComponentForAllUsers12);

            DetectionPoint point2 = new DetectionPoint("IE2", events12minutes5, point2Responses);

            Collection<Response> point3Responses = new Collection<Response>();
            point3Responses.Add(log);
            point3Responses.Add(logout);
            point3Responses.Add(disableUser);
            point3Responses.Add(disableComponentForSpecificUser33);
            point3Responses.Add(disableComponentForAllUsers13);

            DetectionPoint point3 = new DetectionPoint("IE3", events13minutes6, point3Responses);

            Collection<Response> point4Responses = new Collection<Response>();
            point4Responses.Add(log);
            point4Responses.Add(logout);
            point4Responses.Add(disableUser);
            point4Responses.Add(disableComponentForSpecificUser34);
            point4Responses.Add(disableComponentForAllUsers14);

            DetectionPoint point4 = new DetectionPoint("IE4", events14minutes7, point4Responses);

            Collection<Response> point5Responses = new Collection<Response>();
            point5Responses.Add(log);
            point5Responses.Add(logout);
            point5Responses.Add(disableUser);
            point5Responses.Add(disableComponentForSpecificUser35);
            point5Responses.Add(disableComponentForAllUsers15);

            DetectionPoint point5 = new DetectionPoint("IE5", events15minutes8, point5Responses);

            configuredDetectionPoints.Add(point1);
            configuredDetectionPoints.Add(point2);
            configuredDetectionPoints.Add(point3);
            configuredDetectionPoints.Add(point4);
            configuredDetectionPoints.Add(point5);

            return configuredDetectionPoints;
        }
    }
}