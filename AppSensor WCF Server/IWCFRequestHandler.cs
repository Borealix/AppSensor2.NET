using System.Collections.ObjectModel;
using System.ServiceModel;

namespace org.owasp.appsensor.wcf {
    /**
     * This is the soap endpoint interface for handling requests on the server-side. 
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     */
    [ServiceContract(Namespace = "https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl")]
    public interface IWCFRequestHandler { // extends RequestHandler

        /**
         * {@inheritDoc}
         */
        //@Override
        [OperationContract]
        void addEvent(Event Event); // throws NotAuthorizedException

        /**
         * {@inheritDoc}
         */
        //@Override
        [OperationContract]
        void addAttack(Attack attack); // throws NotAuthorizedException

        /**
         * {@inheritDoc}
         */
        //@Override
        [OperationContract]
        Collection<Response> getResponses(string earliest); // throws NotAuthorizedException

    }
}