using Ninject;
using org.owasp.appsensor;
using org.owasp.appsensor.logging;
using org.owasp.appsensor.responses;
using org.owasp.appsensor.storage;
using log4net;
/**
 * This is a reference {@link Response} analysis engine, 
 * and is an implementation of the Observer pattern. 
 * 
 * It is notified with implementations of the {@link Response} class.
 * 
 * The implementation is trivial and simply delegates the work to the configured 
 * {@link ResponseHandler} for processing.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.analysis {
    //@Loggable
    // [Named("ReferenceResponseAnalysisEngine")]
    public class ReferenceResponseAnalysisEngine : ResponseAnalysisEngine {

        private ILog Logger;

        [Inject]
        private AppSensorClient appSensorClient;

        /**
         * This method simply catches responses and calls the 
         * configured {@link ResponseHandler} to process them. 
         * 
         * @param response {@link Response} that has been added to the {@link ResponseStore}.
         */
        //public override void analyze(Response response) {
        public void analyze(Response response) {
            if(response != null) {
                Logger.Info("Response executed for user <" + response.GetUser().getUsername() + "> - executing response action " + response.getAction());

                appSensorClient.getResponseHandler().handle(response);
            }
        }
    }
}