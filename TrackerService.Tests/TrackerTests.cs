using System;
using FluentAssertions;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Interfaces;
using TrackerService.Implementations;
using Xunit;

namespace TrackerService.Tests
{
    public class TrackerTests
    {
        [Fact]
        public void EmptyTest()
        {
            true.Should().BeTrue();
        }

        [Fact]
        public void CreateNewEnvelope_ValidInputs()
        {
            IEnvelopeTracker tracker = new EnvelopeTracker();

            string envelopeName = "TEST_ENVELOPE";
            decimal startingBalance = 100.00M;
            
            ServiceResult createResult = tracker.CreateEnvelope(envelopeName, startingBalance);

            createResult.Should().NotBeNull();
            createResult.IsSuccess.Should().BeTrue();
            createResult.Errors.Should().NotBeNull();
            createResult.Errors.Should().BeEmpty();
            createResult.Data.Should().NotBeNull();
            createResult.Data.Should().BeOfType<MoneyEnvelopeDto>();
            
            MoneyEnvelopeDto envelope = (MoneyEnvelopeDto)createResult.Data;

            envelope.Name.Should().Be(envelopeName);
            envelope.StartingBalance.Should().Be(startingBalance);
            envelope.AvailableBalance.Should().Be(startingBalance);
        }
    }
}