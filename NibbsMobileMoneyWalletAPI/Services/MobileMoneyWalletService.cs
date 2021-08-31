using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NibbsMobileMoneyWalletAPI.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NibbsMobileMoneyWalletAPI.Models;


namespace NibbsMobileMoneyWalletAPI.Services
{
    public class MobileMoneyWalletService : IMobileMoneyWalletService   
    {

        private readonly RestUtility _restUtility;

        private readonly ILogger _logger;

        public MobileMoneyWalletService(RestUtility restUtility, ILoggerFactory logger)
        {
            _restUtility = restUtility;
            _logger = logger.CreateLogger<BookingService>();
        }
        
        public CreateMobileWalletAccountResponse CreateNewMobileWalletAccount(CreateMobileWalletAccountRequest request)
        {
            _logger.LogInformation($"\n\nWe are now inside Mobile Wallet API service... => ");
            CreateMobileWalletAccountResponse response = _restUtility.Post<CreateMobileWalletAccountResponse>("/account/create", request);

            _logger.LogInformation($"\n\nResponse From API call to MobileWalletAccount create: \n\n\t{JsonConvert.SerializeObject(response)}\n\n");

            return response;

        }

        public WalletAccount GetWalletAccount(string AccountId)
        {
            _logger.LogInformation($"\n\nWe are now inside Mobile Wallet API service... => ");
            WalletAccount response = _restUtility.Get<CreateMobileWalletAccountResponse>($"/account/{AccountId}");

            _logger.LogInformation($"\n\nResponse From API call to MobileWalletAccount GetAccount: \n\n\t{JsonConvert.SerializeObject(response)}\n\n");

            return response;
        }

        public NameEnquiryResponse NameEnquiry(string BankCode, string AccountNumber)
        {
            _logger.LogInformation($"\n\nWe are now inside Mobile Wallet API service... => ");
            Dictionary<string, object> requestParams = new Dictionary<string, object>();
            requestParams.Add("bank_code", BankCode);
            requestParams.Add("account_number", AccountNumber);
            NameEnquiryResponse response = _restUtility.GetWithParameters<NameEnquiryResponse>($"/enquiry", requestParams);

            _logger.LogInformation($"\n\nResponse From API call to MobileWalletAccount NameEnquiry: \n\n\t{JsonConvert.SerializeObject(response)}\n\n");

            return response;
        }

        public BankTransferResponse DoBankTransfer(string AccountId, BankTransferRequest request)
        {
            _logger.LogInformation($"\n\nWe are now inside Mobile Wallet API service... => ");
            BankTransferResponse response = _restUtility.Post<BankTransferResponse>($"/account/{AccountId}/transfer", request);

            _logger.LogInformation($"\n\nResponse From API call to MobileWalletAccount transfer: \n\n\t{JsonConvert.SerializeObject(response)}\n\n");

            return response;
        }

        public CheckTransactionStatusResponse CheckTransactionStatus(string XRef)
        {
            _logger.LogInformation($"\n\nWe are now inside Mobile Wallet API service... => ");
            CheckTransactionStatusResponse response = _restUtility.Get<CheckTransactionStatusResponse>($"/transaction/{XRef}");

            _logger.LogInformation($"\n\nResponse From API call to MobileWalletAccount CheckTransactionStatus: \n\n\t{JsonConvert.SerializeObject(response)}\n\n");

            return response;
        }

        public GetTransactionsResponse GetTransactions(string AccountId)
        {
            _logger.LogInformation($"\n\nWe are now inside Mobile Wallet API service... => ");
            GetTransactionsResponse response = _restUtility.Get<GetTransactionsResponse>($"/account/{AccountId}/transactions");

            _logger.LogInformation($"\n\nResponse From API call to MobileWalletAccount GetTransactions: \n\n\t{JsonConvert.SerializeObject(response)}\n\n");

            return response;
        }



        

    }
}
