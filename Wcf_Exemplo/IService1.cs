using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.ObjectModel;
using Wcf_Exemplo.DTO;

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
        List<ClienteBag> SearchClient();

        [OperationContract]
        bool UpdateClient();

        [OperationContract]
        ContatoBag CreateContact(object cliObj);

        [OperationContract]
        bool DeleteContact(object cliObj, object conObj);

        [OperationContract]
        List<ContatoBag> SearchContact(object cliObj);

        // TODO: Add your service operations here
    }
}
