using Tracker.DAL.Tables;

namespace Tracker.DAL.Interfaces
{
    public interface IEnvelopeRepository
    {
        Envelope GetEnvelopeById(int id);
    }
}