using BanqueLib;

namespace Tests
{
    public partial class Tester_Compte_2
    {
        static partial void GénérerCopie(ref Compte2 copie, Compte2 original)
        {
            copie = new Compte2(original);
        }

        static partial void EstSupp(ref bool estSupp)
        {
            estSupp = true;
        }
    }
}
