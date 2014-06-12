<<<<<<< HEAD
using System.Collections.Generic;
/*
import java.io.Serializable;
import java.util.List;
import java.util.Collection;

import javax.xml.bind.annotation.XmlTransient;

import org.apache.commons.lang3.builder.EqualsBuilder;
import org.apache.commons.lang3.builder.HashCodeBuilder;
import org.apache.commons.lang3.builder.StringBuilder;
*/
/**
 * The detection point represents the unique sensor concept in the code. 
 * 
 * A list of project detection points are maintained at {@link https://www.owasp.org/index.php/AppSensor_DetectionPoints}
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System.Collections.ObjectModel;
using System.Text;
using Tools.HashCodeBuilder;
namespace org.owasp.appsensor{
public class DetectionPoint{
	
	//private static long serialVersionUID = -6294211676275622809L;

	/**
	 * Identifier for the detection point. (ex. "IE1", "RE2")
	 */
	private string id;
	
	/**
	 * {@link Threshold} for determining whether given detection point (associated {@link Event}) 
	 * should be considered an {@link Attack}.
	 */
	private Threshold threshold;
	
	/**
	 * Set of {@link Response}s associated with given detection point.
	 */
    //private Collection<Response> responses = new ArrayList<Response>();
    private Collection<Response> responses = new Collection<Response>();
	
	public DetectionPoint() {}
	
	public DetectionPoint(string id) {
		setId(id);
	}
	
	public DetectionPoint(string id, Threshold threshold) {
		setId(id);
		setThreshold(threshold);
	}
	
	public DetectionPoint(string id, Threshold threshold, Collection<Response> responses) {
		setId(id);
		setThreshold(threshold);
		setResponses(responses);
	}
	
	public string getId() {
		return id;
	}

	public DetectionPoint setId(string id) {
		this.id = id;
		return this;
	} 
	
	//@XmlTransient
	public Threshold getThreshold() {
		return threshold;
	}

	public DetectionPoint setThreshold(Threshold threshold) {
		this.threshold = threshold;
		return this;
	}

	//@XmlTransient
	public Collection<Response> getResponses() {
		return responses;
	}

	public DetectionPoint setResponses(Collection<Response> responses) {
		this.responses = responses;
		return this;
	}

	public override int hashCode() {
		return new HashCodeBuilder().
				Add(id).
                Add(threshold).
                Add(responses).
				GetHashCode();
	}
	
	public override bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		DetectionPoint other = (DetectionPoint) obj;
		
		/*return new EqualsBuilder().
				Append(id, other.getId()).
				Append(threshold, other.getThreshold()).
				Append(responses, other.getResponses()).
				isEquals();*/
        if (id.Equals(other.getId()) && 
            threshold.Equals (other.getThreshold()) &&
            responses.Equals(other.getResponses())) {
                return true;
        } else { return false;}
	}
	
	public override string toString() {
		return new StringBuilder().
			       AppendFormat("id", id).
			       AppendFormat("threshold", threshold).
			       AppendFormat("responses", responses).
			       ToString();
	}
}
=======
using System.Collections.Generic;
/*
import java.io.Serializable;
import java.util.List;
import java.util.Collection;

import javax.xml.bind.annotation.XmlTransient;

import org.apache.commons.lang3.builder.EqualsBuilder;
import org.apache.commons.lang3.builder.HashCodeBuilder;
import org.apache.commons.lang3.builder.StringBuilder;
*/
/**
 * The detection point represents the unique sensor concept in the code. 
 * 
 * A list of project detection points are maintained at {@link https://www.owasp.org/index.php/AppSensor_DetectionPoints}
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System.Collections.ObjectModel;
using System.Text;
using Tools.HashCodeBuilder;
namespace org.owasp.appsensor{
public class DetectionPoint{
	
	//private static long serialVersionUID = -6294211676275622809L;

	/**
	 * Identifier for the detection point. (ex. "IE1", "RE2")
	 */
	private string id;
	
	/**
	 * {@link Threshold} for determining whether given detection point (associated {@link Event}) 
	 * should be considered an {@link Attack}.
	 */
	private Threshold threshold;
	
	/**
	 * Set of {@link Response}s associated with given detection point.
	 */
    //private Collection<Response> responses = new ArrayList<Response>();
    private Collection<Response> responses = new Collection<Response>();
	
	public DetectionPoint() {}
	
	public DetectionPoint(string id) {
		setId(id);
	}
	
	public DetectionPoint(string id, Threshold threshold) {
		setId(id);
		setThreshold(threshold);
	}
	
	public DetectionPoint(string id, Threshold threshold, Collection<Response> responses) {
		setId(id);
		setThreshold(threshold);
		setResponses(responses);
	}
	
	public string getId() {
		return id;
	}

	public DetectionPoint setId(string id) {
		this.id = id;
		return this;
	} 
	
	//@XmlTransient
	public Threshold getThreshold() {
		return threshold;
	}

	public DetectionPoint setThreshold(Threshold threshold) {
		this.threshold = threshold;
		return this;
	}

	//@XmlTransient
	public Collection<Response> getResponses() {
		return responses;
	}

	public DetectionPoint setResponses(Collection<Response> responses) {
		this.responses = responses;
		return this;
	}

	public override int hashCode() {
		return new HashCodeBuilder().
				Add(id).
                Add(threshold).
                Add(responses).
				GetHashCode();
	}
	
	public override bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		DetectionPoint other = (DetectionPoint) obj;
		
		/*return new EqualsBuilder().
				Append(id, other.getId()).
				Append(threshold, other.getThreshold()).
				Append(responses, other.getResponses()).
				isEquals();*/
        if (id.Equals(other.getId()) && 
            threshold.Equals (other.getThreshold()) &&
            responses.Equals(other.getResponses())) {
                return true;
        } else { return false;}
	}
	
	public override string toString() {
		return new StringBuilder().
			       AppendFormat("id", id).
			       AppendFormat("threshold", threshold).
			       AppendFormat("responses", responses).
			       ToString();
	}
}
>>>>>>> 4c5c488bf1830235869add0196d743908c2981c2
}