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
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao)){
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Esqueda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            //Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna -1);
            if (Tab.TratarPosicao(posicao) && podeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }
            return mat;



        }
    }
}
