using Ninject;
using org.owasp.appsensor.exceptions;
using System.Text;
using Tools.HashCodeBuilder;
/**
 * This interface is to be fulfilled by implementations that load a configuration 
 * file and provide an object representation of it. 
 * 
 * The current implementation only consists of an XML configuration that utilizes a 
 * standardized XSD schema. However, there is nothing in the interface requiring the 
 * XML implementation. Most standard users will likely stick to the standard implementation. 
 * 
 * TODO: may update this interface is we move to something other than "reading" 
 * the config, ie. supporting configs from data stores/cloud, etc.
 * 
 * @author johnmelton
 */

namespace org.owasp.appsensor.configuration.client {
    // [Named("ServerConnection")]
    public class ServerConnection {

        /** type of server connection: rest/soap */
        private string type;

        /** The protocol that should be used: http or https */
        private string protocol;

        /** The host to connect to: IP or hostname */
        private string host;

        /** The port to connect to  */
        private int port;

        /** The path used: essentially the prefix where the webapp is deployed, eg. "/appsensor-ws-rest-server/api/v1" */
        private string path;

        /** The client application identifier header value */
        private string clientApplicationIdentificationHeaderValue;

        public string getType() {
            return type;
        }

        public ServerConnection setType(string type) {
            this.type = type;
            return this;
        }

        public string getProtocol() {
            return protocol;
        }

        public ServerConnection setProtocol(string protocol) {
            this.protocol = protocol;
            return this;
        }

        public string getHost() {
            return host;
        }

        public ServerConnection setHost(string host) {
            this.host = host;
            return this;
        }

        public int getPort() {
            return port;
        }

        public ServerConnection setPort(int port) {
            this.port = port;
            return this;
        }

        public string getPath() {
            return path;
        }

        public ServerConnection setPath(string path) {
            this.path = path;
            return this;
        }

        public string getClientApplicationIdentificationHeaderValue() {
            return clientApplicationIdentificationHeaderValue;
        }

        public ServerConnection setClientApplicationIdentificationHeaderValue(
                string clientApplicationIdentificationHeaderValue) {
            this.clientApplicationIdentificationHeaderValue = clientApplicationIdentificationHeaderValue;

            return this;
        }

        //public override int hashCode() {
        public int hashCode() {
            //return new HashCodeBuilder(17, 31).
            return new HashCodeBuilder().
                    Add(type).
                    Add(protocol).
                    Add(host).
                    Add(port).
                    Add(path).
                    Add(clientApplicationIdentificationHeaderValue).
                    GetHashCode();
        }

        public override bool Equals(object obj) {
            if(this == obj)
                return true;
            if(obj == null)
                return false;
            if(GetType() != obj.GetType())
                return false;

            ServerConnection other = (ServerConnection)obj;

            /*return new EqualsBuilder().
                    Append(type, other.getType()).
                    Append(protocol, other.getProtocol()).
                    Append(host, other.getHost()).
                    Append(port, other.getPort()).
                    Append(path, other.getPath()).
                    Append(clientApplicationIdentificationHeaderValue, other.getClientApplicationIdentificationHeaderValue()).
                    isEquals();*/
            if(type.Equals(other.getType()) &&
            protocol.Equals(other.getProtocol()) &&
            host.Equals(other.getHost()) &&
            port == other.getPort() &&
            path.Equals(other.getPath()) &&
            clientApplicationIdentificationHeaderValue.Equals (other.getClientApplicationIdentificationHeaderValue())) {
                return true;
            } else {
                return false;
            }
        }

        //public override string toString() {
        public string toString() {
            //return new StringBuilder(this).
            return new StringBuilder().
                    AppendFormat("type", type).
                    AppendFormat("protocol", protocol).
                    AppendFormat("host", host).
                    AppendFormat("port", port).
                    AppendFormat("path", path).
                    AppendFormat("clientApplicationIdentificationHeaderValue", clientApplicationIdentificationHeaderValue).
                    ToString();
        }

    }
}