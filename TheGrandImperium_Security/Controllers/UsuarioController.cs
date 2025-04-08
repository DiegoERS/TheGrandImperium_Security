using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheGrandImperium_Security.Core.Application;
using TheGrandImperium_Security.Core.DTO;

namespace TheGrandImperium_Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Register _register;
        private readonly Login _login;
        private readonly UsuarioActual _usuarioActual;

        public UsuarioController(Register register, Login login, UsuarioActual usuarioActual)
        {
            _register = register;
            _login = login;
            _usuarioActual = usuarioActual;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDTO>> Registrar(UsuarioDTO usuario)
        {
            return await _register.Ejecutar(usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDTO>> Login(UsuarioDTO usuario)
        {
            try
            {
                return await _login.Ejecutar(usuario);
            }
            catch 
            {
                return NoContent(); // Error lógico: usuario o clave incorrecta
            }
        }

        [HttpPost("refrescarToken")]
        public async Task<ActionResult<UsuarioDTO>> RefrescarToken(string refreshToken)
        {
            return await _usuarioActual.Ejecutar(refreshToken);
        }
    }
}
