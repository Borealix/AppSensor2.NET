using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.owasp.appsensor.analysis;
using Ninject;
using System.IO;

namespace org.owasp.appsensor.storage {
    /**
     * Test basic FileBased * Store's by extending the ReferenceStatisticalEventAnalysisEngineTest
     * and only doing the file based setup. All of the same tests execute, but with the file
     * based stores instead of the memory based stores.
     * 
     * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
     * @author Raphaël Taban
     */
    [TestClass]
    public class FileBasedEventStorageTest : ReferenceStatisticalEventAnalysisEngineTest {

	[Inject]
	private AppSensorServer appSensorServer;
	
	//@Before
    /// <exception cref="Exception"></exception>
	public void deleteTestFiles() {
		FileBasedEventStore eventStore = (FileBasedEventStore)appSensorServer.getEventStore();
		FileBasedAttackStore attackStore = (FileBasedAttackStore)appSensorServer.getAttackStore();
		FileBasedResponseStore responseStore = (FileBasedResponseStore)appSensorServer.getResponseStore();

		File.Delete(eventStore.getPath());
		File.Delete(attackStore.getPath());
		File.Delete(responseStore.getPath());
	}
	
}
}
