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

        public ActionResult Listagem(int id)
        {
            var usuarios = _context.Usuarios.ToList();

            return View(usuarios);
        }   

        [HttpPost]
        public IActionResult Index(string nome, string senha)
        {
            var usuarios = _context.Usuarios.ToList();

            foreach(Usuario usuario in usuarios)
            {
                if(nome == usuario.Nome && senha == usuario.Senha)
                {
                    return RedirectToAction(nameof(Listagem));
                }                
            }
            return NotFound();
        }       
    }
}