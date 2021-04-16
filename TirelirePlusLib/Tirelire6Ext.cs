using System;

using TireLireLib;

namespace TirelirePlusLib
{
    public static class Tirelire6Ext
    {
        public static bool Reset(this Tirelire6 tirelire, decimal montant = 0)
        {
            if (montant >= 0)
            {
                _ = tirelire.Vider();
                _ = tirelire.Déposer(montant);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
