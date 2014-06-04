/*
import java.util.Collection;

import javax.jws.WebService;
*/
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.RequestHandler;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.exceptions.NotAuthorizedException;
using System.Collections.ObjectModel;

/**
 * This is the soap endpoint interface for handling requests on the server-side. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.handler {
@WebService(targetNamespace = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl")
public interface SoapRequestHandler : RequestHandler {

	/**
	 * {@inheritDoc}
	 */
    /// <exception cref="NotAuthorizedException"></exception>
	public override void addEvent(Event Event);

	/**
	 * {@inheritDoc}
	 */
	//@Override
	public override void addAttack(Attack attack);

	/**
	 * {@inheritDoc}
	 */
	//@Override
	public override Collection<Response> getResponses(string earliest);

}
}