using GerenciamentoUsuarios.Context.UsuarioContext;
using GerenciamentoUsuarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var usuarios = _context.Usuarios.ToList();

            if(ModelState.IsValid)
            {

                foreach(Usuario usuarioModel in usuarios)
                {
                    if(usuario.Nome == usuarioModel.Nome)
                    {
                        TempData["MensagemCadastro"] = "Um usuario com esse nome ja esta cadastrado! Cadastre um novo.";
                        return RedirectToAction(nameof(Cadastrar));                        
                    }

                    if(usuario.Nome == null || usuario.Senha == null)
                    {
                        TempData["MensagemCadastro"] = "Preencha os campos para cadastrar.";
                        return RedirectToAction(nameof(Cadastrar));
                    }
                }

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public ActionResult Editar(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if(usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            var usuarioBanco = _context.Usuarios.Find(usuario.ID);

            if(usuario.Senha == null || usuario.Nome == null)
            {
                TempData["MensagemEditar"] = "Precisa preencher os campos de Nome e Senha";
                return RedirectToAction(nameof(Editar));
            }

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

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Excluir(Usuario usuario)
        {
            var usuarioBanco = _context.Usuarios.Find(usuario.ID);

            _context.Usuarios.Remove(usuarioBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Listagem));
        }

        public ActionResult Listagem()
        {
            var usuarios = _context.Usuarios.ToList();

            return View(usuarios);
        }   

        public ActionResult ListagemNaoAdmin()
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
                if(nome == usuario.Nome && senha == usuario.Senha && usuario.Admin == true)
                {
                    return RedirectToAction(nameof(Listagem));
                }  

                if(nome == usuario.Nome && senha == usuario.Senha && usuario.Admin == false)
                {
                    return RedirectToAction(nameof(ListagemNaoAdmin));
                }                    
            }

            TempData["Mensagem"] = "Esse usuario/senha nao existe! Cadastre-o para prosseguir.";
            return RedirectToAction(nameof(Index));
        }       
    }
}