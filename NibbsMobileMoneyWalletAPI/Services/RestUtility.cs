using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using RestSharp;
//using RestSharp.Portable;
//using RestSharp.Portable.Deserializers;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using BooklyNG.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.IOptions;


namespace NibbsMobileMoneyWalletAPI.Services
{

    public class RestUtility
    {

        //private static AmadeusSettings _amadeusSettings;
        ILogger _logger;
        private NibbsConfig _nibbsConfig;
        public RestUtility(ILoggerFactory logger, IOptions<NibbsConfig> nibbsConfig)
        {
            //_amadeusSettings = amadeusSettings;
            _logger = logger.CreateLogger<RestUtility>();
            _nibbsConfig = nibbsConfig.Value;
        }

        public T Get<T>(string RelativeUrl) where T : new()
        {
            T result = new T();
            //var result = new TResponse();
            string AccessToken = GetBearerToken().AccessToken;
            string ApiUrl = String.Concat(Constants.SANDBOX_URL, RelativeUrl);
            string Nonce = GetNonce();
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");
            try
            {
                //bypass all server certificate wahala...
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                RestRequest restRequest = new RestRequest(Method.GET);
                ConstructSignatureForGet(Nonce, out string Signature);
                //restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("ClientId", _nibbsConfig.ClientId);
                restRequest.AddHeader("Nonce", Nonce);
                restRequest.AddHeader("Signature", Signature);
                //restRequest.AddHeader("Authorization", $"Bearer {AccessToken}");

                IRestResponse response = restClient.Execute(restRequest);
                _logger.LogInformation($"\nApi Request Execution Done!");

                HttpStatusCode httpStatusCode = response.StatusCode;
                int numericStatusCode = (int)httpStatusCode;
                _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                    //IRestResponse<T> response = restClient.Execute<T>(restRequest);
                if (numericStatusCode == 200)
                {
                    _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");
                    result = JsonConvert.DeserializeObject<T>(response.Content);
                    _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.SerializeObject(result)} at {DateTime.Now}\n");
                }
                else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
                {
                    // tokenResponse.ErrorCode = numericStatusCode.ToString();
                    // tokenResponse.ErrorMessage = response.StatusDescription;
                    _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {ApiUrl} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");


                }
                else
                {
                    _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {ApiUrl} at {DateTime.Now}\n");

                    // var errorResponse = deserial.Deserialize<ErrorResponse>(response);
                    // tokenResponse.ErrorCode = errorResponse.error.code;
                    // tokenResponse.ErrorMessage = errorResponse.error.message;
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                //return result;
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");

            }

            return result;
        }

        public T GetV1<T>(string RelativeUrl) where T : new()
        {
            T result = new T();
            //var result = new TResponse();
            string AccessToken = GetBearerToken().AccessToken;
            string ApiUrl = String.Concat(Constants.SANDBOX_URL_V1, RelativeUrl);
            string Nonce = GetNonce();
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");
            try
            {
                //bypass all server certificate wahala...
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                RestRequest restRequest = new RestRequest(Method.GET);
                ConstructSignatureForGet(Nonce, out string Signature);
                //restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                restRequest.AddHeader("Content-Type", "application/json");
                //restRequest.AddHeader("Authorization", $"Bearer {AccessToken}");
                restRequest.AddHeader("ClientId", _nibbsConfig.ClientId);
                restRequest.AddHeader("Nonce", Nonce);

                restRequest.AddHeader("Signature", Signature);

                IRestResponse response = restClient.Execute(restRequest);
                _logger.LogInformation($"\nApi Request Execution Done!");

                HttpStatusCode httpStatusCode = response.StatusCode;
                int numericStatusCode = (int)httpStatusCode;
                _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                    //IRestResponse<T> response = restClient.Execute<T>(restRequest);
                if (numericStatusCode == 200)
                {
                    _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");
                    result = JsonConvert.DeserializeObject<T>(response.Content);
                    _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.SerializeObject(result)} at {DateTime.Now}\n");
                }
                else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
                {
                    // tokenResponse.ErrorCode = numericStatusCode.ToString();
                    // tokenResponse.ErrorMessage = response.StatusDescription;
                    _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {ApiUrl} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");


                }
                else
                {
                    _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {ApiUrl} at {DateTime.Now}\n");

                    // var errorResponse = deserial.Deserialize<ErrorResponse>(response);
                    // tokenResponse.ErrorCode = errorResponse.error.code;
                    // tokenResponse.ErrorMessage = errorResponse.error.message;
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                //return result;
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");

            }

            return result;
        }

