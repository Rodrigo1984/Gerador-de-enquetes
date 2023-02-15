using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_de_enquetes
{
    internal class OptonScore
    {
        /// <summary>
        /// Opção.
        /// </summary>
        public Option Option { get; private set; }

        /// <summary>
        /// Número de votos.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="option">Opção</param>
        /// <param name="count">Número de votos</param>
        public OptonScore(Option option, int score)
        {
            this.Option = option;
            this.Count = score;
        }

        public int CompareTo(OptonScore other)
        {
            int comp = -Count.CompareTo(other.Count);

            if (comp == 0)
            {
                return Option.Text.CompareTo(other.Option.Text);
            }
            return comp;
        }


    }
}
