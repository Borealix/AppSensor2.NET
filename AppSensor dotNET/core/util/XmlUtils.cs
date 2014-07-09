using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Xml;
using System.Xml.Schema;

namespace org.owasp.appsensor.util{
/**
 * Helper class for XML related utility methods
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
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
    /// <exception cref="SAXException"></exception>
	public static void validateXMLSchema(string xsdPath, string xmlPath){
        Assembly assembly = Assembly.GetExecutingAssembly();
        //InputStream xsdStream = XmlUtils.class.getResourceAsStream(xsdPath);
        StreamReader xsdStream = new StreamReader(assembly.GetManifestResourceStream(xsdPath));
		//InputStream xmlStream = XmlUtils.Class.getResourceAsStream(xmlPath);
        StreamReader xmlStream = new StreamReader(assembly.GetManifestResourceStream(xmlPath));
		
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
    /// <exception cref="SAXException"></exception>
    public static void validateXMLSchema(StreamReader xsdStream, StreamReader xmlStream) {
        //SchemaFactory factory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
        //Schema schema = factory.newSchema(new StreamSource(xsdStream));
        //Validator validator = schema.newValidator();
        //validator.validate(new StreamSource(xmlStream));

        //XmlReaderSettings booksSettings = new XmlReaderSettings();
        //booksSettings.Schemas.Add("http://www.contoso.com/books", "books.xsd");
        //booksSettings.ValidationType = ValidationType.Schema;
        //booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

        //XmlReader books = XmlReader.Create("books.xml", booksSettings);

        //while(books.Read()) {
        //}

        //XmlReader books = XmlReader.Create("books.xml", booksSettings);

        //while(books.Read()) {
        //}

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