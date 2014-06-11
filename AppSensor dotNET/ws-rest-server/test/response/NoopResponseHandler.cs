//import javax.inject.Named;

using Ninject;
using org.owasp.appsensor.Responses;


namespace org.owasp.appsensor.response {
[Named ("NoopResponseHandler")]
public class NoopResponseHandler : ResponseHandler {

    /**
	 * {@inheritDoc}
	 */
	public override void handle(Response response) {
		//
	}
}
}