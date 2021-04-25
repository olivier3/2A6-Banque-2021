using System;
using System.Text.Json.Serialization;

using TireLireLib;

namespace BanqueLib
{
    public class Compte1 : Tirelire9
    {
        public int Numéro { get; set; }
        public string Titulaire { get; set; }

        [JsonConstructor]
        public Compte1(int numéro, string titulaire, decimal montantTotal = 0)
        {
            if (numéro <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numéro), numéro, "trop petit");
            }
            if (string.IsNullOrWhiteSpace(titulaire))
            {
                throw new ArgumentException("null ou blanc", nameof(titulaire));
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

        public Compte1(Compte1 copie)
        {
            this.Numéro = copie.Numéro;
            this.Titulaire = copie.Titulaire;
            this.MontantTotal = copie.MontantTotal;
        }

        public override string ToString()
            => $"{this.Numéro} {this.Titulaire} {this.MontantTotal:C}";

        public override bool Equals(object obj)
            => obj is Compte1 c
            && c.Numéro == this.Numéro
            && c.Titulaire == this.Titulaire
            && c.MontantTotal == this.MontantTotal;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
