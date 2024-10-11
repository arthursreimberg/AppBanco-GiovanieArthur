using appQuintoGiovaniArthur.Models;
using appQuintoGiovaniArthur.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace appQuintoGiovaniArthur.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController (IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult CadastrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.Cadastrar(usuario);
            }
            return View();
        }
     
        public IActionResult Index()
        {
            return View(_usuarioRepository.ObterTodosUsuarios());
        }

        [HttpGet]

        public IActionResult AtualizarUsuario(int id)
        {
            return View(_usuarioRepository.ObterUsuario(id));
        }

        [HttpPost]

        public IActionResult AtualizarUsuario(Usuario usuario)
        {
            _usuarioRepository.Atualizar(usuario);

            return RedirectToAction(nameof(Index));

        }

        public IActionResult ExcluirUsario(int id)
        {
            _usuarioRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult DetalhesUsuario(int id)
        {
            return View(_usuarioRepository.ObterUsuario(id));
        }

        [HttpPost]

        public IActionResult DetalhesUsuario(Usuario usuario)
        {
            //_usuarioRepository.ObterUsuario(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
