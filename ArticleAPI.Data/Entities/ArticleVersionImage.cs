namespace ArticleAPI.Data.Entities;

public class ArticleVersionImage
{
    public Guid Id { get; set; }
    public Guid ArticleVersionId { get; set; }
    public string ImagePath { get; set; } = null!;
    
    public ArticleVersion? ArticleVersion { get; set; }   
}