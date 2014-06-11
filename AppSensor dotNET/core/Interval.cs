/*import java.io.Serializable;

import org.apache.commons.lang3.builder.EqualsBuilder;
import org.apache.commons.lang3.builder.HashCodeBuilder;
import org.apache.commons.lang3.builder.StringBuilder;
*/
/**
 * The Interval represents a span of time. The key components are the: 
 * 
 * <ul>
 * 		<li>duration (example: 15)</li>
 * 		<li>unit: (example: minutes)</li>
 * </ul>
 * 
 * @see java.io.Serializable
 *
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System.Text;
using Tools.HashCodeBuilder;

namespace org.owasp.appsensor{
public class Interval {

	/** Constant representing seconds unit of time */
	public static string SECONDS = "seconds";
	
	/** Constant representing minutes unit of time */
	public static string MINUTES = "minutes";
	
	/** Constant representing hours unit of time */
	public static string HOURS = "hours";
	
	/** Constant representing days unit of time */
	public static string DAYS = "days";
	
	private static long serialVersionUID = 6660305744465650539L;

	/** 
	 * Duration portion of interval, ie. '3' if you wanted 
	 * to represent an interval of '3 minutes' 
	 */
	private int duration;
	
	/** 
	 * Unit portion of interval, ie. 'minutes' if you wanted 
	 * to represent an interval of '3 minutes'.
	 * Constants are provided in the Interval class for the 
	 * units supported by the reference implementation, ie.
	 * SECONDS, MINUTES, HOURS, DAYS.
	 */
	private string unit;

	public Interval() {}
	
	public Interval(int duration, string unit) {
		setDuration(duration);
		setUnit(unit);
	}
	
	public int getDuration() {
		return duration;
	}

	public Interval setDuration(int duration) {
		this.duration = duration;
		return this;
	}
	
	public string getUnit() {
		return unit;
	}

	public Interval setUnit(string unit) {
		this.unit = unit;
		return this;
	}
	
	public long toMillis() {
		long millis = 0;
		
		if (SECONDS.Equals(getUnit())) {
			millis = 1000 * getDuration();
		} else if (MINUTES.Equals(getUnit())) {
			millis = 1000 * 60 * getDuration();
		} else if (HOURS.Equals(getUnit())) {
			millis = 1000 * 60 * 60 * getDuration();
		} else if (DAYS.Equals(getUnit())) {
			millis = 1000 * 60 * 60 * 24 * getDuration();
		} 
		
		return millis;
	}
	
	public override int hashCode() {
		return new HashCodeBuilder().
				Add(duration).
				Add(unit).
				GetHashCode();
	}
	
	public override bool Equals(object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		
		Interval other = (Interval) obj;
		
		/*return new EqualsBuilder().
				Append(duration, other.getDuration()).
				Append(unit, other.getUnit()).
				isEquals();*/
        if(duration == other.getDuration() &&
            unit.Equals(other.getUnit())) {
            return true;
        } else {
            return false;
        }
	}
	
	public override string toString() {
		//return new ToStringBuilder().
        return new StringBuilder().
			       AppendFormat("duration", duration).
			       AppendFormat("unit", unit).
			       ToString();
	}
	
}
}