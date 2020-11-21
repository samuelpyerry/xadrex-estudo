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
        public bool Xeque { get; private set; }

        public Partida()
        {
            Tab = new Tabuleiro(8, 8);
            JogadorAtual = Cor.Branco;
            Turno = 1;
            Final = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
            Xeque = false;
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            peca.QtdMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            //# Jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(destino.Linha, destino.Coluna + 1);
                Peca t = Tab.RetirarPeca(origemT);
                t.QtdMovimento();
                Tab.ColocarPeca(t, destinoT);
            }

            //# Jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(destino.Linha, destino.Coluna - 1);
                Peca t = Tab.RetirarPeca(origemT);
                t.QtdMovimento();
                Tab.ColocarPeca(t, destinoT);
            }

            return pecaCapturada;
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

        private Cor Adversaria (Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            return Cor.Branco;
        }

        private Peca Rei (Cor cor)
        {
            foreach(Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                Peca rei = Rei(cor);
                if (rei == null)
                {
                    throw new DomainExeptions("Não tem erro no tabuleiro dessa cor.");
                }

                bool[,] mat = x.MovimentosPossiveis(); 
                if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca capturarPeca)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.QtdMovimentoDiminuir();
            if (capturarPeca != null)
            {
                Tab.ColocarPeca(capturarPeca, destino);
                Capturadas.Remove(capturarPeca);
            }

            Tab.ColocarPeca(p, origem);

            //# Jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(destino.Linha, destino.Coluna + 1);
                Peca t = Tab.RetirarPeca(destinoT);
                t.QtdMovimentoDiminuir();
                Tab.ColocarPeca(t, origemT);
            }

            //# Jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(destino.Linha, destino.Coluna - 1);
                Peca t = Tab.RetirarPeca(destinoT);
                t.QtdMovimento();
                Tab.ColocarPeca(t, origemT);
            }


        }

        public void AlterarMovimentosTurno(Posicao origem, Posicao destino)
        {
            Peca PecaCapturada = ExecutarMovimento(origem, destino);
            
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, PecaCapturada);
                throw new DomainExeptions("Você não pode se colocar em xeque.");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                Final = true;
            }
            else
            {
                VezJogador();
                Turno++;
            }
            
            
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

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for(int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
                throw new DomainExeptions("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
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
            ColocarNovaPeca('d', 1, new Rei(Cor.Branco, Tab, this));


            ColocarNovaPeca('c', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preto, Tab, this));
            

        }
    }
}
