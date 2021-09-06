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
            List<Category> lstCategory = new List<Category>();
            List<CategoryProduct> lstCatProd = new List<CategoryProduct>();

            lst = ListaDeProdutosParaTeste();
            lstCategory = ListaDeCategoriasParaTeste();
            lstCatProd = ListaDeCategoriasVsProdutoParaTeste(lstCategory, lst);

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
                    Category cat = new Category();
                    a.Id = GeraIdProduto(lst);

                    Console.Write("Nome: ");
                    a.Nome = Console.ReadLine();

                    Console.Write("Preço: ");
                    a.Valor = double.Parse(Console.ReadLine());

                    Console.Write("Quantidade: ");
                    a.Quant = int.Parse(Console.ReadLine());

                    cat.Id = GeraIdCategoria(lstCategory);

                    Console.Write("Categoria: ");
                    cat.Description = Console.ReadLine();

                    Console.Write("Marca: ");
                    cat.Brand = Console.ReadLine();

                    lst.Add(a);

                    lstCategory.Add(cat);

                    #region Cadastra Produto x Categoria
                    CategoryProduct catProd = new CategoryProduct();
                    catProd.Id = GeraIdCategoriaProduto(lstCatProd);
                    catProd.ProductId = a.Id;
                    catProd.CategoryId = cat.Id;
                    lstCatProd.Add(catProd);
                    #endregion

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
                    Console.WriteLine("Digite o id do produto a ser editado:");
                    string tmpEditId = Console.ReadLine();
                    foreach (var item in lst)
                    {
                        if (item.Id.Equals(int.Parse(tmpEditId)))
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
                    Console.WriteLine("Digite o Id do produto a ser excluido:");
                    string tmpProdutoId = Console.ReadLine();
                    Product tmpprod = BuscaProdutoPorId(tmpProdutoId, lst);
                    if(tmpprod != null)
                    {
                        lst.Remove(tmpprod);
                    }
                    Console.WriteLine();
                    MostrarProdutos(lst);
                }
                else if (resultado == "4")
                {
                    Console.WriteLine("Produtos:");
                    MostrarProdutos(lst);
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Categorias:");
                    MostrarCategorias(lstCategory);
                }
                else if(resultado == "5")
                {
                    Console.WriteLine("1. Buscar por nome");
                    Console.WriteLine("2. Buscar por categoria");
                    Console.WriteLine("0. Cancelar");
                    string opcaoSubMenuCategoria = Console.ReadLine();

                    switch(opcaoSubMenuCategoria)
                    {
                        //nome
                        case "1":

                            MostrarProdutos(lst, true);
                            Console.WriteLine("Digite o nome do produto");
                            string nomeProcurado = Console.ReadLine();
                            var tmpProdutosEncontrados = BuscaProduto(nomeProcurado, lst);
                            if(tmpProdutosEncontrados != null && tmpProdutosEncontrados.Any())
                            {
                                MostrarProdutos(tmpProdutosEncontrados);
                            }
                            else
                            {
                                Console.WriteLine($"O produto {nomeProcurado} não está cadastrado!");
                            }

                            break;

                        case "2":

                            MostrarCategorias(lstCategory, true);
                            Console.WriteLine("Digite o nome da categoria");
                            string nomeCategoria = Console.ReadLine();

                            var categoriasEncontradas = BuscaCategoria(nomeCategoria, lstCategory);

                            if(categoriasEncontradas != null && categoriasEncontradas.Any())
                            {
                                Console.WriteLine("Categorias encontradas");
                                MostrarCategorias(categoriasEncontradas);
                                List<CategoryProduct> categoriasVsProdutoEncontradas = new List<CategoryProduct>();

                                foreach (var itemCateg in categoriasEncontradas)
                                {
                                    var categoriasVsProdutoEncontrada = BuscaCategoriaVsProduto(itemCateg.Id, lstCatProd);

                                    if (categoriasVsProdutoEncontradas != null && categoriasVsProdutoEncontradas.Any())
                                    {
                                        categoriasVsProdutoEncontradas.Add(categoriasVsProdutoEncontrada.First());
                                    }
                                }

                                if(categoriasVsProdutoEncontradas != null && categoriasVsProdutoEncontradas.Any())
                                {
                                    List<Product> listaDeProdutosEncontradosPorCategoria = new List<Product>();

                                    foreach (CategoryProduct cateProd in categoriasVsProdutoEncontradas)
                                    {
                                        var produtosEncontrados = BuscaProdutosPorId(cateProd.ProductId, lst);
                                        if(produtosEncontrados != null)
                                        {
                                            listaDeProdutosEncontradosPorCategoria.Add(produtosEncontrados);
                                        }
                                    }
                                    if(listaDeProdutosEncontradosPorCategoria != null && listaDeProdutosEncontradosPorCategoria.Any())
                                    {
                                        Console.WriteLine($"Produtos da categoria {nomeCategoria}");
                                        MostrarProdutos(listaDeProdutosEncontradosPorCategoria);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"A categoria {nomeCategoria} não está cadastrada!");
                            }

                            break;

                        case "0":
                            Console.WriteLine("Saindo..");
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
        public static void MostrarProdutos(List<Product> lista, bool simplificado = false)
        {
            foreach (var item in lista)
            {
                if (simplificado)
                {
                    item.MostraSimplificado();
                }
                else
                {
                    item.Frase();
                }
            }
        }

        public static List<Product> BuscaProduto(string nome, List<Product> lista)
        {
            List<Product> lstReturn = new List<Product>();
            foreach (var item in lista)
            {
                if (item.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase))
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

        public static int GeraIdCategoria(List<Category> lst)
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

        public static int GeraIdCategoriaProduto(List<CategoryProduct> lst)
        {
            List<int> lstInt = new List<int>();
            int maior = 0;

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

        public static void MostrarCategorias(List<Category> lista, bool simplificado = false)
        {
            if (simplificado)
            {
                List<string> descricoes = new List<string>();
                foreach (Category item in lista)
                {
                    if(!descricoes.Contains(item.Description))
                    {
                        descricoes.Add(item.Description);
                    }
                }

                foreach (var item in descricoes)
                {
                    Console.WriteLine($"Nome: {item}");
                }
            }
            else
            {
                foreach (var item in lista)
                {
                    item.Mostrar();
                }
            }
        }        

        public static List<Category> BuscaCategoria(string nome, List<Category> lista)
        {
            List <Category> returnList = new List<Category>();

            foreach (var item in lista)
            {
                if (item.Description.Contains(nome))
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }

        public static List<CategoryProduct> BuscaCategoriaVsProduto(int categoryId, List<CategoryProduct> lst)
        {
            List<CategoryProduct> lstReturn = new List<CategoryProduct>();
            foreach (var item in lst)
            {
                if (item.CategoryId.Equals(categoryId))
                {
                   lstReturn.Add(item);
                }
            }
            return lstReturn;
        }

        public static Product BuscaProdutosPorId(int productId, List<Product> lst)
        {
            foreach (var item in lst)
            {
                if (item.Id.Equals(productId))
                {
                    return item;
                }
            }

            return null;
        }

        public static List<Product> ListaDeProdutosParaTeste()
        {
            List<Product> lst = new List<Product>();
            lst.Add(new Product()
            {
                Id = 1,
                Nome = "Iphone",
                Valor = 7000,
                Quant = 10
            });

            lst.Add(new Product()
            {
                Id = 2,
                Nome = "Tv",
                Valor = 2000,
                Quant = 10
            });

            lst.Add(new Product()
            {
                Id = 3,
                Nome = "Papel Higiênico",
                Valor = 5,
                Quant = 109
            });

            return lst;
        }

        public static List<Category> ListaDeCategoriasParaTeste()
        {
            List<Category> lst = new List<Category>();
            lst.Add(new Category()
            {
                Id = 1,
                Description = "Eletronicos",
                Brand = "Apple"
            });

            lst.Add(new Category()
            {
                Id = 2,
                Description = "Eletronicos",
                Brand = "Sony"
            });

            lst.Add(new Category()
            {
                Id = 3,
                Description = "Higiene",
                Brand = "Neve"
            });
            return lst;
        }

        public static List<CategoryProduct> ListaDeCategoriasVsProdutoParaTeste(List<Category> lstCat, List<Product> lstProd)
        {
            List<CategoryProduct> lstCatProd = new List<CategoryProduct>();

            lstCatProd.Add(new CategoryProduct()
            {
                Id = 1,
                CategoryId = 1,
                ProductId = 1
            });

            lstCatProd.Add(new CategoryProduct()
            {
                Id = 1,
                CategoryId = 2,
                ProductId = 2
            });

            lstCatProd.Add(new CategoryProduct()
            {
                Id = 1,
                CategoryId = 3,
                ProductId = 3
            });

            return lstCatProd;
        }

        public static Product BuscaProdutoPorId(string id, List<Product> lista)
        {
            foreach (var item in lista)
            {
                if (item.Id.Equals(int.Parse(id)))
                {
                    return item;
                }
            }
            return null;
        }
    }
}