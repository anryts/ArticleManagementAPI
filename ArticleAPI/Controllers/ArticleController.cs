using ArticleAPI.Handlers;
using ArticleAPI.Models.RequestModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArticleAPI.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get article by id
    /// </summary>
    /// <param name="articleId">Article ID which you want to get</param>
    /// <returns></returns>
    [HttpGet("{articleId:guid}")]
    public async Task<IActionResult> GetArticle(Guid articleId)
    {
        var response = await _mediator.Send(new GetArticleByIdQuery
        {
            ArticleId = articleId
        });
        return Ok(response);
    }

    /// <summary>
    /// Get a list of articles by filter
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> GetArticles([FromQuery] GetArticlesModel model)
    {
        var response = await _mediator.Send(new GetArticlesQuery
            {
                SearchKey = model.SearchKey,
                Ascending = model.Ascending,
                SortBy = model.SortBy,
                AmountOfArticles = model.AmountOfArticles,
                Offset = model.Offset,
            }
        );
        return Ok(response);
    }

    /// <summary>
    /// Get specific article version by id
    /// </summary>
    /// <param name="articleVersionId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
    [HttpGet("articleVersions/{articleVersionId:guid}")]
    public async Task<IActionResult> GetArticleVersion(Guid articleVersionId)
    {
        var response = await _mediator.Send(new GetArticleVersionQuery
            {
                ArticleVersionId = articleVersionId,
            }
        );
        return Ok(response);
    }

    /// <summary>
    /// Get a list of article versions by article id
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
    [HttpGet("{articleId:guid}/articleVersions")]
    public async Task<IActionResult> GetArticleVersions(Guid articleId)
    {
        var response = await _mediator.Send(new GetArticleVersionsQuery
            {
                ArticleId = articleId,
            }
        );
        return Ok(response);
    }

    /// <summary>
    /// Upload new article
    /// </summary>
    /// <param name="model">Request model</param>
    /// <param name="validator"></param>
    /// <returns>Guid of uploaded article</returns>
    [HttpPost]
    public async Task<IActionResult> UploadArticle([FromBody] CreateNewArticleModel model,
        [FromServices] IValidator<CreateNewArticleModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        var result = await _mediator.Send(new CreateArticleCommand
            {
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags,
            }
        );
        return Ok(result);
    }

    /// <summary>
    /// Upload images to the article
    /// </summary>
    /// <param name="images">A collection of images</param>
    /// <param name="articleVersionId">A guid which you have received from UploadArticle endpoint</param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPut("articleVersions/{articleVersionId:guid}/images")]
    public async Task<IActionResult> UploadImage(IFormFileCollection images, Guid articleVersionId)
    {
        await _mediator.Send(new UploadImageCommand
            {
                ArticleVersionId = articleVersionId,
                Images = images,
            }
        );
        return Ok("Image was uploaded");
    }

    /// <summary>
    /// Edit existing article
    /// </summary>
    /// <param name="model">Request model</param>
    /// <param name="articleId">A guid which you have received from UploadArticle endpoint</param>
    /// <param name="validator"></param>
    /// <returns></returns>
    [HttpPut("{articleId:guid}")]
    public async Task<IActionResult> EditArticle([FromBody] EditArticleModel model, Guid articleId,
        [FromServices] IValidator<EditArticleModel> validator)
    {
        await validator.ValidateAndThrowAsync(model);
        var result = await _mediator.Send(new EditArticleCommand
            {
                Title = model.Title,
                Content = model.Content,
                ArticleId = articleId,
                Tags = model.Tags
            }
        );
        return Ok(result);
    }

    /// <summary>
    /// Delete existing article with all related data and previous versions 
    ///  </summary>
    /// <param name="articleId">A guid which you have received from UploadArticle endpoint</param>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteArticle(Guid articleId, Guid userId)
    {
        var article = await _mediator.Send(new DeleteArticleCommand
        {
            ArticleId = articleId,
            UserId = userId
        });
        await _mediator.Send(new ScheduleArticleDeleteCommand
        {
            Article = article
        });
        return Ok();
    }

    /// <summary>
    /// Add like or dislike to the article
    /// To remove like/dislike just send the same request again
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="isLike">True is Like, False is Dislike</param>
    /// <returns></returns>
    [HttpPut("{articleId:guid}/Likes")]
    public async Task<IActionResult> AddLike(Guid articleId, bool isLike)
    {
        await _mediator.Send(new AddLikeToArticleCommand
        {
            IsLike = isLike,
            ArticleId = articleId
        });
        return Ok("You have successfully added like/dislike to the article");
    }
}