//using java.util.List;
//using java.util.Collection;
using org.owasp.appsensor.accesscontrol.Role;
using org.owasp.appsensor.configuration.client;

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

        public override int hashCode() {
            return new HashCodeBuilder(17, 31).
                            Append(serverConnection).
                            toHashCode();
        }

        public override bool Equals(object obj) {
            if(this == obj)
                return true;
            if(obj == null)
                return false;
            if(GetType() != obj.GetType())
                return false;

            ClientConfiguration other = (ClientConfiguration)obj;

            return new EqualsBuilder().
                            Append(serverConnection, other.getServerConnection()).
                            isEquals();
        }

        public override string ToString() {
            return new StringBuilder(this).
                            Append("serverConnection", serverConnection).
                            ToString();
        }
    }
}