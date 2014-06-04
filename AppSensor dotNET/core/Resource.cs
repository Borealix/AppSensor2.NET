// using java.io.Serializable;
/**
 * Resource represents a generic component of an application. In many cases, 
 * it would represent a URL, but it could also presumably be used for something 
 * else, such as a specific object, function, or even a subsection of an application, etc.
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System;
namespace org.owasp.appsensor {
    [Serializable]
    public class Resource  {
	
	//private static long serialVersionUID = 343899601431699577L;

	/** 
	 * The resource being requested when a given event/attack was triggered, which can be used 
     * later to block requests to a given function.  In this implementation, 
     * the current request URI is used.
     */
	private string location;

	public string getLocation() {
		return location;
	}

	public void setLocation(string location) {
		this.location = location;
	}
}
}