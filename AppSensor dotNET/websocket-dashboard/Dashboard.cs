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
using SuperSocket.SocketBase;
using System;
using System.IO;

namespace org.owasp.appsensor.websocket {
/**
 * A simple dashboard for the websocket implementation.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
//@ServerEndpoint(value = "/dashboard")
public class Dashboard {
    
    //@OnOpen
    public void onOpen(AppSession session) {
        //Console.Error.WriteLine("Opened connection with client: " + session.getId());
        Console.Error.WriteLine("Opened connection with client: " + session.SessionID);
    }
    
    //@OnMessage
    public string onMessage(string message, AppSession session) {
        Console.Error.WriteLine("New message from Client " + session.SessionID + ": " + message);
    	
    	//should echo back whatever is heard from any client to all clients
    	//foreach (Session sess in session.getOpenSessions()) {
        foreach(AppSession sess in session.AppServer.GetAllSessions()) {
            if(sess.Connected) {
    			try {
					//sess.RemoteEndPoint.sendText(message);
                    sess.Send(message);
				} catch (IOException e) {
                    Console.WriteLine(System.Environment.StackTrace);
				}
    		}
    	}

    	return null;
    }
    
    //@OnClose
    public void onClose(AppSession session) {
        Console.Error.WriteLine("Closed connection with client: " + session.SessionID);
    }
    
    //@OnError
    public void onError(Exception exception, AppSession session) {
        Console.Error.WriteLine("Error for client: " + session.SessionID);
    }
}
}