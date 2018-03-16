﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ServiceModel;
using ActionEventService.Models;

namespace Adeptive.ResWare.Services
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReceiveActionEventData", Namespace="http://schemas.datacontract.org/2004/07/Adeptive.ResWare.Services")]
    public partial class ReceiveActionEventData : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ActionEventCodeField;
        
        private string FileNumberField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ActionEventCode
        {
            get
            {
                return this.ActionEventCodeField;
            }
            set
            {
                this.ActionEventCodeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string FileNumber
        {
            get
            {
                return this.FileNumberField;
            }
            set
            {
                this.FileNumberField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReceiveActionEventResponse", Namespace="http://schemas.datacontract.org/2004/07/Adeptive.ResWare.Services")]
    public partial class ReceiveActionEventResponse : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string MessageField;
        
        private Adeptive.ResWare.Services.ReceiveActionEventResponseCode ResponseCodeField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public Adeptive.ResWare.Services.ReceiveActionEventResponseCode ResponseCode
        {
            get
            {
                return this.ResponseCodeField;
            }
            set
            {
                this.ResponseCodeField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReceiveActionEventResponseCode", Namespace="http://schemas.datacontract.org/2004/07/Adeptive.ResWare.Services")]
    public enum ReceiveActionEventResponseCode : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SUCCESS = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALID_LOGIN = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALID_FILE_NUMBER = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DOWN_FOR_MAINTENANCE = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UNEXPECTED_ERROR = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ACTION_EVENT_NOT_FOUND = 5,
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IReceiveActionEventService")]
public interface IReceiveActionEventService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReceiveActionEventService/ReceiveActionEvent", ReplyAction="http://tempuri.org/IReceiveActionEventService/ReceiveActionEventResponse")]
    Adeptive.ResWare.Services.ReceiveActionEventResponse ReceiveActionEvent(Adeptive.ResWare.Services.ReceiveActionEventData data);

    [OperationContract]
    ICollection<ActionEventServiceResult> GetAllActionEvents();

    [OperationContract]
    ActionEventServiceResult GetActionEventById(Guid id);

    [OperationContract]
    int DeleteActionEventById(Guid id);

    [OperationContract]
    int UpdateActionEvent(ActionEventServiceResult actionEventServiceResult);
}
