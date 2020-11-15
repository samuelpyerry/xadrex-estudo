using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;

namespace Xadrez.xadrex_jogo
{
    class PosicaoXadrez
    {
        public int Linhas { get; set; }
        public char Colunas { get; set; }
    
        public PosicaoXadrez(char colunas, int linhas)
        {
            Linhas = linhas;
            Colunas = colunas;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linhas, Colunas - 'a');
        }
        public override string ToString()
        {
            return "" + Colunas + Linhas;
        }
    }
}
