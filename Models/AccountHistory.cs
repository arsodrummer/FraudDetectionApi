namespace FraudDetectionApi.Models
{
    public class AccountHistory
    {
        public float AverageAmount { get; private set; }

        public int TotalTransactions { get; private set; }
        
        public DateTime? LastTimestamp { get; private set; }

        private readonly List<DateTime> _timestamps = new();

        public void Update(TransactionRaw tx)
        {
            AverageAmount =
                (AverageAmount * TotalTransactions + tx.Amount)
                / (TotalTransactions + 1);

            TotalTransactions++;
            LastTimestamp = tx.Timestamp;
            _timestamps.Add(tx.Timestamp);
        }

        public int CountLast24h(DateTime now)
        {
            _timestamps.RemoveAll(t => (now - t).TotalHours > 24);
            return _timestamps.Count;
        }
    }
}