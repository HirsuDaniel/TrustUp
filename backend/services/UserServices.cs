using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class UserServices
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public UserServices(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<Users> RegisterUserAsync(Users user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> AuthenticateUserAsync(string email, string password){

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if(user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)){
            return null;
        }

        var token = GenerateJwtToken(user);

        return token;
    }

 private string GenerateJwtToken(Users user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };

        // Generate a secure key with at least 256 bits (32 bytes) length for HS256 algorithm
        var key = new byte[32]; // 32 bytes = 256 bits
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(key);
        }

        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}