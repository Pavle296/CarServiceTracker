public interface IAuthService  
{
    Task<string> Register(RegisterDTO dtO);  
    Task<string> Login(LoginDTO dto);     
}