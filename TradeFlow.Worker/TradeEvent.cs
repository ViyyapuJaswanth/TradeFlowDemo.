namespace TradeTelemetrySimulator
{
    public class TradeEvent
    {
        public string TradeId { get; set; } = Guid.NewGuid().ToString();
        public string Symbol { get; set; } = "INFY";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}
