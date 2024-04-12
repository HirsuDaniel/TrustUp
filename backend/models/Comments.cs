public class Comments
{
    public int? Id { get; set; }
    public string? Content { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? UserId { get; set; } // Foreign key
    public Users? User { get; set; }  // Navigation property
    public int? PostId { get; set; } // Foreign key
    public Posts? Post { get; set; }  // Navigation property
}