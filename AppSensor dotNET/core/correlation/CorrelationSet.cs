/*
using java.util.List;
using java.util.Collection;

using org.apache.commons.lang3.builder.EqualsBuilder;
using org.apache.commons.lang3.builder.HashCodeBuilder;
using org.apache.commons.lang3.builder.StringBuilder;
*/

using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Text;
using Tools.HashCodeBuilder;
//using System.Collections.List;
//using System.Collections.Generic.List<>;
//using System.Collections.SortedList;

namespace org.owasp.appsensor.correlation {
    public class CorrelationSet {

	    /** {@link ClientApplication}s that are represented in this correlation set */
	    //private Collection<string> clientApplications = new List<>();
        private Collection<string> clientApplications = new Collection<string>();
	
	    public Collection<string> getClientApplications() {
		    return clientApplications;
	    }

	    public CorrelationSet setClientApplications(Collection<string> clientApplications) {
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