        public string GetNonce(){
            Guid guid = new Guid();
            string Nonce = guid.ToString().Replace("-","");
            return Nonce;
        }

        public string ConstructSignatureForGet(string Nonce, out string Signature){
            byte[] clientKeyBytes = Encoding.UTF8.GetBytes(_nibbsConfig.ClientKey);
            using(var shaEncrypt = new System.Security.Cryptography.HmacSha256(clientKeyBytes)){
                Signature = Convert.ToBase64String(shaEncrypt.ComputeHash(Nonce));
            }

        }

        public string ConstructSignatureForPost(string Nonce, object requestObject, out string Signature){
            byte[] clientKeyBytes = Encoding.UTF8.GetBytes(_nibbsConfig.ClientKey);

            using(var shaEncrypt = new System.Security.Cryptography.HmacSha256(clientKeyBytes)){
                Signature = Convert.ToBase64String(shaEncrypt.ComputeHash(String.Concat(Nonce, JsonConvert.SerializeObject(requestObject))));
            }
        }


        public TokenResponse GetBearerToken()
        {
            _logger.LogInformation($"\n\nAT {nameof(GetBearerToken)} now to fetch Auth Token from Amadeus: ");
            string Url = string.Concat(Constants.TOKEN_SANDBOX_URL, "/security/oauth2/token");
            _logger.LogInformation($"\n\nToken URL => {Url}");
            RestClient client = new RestClient(Url);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("content-type", Constants.ContentType);
            request.AddParameter(Constants.ContentType, $"grant_type={Constants.GRANT_TYPE}&client_id={Constants.CLIENT_ID}&client_secret={Constants.CLIENT_SECRET}", ParameterType.RequestBody);
            _logger.LogInformation($"\n\nRequest Parameters => ContentType: {Constants.ContentType} == GrantType: {Constants.GRANT_TYPE} === ClientId: {Constants.CLIENT_ID} === ClientSecret: {Constants.CLIENT_SECRET}\n\n");
            
            _logger.LogInformation($"Attempting to Execute Prepared Token Request now...");
            IRestResponse response = client.Execute(request);
            _logger.LogInformation($"Token Request Execution Done!");


            HttpStatusCode httpStatusCode = response.StatusCode;
            int numericStatusCode = (int)httpStatusCode;
            _logger.LogInformation($"Status Code that came from the Response for Token: {numericStatusCode}");

            TokenResponse tokenResponse = new TokenResponse();
            // Console.WriteLine(passportResponse);


            if (numericStatusCode == 200)
            {
                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                tokenResponse.setAccessToken(tokenResponse.AccessToken);
                _logger.LogInformation($"\n\nBearer Token Fetched Successfully {JsonConvert.SerializeObject(tokenResponse)} at {DateTime.Now}\n");
            }
            else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
            {
                // tokenResponse.ErrorCode = numericStatusCode.ToString();
                // tokenResponse.ErrorMessage = response.StatusDescription;
                _logger.LogInformation($"\n\nOops! An Error Occurred while calling Token Endpoint: {Url} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");


            }
            else
            {
                _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {Url} at {DateTime.Now}\n");

                // var errorResponse = deserial.Deserialize<ErrorResponse>(response);
                // tokenResponse.ErrorCode = errorResponse.error.code;
                // tokenResponse.ErrorMessage = errorResponse.error.message;
            }

            Console.WriteLine("=====================================" + tokenResponse);
            _logger.LogInformation($"\n\nToken Response => {JsonConvert.SerializeObject(tokenResponse)}\n");

            return tokenResponse;
        }

