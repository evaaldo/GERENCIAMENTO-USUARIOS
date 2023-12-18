using GerenciamentoUsuarios.Context.UsuarioContext;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoUsuarios.Controllers.UsuarioController
{
    [Route("Controller")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }
    }
}