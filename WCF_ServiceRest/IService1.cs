using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_ServiceRest
{
    [ServiceContract]
    public interface IService1
    {    
        [OperationContract]
        [WebInvoke(UriTemplate = "SendMessage",  Method = "POST",    ResponseFormat = WebMessageFormat.Json)]
        MessageType SendMessage(MessageType message);
    }




    [DataContract]
    public class MessageType
    {
        [DataMember]
        string Cpf  {get; set; }
        [DataMember]
        string Name { get; set; }
        [DataMember]
        string Message { get; set; }

        public MessageType(string cpf, string name, string message) {
            Cpf = cpf;
            Name = name;
            Message = message;
        }
    }
}

