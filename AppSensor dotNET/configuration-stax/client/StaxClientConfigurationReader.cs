using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Configuration;
using org.owasp.appsensor.util;
using org.owasp.appsensor.exceptions;
/**
 * This implementation parses the {@link ClientConfiguration} objects 
 * from the specified XML file via the StAX API.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.configuration.client {
public class StaxClientConfigurationReader : ClientConfigurationReader {
	
	//private static final string XSD_NAMESPACE = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/xsd/appsensor_client_config_2.0.xsd";
    private static string XSD_NAMESPACE = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/xsd/appsensor_client_config_2.0.xsd";
	
	//private Map<string, string> namespaces = new HashMap<string, string>();
    private IDictionary<string, string> namespaces = new Dictionary<string, string>();
	
	public StaxClientConfigurationReader() {
		/** initialize namespaces **/
		//namespaces.put(XSD_NAMESPACE, "config");
        namespaces.Add(XSD_NAMESPACE, "config");
        /*
         * In Java, we use put. It does almost the same as Add, with the difference that if
         * Put receives a existing key, replaced the old value by the newer.
         * If Add receives a existing key, throws a exception.
         * 
         * Migrator's note.
         * 
         */
	}
	
	/**
	 * {@inheritDoc}
	 */
	//public override ClientConfiguration read() {
    public ClientConfiguration read() {
        /// <exception cref="ConfigurationException"></exception>
		string defaultXmlLocation = "/appsensor-client-config.xml";
		string defaultXsdLocation = "/appsensor_client_config_2.0.xsd";
		
		return read(defaultXmlLocation, defaultXsdLocation);
	}
	
	/**
	 * {@inheritDoc}
	 */
	//public override ClientConfiguration read(string xml, string xsd) {
    public ClientConfiguration read(string xml, string xsd) {
        /// <exception cref="ConfigurationException"></exception>
		ClientConfiguration configuration = null;
		//InputStream xmlInputStream = null;
        Stream xmlInputStream = null;
		//XMLStreamReader xmlReader = null;
        XmlReader xmlReader = null;

		try {
			//XMLInputFactory xmlFactory = XMLInputFactory.newInstance();
			XmlReaderSettings xmlFactory = new XmlReaderSettings();

			//xmlFactory.setProperty(XMLInputFactory.IS_REPLACING_ENTITY_REFERENCES, false);
			//xmlFactory.setProperty(XMLInputFactory.IS_SUPPORTING_EXTERNAL_ENTITIES, false);
            xmlFactory.DtdProcessing = DtdProcessing.Ignore;
            //xmlFactory.setProperty(XMLInputFactory.IS_NAMESPACE_AWARE, true);
            //xmlFactory.setProperty(XMLInputFactory.IS_VALIDATING, false);
            xmlFactory.ValidationType = ValidationType.None;
            //xmlFactory.ValidationType = ValidationType.Schema;
			xmlFactory.XmlResolver = null;
			
			XmlUtils.validateXMLSchema(xsd, xml);
			
			//xmlInputStream = GetType().getResourceAsStream(xml);
            xmlInputStream = new StreamReader(xml).BaseStream;
			
			//xmlReader = xmlFactory.createXMLStreamReader(xmlInputStream);
            xmlReader = XmlReader.Create(xmlInputStream);
			
			configuration = readClientConfiguration(xmlReader);
		//} catch(XMLStreamException | IOException | SAXException e) {
            //throw new ConfigurationException(e.getMessage(), e);
            } catch(XmlException e) {
                throw new org.owasp.appsensor.exceptions.ConfigurationException(e.Message, e);
            } catch(IOException e) {
                throw new org.owasp.appsensor.exceptions.ConfigurationException(e.Message, e);
		} finally {
			if(xmlReader != null) {
				try {
					xmlReader.Close();
				} catch (XmlException xse) {
					/** give up **/
				}
			}
			
			if(xmlInputStream != null) {
				try {
					xmlInputStream.Close();
				} catch (IOException ioe) {
					/** give up **/
				}
			}
		}
		
		return configuration;
	}
	
    /// <exception cref="XMLException"></exception>
	private ClientConfiguration readClientConfiguration(XmlReader xmlReader) {
		ClientConfiguration configuration = new ClientConfiguration();
		bool finished = false;

		while(!finished && xmlReader.MoveToNextAttribute()) {
			//int Event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(xmlReader.NodeType) {			
				case XmlNodeType.Element:
					if("config:appsensor-client-config".Equals(name)) {
						//
					} else if("config:server-connection".Equals(name)) {
						configuration.setServerConnection(readServerConnection(xmlReader));
					} else {
						/** unexpected start element **/
					}
					break;
				case XmlNodeType.EndElement:
					if("config:appsensor-client-config".Equals(name)) {
						finished = true;
					} else {
						/** unexpected end element **/
					}
					break;
				default:
					/** unused xml element - nothing to do **/
					break;
			}
		}
		
		return configuration;
	}
	
    /// <exception cref="XMLException"></exception>
	private ServerConnection readServerConnection(XmlReader xmlReader) {
		ServerConnection serverConnection = new ServerConnection();
		bool finished = false;
		
		//serverConnection.setType(xmlReader.getAttributeValue(null, "type"));
        serverConnection.setType(xmlReader.GetAttribute("type", null));
		
		while(!finished && xmlReader.MoveToNextAttribute()) {
			//int Event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(xmlReader.NodeType) {
				case XmlNodeType.Element:
					if("config:protocol".Equals(name)) {
						serverConnection.setProtocol(xmlReader.ReadString().Trim());
					} else if("config:host".Equals(name)) {
						serverConnection.setHost(xmlReader.ReadString().Trim());
					} else if("config:port".Equals(name)) {
						serverConnection.setPort(Int32.Parse(xmlReader.ReadString().Trim()));
					} else if("config:path".Equals(name)) {
						serverConnection.setPath(xmlReader.ReadString().Trim());
					} else if("config:client-application-identification-header-value".Equals(name)) {
						serverConnection.setClientApplicationIdentificationHeaderValue(xmlReader.ReadString().Trim());
					} else {
						/** unexpected start element **/
					}
					break;
				case XmlNodeType.EndElement:
					if("config:server-connection".Equals(name)) {
						finished = true;
					} else {
						/** unexpected end element **/
					}
					break;
				default:
					/** unused xml element - nothing to do **/
					break;
			}
		}
		
		return serverConnection;
	}
	
}
}