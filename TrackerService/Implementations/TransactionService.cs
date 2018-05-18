using System;
using TrackerService.Base;
using TrackerService.Interfaces;

namespace TrackerService.Implementations
{
    public class TransactionService : ITransactionService
    {
        public ServiceResult AddTransaction(int envelopeId, decimal transactionAmount, DateTime transactionDate,
            string transactionName)
        {
            //Get Envelope
            //Apply transaction to envelope
            //Save to repo
            throw new NotImplementedException();
        }
    }
}