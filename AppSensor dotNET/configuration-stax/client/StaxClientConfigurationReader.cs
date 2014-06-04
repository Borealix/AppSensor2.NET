/*
import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLResolver;
import javax.xml.stream.XMLStreamConstants;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamReader;
using org.owasp.appsensor.util.XmlUtils;
 
import org.xml.sax.SAXException;
 */
using org.owasp.appsensor.exceptions.ConfigurationException;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
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
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override ClientConfiguration read() {
        /// <exception cref="ConfigurationException"></exception>
		string defaultXmlLocation = "/appsensor-client-config.xml";
		string defaultXsdLocation = "/appsensor_client_config_2.0.xsd";
		
		return read(defaultXmlLocation, defaultXsdLocation);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override ClientConfiguration read(string xml, string xsd) {
        /// <exception cref="ConfigurationException"></exception>
		ClientConfiguration configuration = null;
		//InputStream xmlInputStream = null;
        Stream xmlInputStream = null;
		//XMLStreamReader xmlReader = null;
        XmlReader xmlReader = null;

		try {
			//XMLInputFactory xmlFactory = XMLInputFactory.newInstance();
			XmlReader xmlFactory = XmlReader.Create();

			xmlFactory.setProperty(XMLInputFactory.IS_REPLACING_ENTITY_REFERENCES, false);
			xmlFactory.setProperty(XMLInputFactory.IS_SUPPORTING_EXTERNAL_ENTITIES, false);
			xmlFactory.setProperty(XMLInputFactory.IS_NAMESPACE_AWARE, true);
			xmlFactory.setProperty(XMLInputFactory.IS_VALIDATING, false);
			xmlFactory.setXmlResolver(new XmlResolver() {
			/// <exception cref="XMLStreamException"></exception>
            public override object resolveEntity(string arg0, string arg1, string arg2, string arg3) {
					return new ByteArrayInputStream(new byte[0]);
				}
			});
			
			XmlUtils.validateXMLSchema(xsd, xml);
			
			xmlInputStream = GetType().getResourceAsStream(xml);
			
			xmlReader = xmlFactory.createXMLStreamReader(xmlInputStream);
			
			configuration = readClientConfiguration(xmlReader);
		} catch(XMLStreamException | IOException | SAXException e) {
			throw new ConfigurationException (e.getMessage(), e);
		} finally {
			if(xmlReader != null) {
				try {
					xmlReader.close();
				} catch (XMLStreamException xse) {
					/** give up **/
				}
			}
			
			if(xmlInputStream != null) {
				try {
					xmlInputStream.close();
				} catch (IOException ioe) {
					/** give up **/
				}
			}
		}
		
		return configuration;
	}
	
	private ClientConfiguration readClientConfiguration(XMLStreamReader xmlReader) throws XMLStreamException {
		ClientConfiguration configuration = new ClientConfiguration();
		bool finished = false;

		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(event) {			
				case XMLStreamConstants.START_ELEMENT:
					if("config:appsensor-client-config".Equals(name)) {
						//
					} else if("config:server-connection".Equals(name)) {
						configuration.setServerConnection(readServerConnection(xmlReader));
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
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
	
	private ServerConnection readServerConnection(XMLStreamReader xmlReader) throws XMLStreamException {
		ServerConnection serverConnection = new ServerConnection();
		bool finished = false;
		
		serverConnection.setType(xmlReader.getAttributeValue(null, "type"));
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(event) {
				case XMLStreamConstants.START_ELEMENT:
					if("config:protocol".Equals(name)) {
						serverConnection.setProtocol(xmlReader.getElementText().Trim());
					} else if("config:host".Equals(name)) {
						serverConnection.setHost(xmlReader.getElementText().Trim());
					} else if("config:port".Equals(name)) {
						serverConnection.setPort(Integer.parseInt(xmlReader.getElementText().Trim()));
					} else if("config:path".Equals(name)) {
						serverConnection.setPath(xmlReader.getElementText().Trim());
					} else if("config:client-application-identification-header-value".Equals(name)) {
						serverConnection.setClientApplicationIdentificationHeaderValue(xmlReader.getElementText().Trim());
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
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