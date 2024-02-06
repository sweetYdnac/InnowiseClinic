using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctions.Configurations
{
    public class IdentityServerConfiguration
    {
        public string Address { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
    }
}
