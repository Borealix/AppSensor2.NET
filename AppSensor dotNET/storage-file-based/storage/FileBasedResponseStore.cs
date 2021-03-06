using org.owasp.appsensor;
using org.owasp.appsensor.criteria;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.logging;
using org.owasp.appsensor.util;
using System.Linq;
using org.owasp.appsensor.storage;
using log4net;
using System.IO;
using System.Collections.ObjectModel;
using System;
using Ninject;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

/**
 * This is a reference implementation of the {@link ResponseStore}.
 * 
 * Implementations of the {@link ResponseListener} interface can register with 
 * this class and be notified when new {@link Response}s are added to the data store 
 * 
 * This implementation is file-based and has the feature that it will load previous 
 * {@link Response}s if configured to do so.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage {
    //@Loggable
    // [Named("FileBasedResponseStore")]
    public class FileBasedResponseStore : ResponseStore {

        private ILog Logger;

        //@SuppressWarnings("unused")
        [Inject]
        private AppSensorServer appSensorServer;

        public static string DEFAULT_FILE_PATH = Path.DirectorySeparatorChar + "tmp";

        public static string DEFAULT_FILE_NAME = "responses.txt";

        public static string FILE_PATH_CONFIGURATION_KEY = "filePath";

        public static string FILE_NAME_CONFIGURATION_KEY = "fileName";

        private DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));

        //private Path path = null;
        private String path = null;

        /**
         * {@inheritDoc}
         */
        public override void addResponse(Response response) {
            Logger.Warn("Security response " + response + " triggered for user: " + response.GetUser().getUsername());

            writeResponse(response);

            base.notifyListeners(response);
        }

        /**
         * {@inheritDoc}
         */
        public override Collection<Response> findResponses(SearchCriteria criteria) {
            if(criteria == null) {
                throw new ArgumentException("criteria must be non-null");
            }

            Collection<Response> matches = new Collection<Response>();

            User user = criteria.GetUser();
            //Collection<string> detectionSystemIds = criteria.getDetectionSystemIds();
            HashSet<string> detectionSystemIds = criteria.getDetectionSystemIds();
            DateTime? earliest = DateUtils.fromString(criteria.getEarliest());

            Collection<Response> responses = loadResponses();

            foreach(Response response in responses) {
                //check user match if user specified
                bool userMatch = (user != null) ? user.Equals(response.GetUser()) : true;

                //check detection system match if detection systems specified
                bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.Count > 0) ?
                        detectionSystemIds.Contains(response.GetDetectionSystemId()) : true;

                bool earliestMatch = (earliest != null) ? earliest < DateUtils.fromString(response.GetTimestamp()) : true;

                if(userMatch && detectionSystemMatch && earliestMatch) {
                    matches.Add(response);
                }
            }

            return matches;
        }

        protected void writeResponse(Response response) {
            //string json = gson.toJson(response);
            string json = ser.ToString();

            try {
                //Files.write(getPath(), Arrays.asList(json), StandardCharsets.UTF_8, StandardOpenOption.APPEND, StandardOpenOption.WRITE);
                File.WriteAllText(getPath().ToString(), json, System.Text.Encoding.UTF8);
            } catch(IOException e) {
                Logger.Error("Error occurred loading writing event file to path: " + getPath(), e);
            }
        }

        protected Collection<Response> loadResponses() {
            Collection<Response> responses = new Collection<Response>();

            try {
                List<String> lines = File.ReadAllLines(getPath().ToString()).ToList<String>();

                foreach(string line in lines) {
                    //Response response = gson.fromJson(line, Response.Class);
                    Response response = JsonConvert.DeserializeObject<Response>(line); // Revisar

                    responses.Add(response);
                }
            } catch(IOException e) {
                Logger.Error("Error occurred loading configured event file from path: " + getPath(), e);
            }

            return responses;
        }

        //protected Path getPath() {
        public String getPath() {
            if(path != null && File.Exists(path.ToString())) {
                return path;
            }

            path = FileUtils.getOrCreateFile(lookupFilePath(), lookupFileName());

            return path;
        }

        protected string lookupFilePath() {
            return DEFAULT_FILE_PATH;
        }

        protected string lookupFileName() {
            return DEFAULT_FILE_NAME;
        }
    }
}