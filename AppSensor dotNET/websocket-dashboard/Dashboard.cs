//import java.io.IOException;
/*
using javax.websocket.OnClose;
using javax.websocket.OnError;
using javax.websocket.OnMessage;
using javax.websocket.OnOpen;
using javax.websocket.Session;
using javax.websocket.server.ServerEndpoint;
*/
// using System.Net.WebSockets.WebSocket;
using Microsoft.Web.WebSockets;
using System;

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
        //System.Err.println("Opened connection with client: " + session.getId());
        Console.Error.WriteLine("Opened connection with client: " + session.getId());
    }
    
    //@OnMessage
    public string onMessage(string message, Session session) {
    	//System.err.println
        Console.Error.WriteLine("New message from Client " + session.getId() + ": " + message);
    	
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
    	//System.err.println
        Console.Error.WriteLine("Closed connection with client: " + session.getId());
    }
    
    //@OnError
    public void onError(Exception exception, Session session) {
    	//System.err.println
        Console.Error.WriteLine("Error for client: " + session.getId());
    }
}
}