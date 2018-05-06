using AutoMapper;
using Microsoft.Extensions.Logging;
using Tracker.DAL.Interfaces;
using Tracker.DAL.Tables;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Interfaces;

namespace TrackerService.Implementations
{
    public class EnvelopeTracker : IEnvelopeTracker
    {
        private readonly ILogger<EnvelopeTracker> logger;
        private readonly IMapper mapper;
        private readonly IEnvelopeRepository envelopeRepository;

        public EnvelopeTracker(ILogger<EnvelopeTracker> logger,
            IMapper mapper,
            IEnvelopeRepository envelopeRepo)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.envelopeRepository = envelopeRepo;
        }

        public ServiceResult CreateEnvelope(string envelopeName, decimal startingBalance)
        {
            ServiceResult result = ValidateEnvelopeInformation(envelopeName, startingBalance);

            if (result.IsSuccess)
            {
                result.Data = new MoneyEnvelopeDto()
                {
                    Name = envelopeName,
                    StartingBalance = startingBalance,
                    AvailableBalance = startingBalance
                };

                Envelope envelopeRecord = mapper.Map<Envelope>(result.Data);
                
                
            }

            return result;
        }

        private ServiceResult ValidateEnvelopeInformation(string envelopeName, decimal startingBalance)
        {
            ServiceResult result = new ServiceResult();

            if (string.IsNullOrWhiteSpace(envelopeName))
                result.AddErrorMessage("Envelope must have a name.");
            
            return result;
        }
    }
}