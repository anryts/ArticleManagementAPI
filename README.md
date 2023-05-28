To start project you will need provide this json into user secrets
```
{
  "Kestrel:Certificates:Development:Password": "xxxx",
  "AWSCredentials": {
    "S3BucketName": "xxxx"
  },
  "SendGridOptions": {
    "ApiKey": "xxxx",
    "FromEmail": "xxxx",
    "NameOfSender": "xxxx"
  },
  "TwilioOptions": {
    "AccountSid": "xxxx",
    "AuthToken": "xxxx",
    "FromPhoneNumber": "xxxx"
  },
  "StripeOptions": {
    "PublishableKey": "xxxx",
    "SecretKey": "xxxx",
    "MonhtlyPriceId": "xxxx",
    "YearlyPriceId": "xxxx",
    "WebhookSecret": "xxxx"
  },
  "RabbitMQOptions": {
    "HostName": "rabbitmq",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest"
  },
  "JwtOptions": {
    "Token": "xxxx"
  }
}
```
**xxxx -  mean, that you need provide your own values
**MonthlyPriceId & YearlyPriceId - it's created product, so to test this functionality you will also create some product in STRIPE to test it
If you encounter with "database "Article API" does not exist", just disable in first start lines:
26, 37, and 50-56, it must help (:
after successful start return this lines.
After all this configuring project must be working.
