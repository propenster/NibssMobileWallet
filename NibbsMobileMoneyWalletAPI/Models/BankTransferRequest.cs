using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NibbsMobileMoneyWalletAPI.Models 

{
    public class BankTransferRequest
    {

        [JsonProperty("xref")]
        public string Xref { get; set; }

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("narration")]
        public string Narration { get; set; }

        public BankTransferRequest(){
        	Xref = Guid.NewGuid().ToString().Replace("-","");
        }

    }
}



