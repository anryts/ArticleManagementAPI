using ArticleAPI.Data.Entities;
using ArticleAPI.Models.ResponseModels;
using AutoMapper;

namespace ArticleAPI.Profiles;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<Article, ArticleVersion>()
            .ForMember(dest => dest.ArticleId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ArticleVersionImages,
                opt => opt.MapFrom(src => src.Images));

        CreateMap<ArticleImage, ArticleVersionImage>()
            .ForMember(dest => dest.ImagePath,
                opt => opt.MapFrom(src => src.ImagePath));

        CreateMap<Article, GetArticleByIdResponseModel>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(x => x.Author!.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(x => x.Author!.LastName))
            .ForMember(dest => dest.CountOfLikes,
                opt => opt.MapFrom(article => article.Likes.Count(articleLike => articleLike.IsLike == true)))
            .ForMember(dest => dest.CountOfDislikes,
                opt => opt.MapFrom(article => article.Likes.Count(articleLike => articleLike.IsLike == false)))
            .ForMember(dest => dest.ArticleTags,
                opt => opt.MapFrom(x => x.ArticleTags.Select(src => src.TagName)));

        CreateMap<Article, GetArticleResponseModel>()
            .ForMember(dest => dest.AuthorFirstName,
                opt => opt.MapFrom(x => x.Author!.FirstName))
            .ForMember(dest => dest.AuthorLastName,
                opt => opt.MapFrom(x => x.Author!.LastName))
            .ForMember(dest => dest.CountOfLikes,
                opt => opt.MapFrom(article => article.Likes.Count(articleLike => articleLike.IsLike == true)))
            .ForMember(dest => dest.CountOfDislikes,
                opt => opt.MapFrom(article => article.Likes.Count(articleLike => articleLike.IsLike == false)))
            .ForMember(dest => dest.ArticleTags,
                opt => opt.MapFrom(x => x.ArticleTags.Select(src => src.TagName)));

        CreateMap<ArticleVersion, GetArticleVersionsResponseModel>();

        CreateMap<ArticleVersion, GetArticleVersionReposponseModel>()
            .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(x => x.ArticleVersionImages!.Select(src => src.ImagePath)));

        CreateMap<Comment, GetCommentRelatedToArticleResponseModel>()
            .ForMember(dest => dest.AuthorFirstName,
                opt => opt.MapFrom(x => x.Author!.FirstName))
            .ForMember(dest => dest.AuthorLastName,
                opt => opt.MapFrom(x => x.Author!.LastName))
            .ForMember(dest => dest.CommentText,
                opt => opt.MapFrom(x => x.Content))
            .ForMember(dest => dest.CommentId,
                opt => opt.MapFrom(x => x.Id));
        
        CreateMap<Channel, GetChannelsForUserResponseModel>()
            .ForMember(dest => dest.ChannelId,
                opt => opt.MapFrom(x => x.Id))
            .ForMember(dest => dest.CountOfArticles,
                opt => opt.MapFrom(x => x.Articles.Count));
    }
}