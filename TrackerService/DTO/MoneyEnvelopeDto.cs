using TrackerService.Enums;

namespace TrackerService.DTO
{
    public class MoneyEnvelopeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public BalanceRolloverType RolloverType { get; set; }
    }
}