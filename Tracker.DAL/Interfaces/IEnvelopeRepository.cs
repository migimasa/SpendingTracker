using System.Collections.Generic;
using Tracker.DAL.Tables;

namespace Tracker.DAL.Interfaces
{
    public interface IEnvelopeRepository
    {
        Envelope GetEnvelopeById(int id);
        int SaveEnvelope(Envelope envelope);
        IEnumerable<Envelope> GetAllEnvelopes();
    }
}