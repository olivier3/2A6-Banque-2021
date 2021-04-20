using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TireLireLib
{
    public abstract class ActifDeBase
    {
        [JsonInclude]
        public decimal MontantTotal { get; protected set; }
    }
}
