using APIGateway.Providers.Interfaces;
using APIGateway.Services.Interfaces;
using AuthAPI.Data.Repositories.Interfaces;
using Common.Enums;
using Common.Models.ResponseModels;
using Gateway.Data.Entities;
using MediatR;

namespace AuthAPI.Users.Handlers;

public class SignUpUserCommand : IRequest<UserResponseCreateModel>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Gender? Gender { get; set; }
}

public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, UserResponseCreateModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ISmsProvider _smsProvider;
    private readonly ILoginService _loginService;

    public SignUpUserCommandHandler(IUserRepository userRepository,
        IPasswordService passwordService, ISmsProvider smsProvider,
        ILoginService loginService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _smsProvider = smsProvider;
        _loginService = loginService;
    }

    public async Task<UserResponseCreateModel> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.CheckIfEmailsIsUnique(request.Email))
            throw new Exception("Email is already exists");
        if (!await _userRepository.CheckIfPhoneNumbersIsUnique(request.PhoneNumber))
            throw new Exception("Phone is already used");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Gender = request.Gender ?? Gender.Unknown,
            PasswordHash = _passwordService.GenerateHashPassword(request.Password),
            PasswordUpdated = DateTime.UtcNow,
            PhoneNumber = request.PhoneNumber,
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        var otpCode = await _loginService.GenerateOTPCodeForUser(user.Id);
        await _smsProvider.SendSmsAsync(user.PhoneNumber, otpCode);
        return new UserResponseCreateModel { Id = user.Id, Email = user.Email, Name = user.FirstName };
    }
}