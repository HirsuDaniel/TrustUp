using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]

public class PostsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public PostsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment){
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]

    public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Posts>> CreatePosts([FromForm] string content, [FromForm] IFormFile postImage)
    {
        if (postImage == null || postImage.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            // Read the uploaded file into a byte array
            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                await postImage.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            // Convert the byte array to a Base64-encoded string
            string base64String = Convert.ToBase64String(imageBytes);

            // Save the Base64-encoded string to the database
            var post = new Posts
            {
                Content = content,
                PostImage = base64String // Store the image data as a string
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPosts), new { id = post.Id }, post);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

}