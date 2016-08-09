using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.ObjectModel;

namespace Wcf_Exemplo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Clientes CreateClient();

        [OperationContract]
        bool DeleteClient(object cliObj);

        [OperationContract]
        ObservableCollection<Clientes> SearchClient();

        [OperationContract]
        bool UpdateClient();

        [OperationContract]
        Clientes CreateContact(object cliObj);

        [OperationContract]
        bool DeleteContact(object cliObj, object conObj);

        [OperationContract]
        List<Contatos> SearchContact(object cliObj);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
