using log4net;
/*
import java.io.IOException;
import java.nio.file.FileAlreadyExistsException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
*/
using org.owasp.appsensor.logging.Loggable;
using System.IO;

//import org.slf4j.Logger;

/**
 * Helper class for file utilities.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
namespace org.owasp.appsensor.util{
//@Loggable
public class FileUtils {

	private static ILog Logger;

	public static Path getOrCreateFile(string filePath, string fileName) {
		
		string directory = Path.get(filePath);
		
		string file = Path.Resolve(fileName);
		
		if (File.Exists(file)) {
			path = file;
		} else if (!File.Exists(file)) {
			try {
			    // Create the empty file with default permissions
			    path = File.CreateText(file);
			} catch (IOException e) {
			    // Some other sort of failure, such as permissions.
				Logger.Error("Permissions failure for file creation: " + file, e);
			}
		}

		return path;
	}
}
}