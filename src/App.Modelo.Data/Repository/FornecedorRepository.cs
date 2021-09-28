using App.Modelo.Business.Interfaces;
using App.Modelo.Data.produtoRepository;
using AppBascioAspNet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Modelo.Data.Repository
{
   public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(ModeloDbContext context) : base(context)
        {
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await _context.Fornecedores.Include(e => e.Endereco).FirstOrDefaultAsync(f => f.Id==id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await _context.Fornecedores.AsNoTracking()
                .Include(e => e.Endereco)
                .Include(p=>p.Produtos)
               .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
