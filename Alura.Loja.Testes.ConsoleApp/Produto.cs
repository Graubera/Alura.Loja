using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Alura.Loja.Testes.ConsoleApp
{
    public class Produto
    {
        [Required]
        public int Id { get; internal set; }
        [Required]
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; internal set; }
        public IList<PromocaoProduto> Promocoes { get; set; }
        public IList<Compra> Compras { get; set; }


        public override string ToString()
        {
            return $"Produto: {this.Id}, {this.Nome}, {this.PrecoUnitario}, {this.Categoria}";
        }
    }
}