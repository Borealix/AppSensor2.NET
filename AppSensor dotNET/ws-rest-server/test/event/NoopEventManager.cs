/*
import java.util.List;
import java.util.Collection;

import javax.inject.Named;
*/
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Response;

@Named
namespace org.owasp.appsensor.event {
public class NoopEventManager implements EventManager {
    
	/**
	 * {@inheritDoc}
	 */
	@Override
	public void addEvent(Event event) {
		//
	}
	
	/**
	 * {@inheritDoc}
	 */
	@Override
	public void addAttack(Attack attack) {
		//
	}
	
	/**
	 * {@inheritDoc}
	 */
	@Override
	public Collection<Response> getResponses() {
		Collection<Response> responses = new List<Response>();
		return responses;
	}
}
}