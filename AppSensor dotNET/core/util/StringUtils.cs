//using java.util.List;
//using java.util.Collection;
/**
 * Helper class for string related utility methods
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace org.owasp.appsensor.util {

public class StringUtils {
	
	/** Empty string */
	public static string EMPTY = "";
	
	//public static Collection<string> toCollection(string value) {
    public static HashSet<string> toCollection(string value) {
		//Collection<string> collection = new List<string>();
        HashSet<string> collection = new HashSet<string>();
		
		collection.Add(value);
		
		return collection;
	}	
}
}