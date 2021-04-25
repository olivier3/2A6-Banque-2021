using System;

using TireLireLib;

namespace BanqueLib
{
    public class Compte1 : Tirelire9
    {
        public int Numéro { get; set; }
        public string Titulaire { get; set; }

        public Compte1(int numéro, string titulaire, decimal montantTotal = 0)
        {
            if (numéro <= 0)
            {
                throw new ArgumentOutOfRangeException("numéro", numéro, "trop petit");
            }
            if (string.IsNullOrWhiteSpace(titulaire))
            {
                throw new ArgumentException("null ou blanc", "titulaire");
            }
            if (montantTotal < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montantTotal), montantTotal, "trop petit");
            }
            if (Math.Round(montantTotal, 2) != montantTotal)
            {
                throw new ArgumentException("trop précis", nameof(montantTotal));
            }

            this.Numéro = numéro;
            this.Titulaire = titulaire;
            this.MontantTotal = montantTotal;
        }
    }
}
