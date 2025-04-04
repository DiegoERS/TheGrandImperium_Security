using Microsoft.AspNetCore.Identity;
using TheGrandImperium_Security.Core.DTO;
using TheGrandImperium_Security.Core.Entities;
using TheGrandImperium_Security.Core.jwtLogic;

namespace TheGrandImperium_Security.Core.Application
{
    public class UsuarioActual
    {
        private readonly IJWTGenerator _jwtGenerator;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioActual(IJWTGenerator jwtGenerator, UserManager<Usuario> userManager)
        {
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
        }

        public async Task<UsuarioDTO> Ejecutar(string refreshToken)
        {
            // Lógica para validar el Refresh Token
            var usuario = await _userManager.FindByIdAsync("USUARIO_ID");

            if (usuario == null) throw new Exception("Usuario no encontrado");

            return new UsuarioDTO
            {
                UserName = usuario.UserName,
                Token = _jwtGenerator.CreateToken(usuario)
            };
        }
    }
}
