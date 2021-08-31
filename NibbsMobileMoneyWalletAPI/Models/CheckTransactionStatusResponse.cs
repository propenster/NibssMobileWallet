using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NibbsMobileMoneyWalletAPI.Models 

{
    public class CheckTransactionStatusResponse
    {

    	[JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("xref")]
        public string Xref { get; set; }

        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }
}











