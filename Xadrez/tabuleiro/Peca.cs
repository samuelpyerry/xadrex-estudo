using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro.enuns;

namespace Xadrez.tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMov { get; protected set; }
        public Tabuleiro tab { get; protected set; }


    }
}
