//using java.util.List;
//using java.util.Collection;
using org.owasp.appsensor.accesscontrol;
using org.owasp.appsensor.configuration.client;
using System.Text;
using Tools.HashCodeBuilder;

/**
 * The ClientApplication object represents a consumer of the AppSensor 
 * services in any of the client-server style setups.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */

namespace org.owasp.appsensor.configuration.client {

    public class ClientConfiguration {

        // Server connection with configuration info for rest/soap connections
        private ServerConnection serverConnection;
        public ServerConnection getServerConnection() {
            return serverConnection;
        }

        public ClientConfiguration setServerConnection(ServerConnection serverConnection) {
            this.serverConnection = serverConnection;
            return this;
        }

        //public override int hashCode() {
        public int hashCode() {
            return new HashCodeBuilder().
                            Add(serverConnection).
                            GetHashCode();
        }

        public override bool Equals(object obj) {
            if(this == obj)
                return true;
            if(obj == null)
                return false;
            if(GetType() != obj.GetType())
                return false;

            ClientConfiguration other = (ClientConfiguration)obj;

            /*return new EqualsBuilder().
                            Append(serverConnection, other.getServerConnection()).
                            isEquals();*/
            if(serverConnection.Equals(other.getServerConnection())) {
                return true;
            } else {
                return false;
            }
        }

        //public override string toString() {
        public string toString() {
            return new StringBuilder().
                            AppendFormat("serverConnection", serverConnection).
                            ToString();
        }
    }
}