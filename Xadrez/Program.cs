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

                    try
                    {


                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.CapturarLetra().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);
                        bool[,] posicoesPosiveis = partida.Tab.Peca(origem).MovimentosPossiveis();


                        //Marcar os caminhos livres
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPosiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.CapturarLetra().ToPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);


                        partida.AlterarMovimentosTurno(origem, destino);
                    }
                    catch(DomainExeptions erros)
                    {
                        Console.WriteLine(erros.Message);
                        Console.Write("Digite qualquer tecla para tentar novamente.");
                        Console.ReadLine();
                    }

                }
                Console.Clear();
                Tela.imprimirPartida(partida);

            }
            catch (DomainExeptions execao)
            {
                Console.WriteLine("Erro: " + execao.Message);
            }
        }
    }
}
