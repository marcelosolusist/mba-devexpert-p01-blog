using AutoMapper;
using BlogExpert.Mvc.ViewModels;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Notificacoes;
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

        public PostsController(IMapper mapper,
                                      IPostService postService,
                                      INotificador notificador,
                                      IPostRepository postRepository,
                                      IContaAutenticada contaAutenticada) : base(notificador)
        {
            _mapper = mapper;
            _postService = postService;
            _postRepository = postRepository;
            _contaAutenticada = contaAutenticada;
        }

        [AllowAnonymous]
        //[Route("lista-de-posts")]
        public async Task<IActionResult> Index()
        {
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

            return View(postViewModel);
        }

        [Route("novo-post")]
        public async Task<IActionResult> Create()
        {
            ViewBag.AutoresIds = await ObterListaDeAutores();
            return View();
        }

        [Route("novo-post")]
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel, string autoresIds)
        {
            if (!string.IsNullOrEmpty(autoresIds)) postViewModel.AutorId = Guid.Parse(autoresIds);
            ViewBag.AutoresIds = await ObterListaDeAutores();
            if (!ModelState.IsValid) return View(postViewModel);

            var post = _mapper.Map<Post>(postViewModel);
            await _postService.Adicionar(post);

            if (!OperacaoValida()) return View(postViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-post/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var postViewModel = await ObterPost(id);

            if (postViewModel == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }

        [Route("editar-post/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PostViewModel postViewModel)
        {
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
            var postViewModel = await ObterPost(id);

            if (postViewModel == null)
            {
                return NotFound();
            }

            return View(postViewModel);
        }

        [Route("excluir-post/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await ObterPost(id);

            if (post == null) return NotFound();

            await _postService.Remover(id);

            if (!OperacaoValida()) return View(post);

            return RedirectToAction("Index");
        }

        private async Task<PostViewModel> ObterPost(Guid id)
        {
            return _mapper.Map<PostViewModel>(await _postRepository.ObterPorId(id));
        }

        private async Task<SelectList> ObterListaDeAutores()
        {
            return new SelectList(await _postService.ListarAutoresDaContaAutenticada(), "Id", "Nome");
        }
    }
}
