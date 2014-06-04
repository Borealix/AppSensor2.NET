//import java.io.IOException;
/*
using javax.websocket.OnClose;
using javax.websocket.OnError;
using javax.websocket.OnMessage;
using javax.websocket.OnOpen;
using javax.websocket.Session;
using javax.websocket.server.ServerEndpoint;
*/
namespace org.owasp.appsensor.websocket {
/**
 * A simple dashboard for the websocket implementation.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
//@ServerEndpoint(value = "/dashboard")
public class Dashboard {
    
    //@OnOpen
    public void onOpen(Session session) {
        System.err.println("Opened connection with client: " + session.getId());
    }
    
    //@OnMessage
    public string onMessage(string message, Session session) {
    	System.err.println("New message from Client " + session.getId() + ": " + message);
    	
    	//should echo back whatever is heard from any client to all clients
    	foreach (Session sess in session.getOpenSessions()) {
    		if (sess.isOpen()) {
    			try {
					sess.getBasicRemote().sendText(message);
				} catch (IOException e) {
					e.printStackTrace();
				}
    		}
    	}

    	return null;
    }
    
    //@OnClose
    public void onClose(Session session) {
    	System.err.println("Closed connection with client: " + session.getId());
    }
    
    //@OnError
    public void onError(Throwable exception, Session session) {
    	System.err.println("Error for client: " + session.getId());
    }
}
}