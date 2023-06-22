using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contas_Mensais.Data.Context;
using Contas_Mensais.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Contas_Mensais.Data.Repository
{
    public class ComprasRepository
    {
        private readonly DataContext _context;

        public ComprasRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ComprasDto>> GetAllAsync()
        {
            return await _context.Compras
                .Select(c => new ComprasDto
                {
                    Id = c.Id,
                    Pessoas = c.Pessoas,
                    Cartao = c.Cartao,
                    Descricao = c.Descricao,
                    Valor = c.Valor,
                    DataCompra = c.DataCompra,
                    Parcela = c.Parcela,
                    TotalParcelas = c.TotalParcelas
                })
                .ToListAsync();
        }

        public async Task<ComprasDto> GetByIdAsync(int id)
        {
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                throw new Exception($"Não foi encontrada nenhuma compra com o id {id}");
            }

            return new ComprasDto
            {
                Id = compra.Id,
                Pessoas = compra.Pessoas,
                Cartao = compra.Cartao,
                Descricao = compra.Descricao,
                Valor = compra.Valor,
                DataCompra = compra.DataCompra,
                Parcela = compra.Parcela,
                TotalParcelas = compra.TotalParcelas
            };
        }

        public async Task CreateAsync(ComprasRequest request)
        {
            List<Compras> compras = new List<Compras>();
            if(request.TotalParcelas > 0)
            {
                for (int i = 0; i < request.TotalParcelas; i++)
                {
                    var compra = new Compras
                    {
                        Id = request.Id,
                        PessoasId = request.PessoasId,
                        CartaoId = request.CartaoId,
                        Descricao = request.Descricao,
                        Valor = request.Valor/request.TotalParcelas,
                        DataCompra = request.DataCompra.AddMonths(i),
                        Parcela = i+1,
                        TotalParcelas = request.TotalParcelas
                    };
                    compras.Add(compra);
                }
            }else
            {
                var compra = new Compras
                {
                    Id = request.Id,
                    PessoasId = request.PessoasId,
                    CartaoId = request.CartaoId,
                    Descricao = request.Descricao,
                    Valor = request.Valor/request.TotalParcelas,
                    DataCompra = request.DataCompra,
                    Parcela = 1,
                    TotalParcelas = 1
                };
                compras.Add(compra);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var compra in compras)
                {
                    _context.Compras.Add(compra);
                    var cartao = await _context.Cartoes.FirstOrDefaultAsync(x => x.Id == request.CartaoId);
                    cartao.LimiteDisponivel -= request.Valor/request.TotalParcelas;
                    await _context.SaveChangesAsync();
                }

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
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                throw new Exception($"Não foi encontrada nenhuma compra com o id {id}");
            }

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
        }
        public async Task LiberarLimiteCartaoAsync(int cartaoId, double valor)
        {
            var cartao = await _context.Cartoes.FindAsync(cartaoId);

            if (cartao == null)
            {
                throw new Exception($"Não foi encontrado nenhum cartão com o id {cartaoId}");
            }

            if(valor > cartao.LimiteTotal)
            {
                throw new Exception("O valor a ser liberado excede o limite do cartão");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                cartao.LimiteDisponivel += valor;
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

    }
}

