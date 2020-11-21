 using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro.enuns;

namespace Xadrez.tabuleiro
{
    abstract    class Peca
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

        public void QtdMovimentoDiminuir()
        {
            QtdMov--;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i = 0; i < Tab.Linhas; i++)
            {
                for(int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel (Posicao posicaoDestino)
        {
            return MovimentosPossiveis()[posicaoDestino.Linha, posicaoDestino.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();


    }
}
