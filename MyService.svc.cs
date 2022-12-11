using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NFSService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class MyService : IMyService
    {
        public void DoWork()
        {
        }

        [WebInvoke(Method = "GET", UriTemplate = "GetName?Name={Name}")]
        public string GetName(string Name)
        {
            return string.Format("Hello {0}", Name);
        }
    }
}
