using System;
using Xadrez.tabuleiro;
using Xadrez.xadrex_jogo;
using Xadrez.tabuleiro.enuns;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testando class Posição
            //Posicao posicao = new Posicao(4,5);
            //Console.WriteLine(posicao);

            //Testando a class Tabuleiro
            Tabuleiro tab = new Tabuleiro(8, 8);

            //Testando metodo para posicionar peças
            tab.ColocarPeca(new Torre(Cor.amarelo, tab), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(Cor.amarelo, tab), new Posicao(1, 3));
            tab.ColocarPeca(new Rei(Cor.vermelho, tab), new Posicao(2, 4));

            //Testando a impressão do Tabuleiro
            Tela.ImprimirTabuleiro(tab);
        }
    }
}
