using org.owasp.appsensor.criteria;
using org.owasp.appsensor.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using org.owasp.appsensor.accesscontrol;

namespace org.owasp.appsensor.wcf {
    /**
     * This is the soap endpoint that handles requests on the server-side. 
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     */

    //@WebService(
    //        portName = "SoapRequestHandlerPort",
    //        serviceName = "SoapRequestHandlerService",
    //        targetNamespace = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl",
    //        endpointInterface = "org.owasp.appsensor.handler.SoapRequestHandler"
    //        )
    //@HandlerChain(file="handler-chain.xml")
    //@Named

    public class WCFRequestHandler : IWCFRequestHandler {

        //@Inject
        private AppSensorServer appSensorServer;

        //@Inject
        private AccessControlUtils accessControlUtils;

        //@Context
        private IncomingWebRequestContext requestContext;

        /**
         * {@inheritDoc}
         */
        //@Override
        //@POST
        //@Path("/events")
        public void addEvent(Event Event) { // throws NotAuthorizedException
            accessControlUtils.checkAuthorization(org.owasp.appsensor.accesscontrol.Action.ADD_EVENT, requestContext);

            Event.setDetectionSystemId(getClientApplicationName());

            appSensorServer.getEventStore().addEvent(Event);
        }

        /**
         * {@inheritDoc}
         */
        //@Override
        //@POST
        //@Path("/attacks")
        public void addAttack(Attack attack) { // throws NotAuthorizedException
            accessControlUtils.checkAuthorization(org.owasp.appsensor.accesscontrol.Action.ADD_ATTACK, requestContext);

            attack.setDetectionSystemId(getClientApplicationName());

            appSensorServer.getAttackStore().addAttack(attack);
        }

        /**
         * {@inheritDoc}
         */
        //@Override
        //@GET
        //@Path("/responses")
        //@Produces(MediaType.APPLICATION_JSON)
        //public Collection<Response> getResponses(@QueryParam("earliest") String earliest) { // throws NotAuthorizedException
        public Collection<Response> getResponses(String earliest) { // throws NotAuthorizedException
            accessControlUtils.checkAuthorization(org.owasp.appsensor.accesscontrol.Action.GET_RESPONSES, requestContext);

            SearchCriteria criteria = new SearchCriteria().
                    setDetectionSystemIds(StringUtils.toCollection(getClientApplicationName())).
                    setEarliest(earliest);

            return appSensorServer.getResponseStore().findResponses(criteria);
        }

        /**
         * Helper method to retrieve client application name.
         * This is set by the {@link ClientApplicationIdentificationFilter} 
         * 
         * @return client application name
         */
        private string getClientApplicationName() {
            string clientApplicationName = (string)requestContext.GetType().GetProperty(RequestHandler.APPSENSOR_CLIENT_APPLICATION_IDENTIFIER_ATTR).ToString();

            return clientApplicationName;
        }
    }
}