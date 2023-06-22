using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contas_Mensais.Data.Context;
using Contas_Mensais.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Contas_Mensais.Data.Repository
{
    public class PessoasRepository
    {
        private readonly DataContext _context;

        public PessoasRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoas>> GetAllAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoas> GetByIdAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                throw new Exception($"Não foi encontrada nenhuma pessoa com o id {id}");
            }

            return pessoa;
        }

        public async Task CreateAsync(Pessoas pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pessoas pessoa)
        {
            _context.Entry(pessoa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);

            if (pessoa == null)
            {
                throw new Exception($"Não foi encontrada nenhuma pessoa com o id {id}");
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}