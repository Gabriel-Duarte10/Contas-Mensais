using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contas_Mensais.Data.Context;
using Contas_Mensais.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Contas_Mensais.Data.Repository
{
    public class CartoesRepository
    {
        private readonly DataContext _context;

        public CartoesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cartoes>> GetAllAsync()
        {
            return await _context.Cartoes.ToListAsync();
        }

        public async Task<Cartoes> GetByIdAsync(int id)
        {
            var cartao = await _context.Cartoes.FindAsync(id);

            if (cartao == null)
            {
                throw new Exception($"Não foi encontrado nenhum cartão com o id {id}");
            }

            return cartao;
        }

        public async Task CreateAsync(Cartoes cartao)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Cartoes.Add(cartao);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(Cartoes cartao)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Entry(cartao).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var cartao = await _context.Cartoes.FindAsync(id);

            if (cartao == null)
            {
                throw new Exception($"Não foi encontrado nenhum cartão com o id {id}");
            }

            _context.Cartoes.Remove(cartao);
            await _context.SaveChangesAsync();
        }
    }
}