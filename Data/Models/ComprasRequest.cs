using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contas_Mensais.Data.Models
{
    public class ComprasRequest
    {
        public int Id { get; set; }
        public int PessoasId { get; set; }
        public int CartaoId { get; set; }
        public String Descricao { get; set; }
        public Double Valor { get; set; }
        public DateTime DataCompra { get; set; }
        public int TotalParcelas { get; set; }
    }
}