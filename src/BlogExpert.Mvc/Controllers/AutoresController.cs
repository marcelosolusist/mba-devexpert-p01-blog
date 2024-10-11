﻿using AutoMapper;
using BlogExpert.Mvc.ViewModels;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogExpert.Mvc.Controllers
{
    public class AutoresController : BaseController
    {
        private readonly IAutorService _autorService;
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public AutoresController(IMapper mapper,
                                      IAutorService autorService,
                                      INotificador notificador,
                                      IAutorRepository autorRepository,
                                      IUser user) : base(notificador)
        {
            _mapper = mapper;
            _autorService = autorService;
            _autorRepository = autorRepository;
            _user = user;
        }

        [AllowAnonymous]
        [Route("lista-de-autores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<AutorViewModel>>(await _autorRepository.Listar()));
        }

        [AllowAnonymous]
        [Route("dados-do-autor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var autorViewModel = await ObterAutor(id);

            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        [Route("novo-autor")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-autor")]
        [HttpPost]
        public async Task<IActionResult> Create(AutorViewModel autorViewModel)
        {
            if (!ModelState.IsValid) return View(autorViewModel);

            var autor = _mapper.Map<Autor>(autorViewModel);
            await _autorService.Adicionar(autor, _user.ObterContaAutenticada());

            if (!OperacaoValida()) return View(autorViewModel);

            return RedirectToAction("Index");
        }

        [Route("editar-autor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var autorViewModel = await ObterAutor(id);

            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        [Route("editar-autor/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AutorViewModel autorViewModel)
        {
            if (id != autorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(autorViewModel);

            var autor = _mapper.Map<Autor>(autorViewModel);
            await _autorService.Atualizar(autor, _user.ObterContaAutenticada());

            if (!OperacaoValida()) return View(await ObterAutor(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-autor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var autorViewModel = await ObterAutor(id);

            if (autorViewModel == null)
            {
                return NotFound();
            }

            return View(autorViewModel);
        }

        [Route("excluir-autor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var autor = await ObterAutor(id);

            if (autor == null) return NotFound();

            await _autorService.Remover(id, _user.ObterContaAutenticada());

            if (!OperacaoValida()) return View(autor);

            return RedirectToAction("Index");
        }

        private async Task<AutorViewModel> ObterAutor(Guid id)
        {
            return _mapper.Map<AutorViewModel>(await _autorRepository.ObterPorId(id));
        }
    }
}
