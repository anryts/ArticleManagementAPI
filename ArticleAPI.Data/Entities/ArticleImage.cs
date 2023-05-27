namespace ArticleAPI.Data.Entities;

public class ArticleImage
{
    public Guid Id { get; set; } 
    public Guid ArticleId { get; set; }
    public string ImagePath { get; set; } = null!;
    
    public Article? Article { get; set; }
}