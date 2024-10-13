using AutoMapper;
using BlogExpert.Api.Models;
using BlogExpert.Dados.Repository;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogExpert.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        public PostsController(IMapper mapper, IPostService postService, IPostRepository postRepository)
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
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPosts()
        {
            var posts = _mapper.Map<IEnumerable<PostModel>>(await _postRepository.Listar());

            if (posts == null)
            {
                return NotFound();
            }
            return posts.ToList();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostModel>> GetPost(Guid id)
        {
            var post = _mapper.Map<PostModel>(await _postRepository.ObterPorId(id));

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }
    }
}
