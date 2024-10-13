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
    [Route("api/comentarios")]
    public class ComentariosController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IComentarioService _comentarioService;
        private readonly IComentarioRepository _comentarioRepository;
        public ComentariosController(IMapper mapper, IComentarioService comentarioService, IComentarioRepository comentarioRepository, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _comentarioService = comentarioService;
            _comentarioRepository = comentarioRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<ComentarioModel>>> ObterTodos()
        {
            var comentarios = _mapper.Map<IEnumerable<ComentarioModel>>(await _comentarioRepository.Listar());

            if (comentarios == null) return NotFound();

            return comentarios.ToList();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ComentarioModel>> ObterPorId(Guid id)
        {
            var comentario = await ObterComentarioModel(id);

            if (comentario == null) return NotFound();

            return comentario;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ComentarioModel>> Adicionar(ComentarioModel comentarioModel)
        {

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var comentario = _mapper.Map<Comentario>(comentarioModel);

            await _comentarioService.Adicionar(comentario);

            return CustomResponse(HttpStatusCode.Created, await ObterComentarioModel(comentarioModel.Id));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ComentarioModel>> Atualizar(Guid id, ComentarioModel comentarioModel)
        {
            if (id != comentarioModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _comentarioService.Atualizar(_mapper.Map<Comentario>(comentarioModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ComentarioModel>> Excluir(Guid id)
        {
            await _comentarioService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<ComentarioModel> ObterComentarioModel(Guid id)
        {
            return _mapper.Map<ComentarioModel>(await _comentarioRepository.ObterPorId(id));
        }
    }
}
