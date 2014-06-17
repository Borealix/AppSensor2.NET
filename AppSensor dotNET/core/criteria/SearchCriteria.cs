//using java.util.Collection;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.User;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace org.owasp.appsensor.criteria {

    public class SearchCriteria {

        private User user;

        private DetectionPoint detectionPoint;

        //private Collection<string> detectionSystemIds;
        private HashSet<string> detectionSystemIds;

        private string earliest;

        public User GetUser() {
            return user;
        }

        public SearchCriteria setUser(User user) {
            this.user = user;
            return this;
        }

        public DetectionPoint GetDetectionPoint() {
            return detectionPoint;
        }

        public SearchCriteria setDetectionPoint(DetectionPoint detectionPoint) {
            this.detectionPoint = detectionPoint;
            return this;
        }

        //public Collection<string> getDetectionSystemIds() {
        public HashSet<string> getDetectionSystemIds() {
            return detectionSystemIds;
        }

        //public SearchCriteria setDetectionSystemIds(Collection<string> detectionSystemIds) {
        public SearchCriteria setDetectionSystemIds(HashSet<string> detectionSystemIds) {
            this.detectionSystemIds = detectionSystemIds;
            return this;
        }

        public string getEarliest() {
            return earliest;
        }

        public SearchCriteria setEarliest(string earliest) {
            this.earliest = earliest;
            return this;
        }
    }
}