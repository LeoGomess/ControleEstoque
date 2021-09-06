using System;
using System.Collections.Generic;
using System.Text;

namespace ControleEstoque.Classes
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"Id: {Id}, Descrição: {Description}, Marca: {Brand}");
        }

        public void MostraSimplificado()
        {
            Console.WriteLine($"Descrição: {Description}");
        }
}
}
