namespace TheGrandImperium_Security.Core.DTO
{
    public class UsuarioDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
