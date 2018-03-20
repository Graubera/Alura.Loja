using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class PromocaoProduto
    {
        public int PromocaoId { get; set; }
        //[Required]
        public Promocao Promocao { get; set; }
        //[Required]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
