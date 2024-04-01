using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/controller")]
[ApiController]

public class PostsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public PostsController(ApplicationDbContext context){
        _context = context;
    }

    [HttpPost]

    public async Task<ActionResult<Posts>> CreatePost(Posts post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPostById), new {Id = post.Id}, post);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Posts>> GetPostById(int Id)
    {
        var post = await _context.Posts.FindAsync(Id);

        if (post == null)
        {
            return NotFound();
        }

        return post;
    }
}