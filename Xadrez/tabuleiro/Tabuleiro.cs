using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro.Exeptions;

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

        public Peca Peca(Posicao posicao)
        {
            return pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao p)
        {
            if (ExistePeca(p))
            {
                throw new DomainExeptions("Já existe uma peça nessa posição.");
            }
            
            pecas[p.Linha, p.Coluna] = peca;
            peca.Posicao = p;
        }
        public Peca RetirarPeca(Posicao posicao)
        {
            if (Peca(posicao) == null){
                return null;
            }
            Peca auxiliar = Peca(posicao);
            auxiliar.Posicao = null;
            pecas[posicao.Linha, posicao.Coluna] = null;
            return auxiliar;
        }

        public bool ExistePeca (Posicao posicao)
        {

            ValidarPosicao(posicao);
            return Peca(posicao) != null;      
           
        }
        public bool TratarPosicao(Posicao posicao)
        {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao posicao)
        {
            if (!TratarPosicao(posicao))
            {
                throw new DomainExeptions("Não existe essa posição no tabuleiro.");
            }
        }
    }
}
