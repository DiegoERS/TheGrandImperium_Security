using Microsoft.AspNetCore.Identity;
using TheGrandImperium_Security.Core.DTO;
using TheGrandImperium_Security.Core.Entities;
using TheGrandImperium_Security.Core.jwtLogic;

namespace TheGrandImperium_Security.Core.Application
{
    public class Login
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJWTGenerator _jwtGenerator;

        public Login(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IJWTGenerator jwtGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UsuarioDTO> Ejecutar(UsuarioDTO request)
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            if (usuario == null) throw new Exception("Usuario no existe");

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
            if (resultado.Succeeded)
            {
                return new UsuarioDTO
                {
                    UserName = usuario.UserName,
                    Token = _jwtGenerator.CreateToken(usuario),
                    RefreshToken = _jwtGenerator.CreateRefreshToken()
                };
            }
            throw new Exception("Error al autenticar usuario");
        }
    }
}
