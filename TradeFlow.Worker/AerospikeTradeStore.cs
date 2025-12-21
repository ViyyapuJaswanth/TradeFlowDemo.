using Aerospike.Client;
using TradeTelemetrySimulator;

namespace TradeFlow.Worker
{
    public class AerospikeTradeStore : IDisposable
    {
        private readonly AerospikeClient _client;
        private readonly string _ns;
        private readonly string _set;

        public AerospikeTradeStore(IConfiguration cfg)
        {
            var section = cfg.GetSection("Aerospike");
            _ns = section["Namespace"]!;
            _set = section["Set"]!;
            var hosts = section["Hosts"]!.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var first = hosts[0].Split(':', StringSplitOptions.RemoveEmptyEntries);
            var host = first[0];
            var port = int.Parse(first[1]);
            _client = new AerospikeClient(host, port);
        }
        public void Write(TradeEvent trade)
        {
            var key = new Key(_ns, _set, trade.TradeId);
            var bins = new Bin[]
            {
                new Bin("Symbol", trade.Symbol),
                new Bin("Price", trade.Price),
                new Bin("Quantity", trade.Quantity),
                new Bin("TimestampUtc", trade.TimestampUtc.ToString("o"))
            };
            _client.Put(null, key, bins);
        }
        public void WriteBatch(IEnumerable<TradeEvent> trades)
        {
            foreach (var trade in trades)
            {
                Write(trade);
            }          
        }
        private WritePolicy WritePolicyDefault() => new WritePolicy
        {
            totalTimeout = 1000,
            sendKey = true           
        };
        
        public void Dispose() => _client?.Dispose();

    }
}
