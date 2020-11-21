using System.Collections.Generic;
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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public Partida()
        {
            Tab = new Tabuleiro(8, 8);
            JogadorAtual = Cor.Branco;
            Turno = 1;
            Final = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RetirarPeca(origem);
            peca.QtdMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca peca in Capturadas)
            {
                if(peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo (Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in Pecas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('c', 2, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('d', 2, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('e', 2, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branco, Tab));


            ColocarNovaPeca('c', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preto, Tab));
            

        }
    }
}
