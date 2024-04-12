
public class Posts
{
    public int? Id { get; set; }
    public string? Content { get; set; }
    public string? PostImage { get; set; }

    public DateTime? CreatedAt { get; set; }
    public int? UserId { get; set; } // Foreign key
    public Users? User { get; set; }   // Navigation property
    public ICollection<Comments>? Comments { get; set; }
}