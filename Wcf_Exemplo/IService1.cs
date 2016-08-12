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
        bool CreateClient();

        [OperationContract]
        bool DeleteClient(ClienteBag cliObj);

        [OperationContract]
        List<ClienteBag> SearchClient(string query, string value);

        [OperationContract]
        bool UpdateClient(ClienteBag cliObj);
        
        [OperationContract]
        ContatoBag CreateContact(ClienteBag cliObj);

        [OperationContract]
        bool DeleteContact(ClienteBag cliObj, ContatoBag conObj);

        [OperationContract]
        List<ContatoBag> SearchContact(ClienteBag cliObj);

        // TODO: Add your service operations here
    }
}
