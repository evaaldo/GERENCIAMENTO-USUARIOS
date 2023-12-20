using GerenciamentoUsuarios.Context.UsuarioContext;
using GerenciamentoUsuarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoUsuario.Controllers.UsuarioController
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioContext _context;

        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Autenticar()
        {
            return View();
        }

        //TODO: Melhorar sistema de autenticação de usuário
        [HttpPost]
        public IActionResult Autenticar(string nome, string senha)
        {
            var usuarios = _context.Usuarios.ToList();
            int count = 0;

            foreach(Usuario usuario in usuarios)
            {
                if(nome == usuario.Nome && senha == usuario.Senha)
                {
                    count++;
                }
            }

            if(count == 1)
            {
               return Ok(nome); 
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Usuario usuario)
        {
            if(ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }        
    }
}