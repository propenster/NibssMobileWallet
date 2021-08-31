using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NibbsMobileMoneyWalletAPI.Models 

{
    public class GetTransactionsResponse
    {
    	[JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    	
    }


}

















    