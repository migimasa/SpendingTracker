using System.Collections.Generic;
using System.Linq;
using Tracker.DAL.Interfaces;
using Tracker.DAL.Tables;

namespace TrackerService.Tests.Mocks
{
    public class MockEnvelopeRepository : IEnvelopeRepository
    {
        private List<Envelope> envelopes = new List<Envelope>();
        
        public int SaveEnvelope(Envelope envelope)
        {
            envelope.Id = envelopes.Max(env => env.Id) + 1;
            envelopes.Add(envelope);

            return envelope.Id;
        }
        public Envelope GetEnvelopeById(int id)
        {
            return envelopes.FirstOrDefault(env => env.Id == id);
        }
    }
}