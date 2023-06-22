using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contas_Mensais.Data.Models
{
    public class Compras
    {
        public int Id { get; set; }
        public int PessoasId { get; set; }
        public virtual Pessoas Pessoas { get; set; }
        public int CartaoId { get; set; }
        public virtual Cartoes Cartao { get; set; }
        public String Descricao { get; set; }
        public Double Valor { get; set; }
        public DateTime DataCompra { get; set; }
        public int? Parcela { get; set; }
        public int? TotalParcelas { get; set; }
    }
}