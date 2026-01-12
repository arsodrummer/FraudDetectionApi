namespace FraudDetectionApi.Models
{
    public class TransactionData
    {
        public float Amount { get; set; }

        public float AmountVsAvgRatio { get; set; }

        public float TimeSinceLastTransaction { get; set; }

        public float NumTransactionsLast24h { get; set; }
        
        public float IsForeignTransaction { get; set; }

        public float IsNewCurrency { get; set; }

        public float IsNewAccount { get; set; }

        public bool Label { get; set; }
    }
}
