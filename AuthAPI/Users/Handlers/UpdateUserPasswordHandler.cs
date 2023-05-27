using APIGateway.Services.Interfaces;
using AuthAPI.Data;
using AuthAPI.Data.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAPI.API.Services.Interfaces;

namespace AuthAPI.Users.Handlers;

public class UpdateUserPassword : IRequest
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPassword, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly AppDbContext _appDbContext;
    private readonly IPasswordUpdatedHandlerService _passwordUpdatedHandlerService;

    public UpdateUserPasswordHandler(ICurrentUserService currentUserService,
        IUserRepository userRepository, IPasswordService passwordService,
        AppDbContext appDbContext,
        IPasswordUpdatedHandlerService passwordUpdatedHandlerService)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _appDbContext = appDbContext;
        _passwordUpdatedHandlerService = passwordUpdatedHandlerService;
    }

    public async Task<Unit> Handle(UpdateUserPassword request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository
                       .GetByQueryAsync(q => q.Where(user => user.Id == userId)
                           .Include(user => user.IdentityUsers)
                           .FirstOrDefaultAsync())
                   ?? throw new Exception("User not found");

        if (!_passwordService.VerifyHashPassword(request.OldPassword, user.PasswordHash))
            throw new Exception("Old Password not correct");

        var hashedPassword = _passwordService.GenerateHashPassword(request.NewPassword);
        var updatedAt = DateTime.UtcNow;
        user.PasswordHash = hashedPassword;
        user.PasswordUpdated = updatedAt;
        await _userRepository.UpdateAsync(user);

        user.IdentityUsers.Clear();
        await _appDbContext.SaveChangesAsync();
        _passwordUpdatedHandlerService.AddToDictionary(userId, updatedAt);

        return Unit.Value;
    }
}