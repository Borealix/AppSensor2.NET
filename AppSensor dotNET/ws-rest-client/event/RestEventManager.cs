using Ninject;
/*
import java.util.Collection;

import javax.inject.Named;
*/
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Events;
using org.owasp.appsensor.Response;
using System.Collections.ObjectModel;

/**
 * This event manager should perform rest style requests since it functions
 * as the reference rest client.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.events {
[Named("RestEventManager")]
public class RestEventManager : EventManager {

	//TODO: do a rest request based on configuration 
	
	/**
	 * {@inheritDoc}
	 */
	public override void addEvent(Event Event) {
		//make request
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override void addAttack(Attack attack) {
		//make request
	}
	
	/**
	 * {@inheritDoc}
	 */
    public override Collection<Response> getResponses() {
        //make request
        return null;
    }
}
}