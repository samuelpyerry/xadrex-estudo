using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.tabuleiro.enuns;
using Xadrez.xadrex_jogo;

namespace Xadrez
{
    class Tela
    {

        public static void imprimirPartida(Partida partida)
        {
            ImprimirTabuleiro(partida.Tab);
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            
            if (!partida.Final)
            {
                Console.WriteLine("Aguardando jogador: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.Write("Xeque!");
                }
            }
            else
            {
                Console.WriteLine("Xequemate.");
            }
        }

            

        public static void ImprimirPecasCapturadas(Partida partida)
        {
            Console.WriteLine("Peças capturadas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branco));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Pretas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");

        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {


                    ImprimirPeca(tab.PecaTab(i, j));


                }
                Console.WriteLine();

                ;
            }
            Console.WriteLine("  a b c d e f g h ");
        }

        //Mesmo metodo de cima, POREM, com um novo parametro "posicoesPossiveis"

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] possicoesPossiveis)
        {
            //Cores do background
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;



            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {

                    //Validando tipo de background
                    if (possicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.PecaTab(i, j));
                    Console.BackgroundColor = fundoOriginal;


                }
                Console.WriteLine();

                ;
            }
            Console.WriteLine("  a b c d e f g h ");
            Console.BackgroundColor = fundoOriginal;
        }


        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (peca.Cor == Cor.Branco)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez CapturarLetra()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            //Uso de "" para converter totalmente em string Tip.: Obs
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}

