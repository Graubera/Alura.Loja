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

            using (var context = new LojaContext())
            {
                var cliente = context
                    .Clientes
                    .Include(c => c.Endereco)
                    .FirstOrDefault();

                Debug.WriteLine($"Endereco de entrega : {cliente.Endereco.Logradouro}");

                var produto = context
                    .Produtos
                    .Include(p => p.Compras )
                    .Where(p => p.Id == 1)
                    .FirstOrDefault();

                context.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.Preco > 10)
                    .Load();

                Debug.WriteLine($"Mostrando as compras do produto : {produto.Nome}");
                foreach (Compra item in produto.Compras)
                {
                    Debug.WriteLine(item.Produto);
                }

            }
        }

        private static void ExibeProdutosDaPromocao()
        {
            using (var context = new LojaContext())
            {
                Promocao promocao = context
                    .Promocoes
                    .Include(p => p.Produtos)
                    .ThenInclude(pp => pp.Produto)
                    .FirstOrDefault();

                Debug.WriteLine("\nMostrando os produtos da promoção...");
                foreach (var item in promocao.Produtos)
                {
                    Debug.WriteLine(item.Produto);
                }
            }
        }

        private static void incluirPromocao()
        {
            using (var context = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima Total Janeiro 2017";
                promocao.DataInicio = new DateTime(2017, 1, 1);
                promocao.DataTermino = new DateTime(2017, 1, 31);

                var produtos = context
                    .Produtos
                    .Where(p => p.Categoria == "Padaria")
                    .ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }
                context.Promocoes.Add(promocao);
                context.SaveChanges();
            }
        }

        private static void one2many()
        {
            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Francês";
            paoFrances.PrecoUnitario = 0.40;
            paoFrances.Unidade = "Unidade";
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            using (var context = new LojaContext())
            {
                context.Compras.Add(compra);
                context.SaveChanges();
            }
        }

        private static void One2One()
        {
            var pessoa = new Cliente();
            pessoa.Nome = "Ritilino";
            pessoa.Endereco = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua",
                CEP = 1231231,
                Bairro = "cenrto",
                Cidade = "mga"
            };

            using (var context = new LojaContext())
            {
                context.Clientes.Add(pessoa);
                context.SaveChanges();
            }
        }

        private static void Many2Many()
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
