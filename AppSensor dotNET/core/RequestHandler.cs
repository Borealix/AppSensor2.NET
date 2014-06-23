// import java.util.Collection;

using org.owasp.appsensor.exceptions;
using System;
using System.Collections.ObjectModel;
/**
 * The RequestHandler is the key interface that the server side of 
 * AppSensor implements to handle the different functional requests.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor{
    //public interface RequestHandler {
    public abstract class RequestHandler {
	
	//public static string APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR = "APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR";
    static string APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR = "APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR";
	
	/**
	 * Add an Event.
	 * 
	 * @param event Event to Add
	 */
    /// <exception cref="NotAuthorizedException"></exception>
	//public void addEvent (Event Event);
    void addEvent(Event Event) {
    }
	/**
	 * Add an Attack
	 * @param attack Attack to Add
	 */
    /// <exception cref="NotAuthorizedException"></exception>
	//public void addAttack(Attack attack);
    void addAttack(Attack attack) {
    }
	
	/**
	 * Retrieve any responses generated that apply to this client application 
	 * since the last time the client application called this method.
	 *  
	 * @param earliest Timestamp in the http://tools.ietf.org/html/rfc3339 format
	 * @return a Collection of Response objects 
	 */
    /// <exception cref="NotAuthorizedException"></exception>
	//public Collection<Response> getResponses(string earliest);
    Collection<Response> getResponses(string earliest) {
        throw new Exception();
    }
}
}