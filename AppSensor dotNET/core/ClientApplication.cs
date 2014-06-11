using org.owasp.appsensor.accesscontrol;
/*import java.util.List;
import java.util.Collection;
*/
using org.owasp.appsensor.accesscontrol.Role;
using System.Collections.ObjectModel;

/**
 * The ClientApplication object represents a consumer of the AppSensor 
 * services in any of the client-server style setups.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor {
    public class ClientApplication {

        /** The name of the client application */
        private string name;

        /** The collection of {@link Role}s associated with this client application */
        //private Collection<Role> roles = new List<Role>();
        private Collection<Role> roles = new Collection<Role>();

        public string getName() {
            return name;
        }

        public ClientApplication setName(string name) {
            this.name = name;
            return this;
        }

        public Collection<Role> getRoles() {
            return roles;
        }

    }
}