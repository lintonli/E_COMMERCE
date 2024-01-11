namespace BlazorProds.Model
{
    public class LogInUser
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
      
    }
    //handles linking
    public class LoginResponseDto
    {
        public string Token { get; set; }= string.Empty;
        public LogInUser user { get; set; } 
    }
}
