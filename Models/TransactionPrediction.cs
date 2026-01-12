using Microsoft.ML.Data;

public class TransactionPrediction
{
    [ColumnName("PredictedLabel")]
    public bool IsFraud { get; set; }

    public float Score { get; set; }

    public float Probability { get; set; }
}
