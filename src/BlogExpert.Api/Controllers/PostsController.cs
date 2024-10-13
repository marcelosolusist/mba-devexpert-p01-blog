using AutoMapper;
using BlogExpert.Api.Models;
using BlogExpert.Dados.Repository;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogExpert.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        public PostsController(IMapper mapper, IPostService postService, IPostRepository postRepository, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _postService = postService;
            _postRepository = postRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<PostModel>>> ObterTodos()
        {
            var posts = _mapper.Map<IEnumerable<PostModel>>(await _postRepository.Listar());

            if (posts == null) return NotFound();

            return posts.ToList();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostModel>> ObterPorId(Guid id)
        {
            var postModel = await ObterPostModel(id);

            if (postModel == null) return NotFound();

            return postModel;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostModel>> Adicionar(PostModel postModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _postService.Adicionar(_mapper.Map<Post>(postModel));

            return CustomResponse(HttpStatusCode.Created, await ObterPostModel(postModel.Id));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostModel>> Atualizar(Guid id, PostModel postModel)
        {
            if (id != postModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _postService.Atualizar(_mapper.Map<Post>(postModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostModel>> Excluir(Guid id)
        {
            await _postService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<PostModel> ObterPostModel(Guid id)
        {
            return _mapper.Map<PostModel>(await _postRepository.ObterPorId(id));
        }
    }
}
