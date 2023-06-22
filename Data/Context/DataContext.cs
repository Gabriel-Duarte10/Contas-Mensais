using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contas_Mensais.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Contas_Mensais.Data.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Cartoes> Cartoes { get; set; }
        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Compras> Compras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compras>()
                .HasOne(c => c.Pessoas)
                .WithMany()
                .HasForeignKey(c => c.PessoasId);

            modelBuilder.Entity<Compras>()
                .HasOne(c => c.Cartao)
                .WithMany()
                .HasForeignKey(c => c.CartaoId);
        }
    }
}



