

using Microsoft.EntityFrameworkCore;

public class Users
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public ICollection<Posts>? Posts { get ;set; }

    public ICollection<Comments>? Comments { get; set;}

}