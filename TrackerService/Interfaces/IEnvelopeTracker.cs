using System;
using TrackerService.Base;
using TrackerService.DTO;
using TrackerService.Enums;

namespace TrackerService.Interfaces
{
    public interface IEnvelopeTracker
    {
        ServiceResult CreateEnvelope(string envelopeName, decimal startingBalance, BalanceRolloverType rolloverType);
        MoneyEnvelopeDto GetEnvelope(int id);
    }
}
