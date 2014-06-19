using org.owasp.appsensor.util;
using System.Text;
using Tools.HashCodeBuilder;
/**
 * Event is a specific instance that a sensor has detected that 
 * represents a suspicious activity.
 * 
 * The key difference between an {@link Event} and an {@link Attack} is that an {@link Event}
 * is "suspicous" whereas an {@link Attack} has been determined to be "malicious" by some analysis.
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor{
public class Event{
	
	//private static long serialVersionUID = -3235111340901139594L;

	/** User who triggered the event, could be anonymous user */
	private User user;
	
	/** Detection Point that was triggered */
	private DetectionPoint detectionPoint;
	
	/** When the event occurred */
	private string timestamp;

	/** 
	 * Identifier label for the system that detected the event. 
	 * This will be either the client application, or possibly an external 
	 * detection system, such as syslog, a WAF, network IDS, etc.  */
	private string detectionSystemId; 
	
	/** 
	 * The resource being requested when the event was triggered, which can be used 
     * later to block requests to a given function. 
     */
    private Resource resource;
    
    public Event () {}
    
	public Event (User user, DetectionPoint detectionPoint, string detectionSystemId) {
		InitClass(user, detectionPoint, DateUtils.getCurrentTimestampAsString(), detectionSystemId);
	}
	
	public Event (User user, DetectionPoint detectionPoint, string timestamp, string detectionSystemId) {
        InitClass(user, detectionPoint, timestamp, detectionSystemId);
	}

    private void InitClass(User user, DetectionPoint detectionPoint, string timestamp, string detectionSystemId) {
        setUser(user);
        setDetectionPoint(detectionPoint);
        setTimestamp(timestamp);
        setDetectionSystemId(detectionSystemId);
    }
	
	public User GetUser() {
		return user;
	}

	public Event setUser(User user) {
		this.user = user;
		return this;
	}

	public DetectionPoint GetDetectionPoint() {
		return detectionPoint;
	}

	public Event setDetectionPoint(DetectionPoint detectionPoint) {
		this.detectionPoint = detectionPoint;
		return this;
	}

	public string GetTimestamp() {
		return timestamp;
	}

	public Event setTimestamp(string timestamp) {
		this.timestamp = timestamp;
		return this;
	}
	
	public string GetDetectionSystemId() {
		return detectionSystemId;
	}

	public Event setDetectionSystemId(string detectionSystemId) {
		this.detectionSystemId = detectionSystemId;
		return this;
	}

	public Resource getResource() {
		return resource;
	}

	public Event setResource(Resource resource) {
		this.resource = resource;
		return this;
	}
	
	public override int hashCode() {
		return new HashCodeBuilder().
				Add(user).
				Add(detectionPoint).
				Add(timestamp).
				Add(detectionSystemId).
				Add(resource).
				GetHashCode();
	}
	
	public override bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		Event other = (Event) obj;
		
		/*return new EqualsBuilder().
				Append(user, other.GetUser()).
				Append(detectionPoint, other.GetDetectionPoint()).
				Append(timestamp, other.GetTimestamp()).
				Append(detectionSystemId, other.GetDetectionSystemId()).
				Append(resource, other.getResource()).
				isEquals();*/
        if(user.Equals(other.GetUser()) &&
            detectionPoint.Equals(other.GetDetectionPoint()) &&
            timestamp.Equals(other.GetTimestamp()) &&
            detectionSystemId.Equals(other.GetDetectionSystemId()) &&
            resource.Equals(other.getResource())) {
            return true;
        } else {
            return false;
        }
	}
	
	public override string toString() {
		//return new StringBuilder(this).
        return new StringBuilder().
				AppendFormat("user", user).
				AppendFormat("detectionPoint", detectionPoint).
				AppendFormat("timestamp", timestamp).
				AppendFormat("detectionSystemId", detectionSystemId).
				AppendFormat("resource", resource).
			    ToString();
	}
}
}