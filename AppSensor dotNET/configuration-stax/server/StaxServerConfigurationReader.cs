/*
import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.List;
import java.util.Collection;
import java.util.HashMap;
import java.util.Map;

import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLResolver;
import javax.xml.stream.XMLStreamConstants;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamReader;
 
import org.xml.sax.SAXException;
 */
using org.owasp.appsensor.ClientApplication;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.Interval;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.Threshold;
using org.owasp.appsensor.accesscontrol.Role;
using org.owasp.appsensor.correlation.CorrelationSet;
using org.owasp.appsensor.util.XmlUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
/**
 * This implementation parses the {@link ServerConfiguration} objects 
 * from the specified XML file via the StAX API.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.configuration.server{

public class StaxServerConfigurationReader : ServerConfigurationReader {
	//private static final string XSD_NAMESPACE = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/xsd/appsensor_server_config_2.0.xsd";
	private static string XSD_NAMESPACE = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/xsd/appsensor_server_config_2.0.xsd";
	
	//private Map<string, string> namespaces = new HashMap<string, string>();
    private IDictionary<string, string> namespaces = new Dictionary<string, string>();

	
	public StaxServerConfigurationReader() {
		/** initialize namespaces **/
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
	public override ServerConfiguration read() {
        /// <exception cref="ConfigurationException"></exception>
		string defaultXmlLocation = "/appsensor-server-config.xml";
		string defaultXsdLocation = "/appsensor_server_config_2.0.xsd";
		
		return read(defaultXmlLocation, defaultXsdLocation);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override ServerConfiguration read(string xml, string xsd) {
         /// <exception cref="ConfigurationException"></exception>
		ServerConfiguration configuration = null;
		//InputStream xmlInptStream = null;
        Stream xmlInputStream = null; 
		//XMLStreamReader xmlReader = null;
        XmlReader xmlReader = null;
		
		try {
			XMLInputFactory xmlFactory = XMLInputFactory.newInstance();
			
			xmlFactory.setProperty(XMLInputFactory.IS_REPLACING_ENTITY_REFERENCES, false);
			xmlFactory.setProperty(XMLInputFactory.IS_SUPPORTING_EXTERNAL_ENTITIES, false);
			xmlFactory.setProperty(XMLInputFactory.IS_NAMESPACE_AWARE, true);
			xmlFactory.setProperty(XMLInputFactory.IS_VALIDATING, false);
			xmlFactory.setXMLResolver(new XMLResolver() {
				public override object resolveEntity(string arg0, string arg1, string arg2, string arg3) {
                     /// <exception cref="XMLStreamException"></exception>
					return new ByteArrayInputStream(new byte[0]);
				}
			});
			
			XmlUtils.validateXMLSchema(xsd, xml);
			
			xmlInputStream = GetType().getResourceAsStream(xml);
			
			xmlReader = xmlFactory.createXMLStreamReader(xmlInputStream);
			
			configuration = readServerConfiguration(xmlReader);
		} catch(XMLStreamException | IOException | SAXException e) {
			throw new ConfigurationException(e.getMessage(), e);
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
	
	private ServerConfiguration readServerConfiguration(XMLStreamReader xmlReader) {
        /// <exception cref="XMLStreamException"></exception>
		ServerConfiguration configuration = new StaxServerConfiguration(false);
		bool finished = false;
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);

			switch(event) {		
				case XMLStreamConstants.START_ELEMENT:
					if("config:appsensor-server-config".Equals(name)) {
						//
					} else if("config:client-application-identification-header-name".Equals(name)) {
						configuration.setClientApplicationIdentificationHeaderName(xmlReader.getElementText().Trim());
					} else if("config:client-applications".Equals(name)) {
						configuration.getClientApplications().addAll(readClientApplications(xmlReader));
					} else if("config:correlation-config".Equals(name)) {
						configuration.getCorrelationSets().addAll(readCorrelationSets(xmlReader));
					} else if("config:detection-point".Equals(name)) {
						configuration.getDetectionPoints().Add(readDetectionPoint(xmlReader));
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
					if("config:appsensor-server-config".Equals(name)) {
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
	
	private Collection<ClientApplication> readClientApplications(XMLStreamReader xmlReader) {
        /// <exception cref="XMLStreamException"></exception>
		Collection<ClientApplication> clientApplications = new List<>();
		bool finished = false;
		
		ClientApplication clientApplication = null;
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(Event) {
				case XMLStreamConstants.START_ELEMENT:
					if("config:client-application".Equals(name)) {
						clientApplication = new ClientApplication();
					} else if("config:name".Equals(name)) {
						clientApplication.setName(xmlReader.getElementText().Trim());
					} else if("config:role".Equals(name)) {
						clientApplication.getRoles().Add(Role.valueOf(xmlReader.getElementText().Trim()));
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
					if("config:client-application".Equals(name)) {
						clientApplications.Add(clientApplication);
					} else if("config:client-applications".Equals(name)) {
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
		
		return clientApplications;
	}
	/// <exception cref="XMLStreamException"></exception>
	private Collection<CorrelationSet> readCorrelationSets(XMLStreamReader xmlReader) {
		Collection<CorrelationSet> correlationSets = new List<>();
		bool finished = false;
		
		CorrelationSet correlationSet = null;
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(event) {
				case XMLStreamConstants.START_ELEMENT:
					if("config:correlated-client-set".Equals(name)) {
						correlationSet = new CorrelationSet();
					} else if("config:client-application-name".Equals(name)) {
						correlationSet.getClientApplications().Add(xmlReader.getElementText().Trim());
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
					if("config:correlated-client-set".Equals(name)) {
						correlationSets.Add(correlationSet);
					} else if("config:correlation-config".Equals(name)) {
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
		
		return correlationSets;
	}
	/// <exception cref="XMLStreamException"></exception>
	private DetectionPoint readDetectionPoint(XMLStreamReader xmlReader) {
		DetectionPoint detectionPoint = new DetectionPoint();
		bool finished = false;
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(event) {
				case XMLStreamConstants.START_ELEMENT:
					if("config:id".Equals(name)) {
						detectionPoint.setId(xmlReader.getElementText().Trim());
					} else if("config:threshold".Equals(name)) {
						detectionPoint.setThreshold(readThreshold(xmlReader));
					} else if("config:response".Equals(name)) {
						detectionPoint.getResponses().Add(readResponse(xmlReader));
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
					if("config:detection-point".Equals(name)) {
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
		
		return detectionPoint;
	}
	
	private Threshold readThreshold(XMLStreamReader xmlReader) throws XMLStreamException {
		Threshold threshold = new Threshold();
		bool finished = false;
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(event) {
				case XMLStreamConstants.START_ELEMENT:
					if("config:count".Equals(name)) {
						threshold.setCount(Integer.parseInt(xmlReader.getElementText().Trim()));
					} else if("config:interval".Equals(name)) {
						Interval interval = new Interval();
						interval.setUnit(xmlReader.getAttributeValue(null, "unit").Trim());
						interval.setDuration(Integer.parseInt(xmlReader.getElementText().Trim()));
						threshold.setInterval(interval);
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
					if("config:threshold".Equals(name)) {
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
		
		return threshold;
	}
	
	private Response readResponse(XMLStreamReader xmlReader) throws XMLStreamException {
		Response response = new Response();
		bool finished = false;
		
		while(!finished && xmlReader.hasNext()) {
			int event = xmlReader.next();
			string name = XmlUtils.getElementQualifiedName(xmlReader, namespaces);
			
			switch(event) {
				case XMLStreamConstants.START_ELEMENT:
					if("config:action".Equals(name)) {
						response.setAction(xmlReader.getElementText().Trim());
					} else if("config:interval".Equals(name)) {
						Interval interval = new Interval();
						interval.setUnit(xmlReader.getAttributeValue(null, "unit").Trim());
						interval.setDuration(Integer.parseInt(xmlReader.getElementText().Trim()));
						response.setInterval(interval);
					} else {
						/** unexpected start element **/
					}
					break;
				case XMLStreamConstants.END_ELEMENT:
					if("config:response".Equals(name)) {
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
		
		return response;
	}
	
}
}