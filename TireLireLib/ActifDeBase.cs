using System.Text.Json.Serialization;

namespace TireLireLib
{
    public abstract class ActifDeBase
    {
        [JsonInclude]
        public decimal MontantTotal { get; protected set; }
    }
}
