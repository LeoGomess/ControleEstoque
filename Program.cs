using ControleEstoque.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ControleEstoque
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> lst = new List<Product>();
            string resultado = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("Selecione uma opção:\n1. Novo Produto\n2. Alterar Produto\n3. Excluir Produto\n4. Mostrar Produtos\n5. Sair");
                resultado = Console.ReadLine();

                if (resultado == "1")
                {
                    Product a = new Product();

                    Console.Write("Nome: ");
                    a.Nome = Console.ReadLine();
                    Console.Write("Preço: ");
                    a.Valor = double.Parse(Console.ReadLine());
                    Console.Write("Quantidade: ");
                    a.Quant = int.Parse(Console.ReadLine());
                    Console.Write("Marca: ");
                    a.Marca = Console.ReadLine();

                    a.Id = GeraIdProduto(lst);


                    lst.Add(a);
                    a.Frase();
                }
                else if (resultado == "2")
                {
                    foreach (var item in lst)
                    {
                        Console.WriteLine("Nome: " + item.Nome);
                        Console.WriteLine("----------");
                    }
                    Console.WriteLine("Digite o nome do produto a ser editado:");
                    string tmpNome = Console.ReadLine();
                    foreach (var item in lst)
                    {
                        if (item.Nome.Equals(tmpNome))
                        {
                            item.Frase();
                            Console.WriteLine("1. Adicionar Quantidade");
                            Console.WriteLine("2. Remover Quantidade");
                            int resp = int.Parse(Console.ReadLine());
                            if (resp == 1)
                            {
                                Console.WriteLine("Digite a quantidade: ");
                                int qtd = int.Parse(Console.ReadLine());
                                item.AddQuant(qtd);
                            }
                            else if (resp == 2)
                            {
                                Console.WriteLine("Digite a quantidade: ");
                                int qtd = int.Parse(Console.ReadLine());
                                item.RemoveQuant(qtd);
                            }
                            else
                            {
                                Console.WriteLine("JADER");
                            }
                            item.Frase();
                        }
                    }
                }
                else if (resultado == "3")
                {
                    MostrarProdutos(lst);
                    Console.WriteLine("Digite o nome do produto a ser excluido:");
                    string tmpNome = Console.ReadLine();
                    Product tmpprod = BuscaProduto(tmpNome, lst);
                    if (tmpprod != null)
                    {
                        lst.Remove(tmpprod);
                    }
                    Console.WriteLine();
                    MostrarProdutos(lst);
                }
                else if (resultado == "4")
                {
                    MostrarProdutos(lst);
                }
                Console.WriteLine("Operação realizada com sucesso!\nPressione qualquer tecla para continuar");
                Console.ReadKey();
            } while (resultado != "5");
        }
        /// <summary>
        /// Mostrar o total de Produtos
        /// </summary>
        /// <param name="lista">Lista de New Product</param>
        public static void MostrarProdutos(List<Product> lista)
        {
            foreach (var item in lista)
            {
                item.Frase();
            }
        }
        public static Product BuscaProduto(string nome, List<Product> lista)
        {
            foreach (var item in lista)
            {
                if (item.Nome.Equals(nome))
                {
                    return item;
                }

            }
            return null;
        }
        public static int GeraIdProduto(List<Product> lst)
        {
            List<int> lstInt = new List<int>();
            int maior = 1;

            foreach (var item in lst)
            {
                lstInt.Add(item.Id);
            }

            maior = PegaMaiorNumero(lstInt);

            return maior + 1;
        }
        public static int PegaMaiorNumero(List<int> lst)
        {
            int maior = 0;
            foreach (int item in lst)
            {
                if (item > maior)
                {
                    maior = item;
                }
            }
            return maior;
        }
    }
}