using System.Collections.ObjectModel;
using System.Text;
using Tools.HashCodeBuilder;
using System.Collections.Generic;
using System.Linq;

namespace org.owasp.appsensor.correlation {
    /**
     * The CorrelationSet represents a set of {@link ClientApplication}s that 
     * should be considered to share the same {@link User} base. 
     * 
     * For example if server1 and server2 are part of a correlation set, 
     * then client1/userA is considered the same {@link User} as client2/userA. 
     * 
     * This can be useful for simple tracking of {@link User} activity across multiple
     * {@link ClientApplication}s.
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     */
    public class CorrelationSet {
        
	    /** {@link ClientApplication}s that are represented in this correlation set */
	    //private Collection<string> clientApplications = new List<>();
        private HashSet<string> clientApplications = new HashSet<string>();
	
	    //public Collection<string> getClientApplications() {
        public HashSet<string> getClientApplications() {
		    return clientApplications;
	    }

	    //public CorrelationSet setClientApplications(Collection<string> clientApplications) {
        public CorrelationSet setClientApplications(HashSet<string> clientApplications) {
		    this.clientApplications = clientApplications;
		    return this;
	    }

        //public override int hashCode() {
        public int hashCode() {
		    return new HashCodeBuilder().
				    Add(clientApplications).
				    GetHashCode();
	    }
	
	
	    public override bool Equals(object obj) {
		    if (this == obj)
			    return true;
		    if (obj == null)
			    return false;
		    if (GetType() != obj.GetType())
			    return false;
		
		    CorrelationSet other = (CorrelationSet) obj;
		
		    /*return new EqualsBuilder().
				    Append(clientApplications, other.getClientApplications()).
				    isEquals();*/
            if(clientApplications.Equals(other.getClientApplications())) {
                return true;
            } else {
                return false;
            }
	    }
	
	    //public override string toString() {
        public string toString() {
		    //return new StringBuilder(this).
            return new StringBuilder().
			           AppendFormat("clientApplications", clientApplications).
			           ToString();
	    }
    }
}