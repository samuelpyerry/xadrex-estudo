using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro.Exeptions
{
    class DomainExeptions : Exception
    {
        public DomainExeptions(string mensagem) : base(mensagem)
        {

        }
    }
}
