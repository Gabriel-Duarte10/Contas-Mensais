using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contas_Mensais.Data.Models
{
    public class Cartoes
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public int DiaDeCorte { get; set; }
        public Double LimiteDisponivel { get; set; }
        public Double LimiteTotal { get; set; }
    }
}