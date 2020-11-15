using System;
using Xadrez.tabuleiro;
using Xadrez.xadrex_jogo;
using Xadrez.tabuleiro.enuns;
using Xadrez.tabuleiro.Exeptions;

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
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);
            Console.WriteLine(posicaoXadrez.ToPosicao());
            Console.WriteLine(posicaoXadrez);

            //Testando metodo para posicionar peças
            /*try
            {
                tab.ColocarPeca(new Torre(Cor.amarelo, tab), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(Cor.amarelo, tab), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(Cor.vermelho, tab), new Posicao(3, 5));

                //Testando a impressão do Tabuleiro
                Tela.ImprimirTabuleiro(tab);
            }catch (DomainExeptions execao)
            {
                Console.WriteLine("Erro: " + execao.Message);
            }*/
        }
    }
}
