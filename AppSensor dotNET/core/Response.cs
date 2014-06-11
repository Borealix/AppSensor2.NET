using org.owasp.appsensor.util;
/*using java.io.Serializable;
using org.apache.commons.lang3.builder.EqualsBuilder;
using org.apache.commons.lang3.builder.HashCodeBuilder;
using org.apache.commons.lang3.builder.StringBuilder;*/
using org.owasp.appsensor.util.DateUtils;
using System;
using System.Text;
using Tools.HashCodeBuilder;

namespace org.owasp.appsensor {
/**
 * After an {@link Attack} has been determined to have occurred, a Response
 * is executed. The Response configuration is done on the server-side, not 
 * the client application. 
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
[Serializable]
public class Response {
	
	//private static long serialVersionUID = -4183973779552497656L;

	/** User the response is for */
	private User user;
	
	/** When the event occurred */
	private string timestamp;
	
	/** string representing response action name */
	private string action;
	
	/** Interval response should last for, if applicable. Ie. block access for 30 minutes */
	private Interval interval;

	/** Client application name that response applies to. */
	private string detectionSystemId; 
	
	public Response() {}
	
	public Response (User user, string action, string detectionSystemId) {
		InitClass(user, action, DateUtils.getCurrentTimestampAsString(), detectionSystemId, null);
	}
	
	public Response (User user, string action, string timestamp, string detectionSystemId) {
		InitClass(user, action, timestamp, detectionSystemId, null);
	}
	
	public Response (User user, string action, string detectionSystemId, Interval interval) {
		InitClass(user, action, DateUtils.getCurrentTimestampAsString(), detectionSystemId, interval);
	}
	
	public Response (User user, string action, string timestamp, string detectionSystemId, Interval interval) {
        InitClass(user, action, timestamp, detectionSystemId, interval);
	}

    private void InitClass(User user, string action, string timestamp, string detectionSystemId, Interval interval) {
        setUser(user);
        setAction(action);
        setTimestamp(timestamp);
        setDetectionSystemId(detectionSystemId);
        setInterval(interval);
    }
	
	public User GetUser() {
		return user;
	}

	public Response setUser(User user) {
		this.user = user;
		return this;
	}

	public string GetTimestamp() {
		return timestamp;
	}

	public Response setTimestamp(string timestamp) {
		this.timestamp = timestamp;
		return this;
	}

	public string getAction() {
		return action;
	}

	public Response setAction(string action) {
		this.action = action;
		return this;
	}

	public Interval getInterval() {
		return interval;
	}

	public Response setInterval(Interval interval) {
		this.interval = interval;
		return this;
	}

	public string GetDetectionSystemId() {
		return detectionSystemId;
	}

	public Response setDetectionSystemId(string detectionSystemId) {
		this.detectionSystemId = detectionSystemId;
		return this;
	}
	
	public int hashCode() {
		return new HashCodeBuilder().
				Add(user).
                Add(timestamp).
                Add(action).
                Add(interval).
                Add(detectionSystemId).
				GetHashCode();
	}
	
	public bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		Response other = (Response) obj;
		
		/*return new EqualsBuilder().
				Append(user, other.GetUser()).
				Append(timestamp, other.GetTimestamp()).
				Append(action, other.getAction()).
				Append(interval, other.getInterval()).
				Append(detectionSystemId, other.GetDetectionSystemId()).
				isEquals();*/
        if(user.Equals(other.GetUser()) &&
            timestamp.Equals(other.GetTimestamp()) &&
            action.Equals (other.getAction()) &&
            interval.Equals (other.getInterval()) &&
            detectionSystemId.Equals(other.GetDetectionSystemId())) {
            return true;
        } else {
            return false;
        }
	}

	public string toString() {
		return new StringBuilder().
			       AppendFormat("user", user).
			       AppendFormat("timestamp", timestamp).
			       AppendFormat("action", action).
			       AppendFormat("interval", interval).
			       AppendFormat("detectionSystemId", detectionSystemId).
			       ToString();
	}	
}
}