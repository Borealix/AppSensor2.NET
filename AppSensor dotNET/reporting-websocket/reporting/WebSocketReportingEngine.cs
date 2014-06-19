using org.owasp.appsensor;
using org.owasp.appsensor.logging;
using Ninject;
using System.Collections.ObjectModel;
using log4net;
using System;
using SuperSocket.SocketBase;
using System.IO;
using System.Deployment.Application;
using System.Runtime.Serialization.Json;
using SuperSocket.ClientEngine;
using WebSocket4Net;
//using System.Net.Sockets;

/**
 * This is the websocket-based reporting engine, and is an implementation of the observer pattern. 
 * 
 * It is notified with implementations of the *Listener interfaces and is 
 * passed the observed objects. In this case, we are concerned with {@link Event},
 *  {@link Attack} and {@link Response}
 * implementations. 
 * 
 * The implementation simply converts the object to json and sends it out on the websocket.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.reporting {
/*
@Loggable
@ClientEndpoint*/
[Named("WebSocketReportingEngine")]
public class WebSocketReportingEngine : ReportingEngine {
	
	//private Session localSession = null;
    private AppSession localSession = null;
	
	private ILog Logger;
	
	private bool webSocketInitialized = false;
	
	//private Gson gson = new Gson();
	
	public WebSocketReportingEngine() { }
	
	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Event Event) {
		notifyWebSocket("event", Event);
		
		Logger.Info("Reporter observed event by user [" + Event.GetUser().getUsername() + "]");
	}

	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Attack attack) {
		notifyWebSocket("attack", attack);
		
		Logger.Info("Reporter observed attack by user [" + attack.GetUser().getUsername() + "]");
	}

	/**
	 * {@inheritDoc}
	 */
	public override void onAdd(Response response) {
		notifyWebSocket("response", response);
		
		Logger.Info("Reporter observed response for user [" + response.GetUser().getUsername() + "]");
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Event> findEvents(string earliest) {
		throw new NotSupportedException("This method is not implemented for WebSocket reporting implementation");
	}

	/**
	 * {@inheritDoc}
	 */
	public override Collection<Attack> findAttacks(string earliest) {
		throw new NotSupportedException("This method is not implemented for WebSocket reporting implementation");
	}

	/**
	 * {@inheritDoc}
	 */
	public override Collection<Response> findResponses(string earliest) {
		throw new NotSupportedException("This method is not implemented for WebSocket reporting implementation");
	}
	
	private void notifyWebSocket(string type, object Object) {
		ensureConnected();
		
		//if (localSession != null && localSession.isOpen()) {
        if (localSession != null && localSession.Connected) {
			try {
				WebSocketJsonObject jsonObject = new WebSocketJsonObject(type, Object);
				//string json = gson.toJson(jsonObject);
                string json = jsonObject.ToString();
				//localSession.getBasicRemote().sendText(json);
                localSession.Send(json);
			} catch (IOException e) {
				Logger.Error("Error sending data to websocket", e);
			}
		}
	}
	
	//@OnOpen
	//public void onOpen(Session session) {
    public void onOpen(AppSession session) {
		Logger.Info("Connected ... " + session.SessionID);
	}

	//@OnMessage
	public string onMessage(string message, AppSession session) {
		return null;
	}

	//@OnClose
	public void onClose(AppSession session, CloseReason closeReason) {
		Logger.Info(String.Format("Session %s close because of %s",
				session.SessionID, closeReason));
	}

	private void ensureConnected() {
		if (! webSocketInitialized) {
			//WebSocketContainer client = ContainerProvider.getWebSocketContainer();
            WebSocket client = new WebSocket("ws://simple-websocket-dashboard/dashboard:8080");
            //Socket client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try {
                localSession = client.
                //localSession = client.Connect("/simple-websocket-dashboard/dashboard", 8080);
                webSocketInitialized = true;
            } catch(IOException e) {
                throw new SystemException(e.Message);
            } catch (SystemException e) {
                throw new SystemException(e.Message);
            }
	    	Console.Error.WriteLine("started and connected");
		}
	}
}
}