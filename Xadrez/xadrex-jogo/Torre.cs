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
        public override string ToString()
        {
            return "T";
        }
    }
}
