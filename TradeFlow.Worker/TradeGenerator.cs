namespace TradeTelemetrySimulator
{
    public class TradeGenerator
    {
        private readonly Random _random = new();
        private readonly string[] _symbols = new[] { "Infy", "TCS", "Wipro", "HCLTech", "LTI" };

        public TradeEvent Next()
        {
            return new TradeEvent
            {
                Symbol = _symbols[_random.Next(_symbols.Length)],
                Price = Math.Round((decimal)(_random.NextDouble() * 1000 + 100), 2),
                Quantity = _random.Next(1, 1000), 
                TimestampUtc = DateTime.UtcNow 
            };
        }
        public IEnumerable<TradeEvent> GenerateBatch(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return Next();
            }
        }
    }
}