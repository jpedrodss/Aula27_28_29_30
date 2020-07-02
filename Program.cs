using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p1 = new Produto();
            p1.Codigo  = 0;
            p1.Nome    = "GTX 1660 OC";
            p1.Preco   = 1799f;

            p1.Cadastrar(p1);
            // p1.Remover("1499");

            Produto alterado = new Produto();
            alterado.Codigo  = 4;
            alterado.Nome    = "GTX 1660 SUPER";
            alterado.Preco   = 1799f;

            // p1.Alterar(alterado);

            List<Produto> lista = p1.Ler();

            foreach (Produto item in lista){
               System.Console.WriteLine($"R${item.Preco} - {item.Nome}");
            }

        }
    }
}
