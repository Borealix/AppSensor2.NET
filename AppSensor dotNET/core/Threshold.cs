/*import java.io.Serializable;

import org.apache.commons.lang3.builder.EqualsBuilder;
import org.apache.commons.lang3.builder.HashCodeBuilder;
import org.apache.commons.lang3.builder.StringBuilder;
*/
using System;
using System.Text;
using Tools.HashCodeBuilder;
/**
 * The Threshold represents a number of occurrences over a span of time. The key components are the: 
 * 
 * <ul>
 * 		<li>count: (example: 12)</li>
 * 		<li>interval: (example: 15 minutes)</li>
 * </ul>
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor{
[Serializable]
public class Threshold {

	//private static final long serialVersionUID = -9033433180585877243L;

	/** The count at which this threshold is triggered. */
	private int count = 0;
	
	/** 
	 * The time frame within which 'count' number of actions has to be detected in order to
	 * trigger this threshold.
	 */
	private Interval interval;

	public Threshold() {}
	
	public Threshold(int count, Interval interval) {
		setCount(count);
		setInterval(interval);
	}
	
	public int getCount() {
		return count;
	}

	public Threshold setCount(int count) {
		this.count = count;
		return this;
	}

	public Interval getInterval() {
		return interval;
	}

	public Threshold setInterval(Interval interval) {
		this.interval = interval;
		return this;
	}
	
	//public override int hashCode() {
    public int hashCode() {
		return new HashCodeBuilder().
				Add(count).
				Add(interval).
				GetHashCode();
	}
	
	public override bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		Threshold other = (Threshold) obj;
		
		/*return new EqualsBuilder().
				Append(count, other.getCount()).
				Append(interval, other.getInterval()).
				isEquals();*/
        if(count == other.getCount() &&
            interval.Equals(other.getInterval())) {
            return true;
        } else {
            return false;
        }
	}
	
	//public override string toString() {
    public string toString() {
		return new StringBuilder().
			       AppendFormat("count", count).
			       AppendFormat("interval", interval).
			       ToString();
	}	
}
}