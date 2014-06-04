namespace Util.HashCodeBuilder {
    /*
     * In this folder are the codes of the utilities that Appsensor Java version uses of libraries
     * that don´t exist for .NET.
     * 
     * Migrator's note.
     */
    public sealed class HashCodeBuilder {
        /*
         * Source http://stackoverflow.com/questions/2912340/c-sharp-hashcode-builder
         */
        private int hash = 17;

        public HashCodeBuilder Add(int value) {
            unchecked {
                hash = hash * 31 + value; //see Effective Java for reasoning
                // can be any prime but hash * 31 can be opimised by VM to hash << 5 - hash
            }
            return this;
        }

        public HashCodeBuilder Add(object value) {
            if(value == null)
                return this;
            return Add(value.GetHashCode());
        }

        public HashCodeBuilder Add(float value) {
            return Add(value.GetHashCode());
        }

        public HashCodeBuilder Add(double value) {
            return Add(value.GetHashCode());
        }

        public override int GetHashCode() {
            return hash;
        }
    }
}