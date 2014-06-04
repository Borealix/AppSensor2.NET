/*
import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.Collection;

import javax.inject.Named;
import javax.websocket.ClientEndpoint;
import javax.websocket.CloseReason;
import javax.websocket.ContainerProvider;
import javax.websocket.DeploymentException;
import javax.websocket.OnClose;
import javax.websocket.OnMessage;
import javax.websocket.OnOpen;
import javax.websocket.Session;
import javax.websocket.WebSocketContainer;
*/
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.Event;
using org.owasp.appsensor.Response;
using org.owasp.appsensor.logging.Loggable;
using Ninject;
using System.Collections.ObjectModel;
using log4net;

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
	
	private Session localSession = null;
	
	private ILog Logger;
	
	private bool webSocketInitialized = false;
	
	private Gson gson = new Gson();
	
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
		throw new UnsupportedOperationException("This method is not implemented for WebSocket reporting implementation");
	}

	/**
	 * {@inheritDoc}
	 */
	public override Collection<Attack> findAttacks(string earliest) {
		throw new UnsupportedOperationException("This method is not implemented for WebSocket reporting implementation");
	}

	/**
	 * {@inheritDoc}
	 */
	public override Collection<Response> findResponses(string earliest) {
		throw new UnsupportedOperationException("This method is not implemented for WebSocket reporting implementation");
	}
	
	private void notifyWebSocket(string type, object Object) {
		ensureConnected();
		
		if (localSession != null && localSession.isOpen()) {
			try {
				WebSocketJsonObject jsonObject = new WebSocketJsonObject(type, object);
				string json = gson.toJson(jsonObject);
				localSession.getBasicRemote().sendText(json);
			} catch (IOException e) {
				Logger.error("Error sending data to websocket", e);
			}
		}
	}
	
	//@OnOpen
	public void onOpen(Session session) {
		Logger.Info("Connected ... " + session.getId());
	}

	//@OnMessage
	public string onMessage(string message, Session session) {
		return null;
	}

	//@OnClose
	public void onClose(Session session, CloseReason closeReason) {
		Logger.Info(String.format("Session %s close because of %s",
				session.getId(), closeReason));
	}

	private void ensureConnected() {
		if (! webSocketInitialized) {
			WebSocketContainer client = ContainerProvider.getWebSocketContainer();
	
			try {
	            localSession = client.connectToServer(WebSocketReportingEngine.class, new URI("ws://localhost:8080/simple-websocket-dashboard/dashboard"));
	            webSocketInitialized = true;
	        } catch (DeploymentException | URISyntaxException | IOException e) {
	            throw new ApplicationException(e);
	        }
	    	System.err.println("started and connected");
		}
	}
	
}
}