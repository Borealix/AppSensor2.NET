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

using System;
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
        Stream xsdStream = new StreamReader(xsdPath).BaseStream;
		//InputStream xmlStream = XmlUtils.Class.getResourceAsStream(xmlPath);
        Stream xmlStream = new StreamReader(xmlPath).BaseStream;
		
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

    /*  public static void validateXMLSchema(InputStream xsdStream, InputStream xmlStream) throws IOException, SAXException {
        SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
        Schema schema = factory.newSchema(new StreamSource(xsdStream)); Define un schema con "http://www.w3.org/2001/XMLSchema-instance" definido
        Validator validator = schema.newValidator();
        validator.validate(new StreamSource(xmlStream));
    } */

    /// <exception cref="IOException"></exception>
    /// /// <exception cref="SAXException"></exception>
    public static void validateXMLSchema(Stream xsdStream, Stream xmlStream) {
        //XmlSchema schema = XmlSchema.Read(xsdStream, null);        
        //schema.Namespaces.Add(string.Empty, "http://www.w3.org/2001/XMLSchema");
        XmlReaderSettings schema = new XmlReaderSettings();
        schema.Schemas.Add("http://www.w3.org/2001/XMLSchema", xsdStream.ToString());
        schema.ValidationType = ValidationType.Schema;
        schema.ValidationEventHandler += new ValidationEventHandler(schemaValidationEventHandler);
        }

    static void schemaValidationEventHandler(object sender, ValidationEventArgs e) {
        if(e.Severity == XmlSeverityType.Warning) {
            Console.Write("WARNING: ");
            Console.WriteLine(e.Message);
        } else if(e.Severity == XmlSeverityType.Error) {
            Console.Write("ERROR: ");
            Console.WriteLine(e.Message);
        }
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
		
		switch(xmlReader.NodeType) {
			case XmlNodeType.Element:
			case XmlNodeType.EndElement:
				//namespaceUri = xmlReader.getNamespaceURI();
                //localName = xmlReader.getLocalName();
                namespaceUri = xmlReader.NamespaceURI;
				localName = xmlReader.LocalName;
				break;
			default:
				localName = StringUtils.EMPTY;
				break;
		}
		
		//return namespaces.get(namespaceUri) + ":" + localName;
        return namespaces[namespaceUri] + ":" + localName;
	}
	
}
}