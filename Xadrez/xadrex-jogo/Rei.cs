using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.tabuleiro.enuns;
namespace Xadrez.xadrex_jogo
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tab) : base (cor, tab)
        {

        }
        public override string ToString()
        {
            return "R";
        }

        public bool podeMover(Posicao posicao)
        {
            Peca peca = Tab.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao posicao = new Posicao(0, 0);

            //Acima
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Nordeste
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Direita
            posicao.DefinirValores(posicao.Linha, posicao.Coluna + 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Sudeste
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Abaixo
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao)){
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Sudoeste
            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Esqueda
            posicao.DefinirValores(posicao.Linha, posicao.Coluna - 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Noroeste
            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna -1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            return mat;



        }
    }
}
