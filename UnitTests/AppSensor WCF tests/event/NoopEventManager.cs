using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.owasp.appsensor.events;
using System.Collections.ObjectModel;

namespace org.owasp.appsensor.wcf {
	public class NoopEventManager : EventManager {
	
	    /**
	        * {@inheritDoc}
	        */
	    //@Override
	    public void addEvent(Event Event) {
		    //
	    }
	
	    /**
	        * {@inheritDoc}
	        */
	    //@Override
	    public void addAttack(Attack attack) {
		    //
	    }
	
	    /**
	        * {@inheritDoc}
	        */
	    //@Override
	    public Collection<Response> getResponses() {
		    Collection<Response> responses = new Collection<Response>();
		    return responses;
	    }

    }
}