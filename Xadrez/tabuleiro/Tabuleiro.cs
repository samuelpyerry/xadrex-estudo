using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;

            pecas = new Peca[linhas, colunas];
        }

        public Peca PecaTab(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public void ColocarPeca(Peca peca, Posicao p)
        {
            pecas[p.Linha, p.Coluna] = peca;
            peca.Posicao = p;
        }
    }
}
