using System;
using TrackerService.Base;

namespace TrackerService.Interfaces
{
    public interface IEnvelopeTracker
    {
        ServiceResult CreateEnvelope(string envelopeName, decimal startingBalance);
    }
}
