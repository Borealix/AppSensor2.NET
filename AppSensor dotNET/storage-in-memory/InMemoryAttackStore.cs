/*
import java.util.List;
import java.util.Collection;
import java.util.concurrent.CopyOnWriteArrayList;
using org.joda.time.DateTime;
using org.slf4j.Logger;

import javax.inject.Named;
*/
using org.owasp.appsensor.Attack;
using org.owasp.appsensor.DetectionPoint;
using org.owasp.appsensor.User;
using org.owasp.appsensor.criteria.SearchCriteria;
using org.owasp.appsensor.listener.AttackListener;
using org.owasp.appsensor.logging.Loggable;
using org.owasp.appsensor.util.DateUtils;
using log4net;
using System.Collections.ObjectModel;
using org.owasp.appsensor.criteria;
using System;
using Ninject;
using org.owasp.appsensor.util;
using System.Collections.Generic;
/**
 * This is a reference implementation of the {@link AttackStore}.
 * 
 * Implementations of the {@link AttackListener} interface can register with 
 * this class and be notified when new {@link Attack}s are added to the data store 
 * 
 * The implementation is trivial and simply stores the {@link Attack} in an in-memory collection.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 * @author Raphaël Taban
 */
namespace org.owasp.appsensor.storage{
//@Loggable
[Named("InMemoryAttackStore")]
public class InMemoryAttackStore : AttackStore {
	
	private ILog Logger;
	
	/** maintain a collection of {@link Attack}s as an in-memory list */
    private static SynchronizedCollection<Attack> attacks = new SynchronizedCollection<Attack>();
	
	/**
	 * {@inheritDoc}
	 */
	public override void addAttack(Attack attack) {
		Logger.Warn("Security attack " + attack.GetDetectionPoint().getId() + " triggered by user: " + attack.GetUser().getUsername());
	       
		attacks.Add(attack);
		
		//super.notifyListeners(attack);
        base.notifyListeners(attack);
	}
	
	/**
	 * {@inheritDoc}
	 */
	public override Collection<Attack> findAttacks(SearchCriteria criteria) {
		if (criteria == null) {
			throw new ArgumentException("criteria must be non-null");
		}
		
		Collection<Attack> matches = new Collection<Attack>();
		
		User user = criteria.GetUser();
		DetectionPoint detectionPoint = criteria.GetDetectionPoint();
		Collection<string> detectionSystemIds = criteria.getDetectionSystemIds(); 
		DateTime? earliest = DateUtils.fromString(criteria.getEarliest());
		
		foreach (Attack attack in attacks) {
			//check user match if user specified
			bool userMatch = (user != null) ? user.Equals(attack.GetUser()) : true;
			
			//check detection system match if detection systems specified
			bool detectionSystemMatch = (detectionSystemIds != null && detectionSystemIds.Count > 0) ? 
					detectionSystemIds.Contains(attack.GetDetectionSystemId()) : true;
			
			//check detection point match if detection point specified
			bool detectionPointMatch = (detectionPoint != null) ? 
					detectionPoint.getId().Equals(attack.GetDetectionPoint().getId()) : true;
			
			//bool earliestMatch = (earliest != null) ? earliest.isBefore(DateUtils.fromString(attack.GetTimestamp())) : true;
            bool earliestMatch = (earliest != null) ? earliest < DateUtils.fromString(attack.GetTimestamp()) : true;
					
					
			if (userMatch && detectionSystemMatch && detectionPointMatch && earliestMatch) {
				matches.Add(attack);
			}
		}
		
		return matches;
	}
	
}
}