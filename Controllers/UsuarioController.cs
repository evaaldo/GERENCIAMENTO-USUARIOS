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

        [HttpPost]
        public ActionResult Editar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return View(id);
        }

        public ActionResult Editar(Usuario usuario)
        {
            var usuarioBanco = _context.Usuarios.Find(usuario.ID);

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Senha = usuario.Senha;
            usuarioBanco.Admin = usuario.Admin;

            _context.Usuarios.Update(usuarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Listagem));
        }

        public ActionResult Excluir(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(usuario));
        }

        [HttpPost]
        public ActionResult Excluir(Usuario usuario)
        {
            var usuarioBanco = _context.Usuarios.Find(usuario.ID);

            _context.Usuarios.Remove(usuarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Listagem));
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