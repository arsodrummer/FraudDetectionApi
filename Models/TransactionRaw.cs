using Microsoft.ML.Data;

namespace FraudDetectionApi.Models
{
    public class TransactionRaw
    {
        [LoadColumn(0)]
        public string TransactionId { get; set; }

        [LoadColumn(1)]
        public string AccountNumber { get; set; }

        [LoadColumn(2)]
        public string LastName { get; set; }

        [LoadColumn(3)]
        public string BaseCurrency { get; set; }

        [LoadColumn(4)]
        public string Currency { get; set; }

        [LoadColumn(5)]
        public float Amount { get; set; }

        [LoadColumn(6)]
        public DateTime Timestamp { get; set; }

        [LoadColumn(7)]
        public bool Label { get; set; }
    }
}