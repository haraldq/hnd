using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Ess
    {


        public bool IsEss(int[,] payoffMatrix, int d)
        {
            /*The strategy d is an ESS if and only if
              payoff(d,d) > payoff(h,d) or,
              payoff(d,d) = payoff(h,d) with payoff(d,h) > payoff(h,h).
            */
            int h = d == 0 ? 1 : 0;

            if (payoffMatrix[d, d] > payoffMatrix[h, d])
                return true;

            if (payoffMatrix[d, d] == payoffMatrix[h, d] &&
                payoffMatrix[d, h] > payoffMatrix[h, h])
                return true;
            return false;

        }
    }
}
