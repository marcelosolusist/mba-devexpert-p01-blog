using AutoMapper;
using BlogExpert.Mvc.ViewModels;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogExpert.Mvc.Controllers
{
    [Authorize]
    public class PostsController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IContaAutenticada _contaAutenticada;
        private readonly IComentarioService _comentarioService;
        private readonly IComentarioRepository _comentarioRepository;

        public PostsController(IMapper mapper,
                                      IPostService postService,
                                      INotificador notificador,
                                      IPostRepository postRepository,
                                      IContaAutenticada contaAutenticada,
                                      IComentarioService comentarioService,
                                      IComentarioRepository comentarioRepository) : base(notificador)
        {
            _mapper = mapper;
            _postService = postService;
            _postRepository = postRepository;
            _contaAutenticada = contaAutenticada;
            _comentarioService = comentarioService;
            _comentarioRepository = comentarioRepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewData["detalhar"] = "true";
            ViewData["editar"] = "true";
            ViewData["excluir"] = "true";
            ViewData["incluircomentario"] = "true";
            ViewData["editarcomentario"] = "true";
            ViewData["excluircomentario"] = "true";
            return View(_mapper.Map<IEnumerable<PostViewModel>>(await _postRepository.Listar()));
        }

        [AllowAnonymous]
        [Route("dados-do-post/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var postViewModel = await ObterPost(id);

            if (postViewModel == null)
            {
                return NotFound();
            }

            ViewData["editar"] = "true";
            ViewData["excluir"] = "true";
            ViewData["incluircomentario"] = "true";
            ViewData["editarcomentario"] = "true";
            ViewData["excluircomentario"] = "true";

            return View(postViewModel);
        }

        [Route("novo-post")]
        public async Task<IActionResult> Create()
        {
            ViewBag.AutoresIds = await ObterListaDeAutores(null);
            return View();
        }

        [Route("novo-post")]
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel, string autoresIds)
        {
            if (!string.IsNullOrEmpty(autoresIds)) postViewModel.AutorId = Guid.Parse(autoresIds);
            ViewBag.AutoresIds = await ObterListaDeAutores(null);
            if (!ModelState.IsValid) return View(postViewModel);

            var post = _mapper.Map<Post>(postViewModel);
            await _postService.Adicionar(post);

            if (!OperacaoValida()) return View(postViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-post/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var postViewModel = await ObterPostParaEdicao(id);

            ViewBag.AutoresIds = await ObterListaDeAutores(id.ToString());

            return View(postViewModel);
        }

        [Route("editar-post/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PostViewModel postViewModel, string autoresIds)
        {
            if (!string.IsNullOrEmpty(autoresIds)) postViewModel.AutorId = Guid.Parse(autoresIds);
            ViewBag.AutoresIds = await ObterListaDeAutores(id.ToString());

            if (id != postViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(postViewModel);

            var post = _mapper.Map<Post>(postViewModel);
            await _postService.Atualizar(post);

            if (!OperacaoValida()) return View(await ObterPost(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-post/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var postViewModel = await ObterPostParaEdicao(id);

            return View(postViewModel);
        }

        [Route("excluir-post/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var postViewModel = await ObterPost(id);

            if (postViewModel == null) return NotFound();

            await _postService.Remover(id);

            if (!OperacaoValida()) return View(postViewModel);

            return RedirectToAction("Index");
        }

        private async Task<PostViewModel> ObterPost(Guid id)
        {
            return _mapper.Map<PostViewModel>(await _postRepository.ObterPorId(id));
        }
        private async Task<PostViewModel> ObterPostParaEdicao(Guid id)
        {
            return _mapper.Map<PostViewModel>(await _postService.ObterParaEdicao(id));
        }

        private async Task<SelectList> ObterListaDeAutores(string? autorId)
        {
            var listaDeAutores = await _postService.ListarAutoresDaContaAutenticada();
            if (!string.IsNullOrEmpty(autorId)) return new SelectList(listaDeAutores, "Id", "Nome", autorId);
            return new SelectList(listaDeAutores, "Id", "Nome");
        }

        [Route("novo-comentario")]
        public async Task<IActionResult> CreateComentario(Guid id)
        {
            var comentarioViewModel = new ComentarioViewModel() { PostId = id };
            comentarioViewModel.Post = await ObterPost(id);
            return View(comentarioViewModel);
        }

        [Route("novo-comentario")]
        [HttpPost]
        public async Task<IActionResult> CreateComentario(ComentarioViewModel comentarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                comentarioViewModel.Post = await ObterPost(comentarioViewModel.PostId);
                return View(comentarioViewModel);
            }
            var comentario = _mapper.Map<Comentario>(comentarioViewModel);
            await _comentarioService.Adicionar(comentario);

            if (!OperacaoValida()) return View(comentarioViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-comentario/{id:guid}")]
        public async Task<IActionResult> EditComentario(Guid id)
        {
            var comentarioViewModel = await ObterComentarioParaEdicao(id);

            return View(comentarioViewModel);
        }

        [Route("editar-comentario/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> EditComentario(Guid id, ComentarioViewModel comentarioViewModel)
        {
            comentarioViewModel.Post = null;

            if (id != comentarioViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(comentarioViewModel);

            var comentario = _mapper.Map<Comentario>(comentarioViewModel);
            await _comentarioService.Atualizar(comentario);

            if (!OperacaoValida()) return View(await ObterComentario(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-comentario/{id:guid}")]
        public async Task<IActionResult> DeleteComentario(Guid id)
        {
            var comentarioViewModel = await ObterComentarioParaEdicao(id);

            return View(comentarioViewModel);
        }

        [Route("excluir-comentario/{id:guid}")]
        [HttpPost, ActionName("DeleteComentario")]
        public async Task<IActionResult> DeleteComentarioConfirmed(Guid id)
        {
            var comentarioViewModel = await ObterComentario(id);

            if (comentarioViewModel == null) return NotFound();

            await _comentarioService.Remover(id);

            if (!OperacaoValida()) return View(await ObterComentario(id));

            return RedirectToAction("Index");
        }

        private async Task<ComentarioViewModel> ObterComentario(Guid id)
        {
            var comentarioViewModel = _mapper.Map<ComentarioViewModel>(await _comentarioRepository.ObterPorId(id));
            return await PreencherPostDoComentarioViewModel(comentarioViewModel);
        }

        private async Task<ComentarioViewModel> ObterComentarioParaEdicao(Guid id)
        {
            var comentarioViewModel = _mapper.Map<ComentarioViewModel>(await _comentarioService.ObterParaEdicao(id));
            return await PreencherPostDoComentarioViewModel(comentarioViewModel);
        }

        private async Task<ComentarioViewModel> PreencherPostDoComentarioViewModel(ComentarioViewModel comentarioViewModel)
        {
            if (comentarioViewModel != null) comentarioViewModel.Post = await ObterPost(comentarioViewModel.PostId);
            return comentarioViewModel;
        }
    }
}
