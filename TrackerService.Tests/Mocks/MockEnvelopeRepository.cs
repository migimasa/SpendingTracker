using System.Collections.Generic;
using System.Linq;
using Tracker.DAL.Interfaces;
using Tracker.DAL.Tables;

namespace TrackerService.Tests.Mocks
{
    public class MockEnvelopeRepository : IEnvelopeRepository
    {
        private List<Envelope> envelopes = new List<Envelope>();

        public MockEnvelopeRepository()
        {
            envelopes.Add(new Envelope()
            {
                AvailableBalance = 25M,
                StartingBalance = 50M,
                Name = "__TEST_ENVELOPE__",
                RolloverType = 0,
                Id = 1
            });
        }

        public int SaveEnvelope(Envelope envelope)
        {
            envelope.Id = envelopes.Count == 0 ? 1 : envelopes.Max(env => env.Id) + 1;
            envelopes.Add(envelope);

            return envelope.Id;
        }

        public IEnumerable<Envelope> GetAllEnvelopes()
        {
            return this.envelopes;
        }

        public Envelope GetEnvelopeById(int id)
        {
            return envelopes.FirstOrDefault(env => env.Id == id);
        }
    }
}