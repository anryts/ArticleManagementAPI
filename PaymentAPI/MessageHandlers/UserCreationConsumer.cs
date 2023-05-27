using Common.DTOs;
using MassTransit;
using PaymentAPI.Data.Entities;
using PaymentAPI.Data.Repositories.Interfaces;
using Stripe;
using Subscription = PaymentAPI.Data.Entities.Subscription;

namespace PaymentAPI.MessageHandlers;

public class UserCreationConsumer : IConsumer<UserCreationDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IStripeClient _stripeClient;

    public UserCreationConsumer(IUserRepository userRepository, IStripeClient stripeClient)
    {
        _userRepository = userRepository;
        _stripeClient = stripeClient;
    }

    public async Task Consume(ConsumeContext<UserCreationDto> context)
    {
        if (await _userRepository.GetByIdAsync(context.Message.UserId) is not null)
            throw new Exception("User have already created");

        var subscription = new Subscription
        {
            UserId = context.Message.UserId,
            Id = Guid.NewGuid(),
            IsActive = false
        };

        var user = new User
        {
            Id = context.Message.UserId,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            Email = context.Message.Email,
            PhoneNumber = context.Message.PhoneNumber,
            Subscription = subscription,
        };

        var customerService = new CustomerService(_stripeClient);
        var customer = await customerService.CreateAsync(new CustomerCreateOptions
        {
            Name = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
        });
        
        user.CustomerId = customer.Id;
        await _userRepository.AddAsync(user);
    }
}