namespace org.owasp.appsensor.reporting{

/**
 * Simple bean representing a generic key-value pair for json data.
 * 
 * @author John Melton (jtmelton@gmail.com) http://www.jtmelton.com/
 */
public class WebSocketJsonObject {
	
	private string dataType;
	private object dataValue;
	
	public WebSocketJsonObject() { }
	
	public WebSocketJsonObject(string dataType, object dataValue) {
		setDataType(dataType);
		setDataValue(dataValue);
	}

	public string getDataType() {
		return dataType;
	}

	public void setDataType(string dataType) {
		this.dataType = dataType;
	}

	public object getDataValue() {
		return dataValue;
	}

	public void setDataValue(object dataValue) {
		this.dataValue = dataValue;
	}
	
}
}