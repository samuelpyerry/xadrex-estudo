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
    }
}
