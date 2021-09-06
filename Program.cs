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

            //Console.WriteLine("Preencher lista automaticamente? S/N");
            //string op = Console.ReadLine();
            //if (op.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    lst = ListaDeProdutosParaTeste();
            //}

            lst = ListaDeProdutosParaTeste();
            string resultado = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Novo Produto");
                Console.WriteLine("2. Alterar Produto");
                Console.WriteLine("3. Excluir Produto");
                Console.WriteLine("4. Mostrar Produtos");
                Console.WriteLine("5. Buscar Produto");
                Console.WriteLine("0. Sair");
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
                        Console.WriteLine("Id: " + item.Id);
                        Console.WriteLine("Nome: " + item.Nome);
                        Console.WriteLine("----------");
                    }
                    Console.WriteLine("Digite o Id do produto a ser editado:");
                    string tmpId = Console.ReadLine();
                    foreach (var item in lst)
                    {
                        if (item.Id.Equals(int.Parse(tmpId)))
                        {
                            item.Frase();
                            Console.WriteLine("1. Adicionar Quantidade");
                            Console.WriteLine("2. Remover Quantidade");
                            Console.WriteLine("3. Alterar Marca");
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
                            else if (resp == 3)
                            {
                                Console.WriteLine("Digite a nova marca: ");
                                string marcaNova = Console.ReadLine();
                                item.Marca = marcaNova;
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
                    Console.WriteLine("Digite o Id do produto a ser excluido:");
                    string tmpId = Console.ReadLine();
                    Product tmpprod = BuscaProdutoPorId(lst, tmpId);
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

                else if (resultado == "5")
                {
                    Console.WriteLine("Buscar Produto:");
                    Console.WriteLine("1. Por marca");
                    Console.WriteLine("2. Por nome");
                    Console.WriteLine("3. Por Id");
                    string respostaMenuDeBuscaProduto = Console.ReadLine();

                    switch (respostaMenuDeBuscaProduto)
                    {
                        case "1":

                            List<string> marcasDistintas = PegaMarcasDistintas(lst);

                            foreach (string item in marcasDistintas)
                            {
                                Console.WriteLine(item);
                            }

                            Console.WriteLine("Digite o nome da marca:");
                            string termoPesquisaPelaMarca = Console.ReadLine();

                            var produtosEncontradosPorMarca = BuscaProdutoPorMarca(lst, termoPesquisaPelaMarca);
                            if (produtosEncontradosPorMarca != null && produtosEncontradosPorMarca.Any())
                            {
                                Console.WriteLine();
                                Console.WriteLine("Produtos encontrados:");
                                MostrarProdutos(produtosEncontradosPorMarca);
                            }
                            else
                            {
                                MensagemErro($"Nenhum produto da marca {termoPesquisaPelaMarca} encontrado!");
                            }

                            break;

                        case "2":

                            foreach (var item in lst)
                            {
                                Console.WriteLine("Nome: " + item.Nome);
                                Console.WriteLine("----------");
                            }

                            Console.WriteLine("Digite o nome do produto:");
                            string termoPesquisaPorNome = Console.ReadLine();

                            var produtosEncontradosPorNome = BuscaProdutoPorNome(lst, termoPesquisaPorNome);
                            if (produtosEncontradosPorNome != null && produtosEncontradosPorNome.Any())
                            {
                                Console.WriteLine();
                                Console.WriteLine("Produtos encontrados:");
                                MostrarProdutos(produtosEncontradosPorNome);
                            }
                            else
                            {
                                MensagemErro($"Nenhum produto com o nome {termoPesquisaPorNome} encontrado!");
                            }

                            break;
                    }

                }

                Console.WriteLine("Operação realizada com sucesso!\nPressione qualquer tecla para continuar");
                Console.ReadKey();
            } while (resultado != "0");
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
        public static List<Product> BuscaProdutoPorNome(List<Product> products, string nome)
        {
            List<Product> lstReturn = new List<Product>();

            foreach (var item in products)
            {
                if (item.Nome.Contains(nome))
                {
                    lstReturn.Add(item);
                }
            }

            return lstReturn;
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

        public static List<Product> BuscaProdutoPorMarca(List<Product> products, string termo)
        {
            List<Product> lstReturn = new List<Product>();

            foreach (var item in products)
            {
                if (item.Marca.Contains(termo))
                {
                    lstReturn.Add(item);
                }
            }

            return lstReturn;
        }

        public static Product BuscaProdutoPorId(List<Product> products, string id)
        {
            foreach (var item in products)
            {
                if (item.Id.Equals(int.Parse(id)))
                {
                    return item;
                }
            }

            return null;
        }

        public static List<string> PegaMarcasDistintas(List<Product> products)
        {
            List<string> returnList = new List<string>();

            foreach (Product item in products)
            {
                if (!returnList.Contains(item.Marca))
                {
                    returnList.Add(item.Marca);
                }
            }

            return returnList;
        }

        public static List<Product> ListaDeProdutosParaTeste()
        {
            List<Product> lst = new List<Product>();
            lst.Add(new Product()
            {
                Id = 1,
                Nome = "Iphone 11",
                Valor = 7000,
                Quant = 10,
                Marca = "Apple"
            });

            lst.Add(new Product()
            {
                Id = 2,
                Nome = "Tv",
                Valor = 2000,
                Quant = 10,
                Marca = "Sony"
            });

            lst.Add(new Product()
            {
                Id = 3,
                Nome = "Papel Higiênico",
                Valor = 5,
                Quant = 109,
                Marca = "P&G"
            });

            lst.Add(new Product()
            {
                Id = 4,
                Nome = "Iphone 8",
                Valor = 1500,
                Quant = 50,
                Marca = "Apple"
            });

            return lst;
        }

        public static void MensagemErro(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}