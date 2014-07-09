using org.owasp.appsensor.exceptions;
using log4net;

/**
 * This particular {@link AccessController} implementation simply checks the {@link clientApplication}s 
 * role(s) to see if it matches the expected {@link Action}. If there is a match found, 
 * then the access is considered valid.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.accesscontrol {
//@Loggable
// [Named("ReferenceAccessController")]
public class ReferenceAccessController : AccessController {
	
	//@SuppressWarnings("unused")
	private ILog Logger;
	
	/**
	 * {@inheritDoc}
	 */
    //public override bool isAuthorized(clientApplication clientApplication, Action Action, Context Context) {
	public bool isAuthorized(ClientApplication clientApplication, Action Action, Context Context) {
		bool authorized = false;

		//if (clientApplication != null && Action != null && Action.ToString() != null && Action.ToString().Trim().Length() > 0) {
        if (clientApplication != null && Action != null && Action.ToString() != null && Action.ToString().Trim().Length > 0) {
			foreach (Role role in clientApplication.getRoles()) {
				
				//simple check to make sure that 
				//the value of the action matches the value of one of the roles (exact match)
				if (role != null && role.ToString() != null && 
						role.ToString().Equals(Action.ToString())) {
					authorized = true; 
					break;
				}
			}
		}
		
		return authorized;
	}
	
	/**
	 * {@inheritDoc}
	 */
	//public override void assertAuthorized(clientApplication clientApplication, Action action, Context context) {
    public void assertAuthorized(ClientApplication clientApplication, Action action, Context context) {
        //throws NotAuthorizedException {
        /// <exception cref="NotAuthorizedException"></exception>
		if (! isAuthorized(clientApplication, action, context)) {
            throw new NotAuthorizedException("Access is not allowed for client application: " + clientApplication + 
            //throw new AuthenticationException("Access is not allowed for client application: " + clientApplication + 
					" when trying to perform action : " + action + 
					" using context: " + context);
		}
	}
	
}
}