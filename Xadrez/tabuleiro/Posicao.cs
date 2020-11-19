using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro
{

    //Classe Posição, onde toda peça estara em uma
    class Posicao
    {
        public int Coluna { get; set; }
        public int Linha { get; set; }

        public Posicao(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public void DefinirValores(int linha, int coluna)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public override string ToString()
        {
            return Linha +
                ", "
                + Coluna;
        }
    }
}
