/*import java.io.IOException;
import java.io.InputStream;
import java.util.Map;

import javax.xml.XMLConstants;
import javax.xml.stream.XMLStreamConstants;
import javax.xml.stream.XMLStreamReader;
import javax.xml.transform.stream.StreamSource;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import javax.xml.validation.Validator;

import org.xml.sax.SAXException;*/

/**
 * Helper class for XML related utility methods
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.util{
public class XmlUtils {
	
	/**
	 * Validate XML matches XSD. Path-based method.
	 * Simple redirect to the overloaded version that accepts streams.
	 * 
	 * @param xsdPath resource based path to XSD file
	 * @param xmlPath resource based path to XML file
	 * @throws IOException io exception for loading files
	 * @throws SAXException sax exception for parsing files
	 */
    /// <exception cref="IOException"></exception>
    /// /// <exception cref="SAXException"></exception>
	public static void validateXMLSchema(string xsdPath, string xmlPath){
		InputStream xsdStream = XmlUtils.Class.getResourceAsStream(xsdPath);
		InputStream xmlStream = XmlUtils.Class.getResourceAsStream(xmlPath);
		
		validateXMLSchema(xsdStream, xmlStream);
    }
	
	/**
	 * Validate XML matches XSD. Stream-based method.
	 * 
	 * @param xsdStream resource based path to XSD file
	 * @param xmlStream resource based path to XML file
	 * @throws IOException io exception for loading files
	 * @throws SAXException sax exception for parsing files
	 */
    /// <exception cref="IOException"></exception>
    /// /// <exception cref="SAXException"></exception>
	public static void validateXMLSchema(InputStream xsdStream, InputStream xmlStream) {
        SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
        Schema schema = factory.newSchema(new StreamSource(xsdStream));
        Validator validator = schema.newValidator();
        validator.validate(new StreamSource(xmlStream));
    }
	
	/**
	 * Helper method for getting qualified name from stax reader given a set of specified schema namespaces
	 * 
	 * @param xmlReader stax reader
	 * @param namespaces specified schema namespaces
	 * @return qualified element name
	 */
	public static string getElementQualifiedName(XMLStreamReader xmlReader, Map<string, string> namespaces) {
		string namespaceUri = null;
		string localName = null;
		
		switch(xmlReader.getEventType()) {
			case XMLStreamConstants.START_ELEMENT:
			case XMLStreamConstants.END_ELEMENT:
				namespaceUri = xmlReader.getNamespaceURI();
				localName = xmlReader.getLocalName();
				break;
			default:
				localName = StringUtils.EMPTY;
				break;
		}
		
		return namespaces.get(namespaceUri) + ":" + localName;
	}
	
}
}