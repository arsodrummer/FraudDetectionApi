using FraudDetectionApi.Models;
using Microsoft.ML;

namespace FraudDetectionApi.Services
{
    public class FraudDetectionModelTrainer
    {
        private readonly string _modelPath = "fraudDetectionModel.zip";
        private readonly MLContext _mlContext = new();

        public void Train(string dataPath)
        {
            var raw = _mlContext.Data.LoadFromTextFile<TransactionRaw>(
                dataPath, hasHeader: true, separatorChar: ',');

            var rawList = _mlContext.Data
                .CreateEnumerable<TransactionRaw>(raw, false)
                .ToList();

            var featureEngineeringService = new FeatureEngineeringService();

            var featureList = rawList
                .OrderBy(r => r.Timestamp)
                .Select(featureEngineeringService.Transform)
                .ToList();

            var data = _mlContext.Data.LoadFromEnumerable(featureList);

            var allData = _mlContext.Data.CreateEnumerable<TransactionData>(data, false).ToList();

            var fraudCount = allData.Count(x => x.Label == true);
            var normalCount = allData.Count(x => x.Label == false);

            Console.WriteLine($"Test set -> Fraud: {fraudCount}, Normal: {normalCount}");

            var balancedList = new List<TransactionData>();

            var normalTransactions = allData.Where(x => x.Label == false).Take(fraudCount);
            var fraudTransactions = allData.Where(x => x.Label == true);
            balancedList = [.. normalTransactions, .. fraudTransactions];

            // 6 Convert back to IDataView
            var balancedData = _mlContext.Data.LoadFromEnumerable(balancedList);

            // 7 Split into train/test
            var split = _mlContext.Data.TrainTestSplit(balancedData, testFraction: 0.2); // 80% for learning, 20% for testing

            var pipeline = _mlContext.Transforms.Concatenate(
                "Features",
                [ "Amount",
                "AmountVsAvgRatio",
                "TimeSinceLastTransaction",
                "NumTransactionsLast24h",
                "IsForeignTransaction",
                "IsNewCurrency",
                "IsNewAccount" ])
                .Append(_mlContext.BinaryClassification.Trainers.FastTree("Label"));

            var model = pipeline.Fit(split.TrainSet);

            _mlContext.Model.Save(model, split.TrainSet.Schema, _modelPath);
        }

        public PredictionEngine<TransactionData, TransactionPrediction> CreatePredictor()
        {
            ITransformer model = _mlContext.Model.Load(_modelPath, out _);
            return _mlContext.Model.CreatePredictionEngine<TransactionData, TransactionPrediction>(model);
        }
    }
}
