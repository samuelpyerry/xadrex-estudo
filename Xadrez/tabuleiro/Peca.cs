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
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            Posicao = null;
            Cor = cor;
            QtdMov = 0;
            Tab = tab;
        }

        public void QtdMovimento()
        {
            QtdMov++;
        }


    }
}
