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

using System.Collections.Generic;
/**
 * Helper class for XML related utility methods
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
using System.IO;
using System.Resources;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Schema.XmlSchema;
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
		//InputStream xsdStream = XmlUtils.Class.getResourceAsStream(xsdPath);
        Stream xsdStream = XmlUtils.GetType(new ResourceManager.GetStream(xsdPath));
		//InputStream xmlStream = XmlUtils.Class.getResourceAsStream(xmlPath);
        Stream xmlStream = XmlUtils.GetType(new ResourceManager.GetStream(xmlPath));
		
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
	//public static void validateXMLSchema(InputStream xsdStream, InputStream xmlStream) {
    public static void validateXMLSchema(Stream xsdStream, Stream xmlStream) {
        //SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
        XmlSchema factory = new XmlSchema (XMLConstants.W3C_XML_SCHEMA_NS_URI);
        //Schema schema = factory.newSchema(new StreamSource(xsdStream));
        XmlSchemaElement schema = new XmlSchemaElement();
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
	//public static string getElementQualifiedName(XMLStreamReader xmlReader, Map<string, string> namespaces) {
    public static string getElementQualifiedName(XmlReader xmlReader, IDictionary<string, string> namespaces) {
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