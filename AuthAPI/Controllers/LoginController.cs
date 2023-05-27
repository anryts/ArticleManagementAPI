using APIGateway.CustomFilters;
using APIGateway.Services.Interfaces;
using AuthAPI.Users.Handlers;
using Common.Models.RequestModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UpdateUserPassword = AuthAPI.Users.Handlers.UpdateUserPassword;

namespace AuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILoginService _loginService;

    public LoginController(IMediator mediator, ILoginService loginService)
    {
        _mediator = mediator;
        _loginService = loginService;
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] UserCreateModel model,
        [FromServices] IValidator<UserCreateModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        var result = await _mediator.Send(new SignUpUserCommand
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password,
            PhoneNumber = model.PhoneNumber,
            Gender = model.Gender,
        });
        return new OkObjectResult(result);
    }

    [HttpPut("VerifyUserMobilePhone")]
    public async Task<IActionResult> VerifyUserMobilePhone(Guid userId, string smsToken)
    {
        await _mediator.Send(new VerifyUserPhoneNumber
        {
            UserId = userId,
            SmsToken = smsToken
        });
        return new OkObjectResult("Success");
    }

    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] UserSignInModel model,
        [FromServices] IValidator<UserSignInModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        await _mediator.Send(new EnterUserCommand
        {
            Email = model.Email,
            Password = model.Password,
            OtpSendServiceType = model.OtpSendServiceType
        });
        return Ok($"OTP will be sent {model.OtpSendServiceType}");
    }

    [HttpPost("SignInWith2FA")]
    public async Task<IActionResult> SignInWith2FA(Guid userId, string otp)
    {
        var result = await _loginService.EnterUserVia2FA(userId, otp);
        return new OkObjectResult(result);
    }

    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
    {
        var result = await _loginService.RefreshToken(model);
        return new OkObjectResult(result);
    }

    [HttpPut("ResetPassword"), Auth]
    public async Task<IActionResult> CreateNewPassword([FromBody] CreateNewPasswordModel model,
        [FromServices] IValidator<CreateNewPasswordModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        await _mediator.Send(new UpdateUserPassword
        {
            NewPassword = model.NewPassword,
            OldPassword = model.OldPassword
        });
        return Ok();
    }
}