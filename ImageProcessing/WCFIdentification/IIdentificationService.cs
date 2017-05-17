using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFIdentification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IIdentificationService" in both code and config file together.
    
    [ServiceContract]
    public interface IIdentificationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Identify")]
        IdentificationService.IdentificationResponse Identify(IdentificationService.IdentificationRequest request);

        

    }
}
