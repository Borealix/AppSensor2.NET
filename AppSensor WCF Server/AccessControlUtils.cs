using Ninject;
using org.owasp.appsensor.accesscontrol;
using System.ServiceModel.Web;
namespace org.owasp.appsensor.wcf {
    /**
     * This is a simple helper class for performing access control checks for REST requests.
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     * @author RaphaÃ«l Taban
     */
    //@Named
    public class AccessControlUtils {

        //@Inject
        [Inject]
        private AppSensorServer appSensorServer;

        /**
         * Check authz before performing action.
         * 
         * @param action desired action
         * @throws NotAuthorizedException thrown if user does not have role.
         */
        public void checkAuthorization(Action action, IncomingWebRequestContext context) { // throws NotAuthorizedException
            string clientApplicationName = (string)context.GetType().GetProperty(RequestHandler.APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR).ToString();

            ClientApplication clientApplication = appSensorServer.getConfiguration().findClientApplication(clientApplicationName);

            appSensorServer.getAccessController().assertAuthorized(clientApplication, action, new Context());
        }

    }
}