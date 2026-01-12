using FraudDetectionApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<FraudDetectionModelTrainer>();
builder.Services.AddSingleton<FeatureEngineeringService>();

var app = builder.Build();

var trainer = app.Services.GetRequiredService<FraudDetectionModelTrainer>();
//trainer.Train("Data/transactions.csv");
trainer.TrainSimpleVersion("Data/transactions.csv");

app.MapControllers();
app.Run();