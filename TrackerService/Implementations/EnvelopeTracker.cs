using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tracker.DAL.Interfaces;
using Tracker.DAL.Tables;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Enums;
using TrackerService.Interfaces;
using TrackerService.Mappings;

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

        public ServiceResult CreateEnvelope(string envelopeName, decimal startingBalance,
            BalanceRolloverType rolloverType)
        {
            ServiceResult result = ValidateEnvelopeInformation(envelopeName, startingBalance);

            if (result.IsSuccess)
            {
                MoneyEnvelopeDto envelope = new MoneyEnvelopeDto()
                {
                    Name = envelopeName,
                    StartingBalance = startingBalance,
                    AvailableBalance = startingBalance,
                    RolloverType = rolloverType
                };

                int envelopeId = envelopeRepository.SaveEnvelope(mapper.Map<Envelope>(envelope));
                if (envelopeId > 0)
                    result.Data = envelopeId;
                else
                    result.AddErrorMessage("Could not save envelope");
            }

            return result;
        }

        public MoneyEnvelopeDto GetEnvelope(int id)
        {
            return mapper.Map<MoneyEnvelopeDto>(envelopeRepository.GetEnvelopeById(id));
        }

        private ServiceResult ValidateEnvelopeInformation(string envelopeName, decimal startingBalance)
        {
            ServiceResult result = new ServiceResult();

            if (string.IsNullOrWhiteSpace(envelopeName))
                result.AddErrorMessage("Envelope must have a name.");
            else if (IsNameInUse(envelopeName))
                result.AddErrorMessage($"{envelopeName} is already in use. Please select another name.");
            
            if (startingBalance <= 0)
                result.AddErrorMessage("Starting balance must be positive.");
            
            
            
            return result;
        }

        private bool IsNameInUse(string envelopeName)
        {
            bool isNameInUse = envelopeRepository.GetAllEnvelopes().Select(env => env.Name).Contains(envelopeName);

            return isNameInUse;
        }
    }
}