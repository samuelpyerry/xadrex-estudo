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
        public Peca VulneravelEnPassant { get; private set; }

        public Partida()
        {
            Tab = new Tabuleiro(8, 8);
            JogadorAtual = Cor.Branco;
            Turno = 1;
            Final = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
            Xeque = false;
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.QtdMovimento();
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

            //# Jogada especial Empasant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tab.RetirarPeca(posP);
                    Capturadas.Add(pecaCapturada);
                }
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

            //# Jogada especial En passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && capturarPeca == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branco)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                }
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

            Peca p = Tab.Peca(destino);

            //Jogada especial promocao
            if (p is Peao)
            {
                if (p.Cor == Cor.Branco && destino.Linha == 0 || p.Cor == Cor.Preto && destino.Linha == 7)
                {
                    p = Tab.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(p.Cor, Tab);
                    Tab.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
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

            
            //Jogada especial Empasant

            if (p is Peao &&(destino.Linha == origem.Linha - 2  || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
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
            ColocarNovaPeca('a', 1, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branco, Tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branco, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branco, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branco, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branco, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branco, Tab));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branco, Tab, this));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branco, Tab, this));



            ColocarNovaPeca('a', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preto, Tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preto, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preto, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preto, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preto, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preto, Tab));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preto, Tab, this));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preto, Tab, this));


        }
    }
}
