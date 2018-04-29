using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Interfaces;

namespace TrackerService.Implementations
{
    public class EnvelopeTracker : IEnvelopeTracker
    {
        public ServiceResult CreateEnvelope(string envelopeName, decimal startingBalance)
        {
            ServiceResult result = new ServiceResult();

            result.Data = new MoneyEnvelopeDto()
            {
                Name = envelopeName,
                StartingBalance = startingBalance,
                AvailableBalance = startingBalance
            };
            
            return result;
        }
    }
}