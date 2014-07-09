using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using org.owasp.appsensor.events;
using org.owasp.appsensor;
using System.Collections.ObjectModel;

namespace AppSensor_WCF_Client {
    /**
     * This event manager should perform rest style requests since it functions
     * as the reference rest client.
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     * @author Raphaël Taban
     */
    //@Named
    public class clientEventManager : EventManager {
        //TODO: do a rest request based on configuration 
	
	    /**
	     * {@inheritDoc}
	     */
	    //@Override
	    public void addEvent(Event Event) {
		    //make request
	    }
	
	    /**
	     * {@inheritDoc}
	     */
	    //@Override
	    public void addAttack(Attack attack) {
		    //make request
	    }
	
	    /**
	     * {@inheritDoc}
	     */
	    //@Override
	    public Collection<Response> getResponses() {
		    //make request
		    return null;
	    }
    }
}