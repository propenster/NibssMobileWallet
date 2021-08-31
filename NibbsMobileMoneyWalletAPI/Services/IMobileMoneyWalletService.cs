using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NibbsMobileMoneyWalletAPI.Models;



namespace NibbsMobileMoneyWalletAPI.Services
{
    public interface IMobileMoneyWalletService
    { 
        

        CreateMobileWalletAccountResponse CreateNewMobileWalletAccount(CreateMobileWalletAccountRequest request);
        WalletAccount GetWalletAccount(string AccountId);
        NameEnquiryResponse NameEnquiry(string BankCode, string AccountNumber);
        BankTransferResponse DoBankTransfer(string AccountId, BankTransferRequest request);
        CheckTransactionStatusResponse CheckTransactionStatus(string XRef);
        GetTransactionsResponse GetTransactions(string AccountId);

    }


}
