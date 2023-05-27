using APIGateway.Providers.Interfaces;
using APIGateway.Services.Interfaces;
using AuthAPI.Data.Repositories.Interfaces;
using Common.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Users.Handlers;

public class EnterUserCommand : IRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    /// <summary>
    /// 1 - ViaSms
    /// 2- ViaEmail 
    /// </summary>
    public OTPSendServiceType OtpSendServiceType { get; set; }
}

public class EnterUserCommandHandler : IRequestHandler<EnterUserCommand, Unit>
{
    private const string EmailSubject = "Your OTP";
    private const string OTPMessage = "Your OTP is";
    private readonly ILoginService _loginService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ISmsProvider _smsProvider;
    private readonly IEmailProvider _emailProvider;

    public EnterUserCommandHandler(ILoginService loginService,
        IUserRepository userRepository,
        IPasswordService passwordService,
        ISmsProvider smsProvider,
        IEmailProvider emailProvider)
    {
        _loginService = loginService;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _smsProvider = smsProvider;
        _emailProvider = emailProvider;
    }

    public async Task<Unit> Handle(EnterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByQueryAsync(
                       q => q.FirstOrDefaultAsync(user => user.Email == request.Email))
                   ?? throw new Exception("User wasn't found");
        if (!_passwordService.VerifyHashPassword(request.Password, user.PasswordHash))
            throw new Exception("Password was incorrect");

        if (!user.IsActivated)
            throw new Exception("User was not activated");

        var result = await _loginService.GenerateOTPCodeForUser(user.Id);

        switch (request.OtpSendServiceType)
        {
            case OTPSendServiceType.ViaSms:
            {
                await _smsProvider.SendSmsAsync(user.PhoneNumber,
                    $"{OTPMessage}: {result}");
                break;
            }
            case OTPSendServiceType.ViaEmail:
            {
                await _emailProvider
                    .SendEmailAsync(user.Email,
                        EmailSubject,
                        user.FirstName + user.LastName,
                        string.Format("{0}:{1}",
                            OTPMessage, result));
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Unit.Value;
    }
}