        public T GetWithParameters<T>(string RelativeUrl, Dictionary<string, object> requestParams = null) where T : new()
        {
            T result = new T();
            var bearerResponse = GetBearerToken();
            string AccessToken = bearerResponse.AccessToken;
            string ApiUrl = String.Concat(Constants.SANDBOX_URL, RelativeUrl);
            string Nonce = GetNonce();
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                RestRequest restRequest = new RestRequest(Method.GET);
                ConstructSignatureForGet(Nonce, out string Signature);
                restRequest.AddHeader("Content-Type", "application/json");
                //restRequest.AddHeader("Authorization", $"Bearer {AccessToken}");
                restRequest.AddHeader("ClientId", _nibbsConfig.ClientId);
                restRequest.AddHeader("Nonce", Nonce);
                restRequest.AddHeader("Signature", Signature);


                if (requestParams != null && requestParams.Count > 0)
                {
                    foreach (KeyValuePair<string, object> item in requestParams)
                    {
                        restRequest.AddParameter(item.Key, item.Value, ParameterType.QueryStringWithoutEncode);
                    }
                }
                var UrlGenerated = restClient.BuildUri(restRequest).AbsoluteUri;
                _logger.LogInformation($"\n\n\nREQUEST URL after adding QueryParams => {UrlGenerated}\n\n");

                _logger.LogInformation($"\n\nREQUEST PARAMETERS => {JsonConvert.SerializeObject(requestParams)}");

            IRestResponse response = restClient.Execute(restRequest);
            _logger.LogInformation($"\nApi Request Execution Done!");

            HttpStatusCode httpStatusCode = response.StatusCode;
            int numericStatusCode = (int)httpStatusCode;
            _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                //IRestResponse<T> response = restClient.Execute<T>(restRequest);
            if (numericStatusCode == 200)
            {
                _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");
                result = JsonConvert.DeserializeObject<T>(response.Content);
                _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.SerializeObject(result)} at {DateTime.Now}\n");
            }
            else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
            {
                // tokenResponse.ErrorCode = numericStatusCode.ToString();
                // tokenResponse.ErrorMessage = response.StatusDescription;
                _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {ApiUrl} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");

            }
            else
            {
                _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {ApiUrl} at {DateTime.Now}\n\nError Status Description for 400: {response.StatusDescription}");

                // var errorResponse = deserial.Deserialize<ErrorResponse>(response);
                // tokenResponse.ErrorCode = errorResponse.error.code;
                // tokenResponse.ErrorMessage = errorResponse.error.message;
            }               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION {ex.Message}\n {ex.StackTrace}");
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");
                //return result;
            }

