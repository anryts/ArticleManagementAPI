using ArticleAPI.Channels.Commands;
using ArticleAPI.Channels.Queries;
using ArticleAPI.Models.RequestModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleAPI.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class ChannelController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChannelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateChannel")]
    public async Task<IActionResult> CreateChannel([FromBody] CreateChannelRequestModel model,
        [FromServices] IValidator<CreateChannelRequestModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        await _mediator.Send(new CreateChannelCommand
        {
            Title = model.Title,
            Description = model.Description,
            IsPrivate = model.IsPrivate
        });
        return Ok("Channel created");
    }

    [HttpDelete("DeleteChannel")]
    public async Task<IActionResult> DeleteChannel(string title)
    {
        await _mediator.Send(new DeleteChannelCommand
        {
            Title = title
        });
        return Ok("Channel deleted");
    }

    /// <summary>
    /// Change channel title or/and description
    /// </summary>
    /// <returns></returns>
    [HttpPut("UpdateChannel")]
    public async Task<IActionResult> UpdateChannel([FromBody] EditChannelRequestModel model,
        [FromServices] IValidator<EditChannelRequestModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        await _mediator.Send(new EditChannelCommand
        {
            ChannelId = model.ChannelId,
            Title = model.Title,
            Description = model.Description
        });
        return Ok("Channel was updated");
    }

    /// <summary>
    ///  Add your article to channel
    /// </summary>
    /// <returns></returns>
    [HttpPut("AddArticleToChannel")]
    public async Task<IActionResult> AddArticleToChannel(Guid articleId, Guid channelId)
    {
        await _mediator.Send(new AddArticleToChannelCommand
        {
            ArticleId = articleId,
            ChannelId = channelId
        });
        return Ok("Article added to channel");
    }

    /// <summary>
    /// Get channels for user
    /// </summary>
    /// <param name="isMine">Show only my channels : true or false</param>
    /// <param name="offset"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [HttpGet("GetChannels"), Authorize]
    public async Task<IActionResult> GetChannels(bool isMine, int offset, int take)
    {
        var result = await _mediator
            .Send(new GetChannelsForUserQuery
            {
                ShowOnlyMyChannels = isMine,
                Offset = offset,
                Take = take
            });
        return Ok(result);
    }
}