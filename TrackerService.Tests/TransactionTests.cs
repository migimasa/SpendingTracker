using System;
using FluentAssertions;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Implementations;
using TrackerService.Interfaces;
using Xunit;

namespace TrackerService.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void EmptyTest()
        {
            true.Should().BeTrue();
        }

        [Fact]
        public void AddTransaction()
        {
            ITransactionService transactions = new TransactionService();

            int envelopeId = 1;
            decimal transactionAmount = 10M;
            //TODO: Look into NodaTime
            DateTime transactionDate = DateTime.Parse("1/30/1988");
            string transactionName = string.Empty;

            ServiceResult addResult = transactions.AddTransaction(envelopeId, transactionAmount, transactionDate, transactionName);

            addResult.Should().NotBeNull();
            addResult.IsSuccess.Should().BeTrue();
            addResult.Errors.Should().NotBeNull();
            addResult.Errors.Should().BeEmpty();
            addResult.Data.Should().NotBeNull();
            addResult.Data.Should().BeOfType<MoneyEnvelopeDto>();

            var envelope = (MoneyEnvelopeDto) addResult.Data;

            envelope.Id.Should().Be(envelopeId);
            envelope.AvailableBalance.Should().Be(15M);

        }
    }
}