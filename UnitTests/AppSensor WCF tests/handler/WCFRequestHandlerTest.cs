using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using UnitTests.ServiceReference;
using org.owasp.appsensor.util;
using System.ServiceModel.Channels;
using System.Windows.Data;
//import org.owasp.appsensor.AppSensorClient;
//import org.owasp.appsensor.DetectionPoint;
//import org.owasp.appsensor.Event;
//import org.owasp.appsensor.User;
//import org.owasp.appsensor.util.DateUtils;

namespace org.owasp.appsensor.wcf {
    /**
     * Test basic soap event handling. Add a number of events matching 
     * the known set of criteria and ensure the attacks are triggered at 
     * the appropriate points and that the expected responses are returned 
     * from the soap handler
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     */
    //@RunWith(SpringJUnit4ClassRunner.class)
    //@ContextConfiguration(locations={"classpath:applicationContext.xml"})
    [TestClass]
    public class WCFRequestHandlerTest {
        
        //@Inject
	    private AppSensorClient appSensorClient;
	
	    private static User bob = new User("bob");
	
	    private static DetectionPoint detectionPoint1 = new DetectionPoint("IE1");
	
	    //appsensor/services/
	    //private static String SERVICE_URL = "http://localhost:8080/SoapRequestHandlerService";
        private static String SERVICE_URL = "http://localhost:65499/Service.svc?singleWsdl";

        //@SuppressWarnings("rawtypes")
        //@Test
        [TestMethod]
        public void test() { // throws Exception
            Console.Error.WriteLine("Starting service");

            var request = (HttpWebRequest)WebRequest.Create(SERVICE_URL);
            var response = (HttpWebResponse)request.GetResponse();

            Assert.IsNotNull(response);

            IWCFRequestHandler requestHandler = (IWCFRequestHandler)response.CreateObjRef(typeof(IWCFRequestHandler));
        
            // HandlerChain installation
            //Binding binding = BindingOperations.GetBinding(requestHandler );
            //List<Handler> handlerChain = binding.getHandlerChain();
            //if (handlerChain == null) {
            //    handlerChain = new ArrayList<Handler>();
            //}
        
            //RegisterClientApplicationIdentificationHandler handler = 
            //        new RegisterClientApplicationIdentificationHandler();
            //handler.setAppSensorClient(appSensorClient);

            //handlerChain.add(handler);
            //binding.setHandlerChain(handlerChain);
        
            DateTime currentTimestamp = DateUtils.getCurrentTimestamp();
            currentTimestamp = currentTimestamp.AddSeconds(-1);
            String timestamp = currentTimestamp.ToString();
        
            Assert.Equals(0, requestHandler.getResponses(timestamp).Length);
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        
            Assert.Equals(0, requestHandler.getResponses(timestamp).Length);
            //this is 11th
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            Assert.Equals(1, requestHandler.getResponses(timestamp).Length);
        
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        
            Assert.Equals(1, requestHandler.getResponses(timestamp).Length);
            //this is 22nd
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            Assert.Equals(2, requestHandler.getResponses(timestamp).Length);
        
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
            requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        
            Assert.Equals(2, requestHandler.getResponses(timestamp).Length);
    //        
    //        endpoint.stop();
            Console.Error.WriteLine("Stopped service");
        }
    }
}