using org.owasp.appsensor.util;
using System.Text;
using Tools.HashCodeBuilder;
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
		InitClass(user, detectionPoint, DateUtils.getCurrentTimestampAsString(), detectionSystemId);
	}
	
	public Attack (User user, DetectionPoint detectionPoint, string timestamp, string detectionSystemId) {
        InitClass(user, detectionPoint, timestamp, detectionSystemId);
	}

    private void InitClass(User user, DetectionPoint detectionPoint, string timestamp, string detectionSystemId) {
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
	
	//public override int hashCode() {
    public int hashCode() {
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
		
		Attack other = (Attack) obj;
		
        /*
         * return new EqualsBuilder().
		 *      append(user, other.getUser()).
		 *      append(detectionPoint, other.getDetectionPoint()).
		 *      append(timestamp, other.getTimestamp()).
		 *      append(detectionSystemId, other.getDetectionSystemId()).
		 *      append(resource, other.getResource()).
		 *      isEquals();
         */

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
	
	//public override string toString() {
    public string toString() {
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