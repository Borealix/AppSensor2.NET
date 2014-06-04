//import javax.inject.Named;

using org.owasp.appsensor.Response;

@Named
namespace org.owasp.appsensor.response {
public class NoopResponseHandler implements ResponseHandler {

    /**
	 * {@inheritDoc}
	 */
	@Override
	public void handle(Response response) {
		//
	}
}
}