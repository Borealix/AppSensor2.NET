/*
import java.io.File;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.StandardOpenOption;
import java.util.List;
import java.util.Arrays;
import java.util.Collection;

import javax.inject.Inject;
import javax.inject.Named;

import org.joda.time.DateTime;
 */
using org.owasp.appsensor;
using org.owasp.appsensor.criteria;
using org.owasp.appsensor.listener;
using org.owasp.appsensor.logging;
using org.owasp.appsensor.util;
using log4net;
using System.IO;
using System.Collections.ObjectModel;
using System;
using Ninject;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

/**
 * This is a reference implementation of the {@link AttackStore}.
 * 
 * Implementations of the {@link AttackListener} interface can register with 
 * this class and be notified when new {@link Attack}s are added to the data store 
 * 
 * This implementation is file-based and has the feature that it will load previous 
 * {@link Attack}s if configured to do so.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage {
    //@Loggable
    // [Named("FileBasedAttackStore")]
    public class FileBasedAttackStore : AttackStore {

        private ILog Logger;

        //@SuppressWarnings("unused")
        [Inject]
        private AppSensorServer appSensorServer;

        //public static string DEFAULT_FILE_PATH = File.separator + "tmp";
        public static string DEFAULT_FILE_PATH = Path.DirectorySeparatorChar + "tmp";

        public static string DEFAULT_FILE_NAME = "attacks.txt";

        public static string FILE_PATH_CONFIGURATION_KEY = "filePath";

        public static string FILE_NAME_CONFIGURATION_KEY = "fileName";

        //private Json gson = new Gson();

        //private Path path = null;
        private String path = null;

        /**
         * {@inheritDoc}
         */
        public override void addAttack(Attack attack) {
            Logger.Warn("Security attack " + attack.GetDetectionPoint().getId() + " triggered by user: " + attack.GetUser().getUsername());

            writeAttack(attack);

            //super.notifyListeners(attack);
            base.notifyListeners(attack);
        }

        /**
         * {@inheritDoc}
         */
        public override Collection<Attack> findAttacks(SearchCriteria criteria) {
            if(criteria == null) {
                //throw new IllegalArgumentException("criteria must be non-null");
                throw new ArgumentException("criteria must be non-null");
            }

            //Collection<Attack> matches = new List<Attack>();
            Collection<Attack> matches = new Collection<Attack>();

            User user = criteria.GetUser();
            DetectionPoint detectionPoint = criteria.GetDetectionPoint();
            //Collection<string> detectionSystemIds = criteria.getDetectionSystemIds();
            HashSet<string> detectionSystemIds = criteria.getDetectionSystemIds();
            DateTime? earliest = DateUtils.fromString(criteria.getEarliest());

            Collection<Attack> attacks = loadAttacks();

            foreach(Attack attack in attacks) {
                //check user match if user specified
                bool userMatch = (user != null) ? user.Equals(attack.GetUser()) : true;

                //check detection system match if detection systems specified
                //bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.size() > 0) ? 
                bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.Count > 0) ?
                        detectionSystemIds.Contains(attack.GetDetectionSystemId()) : true;

                //check detection point match if detection point specified
                bool detectionPointMatch = (detectionPoint != null) ?
                        detectionPoint.getId().Equals(attack.GetDetectionPoint().getId()) : true;

                //bool earliestMatch = (earliest != null) ? earliest.isBefore(DateUtils.fromString(attack.GetTimestamp())) : true;
                bool earliestMatch = (earliest != null) ? earliest < DateUtils.fromString(attack.GetTimestamp()) : true;

                if(userMatch && detectionSystemMatch && detectionPointMatch && earliestMatch) {
                    matches.Add(attack);
                }
            }

            return matches;
        }

        protected void writeAttack(Attack attack) {
            //string json = gson.toJson(attack);
            string json = attack.toString();
            try {
                //Files.write(getPath(), Arrays.asList(json), StandardCharsets.UTF_8, StandardOpenOption.APPEND, StandardOpenOption.WRITE);
                File.WriteAllText(getPath().ToString(), json, System.Text.Encoding.UTF8);
            } catch(IOException e) {
                Logger.Error("Error occurred loading writing event file to path: " + getPath(), e);
            }
        }

        protected Collection<Attack> loadAttacks() {
            //Collection<Attack> attacks = new List<>();
            Collection<Attack> attacks = new Collection<Attack>();

            try {
                //Collection<string> lines = File.ReadAllLines(getPath());
                List<String> lines = File.ReadAllLines(getPath().ToString()).ToList<String>();

                foreach(string line in lines) {
                    //Attack attack = gson.fromJson(line, Attack.GetType);
                    Attack attack = JsonConvert.DeserializeObject<Attack>(line); // Revisar

                    attacks.Add(attack);
                }
            } catch(IOException e) {
                Logger.Error("Error occurred loading configured attack file from path: " + getPath(), e);
            }

            return attacks;
        }

        //protected Path getPath() {
        protected String getPath() {
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