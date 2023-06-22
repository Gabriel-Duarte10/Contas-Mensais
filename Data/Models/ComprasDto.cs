using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contas_Mensais.Data.Models
{
    public class ComprasDto
    {
        public int Id { get; set; }
        public virtual Pessoas Pessoas { get; set; }
        public virtual Cartoes Cartao { get; set; }
        public String Descricao { get; set; }
        public Double Valor { get; set; }
        public DateTime DataCompra { get; set; }
        public int? Parcela { get; set; }
        public int? TotalParcelas { get; set; }
    }
}