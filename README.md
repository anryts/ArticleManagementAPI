# To start project you will need provide this json into user secrets
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
* xxxx -  mean, that you need provide your own values
* MonthlyPriceId & YearlyPriceId - it's created product, so to test this functionality you will also create some product in STRIPE to test it
___
If you encounter with *"database Article API does not exist"*, just disable in first start ArticlleAPI/Program.cs lines:
26, 37, and 50-56, it must help (:
___
# after successful start return this lines.
# After all this configuring project must be working.

# Short summary for project
```
Developed a microservice-based application for article management using the
.NET.
Utilized RabbitMQ as the message broker for asynchronous communication
between microservices.
Implemented MassTransit as the messaging library to facilitate message-based
communication.
Leveraged C# and ASP.NET Core for the back-end development of the
microservice.
Utilized Entity Framework Core as the ORM (Object-Relational Mapping) tool for
database operations.
Integrated PostgreSQL as the main relational database
Integrated Amazon S3 for storing and managing images and article text
Utilized the Stripe API for subscription processing.
Implemented 2FA with SendGrid and Twilio APIs
For Background Jobs utilized Hangfire library
```
