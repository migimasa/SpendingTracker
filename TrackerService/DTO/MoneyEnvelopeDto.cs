namespace TrackerService.DTO
{
    public class MoneyEnvelopeDto
    {
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}