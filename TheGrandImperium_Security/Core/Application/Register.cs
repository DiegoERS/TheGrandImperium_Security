using Microsoft.AspNetCore.Identity;
using TheGrandImperium_Security.Core.DTO;
using TheGrandImperium_Security.Core.Entities;
using TheGrandImperium_Security.Core.jwtLogic;

namespace TheGrandImperium_Security.Core.Application
{
    public class Register
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IJWTGenerator _jWTGenerator;

        public Register(UserManager<Usuario> userManager, IJWTGenerator jWTGenerator)
        {
            _userManager = userManager;
            _jWTGenerator = jWTGenerator;
        }

        public async Task<UsuarioDTO> Ejecutar(UsuarioDTO request)
        {
            var usuario = new Usuario
            {
                UserName = request.UserName,
                Email = request.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, request.Password);
            if (resultado.Succeeded)
            {
                return new UsuarioDTO
                {
                    UserName = usuario.UserName,
                    Token = _jWTGenerator.CreateToken(usuario),
                    RefreshToken = _jWTGenerator.CreateRefreshToken()
                };
            }
            throw new Exception("Error al registrar usuario");
        }
    }
}
