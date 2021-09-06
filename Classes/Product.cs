using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Classes
{
    public class Product
    {
        public string Nome;
        public double Valor;
        public int Quant;
        public string Marca;
        public int Id { get; set; }

        public void AddQuant(int qnt)
        {
            if (qnt >= 0)
            {
                Quant += qnt;
            }
            else
            {
                Console.WriteLine("Digite um valor válido");
            }
        }
        public void RemoveQuant(int qnt)
        {
            if (qnt >= 0)
            {
                Quant -= qnt;
            }
            else
            {
                Console.WriteLine("Digite um valor válido");
            }
        }
        /// <summary>
        /// Retorna o Valor total do Produto (qtd x valor)
        /// </summary>
        /// <returns></returns>
        public double TotalValue()
        {
            return Quant * Valor;
        }
        public void Frase()
        {
            Console.WriteLine($"Id: {Id}, Nome: {Nome}, Valor: {Valor}, {Quant} unidades," +
                $" valor total:{TotalValue().ToString(CultureInfo.InvariantCulture)}");
        }

        public void MostraSimplificado()
        {
            Console.WriteLine($"Nome: {Nome}");
        }
    }
}