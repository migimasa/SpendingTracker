using System;
using TrackerService.Base;

namespace TrackerService.Interfaces
{
    public interface ITransactionService
    {
        ServiceResult AddTransaction(int envelopeId, decimal transactionAmount, DateTime transactionDate, string transactionName);
    }
}