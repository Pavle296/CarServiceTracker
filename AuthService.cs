using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly Context _context;
    private readonly IConfiguration _configuration;

    public AuthService(Context context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> Register(RegisterDTO dto)
    {
        
        var existingUser = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Username == dto.Username);

        if (existingUser != null)
            throw new Exception("Username already exists!");
        

        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        
        var newAccount = new Account
        {
            Username = dto.Username,
            PasswordHash = passwordHash
        };

        
        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        
        string token = GenerateJWT(newAccount);
        return token;
    }

    public async Task<string> Login(LoginDTO dto)
    {
        
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Username == dto.Username);

        if (account == null)
            throw new Exception("Incorrect username or password!");


        
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, account.PasswordHash);

        if (!isPasswordValid)
            throw new Exception("Incorrect username or password!");


        
        string token = GenerateJWT(account);
        return token;
    }
    private string GenerateJWT(Account account)
    {
        
        var key = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, account.Username),
        new Claim("userId", account.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),  
            signingCredentials: credentials
        );

        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}