﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.34014
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppSensor_WCF_Client.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Event", Namespace="http://schemas.datacontract.org/2004/07/org.owasp.appsensor")]
    [System.SerializableAttribute()]
    public partial class Event : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Attack", Namespace="http://schemas.datacontract.org/2004/07/org.owasp.appsensor")]
    [System.SerializableAttribute()]
    public partial class Attack : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Response", Namespace="http://schemas.datacontract.org/2004/07/org.owasp.appsensor")]
    [System.SerializableAttribute()]
    public partial class Response : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string actionField;
        
        private string detectionSystemIdField;
        
        private AppSensor_WCF_Client.ServiceReference.Interval intervalField;
        
        private string timestampField;
        
        private AppSensor_WCF_Client.ServiceReference.User userField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string action {
            get {
                return this.actionField;
            }
            set {
                if ((object.ReferenceEquals(this.actionField, value) != true)) {
                    this.actionField = value;
                    this.RaisePropertyChanged("action");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string detectionSystemId {
            get {
                return this.detectionSystemIdField;
            }
            set {
                if ((object.ReferenceEquals(this.detectionSystemIdField, value) != true)) {
                    this.detectionSystemIdField = value;
                    this.RaisePropertyChanged("detectionSystemId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public AppSensor_WCF_Client.ServiceReference.Interval interval {
            get {
                return this.intervalField;
            }
            set {
                if ((object.ReferenceEquals(this.intervalField, value) != true)) {
                    this.intervalField = value;
                    this.RaisePropertyChanged("interval");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string timestamp {
            get {
                return this.timestampField;
            }
            set {
                if ((object.ReferenceEquals(this.timestampField, value) != true)) {
                    this.timestampField = value;
                    this.RaisePropertyChanged("timestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public AppSensor_WCF_Client.ServiceReference.User user {
            get {
                return this.userField;
            }
            set {
                if ((object.ReferenceEquals(this.userField, value) != true)) {
                    this.userField = value;
                    this.RaisePropertyChanged("user");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Interval", Namespace="http://schemas.datacontract.org/2004/07/org.owasp.appsensor")]
    [System.SerializableAttribute()]
    public partial class Interval : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/org.owasp.appsensor")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string usernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl", ConfigurationName="ServiceReference.IWCFRequestHandler")]
    public interface IWCFRequestHandler {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddEvent", ReplyAction="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddEventResponse")]
        void addEvent(AppSensor_WCF_Client.ServiceReference.Event Event);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddEvent", ReplyAction="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddEventResponse")]
        System.Threading.Tasks.Task addEventAsync(AppSensor_WCF_Client.ServiceReference.Event Event);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddAttack", ReplyAction="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddAttackResponse")]
        void addAttack(AppSensor_WCF_Client.ServiceReference.Attack attack);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddAttack", ReplyAction="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/a" +
            "ddAttackResponse")]
        System.Threading.Tasks.Task addAttackAsync(AppSensor_WCF_Client.ServiceReference.Attack attack);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/g" +
            "etResponses", ReplyAction="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/g" +
            "etResponsesResponse")]
        AppSensor_WCF_Client.ServiceReference.Response[] getResponses(string earliest);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/g" +
            "etResponses", ReplyAction="https://www.owasp.org/index.php/OWASP_AppSensor_Project/wsdl/IWCFRequestHandler/g" +
            "etResponsesResponse")]
        System.Threading.Tasks.Task<AppSensor_WCF_Client.ServiceReference.Response[]> getResponsesAsync(string earliest);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWCFRequestHandlerChannel : AppSensor_WCF_Client.ServiceReference.IWCFRequestHandler, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WCFRequestHandlerClient : System.ServiceModel.ClientBase<AppSensor_WCF_Client.ServiceReference.IWCFRequestHandler>, AppSensor_WCF_Client.ServiceReference.IWCFRequestHandler {
        
        public WCFRequestHandlerClient() {
        }
        
        public WCFRequestHandlerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WCFRequestHandlerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFRequestHandlerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WCFRequestHandlerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void addEvent(AppSensor_WCF_Client.ServiceReference.Event Event) {
            base.Channel.addEvent(Event);
        }
        
        public System.Threading.Tasks.Task addEventAsync(AppSensor_WCF_Client.ServiceReference.Event Event) {
            return base.Channel.addEventAsync(Event);
        }
        
        public void addAttack(AppSensor_WCF_Client.ServiceReference.Attack attack) {
            base.Channel.addAttack(attack);
        }
        
        public System.Threading.Tasks.Task addAttackAsync(AppSensor_WCF_Client.ServiceReference.Attack attack) {
            return base.Channel.addAttackAsync(attack);
        }
        
        public AppSensor_WCF_Client.ServiceReference.Response[] getResponses(string earliest) {
            return base.Channel.getResponses(earliest);
        }
        
        public System.Threading.Tasks.Task<AppSensor_WCF_Client.ServiceReference.Response[]> getResponsesAsync(string earliest) {
            return base.Channel.getResponsesAsync(earliest);
        }
    }
}