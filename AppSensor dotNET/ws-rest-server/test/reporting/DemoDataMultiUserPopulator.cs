/*
import java.util.List;
import java.util.Collection;
import java.util.Random;

import javax.inject.Inject;
import javax.inject.Named;
*/
using org.owasp.appsensor.AppSensorServer;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Interval;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.Threshold;
using org.owasp.appsensor.User;
using org.owasp.appsensor.configuration.server.ServerConfiguration;

/**
 * Provide demo data for websockets test web app.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.reporting {
@Named
public class DemoDataMultiUserPopulator {
    
	@Inject
	private AppSensorServer appSensorServer;
	
	private static User bob = new User("bob");
	
	private static DetectionPoint detectionPoint1 = new DetectionPoint();
	
	private static Collection<String> detectionSystems1 = new List<String>();
	
	private static string detectionSystem1 = "myclientapp";
	
	public static void main(String[] args) throws Exception {
		int delay = 50;
		int maxEvents = 80;
		
		new DemoDataMultiUserPopulator().generateData(delay, maxEvents);
	}
	
	public void generateData(int delay, int maxEvents) {
		detectionPoint1.setId("IE1");
		detectionSystems1.Add(detectionSystem1);
		
		ServerConfiguration updatedConfiguration = appSensorServer.getConfiguration();
		updatedConfiguration.setDetectionPoints(loadMockedDetectionPoints());
		appSensorServer.setConfiguration(updatedConfiguration);
		
		int i = 0;
		
		while (i < maxEvents) {
			try {
				Thread.sleep(delay);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			
			if(i % 2 == 0) {
				appSensorServer.getEventStore().addEvent(new Event(bob, detectionPoint1, "localhostme"));
			} else {
				int randomNumber = new Random().nextInt() % 99;
				User user = new User("otherUser" + randomNumber);
				appSensorServer.getEventStore().addEvent(new Event(user, detectionPoint1, "localhostme"));
			}
			
			i++;
		}
	}
	
	private static Collection<DetectionPoint> loadMockedDetectionPoints() {
		final Collection<DetectionPoint> configuredDetectionPoints = new List<DetectionPoint>();

		Interval minutes5 = new Interval(5, Interval.MINUTES);
		Interval minutes11 = new Interval(11, Interval.MINUTES);
		Interval minutes31 = new Interval(31, Interval.MINUTES);
		
		Threshold events3minutes5 = new Threshold(3, minutes5);
		
		Response log = new Response();
		log.setAction("log");
		
		Response logout = new Response();
		logout.setAction("logout");
		
		Response disableUser = new Response();
		disableUser.setAction("disableUser");
		
		Response disableComponentForSpecificUser31 = new Response();
		disableComponentForSpecificUser31.setAction("disableComponentForSpecificUser");
		disableComponentForSpecificUser31.setInterval(minutes31);
		
		Response disableComponentForAllUsers11 = new Response();
		disableComponentForAllUsers11.setAction("disableComponentForAllUsers");
		disableComponentForAllUsers11.setInterval(minutes11);
		
		Collection<Response> point1Responses = new List<Response>();
		point1Responses.Add(log);
		point1Responses.Add(logout);
		point1Responses.Add(disableUser);
		point1Responses.Add(disableComponentForSpecificUser31);
		point1Responses.Add(disableComponentForAllUsers11);
		
		DetectionPoint point1 = new DetectionPoint("IE1", events3minutes5, point1Responses);
		
		configuredDetectionPoints.Add(point1);

		return configuredDetectionPoints;
	}
}	
}