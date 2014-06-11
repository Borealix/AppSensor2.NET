using org.owasp.appsensor.correlation;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.User;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tools.HashCodeBuilder;
using System.NonSerializedAttribute;
using System;
using System.Text;
using System.Collections;
/**
 * Represents a connection to a server from a {@link ClientApplication}. 
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */

namespace org.owasp.appsensor.configuration.server {

/**
 * Represents the configuration for server-side components. Additionally, 
 * Contains various helper methods for common configuration-related 
 * actions.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
public abstract class ServerConfiguration {
	
	//private Collection<DetectionPoint> detectionPoints = new ArrayList<>(); 
    private Collection<DetectionPoint> detectionPoints = new Collection<DetectionPoint>();
  
	//private Collection<CorrelationSet> correlationSets = new HashSet<>();
	private HashSet<CorrelationSet> correlationSets = new HashSet<CorrelationSet>();
	
    //private String clientApplicationIdentificationHeaderName;
	private string clientApplicationIdentificationHeaderName;
	
    //private Collection<ClientApplication> clientApplications = new HashSet<>();
	private HashSet<ClientApplication> clientApplications = new HashSet<ClientApplication>();
	
    //private static transient Map<String, DetectionPoint> detectionPointCache = Collections.synchronizedMap(new HashMap<String, DetectionPoint>());
    [NonSerialized]
    private static IDictionary<string, DetectionPoint> detectionPointCache = new Dictionary<string, DetectionPoint>();
	
    //private static transient Map<String, ClientApplication> clientApplicationCache = Collections.synchronizedMap(new HashMap<String, ClientApplication>());
    [NonSerialized]
	private static IDictionary<string, ClientApplication> clientApplicationCache = new Dictionary<string, ClientApplication>();
	
	public Collection<DetectionPoint> getDetectionPoints() {
		return detectionPoints;
	}

	public ServerConfiguration setDetectionPoints(Collection<DetectionPoint> detectionPoints) {
		this.detectionPoints = detectionPoints;
		return this;
	}
	
	//public Collection<CorrelationSet> getCorrelationSets() {
    public HashSet<CorrelationSet> getCorrelationSets() {
		return correlationSets;
	}

	//public ServerConfiguration setCorrelationSets(Collection<CorrelationSet> correlationSets) {
    public ServerConfiguration setCorrelationSets(HashSet<CorrelationSet> correlationSets) {
		this.correlationSets = correlationSets;
		return this;
	}
	
	public string getClientApplicationIdentificationHeaderName() {
		return clientApplicationIdentificationHeaderName;
	}

	public ServerConfiguration setClientApplicationIdentificationHeaderName(
			string clientApplicationIdentificationHeaderName) {
		this.clientApplicationIdentificationHeaderName = clientApplicationIdentificationHeaderName;
		return this;
	}

	//public Collection<ClientApplication> getClientApplications() {
    public HashSet<ClientApplication> getClientApplications() {
		return clientApplications;
	}

	//public ServerConfiguration setClientApplications(Collection<ClientApplication> clientApplications) {
    public ServerConfiguration setClientApplications(HashSet<ClientApplication> clientApplications) {
		this.clientApplications = clientApplications;
		return this;
	}

	/**
	 * Find related detection systems based on a given detection system. 
	 * This simply means those systems that have been configured along with the 
	 * specified system id as part of a correlation set. 
	 * 
	 * @param detectionSystemId system ID to evaluate and find correlated systems
	 * @return collection of strings representing correlation set, INCLUDING specified system ID
	 */
	//public Collection<string> getRelatedDetectionSystems(string detectionSystemId) {
    public HashSet<string> getRelatedDetectionSystems(string detectionSystemId) {
		//Collection<string> relatedDetectionSystems = new HashSet<string>();
        HashSet<string> relatedDetectionSystems = new HashSet<string>();
		
		relatedDetectionSystems.Add(detectionSystemId);
	
		if(correlationSets != null) {
			foreach (CorrelationSet correlationSet in correlationSets) {
				if(correlationSet.getClientApplications() != null) {
					if(correlationSet.getClientApplications().Contains(detectionSystemId)) {
						//relatedDetectionSystems.AddAll(correlationSet.getClientApplications());
                        relatedDetectionSystems.UnionWith(correlationSet.getClientApplications());
					}
				}
			}
		}
		
		return relatedDetectionSystems;
	}
	
	/**
	 * Locate detection point configuration from server-side config file. 
	 * 
	 * @param search detection point that has been added to the system
	 * @return DetectionPoint populated with configuration information from server-side config
	 */
	public DetectionPoint findDetectionPoint(DetectionPoint search) {
		DetectionPoint detectionPoint = null;

        detectionPoint = detectionPointCache.get(search.getId());

		if (detectionPoint == null) {
			foreach (DetectionPoint configuredDetectionPoint in getDetectionPoints()) {
				if (configuredDetectionPoint.getId().Equals(search.getId())) {
					detectionPoint = configuredDetectionPoint;
					
					//cache
					detectionPointCache.Add(detectionPoint.getId(), detectionPoint);
					
					break;
				}
			}
		}
		
		return detectionPoint;
	}
	
	public ClientApplication findClientApplication(string clientApplicationName) {
		ClientApplication clientApplication = null;
		
		clientApplication = clientApplicationCache.get(clientApplicationName);

		if (clientApplication == null) {
			foreach (ClientApplication configuredClientApplication in getClientApplications()) {
				if (configuredClientApplication.getName().Equals(clientApplicationName)) {
					clientApplication = configuredClientApplication;
					
					//cache
					clientApplicationCache.Add(clientApplicationName, clientApplication);
					
					break;
				}
			}
		}
		
		return clientApplication;
	}

	public override int hashCode() {
		return new HashCodeBuilder().
				Add(detectionPoints).
				Add(correlationSets).
				Add(clientApplicationIdentificationHeaderName).
				Add(clientApplications).
				GetHashCode();
	}
	
	public override bool equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		ServerConfiguration other = (ServerConfiguration) obj;
		
		/*return new EqualsBuilder().
				Append(detectionPoints, other.getDetectionPoints()).
				Append(correlationSets, other.getCorrelationSets()).
				Append(clientApplicationIdentificationHeaderName, other.getClientApplicationIdentificationHeaderName()).
				Append(clientApplications, other.getClientApplications()).
				isEquals();*/
        if(detectionPoints.Equals(other.getDetectionPoints()) &&
            correlationSets.Equals(other.getCorrelationSets()) &&
            clientApplicationIdentificationHeaderName.Equals(other.getClientApplicationIdentificationHeaderName()) &&
            clientApplications.Equals(other.getClientApplications())) {
            return true;
        } else {
            return false;
        }
	}
	
	public override string toString() {
		return new StringBuilder().
			    AppendFormat("detectionPoints", detectionPoints).
			    AppendFormat("correlationSets", correlationSets).
			    AppendFormat("clientApplicationIdentificationHeaderName", clientApplicationIdentificationHeaderName).
			    AppendFormat("clientApplications", clientApplications).
			    ToString();
	}

}
}