using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using App.Modelo.Web.ViewModels;

namespace App.Modelo.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<App.Modelo.Web.ViewModels.FornecedorViewModel> FornecedorViewModel { get; set; }
        public DbSet<App.Modelo.Web.ViewModels.ProdutoViewModel> ProdutoViewModel { get; set; }
        public DbSet<App.Modelo.Web.ViewModels.EnderecoViewModel> EnderecoViewModel { get; set; }
    }
}