            return result;
        }

        public T GetWithParametersV1<T>(string RelativeUrl, Dictionary<string, object> requestParams = null) where T : new()
        {
            T result = new T();
            var bearerResponse = GetBearerToken();
            string AccessToken = bearerResponse.AccessToken;
            string ApiUrl = String.Concat(Constants.SANDBOX_URL_V1, RelativeUrl);
            string Nonce = GetNonce();
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                RestRequest restRequest = new RestRequest(Method.GET);
                ConstructSignatureForGet(Nonce, out string Signature);
                restRequest.AddHeader("Content-Type", "application/json");
                //restRequest.AddHeader("Authorization", $"Bearer {AccessToken}");
                restRequest.AddHeader("ClientId", _nibbsConfig.ClientId);
                restRequest.AddHeader("Nonce", Nonce);
                restRequest.AddHeader("Signature", Signature);

                if (requestParams != null && requestParams.Count > 0)
                {
                    foreach (KeyValuePair<string, object> item in requestParams)
                    {
                        restRequest.AddParameter(item.Key, item.Value, ParameterType.QueryStringWithoutEncode);
                    }
                }
                var UrlGenerated = restClient.BuildUri(restRequest).AbsoluteUri;
                _logger.LogInformation($"\n\n\nREQUEST URL after adding QueryParams => {UrlGenerated}\n\n");

                _logger.LogInformation($"\n\nREQUEST PARAMETERS => {JsonConvert.SerializeObject(requestParams)}");

            IRestResponse response = restClient.Execute(restRequest);
            _logger.LogInformation($"\nApi Request Execution Done!");

            HttpStatusCode httpStatusCode = response.StatusCode;
            int numericStatusCode = (int)httpStatusCode;
            _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                //IRestResponse<T> response = restClient.Execute<T>(restRequest);
            if (numericStatusCode == 200)
            {
                _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");
                result = JsonConvert.DeserializeObject<T>(response.Content);
                _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.SerializeObject(result)} at {DateTime.Now}\n");
            }
            else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
            {
                // tokenResponse.ErrorCode = numericStatusCode.ToString();
                // tokenResponse.ErrorMessage = response.StatusDescription;
                _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {ApiUrl} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");

            }
            else
            {
                _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {ApiUrl} at {DateTime.Now}\n\nError Status Description for 400: {response.StatusDescription}");

                // var errorResponse = deserial.Deserialize<ErrorResponse>(response);
                // tokenResponse.ErrorCode = errorResponse.error.code;
                // tokenResponse.ErrorMessage = errorResponse.error.message;
            }               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION {ex.Message}\n {ex.StackTrace}");
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");
                //return result;
            }

            return result;
        }

        public T Post<T>(string RelativeUrl, object requestObject) where T : new()
        {
            T result = new T();
            string AccessToken = GetBearerToken().AccessToken;
            string ApiUrl = String.Concat(Constants.SANDBOX_URL, RelativeUrl);
            string Nonce = GetNonce();
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                RestRequest restRequest = new RestRequest(Method.POST);
                ConstructSignatureForPost(Nonce, requestObject, out string Signature)

                restRequest.AddHeader("Content-Type", "application/json");
                //restRequest.AddHeader("Authorization", $"Bearer {AccessToken}");
                restRequest.AddHeader("ClientId", _nibbsConfig.ClientId);
                restRequest.AddHeader("Nonce", Nonce);
                restRequest.AddHeader("Signature", Signature);

                restRequest.AddJsonBody(requestObject);

                IRestResponse response = restClient.Execute(restRequest);

                _logger.LogInformation($"\nApi Request Execution Done!");

                HttpStatusCode httpStatusCode = response.StatusCode;
                int numericStatusCode = (int)httpStatusCode;
                _logger.LogInformation($"\nStatus Code that came from the Response for API Call: {numericStatusCode}");

                    //IRestResponse<T> response = restClient.Execute<T>(restRequest);
                if (numericStatusCode == 200)
                {
                    _logger.LogInformation($"\n\nREQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");
                    result = JsonConvert.DeserializeObject<T>(response.Content);
                    _logger.LogInformation($"\n\nAPI Fetched Successfully {JsonConvert.SerializeObject(result)} at {DateTime.Now}\n");
                }
                else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
                {
                    // tokenResponse.ErrorCode = numericStatusCode.ToString();
                    // tokenResponse.ErrorMessage = response.StatusDescription;
                    _logger.LogInformation($"\n\nOops! An Error Occurred while calling API Endpoint: {ApiUrl} \nError StatusCode: {numericStatusCode.ToString()}\nError Status Description: {response.StatusDescription} at {DateTime.Now}\n");

                }
                else
                {
                    _logger.LogInformation($"\n\nOops! An Unknown Error Occurred while calling Token Endpoint: {ApiUrl} at {DateTime.Now}\n\nError Status Description for 400: {response.StatusDescription}");

                    // var errorResponse = deserial.Deserialize<ErrorResponse>(response);
                    // tokenResponse.ErrorCode = errorResponse.error.code;
                    // tokenResponse.ErrorMessage = errorResponse.error.message;
                }       

            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION {ex.Message}\n {ex.StackTrace}");
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");

            }

            return result;
        }

        public T Put<T>(string RelativeUrl, object requestObject) where T : new()
        {
            T result = new T();
            string AccessToken = GetBearerToken().AccessToken;
            string ApiUrl = String.Concat(Constants.SANDBOX_URL, RelativeUrl);
            _logger.LogInformation($"\n\nCall to Endpoint Url => {ApiUrl} at {DateTime.Now}");
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                RestClient restClient = new RestClient($"{ApiUrl}");
                RestRequest restRequest = new RestRequest(Method.PUT);
                //restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddHeader("Authorization", $"Bearer {AccessToken}");

                restRequest.AddJsonBody(requestObject);

                IRestResponse<T> response = restClient.Execute<T>(restRequest);

                result = response.Data;
                _logger.LogInformation($"REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => SUCCESS");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION {ex.Message}\n {ex.StackTrace}");
                _logger.LogError($"ERROR OCCURRED REQUEST MADE TO ENDPOINT => {ApiUrl} STATUS => FAILED EXCEPTION => {ex.Message}");

            }

            return result;
        }


        // public Dictionary<string, string> SendWithAccessToken(String uri, String httpMethod, object data = null, Dictionary<string, string> headers = null, String signedParameters = null)
        // {
        //     Dictionary<string, string> dictionary = new Dictionary<string, string>();
        //     try
        //     {
        //         string url = getUrl(environment);
        //         url = String.Concat(url, uri);

        //         Console.WriteLine("===============================Url from  SendwithAccessToken " + url);

        //         RestClient client = new RestClient(url);
        //         client.IgnoreResponseStatusCode = true;
        //         IRestResponse response = null;
        //         //Config authConfig = new Config(httpMethod, url, Constants.CLIENT_ID, Constants.CLIENT_SECRET);

        //         HttpMethod httpMethodObj = (httpMethod == null || httpMethod.Equals("")) ? HttpMethod.Get : new HttpMethod(httpMethod);

        //         var paymentRequests = new RestRequest(url, httpMethodObj);
        //         paymentRequests.AddHeader(Constants.Contenttype, "application/json");
        //         //paymentRequests.AddHeader("Signature", authConfig.Signature);
        //         //paymentRequests.AddHeader("SignatureMethod", "SHA1");
        //         //paymentRequests.AddHeader("Timestamp", authConfig.TimeStamp);
        //         //paymentRequests.AddHeader("Nonce", authConfig.Nonce);
        //         //paymentRequests.AddHeader("Authorization", authConfig.GetInterswitchAuth(Constants.CLIENT_ID));
        //         if (headers != null && headers.Count() > 0)
        //         {
        //             foreach (KeyValuePair<string, string> entry in headers)
        //             {
        //                 paymentRequests.AddHeader(entry.Key, entry.Value);
        //             }
        //         }

        //         if (data != null)
        //         {
        //             paymentRequests.AddJsonBody(data);
        //         }

        //         ServicePointManager.Expect100Continue = true;
        //         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //         //JsonDeserializer deserial = new JsonDeserializer();
        //         //try
        //         //{
        //         response = client.Execute(paymentRequests).Result;

        //         /*
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine(ex.StackTrace.ToString());
        //             throw ex;
        //         }
        //         */

        //         HttpStatusCode httpStatusCode = response.StatusCode;
        //         int numericStatusCode = (int)httpStatusCode;
        //         Dictionary<string, string> responseObject = new Dictionary<string, string>();
        //         responseObject.Add(HTTP_CODE, numericStatusCode.ToString());
        //         responseObject.Add(HTTP_RESPONSE, System.Text.Encoding.UTF8.GetString(response.RawBytes));

        //         return responseObject;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(">>>>>>>>>>>>>>>>>>>>" + ex.StackTrace.ToString());
        //     }
        //     return dictionary;
        // }







        // public virtual async Task<Token> GetClientAccessToken(String ClientId, String ClientSecret)
        //        {
        //            string url = getTokenEndpointUrl(environment);

        //            url = String.Concat(url, "/security/oauth2/token");
        //            Console.WriteLine("This is the Url =====" + url);


        //            RestClient client = new RestClient(url);
        //            client.IgnoreResponseStatusCode = true;

        //            var request = new RestRequest(url, HttpMethod.Post);
        //            request.AddHeader(Constants.Contenttype, Constants.ContentType);
        //            request.AddHeader(Constants.Authorization, setAuthorization(ClientId, ClientSecret));
        //            request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
        //            request.AddParameter("Scope", "profile", ParameterType.GetOrPost);

        //            JsonDeserializer deserial = new JsonDeserializer();
        //            ServicePointManager.Expect100Continue = true;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //            IRestResponse response = await client.Execute(request);

        //            HttpStatusCode httpStatusCode = response.StatusCode;
        //            int numericStatusCode = (int)httpStatusCode;
        //            Token passportResponse = new Token();
        //           // Console.WriteLine(passportResponse);


        //            if (numericStatusCode == 200)
        //            {
        //                passportResponse = deserial.Deserialize<Token>(response);
        //                passportResponse.setAccessToken(passportResponse.access_token);
        //            }
        //            else if (response.ContentType == "text/html" || (numericStatusCode == 401 || numericStatusCode == 404 || numericStatusCode == 502 || numericStatusCode == 504))
        //            {
        //                passportResponse.ErrorCode = numericStatusCode.ToString();
        //                passportResponse.ErrorMessage = response.StatusDescription;
        //            }
        //            else
        //            {
        //                var errorResponse = deserial.Deserialize<ErrorResponse>(response);
        //                passportResponse.ErrorCode = errorResponse.error.code;
        //                passportResponse.ErrorMessage = errorResponse.error.message;
        //            }
        //            Console.WriteLine ( "=====================================" +passportResponse);
        //            return passportResponse;
        //        }


        //        public String getTokenEndpointUrl(String env)
        //        {
        //            if (env == null) {
        //                // return Constants.SANDBOX_URL;//default to sandbox

        //                return "https://test.api.amadeus.com/v1";
        //            }
        //            if (env.Equals(PRODUCTION, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return Constants.PRODUCTION_URL;
        //            }
        //            else if (env.Equals(SANDBOX, StringComparison.OrdinalIgnoreCase))
        //            {
        //                // return Constants.SANDBOX_URL;
        //                return "https://test.api.amadeus.com/v1";
        //            }
        //            else if (env.Equals(DEV, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return "https://test.api.amadeus.com/v1";
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }



    }



}