/*using java.io.Serializable;

using org.apache.commons.lang3.builder.EqualsBuilder;
using org.apache.commons.lang3.builder.HashCodeBuilder;
using org.apache.commons.lang3.builder.ToStringBuilder;*/
using System;
namespace org.owasp.appsensor{
/**
 * The standard User object. This represents the end user in the system, 
 * NOT the client application. 
 * 
 * The base implementation assumes the username is provided by the client application. 
 * 
 * It is up to the client application to manage the username. 
 * The username could be anything, an actual username, an IP address, 
 * or any other identifier desired. The core notion is that any desired 
 * correlation on the user is done by comparing the username.
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */

[Serializable]
public class User {

	//private static final long serialVersionUID = 5084152023446797592L;

	private string username;

	public User() {}
	
	public User(string username) {
		setUsername(username);
	}
	
	public string getUsername() {
		return username;
	}

	public User setUsername(string username) {
		this.username = username;
		
		return this;
	}

	public int hashCode() {
		return new HashCodeBuilder(17,31).
				append(username).
				toHashCode();
	}
	
	public bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		User other = (User) obj;
		
		return new EqualsBuilder().
				append(username, other.getUsername()).
				isEquals();
	}
	
	public string ToString() {
		return new ToStringBuilder(this).
				append("username", username).
			    ToString();
	}
}
}