using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibbsMobileMoneyWalletAPI.Services
{
    public class Constants
    {
        public static string SANDBOX_URL = "https://nibss.cowriesys.com/api";
        public static string SANDBOX_URL_V1 = "https://nibss.cowriesys.com/api";
        public static string TOKEN_SANDBOX_URL = "https://test.api.amadeus.com/v1";
        public static string PRODUCTION_URL = "https://saturn.interswitchng.com";
        public static string DEVELOPMENT_URL = "https://qa.interswitchng.com";

        public static string Contenttype = "Content-Type";
        public static string Cachecontrol = "cache-control";
        public static string Authorization = "Authorization";
        public static string ContentType = "application/x-www-form-urlencoded";

        public static string CLIENT_ID = "6myVUL94wPpADGa1GaiaIKaMQLWxsdq9";
        public static string CLIENT_SECRET = "50g5JbZbHGgAOXXv";
        public static string GRANT_TYPE = "client_credentials";
        


        public static String CARD_NAME = "default";
        public static String GET = "GET";
        public static String POST = "POST";
        public static String SECURE_HEADER = "4D";
        //public static String SECURE_FORMAT_VERSION = "11";
        public static String SECURE_FORMAT_VERSION = "12";
        public static String SECURE_MAC_VERSION = "05";
        public static String SECURE_FOOTER = "5A";
        public static String SIGNATURE_HEADER = "Signature";
        public static String SIGNATURE_METHOD_HEADER = "SignatureMethod";
        public static String INTERSWITCH_AUTH = "InterswitchAuth";


        //FLUTTERWAVEPAYMENT KEYS
        public static string publicKey = "FLWPUBK-5e5165251607d2fd85a8be6917a950bc-X";
        public static string secretKey = "FLWSECK-49690de4b5f1f96af972a8eb96da57d4-X";

        //FLUTTERWAVEPAYMENT URLS
        public static string initiateCardChargeUrl = "https://api.ravepay.co/flwv3-pug/getpaidx/api/charge";
        public static string validateChargeUrl =  "https://api.ravepay.co/flwv3-pug/getpaidx/api/validatecharge";
    }
}
