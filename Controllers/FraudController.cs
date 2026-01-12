using FraudDetectionApi.Models;
using FraudDetectionApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace FraudDetectionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FraudController : ControllerBase
    {
        private readonly PredictionEngine<TransactionData, TransactionPrediction> _predictor;
        private readonly FeatureEngineeringService featureEngineeringService;

        public FraudController(FraudDetectionModelTrainer trainer, FeatureEngineeringService featureEngineeringService)
        {
            _predictor = trainer.CreatePredictor();
            this.featureEngineeringService = featureEngineeringService;
        }

        [HttpPost("analyze")]
        public ActionResult<TransactionPrediction> Analyze([FromBody] TransactionRaw transactionRaw)
        {
            var transaction = featureEngineeringService.Transform(new TransactionRaw
            {
                AccountNumber = transactionRaw.AccountNumber,
                Amount = transactionRaw.Amount,
                BaseCurrency = transactionRaw.BaseCurrency,
                Currency = transactionRaw.Currency,
                Timestamp = DateTime.UtcNow,
            });

            var result = _predictor.Predict(transaction);
            return Ok(result);
        }

        [HttpGet("test")]
        public IActionResult Test() => Ok("API is working!");
    }
}
