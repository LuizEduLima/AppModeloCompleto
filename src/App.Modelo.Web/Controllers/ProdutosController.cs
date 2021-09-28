using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Modelo.Web.Data;
using App.Modelo.Web.ViewModels;
using App.Modelo.Data.produtoRepository;
using App.Modelo.Business.Interfaces;
using AutoMapper;
using AppBascioAspNet.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace App.Modelo.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;



        public ProdutosController(IProdutoRepository produtoRepository,
                                  IFornecedorRepository fornecedorRepository,
                                  IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores()));
        }


        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModel = ObterProdutoViewModel(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        public async Task<IActionResult> Create()
        {
            var ProdutoViewModel = await PolularFornecedores(new ProdutoViewModel());

            return View(ProdutoViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PolularFornecedores(produtoViewModel);

            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await ArquivoUpload(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));           
           
            return View(produtoViewModel);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {

            var produtoViewModel =  _mapper.Map< ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
            
            if (produtoViewModel == null){return NotFound();}

            return RedirectToAction(nameof(Index));
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(produtoViewModel);

           

            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoViewModel));                                 
   
            return View(produtoViewModel);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {

            var produtoViewModel = await ObterProdutoViewModel(id);
                
            if (produtoViewModel == null)
            {
                return NotFound();
            }          

            return View(produtoViewModel);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = ObterProdutoViewModel(id);
            if (produto==null)
            {
                return NotFound();
            }

            await _produtoRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProdutoViewModel(Guid id)
        {

            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }
        private async Task<ProdutoViewModel>PolularFornecedores(ProdutoViewModel produto)
        {
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<bool>ArquivoUpload(IFormFile arquivo, string imgPrefixo)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);
            if (arquivo.Length <= 0) return false;

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Uma imagem já existe com esse nome");
                return false;
            }

            using(var st = new FileStream(path,FileMode.Create))
            {
                await arquivo.CopyToAsync(st);
            }
            return true;
        }

        
    }
}
