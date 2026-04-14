# Fraud Detection API

<img width="794" height="547" alt="image" src="https://github.com/user-attachments/assets/3e311474-a921-437f-ab14-d137be81b3e3" />

A prototype fraud detection API built with .NET and ML.NET, designed to identify suspicious transactions using machine learning.

This project demonstrates how a backend developer can integrate ML models into a real-world API and build a foundation for scalable fraud detection systems.

## Overview

Fraud detection is a classic problem in fintech systems where real-time decisions are critical.
This project explores how to:

Train a machine learning model using transaction data

Integrate the model into a .NET Web API

Predict whether a transaction is fraudulent

Build a clean, extensible backend architecture for ML-powered features

## Tech Stack

- .NET (ASP.NET Core Web API)
- ML.NET
- RESTful API endpoints
- Dependency Injection
- C#

## How it works
1. Model Training
    - Historical transaction data is used to train a binary classification model.
    - The model learns patterns that distinguish fraudulent vs legitimate transactions.

2. Prediction Engine
    - The trained model is loaded into the API.
    - Incoming transactions are passed through the model.

3. Fraud Detection
    - The API returns a prediction:
    - `IsFraud = true/false`
    - Probability score (confidence)

## Prerequisites

- .NET 6.0 or higher
- Visual Studio 2022 (optional)

## Installation

`git clone https://github.com/arsodrummer/FraudDetectionApi.git`

`cd FraudDetectionApi`

`dotnet clean | dotner build`

`dotnet run` The API will start locally (typically on https://localhost:50000 or similar).

## Usage
### Predict Transaction

#### Endpoint:

`POST /api/Fraud/analyze`

#### Request Example:

`{
  "TransactionId": "123456789",
  "AccountNumber": "DE123",
  "LastName": "Pitson",
  "BaseCurrency": "USD",
  "Currency": "EUR",
  "Amount": 1000
}`

#### Response Example:

`{
  "isFraud": true,
  "score": 51,
  "probability": 1
}`

## Architecture Notes
- Clean separation between:
    - API layer
    - ML logic
    - Data models
- Designed to be extended with:
    - Real datasets
    - Feature engineering pipelines
    - Model retraining workflows
- Can evolve into:
    - Microservice
    - Event-driven fraud detection system

## Learnings
- Basics of machine learning in .NET
- Using ML.NET for classification tasks
- Structuring ML-powered APIs
- Challenges of fraud detection systems

## Support
If you find this project useful:
- Star the repo ⭐
- Share it
- Build on top of it 🚀
