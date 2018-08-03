using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCF_Service
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        MessageType SendMessage(MessageType message);

        [OperationContract]
        MessageType ReceivedMessage();

    }


    [DataContract]
    public class MessageType
    {
        string Cpf = string.Empty;
        string Name = string.Empty;
        string Message = string.Empty;

        [DataMember]
        public string CpfValue
        {
            get { return Cpf; }
            set { Cpf = value; }
        }

        [DataMember]
        public string NameValue
        {
            get { return Name; }
            set { Name = value; }
        }

        [DataMember]
        public string MessageValue
        {
            get { return Message; }
            set { Message = value; }
        }
    }
}
