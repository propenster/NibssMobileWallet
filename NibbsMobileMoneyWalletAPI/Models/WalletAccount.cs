using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NibbsMobileMoneyWalletAPI.Models 

{
    public class WalletAccount
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("account_balance")]
        public string AccountBalance { get; set; }

        [JsonProperty("kyc")]
        public string Kyc { get; set; }
    }
}


    
