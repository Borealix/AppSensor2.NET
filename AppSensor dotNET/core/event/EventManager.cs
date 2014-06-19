//using java.util.Collection;
using org.owasp.appsensor;
using System.Collections.ObjectModel;
/**
 * The EventManager is the key interface that the {@link ClientApplication} accesses to 
 * interact with AppSensor.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.events {
    /** In AppSensor Java version appears "event" in place of "Event" but "event" is a reseved word in C#.
     *  I decided to use:
     *  Events for the namespace.
     *  Event for the method.
     *  event for the instruction, obviously.
     *  
     * by Luis Serna (luis.sego@yahoo.com.mx)
     */

    public class EventManager{
    /**
    * Add an {@link Event}.
    * 
    * @param event {@link Event} to Add
    */
    public void addEvent(Event Event) {
    }
    /**
    * Add an {@link Attack}
    * @param attack {@link Attack} to Add
    */
    public void addAttack(Attack attack) {
    }
    /**
    * Retrieve any {@link Response}s generated that apply to this 
    * client since the last time the client called this method.
    *  
    * @return a Collection of {@link Response} objects 
    */
    public Collection<Response> getResponses();
    }
}