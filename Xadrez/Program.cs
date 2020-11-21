﻿using System;
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

           
            
            /*
            * Testando a class PosicaoXadrez
            PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);
            Console.WriteLine(posicaoXadrez.ToPosicao());
            Console.WriteLine(posicaoXadrez);
            *
            */

            //Testando metodo para posicionar peças
            try
            {
                //Testando a class Tabuleiro
                Partida partida = new Partida();
                

                while (!partida.Final)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);

                    Console.WriteLine();
                    Console.Write(" Origem: ");
                    Posicao origem = Tela.CapturarLetra().ToPosicao();

                    bool[,] posicoesPosiveis = partida.Tab.Peca(origem).MovimentosPossiveis();


                    //Marcar os caminhos livres
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab, posicoesPosiveis);


                    Console.Write(" Destino: ");
                    Posicao destino = Tela.CapturarLetra().ToPosicao();

                    partida.ExecutarMovimento(origem, destino);


                }

            }
            catch (DomainExeptions execao)
            {
                Console.WriteLine("Erro: " + execao.Message);
            }
        }
    }
}
