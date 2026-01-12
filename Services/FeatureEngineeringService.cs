using FraudDetectionApi.Models;

namespace FraudDetectionApi.Services
{
    public class FeatureEngineeringService
    {
        private readonly Dictionary<string, AccountHistory> _accounts = [];

        public TransactionData Transform(TransactionRaw raw)
        {
            if (!_accounts.TryGetValue(raw.AccountNumber, out var history))
            {
                history = new AccountHistory();
                _accounts[raw.AccountNumber] = history;
            }

            bool hasHistory = history.TotalTransactions > 0;

            float avgAmount = hasHistory
                ? history.AverageAmount
                : raw.Amount; // first transaction = baseline

            var data = new TransactionData
            {
                Amount = raw.Amount,

                // burst / automation
                TimeSinceLastTransaction = hasHistory
                    ? (float)(raw.Timestamp - history.LastTimestamp!.Value).TotalSeconds
                    : 0f,

                // high-frequency behavior
                NumTransactionsLast24h = history.CountLast24h(raw.Timestamp),

                // foreign transaction
                IsForeignTransaction =
                    raw.Currency != raw.BaseCurrency ? 1f : 0f,

                // first time currency usage (proxy)
                IsNewCurrency =
                    hasHistory && history.AverageAmount > 0
                        ? (raw.Currency != raw.BaseCurrency ? 1f : 0f)
                        : 0f,

                // deviation from personal baseline
                AmountVsAvgRatio =
                    avgAmount > 0 ? raw.Amount / avgAmount : 1f,

                Label = raw.Label
            };

            history.Update(raw);
            return data;
        }
    }
}