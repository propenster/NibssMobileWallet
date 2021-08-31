using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NibbsMobileMoneyWalletAPI.Models 

{
    public class CreateMobileWalletAccountRequest
    {
        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}


    
