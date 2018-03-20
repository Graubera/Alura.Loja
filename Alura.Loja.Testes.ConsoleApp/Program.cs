using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new LojaContext())
            {
                context.Database.Migrate();
            }
            IncluiPromocao();
            //promocaoDePascoa.Produtos.Add(new PromocaoProduto());
            //promocaoDePascoa.Produtos.Add(new PromocaoProduto());
            //promocaoDePascoa.Produtos.Add(new PromocaoProduto());

            //var paoFrances = new Produto();
            //paoFrances.Nome = "Pão Francês";
            //paoFrances.PrecoUnitario = 0.40;
            //paoFrances.Unidade = "Unidade";
            //paoFrances.Categoria = "Padaria";

            //var compra = new Compra();
            //compra.Quantidade = 6;
            //compra.Produto = paoFrances;
            //compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            //using (var context = new LojaContext())
            //{
            //    context.Compras.Add(compra);
            //    context.SaveChanges();
            //}
        }

        private static void IncluiPromocao()
        {
            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            Produto p1 = new Produto();
            Produto p2 = new Produto();
            Produto p3 = new Produto();

            p1.Nome = "Pão Francês1";
            p1.PrecoUnitario = 0.40;
            p1.Unidade = "Unidade";
            p1.Categoria = "Padaria";

            p2.Nome = "Pão Francês2";
            p2.PrecoUnitario = 0.40;
            p2.Unidade = "Unidade";
            p2.Categoria = "Padaria";

            p3.Nome = "Pão Francês3";
            p3.PrecoUnitario = 0.40;
            p3.Unidade = "Unidade";
            p3.Categoria = "Padaria";

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var context = new LojaContext())
            {
                context.Promocoes.Add(promocaoDePascoa);
                context.SaveChanges();
            }
        }

        private static void AtualizaProduto()
        {
            using (var context = new ProdutoDAOEntity())
            {
                Produto primeiro = context.Produtos().First();
                primeiro.Nome = "VVVVV";
                context.Atualizar(primeiro);                
            }
        }

        private static void ExcluirProdutos()
        {
            using (var context = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = context.Produtos();
                foreach (var item in produtos)
                {
                    context.Remover(item);
                }                
            }
        }

        private static void RecuperarProdutos()
        {
            using (var context = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = context.Produtos();
                Debug.WriteLine("Foram encontrado(s) {0} produtos", produtos.Count());
                foreach (var item in produtos)
                {
                    Debug.WriteLine(item.Nome);
                    //Console.WriteLine(item.Nome);
                }
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.PrecoUnitario = 19.89;

            using (var context = new ProdutoDAOEntity())
            {
                context.Adicionar(p);
                
            }
        }

        //private static void GravarUsandoAdoNet()
        //{
        //    Produto p = new Produto();
        //    p.Nome = "Harry Potter e a Ordem da Fênix";
        //    p.Categoria = "Livros";
        //    p.PrecoUnitario = 19.89;

        //    using (var repo = new ProdutoDAO())
        //    {
        //        repo.Adicionar(p);
        //    }
        //}
    }
}
