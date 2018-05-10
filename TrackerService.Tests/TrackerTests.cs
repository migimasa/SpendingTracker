using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Tracker.DAL.Interfaces;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Enums;
using TrackerService.Interfaces;
using TrackerService.Implementations;
using TrackerService.Mappings;
using TrackerService.Tests.Mocks;
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
            IEnvelopeRepository envelopeRepo = new MockEnvelopeRepository();
            var tracker = CreateEnvelopeTrackerService(envelopeRepo);

            string envelopeName = "TEST_ENVELOPE";
            decimal startingBalance = 100.00M;
            BalanceRolloverType rolloverType = BalanceRolloverType.ApplyToSurplus;
            
            ServiceResult createResult = tracker.CreateEnvelope(envelopeName, startingBalance, rolloverType);

            createResult.Should().NotBeNull();
            createResult.IsSuccess.Should().BeTrue();
            createResult.Errors.Should().NotBeNull();
            createResult.Errors.Should().BeEmpty();
            createResult.Data.Should().NotBeNull();
            createResult.Data.Should().BeOfType<int>();

            int id = (int)createResult.Data;

            var envelopeRecord = envelopeRepo.GetEnvelopeById(id);
            envelopeRecord.Should().NotBeNull();
            envelopeRecord.Id.Should().BePositive();
            envelopeRecord.Name.Should().Be(envelopeName);
            envelopeRecord.StartingBalance.Should().Be(startingBalance);
            envelopeRecord.AvailableBalance.Should().Be(startingBalance);
            envelopeRecord.RolloverType.Should().Be(rolloverType.Value);
        }

        private IEnvelopeTracker CreateEnvelopeTrackerService(IEnvelopeRepository envelopeRepo)
        {
            IEnvelopeTracker tracker = new EnvelopeTracker(new NullLogger<EnvelopeTracker>(),
                                                           this.CreateMapper(),
                                                           envelopeRepo);
            return tracker;
        }

        private IMapper CreateMapper()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EnvelopeMappingProfile>();
            });

            return mapConfig.CreateMapper();
        }

        [Fact]
        public void CreateNewEnvelope_InvalidName_NotCreated()
        {
            IEnvelopeTracker tracker = CreateEnvelopeTrackerService();

            string envelopeName = string.Empty;
            decimal startingBalance = 100.00M;
            BalanceRolloverType rolloverType = BalanceRolloverType.ApplyToSurplus;

            ServiceResult createResult = tracker.CreateEnvelope(envelopeName, startingBalance, rolloverType);

            createResult.Should().NotBeNull();
            createResult.IsSuccess.Should().BeFalse();
            createResult.Errors.Should().NotBeNull();
            createResult.Errors.Should().HaveCount(1);
            createResult.Errors[0].Should().Be("Envelope must have a name.");
            createResult.Data.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void CreateNewEnvelope_InvalidStartingBalance_NotCreated(decimal startingBalance)
        {
            IEnvelopeTracker tracker = CreateEnvelopeTrackerService();

            string envelopeName = "__TEST_NAME__";
            BalanceRolloverType rolloverType = BalanceRolloverType.ApplyToSurplus;

            ServiceResult createResult = tracker.CreateEnvelope(envelopeName, startingBalance, rolloverType);

            createResult.Should().NotBeNull();
            createResult.IsSuccess.Should().BeFalse();
            createResult.Errors.Should().NotBeNull();
            createResult.Errors.Should().HaveCount(1);
            createResult.Errors.Should().Contain("Starting balance must be positive.");
            createResult.Data.Should().BeNull();
        }

        [Fact]
        public void GetEnvelope_ValidEnvelopeId()
        {
            var tracker = CreateEnvelopeTrackerService();

            int id = 1;

            MoneyEnvelopeDto envelope = tracker.GetEnvelope(id);

            envelope.Should().NotBeNull();
            envelope.Name.Should().Be("__TEST_ENVELOPE__");
            envelope.StartingBalance.Should().Be(50M);
            envelope.AvailableBalance.Should().Be(25M);
            envelope.RolloverType.Should().Be(BalanceRolloverType.Unknown);
        }

        private IEnvelopeTracker CreateEnvelopeTrackerService()
        {
            IEnvelopeTracker tracker = CreateEnvelopeTrackerService(new MockEnvelopeRepository());
            return tracker;
        }

        [Fact]
        public void GetEnvelope_InvalidId_ReturnsNull()
        {
            var tracker = CreateEnvelopeTrackerService();

            int id = 0;

            var envelope = tracker.GetEnvelope(id);

            envelope.Should().BeNull();
        }

        [Fact]
        public void CreateEnvelope_DuplicateName_DoesNotSave()
        {
            var tracker = CreateEnvelopeTrackerService();
            
            string envelopeName = "__TEST_ENVELOPE__";
            decimal startingBalance = 100.00M;
            BalanceRolloverType rolloverType = BalanceRolloverType.ApplyToSurplus;

            ServiceResult createResult = tracker.CreateEnvelope(envelopeName, startingBalance, rolloverType);

            createResult.Should().NotBeNull();
            createResult.IsSuccess.Should().BeFalse();
            createResult.Errors.Should().NotBeNullOrEmpty();
            createResult.Errors.Should().HaveCount(1);
            createResult.Errors.Should().Contain("__TEST_ENVELOPE__ is already in use. Please select another name.");
            createResult.Data.Should().BeNull();
        }
    }
}