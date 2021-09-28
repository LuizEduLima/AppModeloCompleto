using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Modelo.Web.Data;
using App.Modelo.Web.ViewModels;
using App.Modelo.Business.Interfaces;
using AutoMapper;
using AppBascioAspNet.Models;

namespace App.Modelo.Web.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IMapper mapper,
                                      IProdutoRepository produtoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        // GET: Fornecedoes
        public async Task<IActionResult> Index()
        {
            return View(await ObterTodosFornecedoresViewModel());
        }

        // GET: Fornecedoes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEnderecoViewModel(id);
            if (fornecedorViewModel == null) return NotFound();

            return View(fornecedorViewModel);
        }

        // GET: Fornecedoes/Create
        public IActionResult Create()
        {           
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            await _fornecedorRepository.Adicionar(_mapper.Map<Fornecedor>(fornecedorViewModel));
            return RedirectToAction(nameof(Index));
        }

        // GET: Fornecedoes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedor = await ObterFornecedorEnderecoViewModel(id);

            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            var fornecedor = await ObterFornecedorEnderecoViewModel(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            await _fornecedorRepository.Atualizar(Mapper.Map<Fornecedor>(fornecedorViewModel));

            return RedirectToAction(nameof(Index));

           
        }

        // GET: Fornecedoes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEnderecoViewModel(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }          

            return View(fornecedorViewModel);
        }

        // POST: Fornecedoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEnderecoViewModel(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            await _fornecedorRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<FornecedorViewModel>> ObterTodosFornecedoresViewModel()
        {
            return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        }

        private async Task<FornecedorViewModel> ObterFornecedorEnderecoViewModel(Guid id)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));

            return fornecedor;
        }
        private async Task<FornecedorViewModel> ObterFornecedoresProdutosEnderecoViewModel(Guid FornecedorId)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(FornecedorId));
        }
    }
}
