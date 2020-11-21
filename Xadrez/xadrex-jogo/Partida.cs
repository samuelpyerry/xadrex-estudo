using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.tabuleiro.enuns;
using Xadrez.tabuleiro.Exeptions;

namespace Xadrez.xadrex_jogo
{
    class Partida
    {
        public Tabuleiro Tab;
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Final { get; private set; }

        public Partida()
        {
            Tab = new Tabuleiro(8, 8);
            JogadorAtual = Cor.Branco;
            Turno = 1;
            ColocarPecas();
            Final = false;
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.QtdMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);

        }

        public void AlterarMovimentosTurno(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            VezJogador();
            Turno++;
        }

        public void VezJogador()
        {
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            if (Tab.Peca(posicao) == null)
            {
                throw new DomainExeptions("Não existe peça nessa cordenada.");
            }
            if (JogadorAtual != Tab.Peca(posicao).Cor)
            {
                throw new DomainExeptions("A peça selecionada não é sua.");
            }
            if (!Tab.Peca(posicao).ExisteMovimentosPossiveis())
            {
                throw new DomainExeptions("Não existe movimentos possíveis.");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new DomainExeptions("Não é possivel mover a peça para esse destino.");
            }
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branco, Tab), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Branco, Tab), new PosicaoXadrez('d', 1).ToPosicao());

            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preto, Tab), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.ColocarPeca(new Rei(Cor.Preto, Tab), new PosicaoXadrez('d', 8).ToPosicao());

        }
    }
}
