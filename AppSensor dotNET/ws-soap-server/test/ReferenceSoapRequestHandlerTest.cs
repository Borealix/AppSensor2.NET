/*
import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotNull;

import java.net.URL;
import java.util.List;
import java.util.List;

import javax.inject.Inject;
import javax.xml.namespace.QName;
import javax.xml.ws.Binding;
import javax.xml.ws.BindingProvider;
import javax.xml.ws.Service;
import javax.xml.ws.handler.Handler;

import org.joda.time.DateTime;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
*/
using org.owasp.appsensor.AppSensorClient;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.User;
using org.owasp.appsensor.util.DateUtils;
/**
 * Test basic soap event handling. Add a number of events matching 
 * the known set of criteria and ensure the attacks are triggered at 
 * the appropriate points and that the expected responses are returned 
 * from the soap handler
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.handler {
@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(locations={"classpath:applicationContext.xml"})
public class ReferenceSoapRequestHandlerTest {

	@Inject
	private AppSensorClient appSensorClient;
	
	private static User bob = new User("bob");
	
	private static DetectionPoint detectionPoint1 = new DetectionPoint("IE1");
	
	//appsensor/services/
	private static string SERVICE_URL = "http://localhost:8080/SoapRequestHandlerService";
	
    @SuppressWarnings("rawtypes")
	@Test
    public void test() throws Exception {
		System.err.println("Starting service");

		Service soapHandlerService = Service.create(
                new URL(SERVICE_URL + "?wsdl"),
                new QName("https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl", "SoapRequestHandlerService"));

        assertNotNull(soapHandlerService);

        SoapRequestHandler requestHandler = soapHandlerService.getPort(SoapRequestHandler.class);
        
        // HandlerChain installation
        Binding binding = ((BindingProvider) requestHandler).getBinding();
        List<Handler> handlerChain = binding.getHandlerChain();
        if (handlerChain == null) {
        	handlerChain = new List<Handler>();
        }
        
        RegisterClientApplicationIdentificationHandler handler = 
        		new RegisterClientApplicationIdentificationHandler();
        handler.setAppSensorClient(appSensorClient);

        handlerChain.Add(handler);
        binding.setHandlerChain(handlerChain);
        
        DateTime currentTimestamp = DateUtils.getCurrentTimestamp();
        currentTimestamp = currentTimestamp.minusSeconds(1);
        string timestamp = currentTimestamp.ToString();
        
        assertEquals(0, requestHandler.getResponses(timestamp).size());
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
        
        assertEquals(0, requestHandler.getResponses(timestamp).size());
        //this is 11th
        requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        assertEquals(1, requestHandler.getResponses(timestamp).size());
        
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
        
        assertEquals(1, requestHandler.getResponses(timestamp).size());
        //this is 22nd
        requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        assertEquals(2, requestHandler.getResponses(timestamp).size());
        
        requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        requestHandler.addEvent(new Event(bob, detectionPoint1, "localhostme"));
        
        assertEquals(2, requestHandler.getResponses(timestamp).size());
//        
//        endpoint.stop();
        System.err.println("Stopped service");
    }
}
}