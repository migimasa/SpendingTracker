namespace Tracker.DAL.Tables
{
    public class Envelope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public int RolloverType { get; set; }
    }
}