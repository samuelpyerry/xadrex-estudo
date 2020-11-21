using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro.enuns;
using Xadrez.tabuleiro;
namespace Xadrez.xadrex_jogo
{
    class Torre : Peca
    {
        public Torre (Cor cor, Tabuleiro tab) : base (cor, tab)
        {

        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor; 
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);
            
            //Verificando posições acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tab.TratarPosicao(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha -= 1;
            }

            //Verificando posições abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tab.TratarPosicao(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Linha += 1;
            }

            //Verificando posições a direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tab.TratarPosicao(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Coluna += 1;

            }

            //Verificando posições a esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tab.TratarPosicao(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
                if (Tab.Peca(posicao) != null && Tab.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                posicao.Coluna -= 1;
            }
            return mat;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
