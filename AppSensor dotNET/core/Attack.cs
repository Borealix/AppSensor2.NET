/*import java.io.Serializable;

import org.apache.commons.lang3.builder.EqualsBuilder;
import org.apache.commons.lang3.builder.HashCodeBuilder;
import org.apache.commons.lang3.builder.StringBuilder;*/
using org.owasp.appsensor.util.DateUtils;
/**
 * An attack can be added to the system in one of two ways: 
 * <ol>
 * 		<li>Analysis is performed by the event analysis engine and determines an attack has occurred</li>
 * 		<li>Analysis is performed by an external system (ie. WAF) and added to the system.</li>
 * </ol>
 * 
 * The key difference between an {@link Event} and an {@link Attack} is that an {@link Event}
 * is "suspicous" whereas an {@link Attack} has been determined to be "malicious" by some analysis.
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor{
public class Attack {

	//private static long serialVersionUID = 7231666413877649836L;

	/** User who triggered the attack, could be anonymous user */
	private User user;
	
	/** Detection Point that was triggered */
	private DetectionPoint detectionPoint;
	
	/** When the attack occurred */
	private string timestamp;

	/** 
	 * Identifier label for the system that detected the attack. 
	 * This will be either the client application, or possibly an external 
	 * detection system, such as syslog, a WAF, network IDS, etc.  */
	private string detectionSystemId; 
	
	/** 
	 * The resource being requested when the attack was triggered, which can be used 
     * later to block requests to a given function. 
     */
    private Resource resource;
	
    public Attack () { }

    public Attack (User user, DetectionPoint detectionPoint, string detectionSystemId) {
		this(user, detectionPoint, DateUtils.getCurrentTimestampAsString(), detectionSystemId);
	}
	
	public Attack (User user, DetectionPoint detectionPoint, string timestamp, string detectionSystemId) {
		setUser(user);
		setDetectionPoint(detectionPoint);
		setTimestamp(timestamp);
		setDetectionSystemId(detectionSystemId);
	}
	
	public Attack (Event Event) {
		setUser(Event.GetUser());
		setDetectionPoint(Event.GetDetectionPoint());
		setTimestamp(Event.GetTimestamp());
		setDetectionSystemId(Event.GetDetectionSystemId());
		setResource(Event.getResource());
	}
	
	public User GetUser() {
		return user;
	}

	public Attack setUser(User user) {
		this.user = user;
		return this;
	}
	
	public DetectionPoint GetDetectionPoint() {
		return detectionPoint;
	}

	public Attack setDetectionPoint(DetectionPoint detectionPoint) {
		this.detectionPoint = detectionPoint;
		return this;
	}

	public string GetTimestamp() {
		return timestamp;
	}

	public Attack setTimestamp(string timestamp) {
		this.timestamp = timestamp;
		return this;
	}
	
	public string GetDetectionSystemId() {
		return detectionSystemId;
	}

	public Attack setDetectionSystemId(string detectionSystemId) {
		this.detectionSystemId = detectionSystemId;
		return this;
	}

	public Resource getResource() {
		return resource;
	}

	public Attack setResource(Resource resource) {
		this.resource = resource;
		return this;
	}
	
	public override int hashCode() {
		return new HashCodeBuilder(17,31).
				Append(user).
				Append(detectionPoint).
				Append(timestamp).
				Append(detectionSystemId).
				Append(resource).
				toHashCode();
	}

	public override bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		Attack other = (Attack) obj;
		
		return new EqualsBuilder().
				Append(user, other.GetUser()).
				Append(detectionPoint, other.GetDetectionPoint()).
				Append(timestamp, other.GetTimestamp()).
				Append(detectionSystemId, other.GetDetectionSystemId()).
				Append(resource, other.getResource()).
				isEquals();
	}
	
	public override string ToString() {
		return new StringBuilder(this).
			       Append("user", user).
			       Append("detectionPoint", detectionPoint).
			       Append("timestamp", timestamp).
			       Append("detectionSystemId", detectionSystemId).
			       Append("resource", resource).
			       ToString();
	}
}
}