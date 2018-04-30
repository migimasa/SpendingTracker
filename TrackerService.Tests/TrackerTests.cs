using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Tracker.DAL.Interfaces;
using TrackerService.Base;
using TrackerService.DTO;
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

//        [Fact]
//        public void CreateNewEnvelope_InvalidName_NotCreated()
//        {
//            IEnvelopeTracker tracker = new EnvelopeTracker();
//
//            string envelopeName = "";
//            decimal startingBalance = 100.00M;
//
//            ServiceResult createResult = tracker.CreateEnvelope(envelopeName, startingBalance);
//
//            createResult.Should().NotBeNull();
//            createResult.IsSuccess.Should().BeFalse();
//            createResult.Errors.Should().NotBeNull();
//            createResult.Errors.Count.Should().Be(1);
//            createResult.Errors[0].Should().Be("Envelope must have a name.");
//            createResult.Data.Should().BeNull();
//
//        }
        
    }
}