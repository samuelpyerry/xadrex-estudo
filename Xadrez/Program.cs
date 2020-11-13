using System;
using Xadrez.tabuleiro;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testando class Posição
            Posicao posicao = new Posicao(4,5);
            Console.WriteLine(posicao);

            //Testando a class Tabuleiro
            Tabuleiro tab = new Tabuleiro(8, 8);

            //Testando a impressão do Tabuleiro
            Tela.ImprimirTabuleiro(tab);
        }
    }
}
