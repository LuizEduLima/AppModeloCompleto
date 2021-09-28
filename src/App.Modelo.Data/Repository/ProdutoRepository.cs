using App.Modelo.Business.Interfaces;

using AppBascioAspNet.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Modelo.Data.produtoRepository;

namespace App.Modelo.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ModeloDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Produto>> ObterProdutoFornecedor(Guid fornecedorId)
        {
            return await _context.Produtos.AsNoTracking().Include(p => p.Fornecedor)
                .Where(f => f.FornecedorId == fornecedorId).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _context.Produtos.AsNoTracking().Include(p => p.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task <IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
