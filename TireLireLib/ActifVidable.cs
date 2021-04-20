using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireLireLib
{
    public abstract class ActifVidable : ActifDeBase
    {
        public decimal Vider()
        {
            decimal totalVider = this.MontantTotal;
            this.MontantTotal = 0;
            return totalVider;
        }
    }
}
