using APIGateway.Services.Interfaces;
using AuthAPI.Data.Repositories.Interfaces;
using AutoMapper;
using Common.DTOs;
using Common.EventBus;
using MediatR;

namespace AuthAPI.Users.Handlers;

public class VerifyUserPhoneNumber : IRequest
{
    public Guid UserId { get; set; }
    public string SmsToken { get; set; }
}

public class VerifyUserPhoneNumberHandler : IRequestHandler<VerifyUserPhoneNumber, Unit>
{
    private readonly ILoginService _loginService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public VerifyUserPhoneNumberHandler(ILoginService loginService,
        IUserRepository userRepository,
        IMapper mapper, IEventBus eventBus)
    {
        _loginService = loginService;
        _userRepository = userRepository;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(VerifyUserPhoneNumber request, CancellationToken cancellationToken)
    {
        if (!await _loginService.Verify2FA(request.UserId, request.SmsToken))
            return Unit.Value;

        var user = await _userRepository.GetByIdAsync(request.UserId);
        user.IsActivated = true;
        var createdUser = _mapper.Map<UserCreationDto>(user);
        await _eventBus.Publish(createdUser, cancellationToken);
        await _userRepository.SaveChangesAsync();

        return Unit.Value;
    }
}