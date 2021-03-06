using System;
using System.Diagnostics;

using cstjean.info.fg.consoleplus;
using static TPConsole.Instances;
using TireLireLib;
using BanqueLib;

namespace TPConsole
{
    public static class MenuGénéral
    {
        private static bool menu = false;
        public static void Afficher()
        {
            do
            {
                ConsolePlus.Clear();
                AfficherEntête();
            } while (TraiterMenuEtContinuer());
        }

        private static void AfficherEntête()
        {
            ConsolePlus.WriteLine();
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "OB - Menu Général");
            ConsolePlus.WriteLine(ConsoleColor.Magenta, "===============");
            ConsolePlus.WriteLine();
            ConsolePlus.Afficher("Historique", "");
            ConsolePlus.WriteLine(Historique.ConstructionHistorique());
            ConsolePlus.WriteLine();
            if (menu)
            {
                ConsolePlus.Afficher("Tirelire 1", $"{Tirelire1.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 2", $"{Tirelire2.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 3a", $"{MesInstances.Tirelire3a.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 3a", $"{MesInstances.Tirelire3b.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 4a", $"{MesInstances.Tirelire4a.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 5a", $"{MesInstances.Tirelire5a.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 6a", $"{MesInstances.Tirelire6a.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 6p", $"{MesInstances.Tirelire6p.MontantTotal:C}");
                ConsolePlus.Afficher("Tirelire 7a", $"{MesInstances.Tirelire7a.MontantTotal:C}");
            }
            ConsolePlus.Afficher("Tirelire 9a", $"{MesInstances.Tirelire9a.MontantTotal:C}");
            ConsolePlus.Afficher("Mon compte 1", $"{MesInstances.Compte1.MontantTotal:C}");
            ConsolePlus.Afficher("Mon compte 2", $"{MesInstances.Compte2.MontantTotal:C}");
            ConsolePlus.Afficher("Mon compte 3", $"{MesInstances.Compte3.MontantTotal:C}");
            ConsolePlus.Afficher("Banque OB", $"{MesInstances.Banque.ActifTotal:C}");
            ConsolePlus.Afficher("Banque Jedi", $"{MesInstances.BanqueJedi.ActifTotal:C}");
            ConsolePlus.WriteLine();
        }

        private static string[] Option()
        {

            if (menu)
            {
                string[] menuLong = {
                    "Quitter", "Reset", "Planter", "Réduire", "Tirelire 1", "Tirelire 2", "Tirelire 3a"
                    , "Tirelire 3b", "Tirelire 4a", "Tirelire 5a", "Tirelire 6a", "Tirelire 6p", "Tirelire 7a"
                    , "Tirelire 9a", "Mon compte 1", "Mon compte 2", "Mon compte 3", "Banque OB", "Banque Jedi"};
                return menuLong;
            }
            else
            {
                string[] menuCourt = { "Quitter", "Reset", "Planter", "Étendre", "Tirelire 9a", "Mon compte 1"
                        , "Mon compte 2", "Mon compte 3", "Banque OB", "Banque Jedi" };
                return menuCourt;
            }
        }

        private static bool TraiterMenuEtContinuer()
        {
            if (ConsolePlus.LireChoix(out string? choix, 'A', Option()))
            {
                ConsolePlus.WriteLine();
                switch (choix)
                {
                    case "Quitter":
                        return false;

                    case "Reset":
                        Reset();
                        break;
                    case "Planter":
                        throw new InvalidOperationException("OB dit Boom!");
                    case "Réduire":
                        menu = false;
                        break;
                    case "Étendre":
                        menu = true;
                        break;
                    case "Tirelire 1":
                        //Mettre add \n
                        Historique.Suivi().Add("\n    >> Tirelire 1 ");
                        MenuTirelire1();
                        break;
                    case "Tirelire 2":
                        Historique.Suivi().Add("\n    >> Tirelire 2 ");
                        MenuTirelire2();
                        break;
                    case "Tirelire 3a":
                        Historique.Suivi().Add("\n    >> Tirelire 3a ");
                        MenuTirelire3(MesInstances.Tirelire3a, "3a");
                        break;
                    case "Tirelire 3b":
                        Historique.Suivi().Add("\n    >> Tirelire 3b ");
                        MenuTirelire3(MesInstances.Tirelire3b, "3b");
                        break;
                    case "Tirelire 4a":
                        Historique.Suivi().Add("\n    >> Tirelire 4a ");
                        MenuTirelire4(MesInstances.Tirelire4a, "4a");
                        break;
                    case "Tirelire 5a":
                        Historique.Suivi().Add("\n    >> Tirelire 5a ");
                        MenuTirelire5(MesInstances.Tirelire5a, "5a");
                        break;
                    case "Tirelire 6a":
                        Historique.Suivi().Add("\n    >> Tirelire 6a ");
                        MenuTirelire6(MesInstances.Tirelire6a, "6a");
                        break;
                    case "Tirelire 6p":
                        Historique.Suivi().Add("\n    >> Tirelire 6p ");
                        MenuTirelire6(MesInstances.Tirelire6p, "6p");
                        break;
                    case "Tirelire 7a":
                        Historique.Suivi().Add("\n    >> Tirelire 7a ");
                        MenuTirelire6(MesInstances.Tirelire7a, "7a");
                        break;
                    case "Tirelire 9a":
                        Historique.Suivi().Add("\n    >> Tirelire 9a ");
                        MenuTirelire9(MesInstances.Tirelire9a, "9a");
                        break;
                    case "Mon compte 1":
                        Historique.Suivi().Add("\n    >> Mon compte 1 ");
                        MenuCompte1(MesInstances.Compte1, MesInstances.Compte1.Numéro);
                        break;
                    case "Mon compte 2":
                        Historique.Suivi().Add("\n    >> Mon compte 2 ");
                        MenuCompte2(MesInstances.Compte2, MesInstances.Compte2.Numéro);
                        break;
                    case "Mon compte 3":
                        Historique.Suivi().Add("\n    >> Mon compte 3 ");
                        MenuCompte3(MesInstances.Compte3, MesInstances.Compte3.Numéro);
                        break;
                    case "Banque OB":
                        MenuBanque.Afficher(MesInstances.Banque, "OB");
                        break;
                    case "Banque Jedi":
                        MenuBanque.Afficher(MesInstances.BanqueJedi, "Jedi");
                        break;
                    default:
                        Debug.Fail($"Cas non traité: {choix}");
                        break;
                }
            }
            return true;
        }
        private static void Reset()
        {
            _ = ConsolePlus.LireBooléen("Voulez-vous vraiment (o/n)", out bool choix);
            if (choix == true)
            {
                Tirelire1.MontantTotal = 0;
                _ = Tirelire2.Vider();
                //MesInstances.Tirelire3a.MontantTotal = 0;
                //MesInstances.Tirelire3b.MontantTotal = 0;
                //MesInstances.Tirelire4a.MontantTotal = 0;
                //_ = Tirelire5.Vider(MesInstances.Tirelire5a);
                //_ = MesInstances.Tirelire6a.Vider();
                //_ = MesInstances.Tirelire6p.Vider();
                //_ = MesInstances.Tirelire7a.Vider();
                //_ = MesInstances.Tirelire9a.Vider();
                //_ = MesInstances.Compte1.Vider();
                //_ = MesInstances.Compte2.Vider();
                //_ = MesInstances.Compte3.Vider();
                MesInstances = new Instances();
                Historique.Suivi().Clear();
            }
            else
            {
                Afficher();
            }
        }
        private static void MenuTirelire1()
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(Tirelire1.MontantTotal, "1");
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Opérations1.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Opérations1.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Opérations1.Vider()));
            }
        }
        private static void MenuTirelire2()
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(Tirelire2.MontantTotal, "2");
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Tirelire2.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Tirelire2.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Tirelire2.Vider()));
            }
        }
        private static void MenuTirelire3(Tirelire3 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Opérations3.Déposer(tirelire, montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Opérations3.Retirer(tirelire, montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Opérations3.Vider(tirelire)));
            }
        }
        private static void MenuTirelire4(this Tirelire4 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()));
            }
        }
        private static void MenuTirelire5(this Tirelire5 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => Tirelire5.Déposer(tirelire, montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => Tirelire5.Retirer(tirelire, montant)),
                    //Vider
                    () => MenuUtil.Vider(() => Tirelire5.Vider(tirelire)));
            }
        }
        private static void MenuTirelire6(this Tirelire6 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()));
            }
        }
        private static void MenuTirelire9(this Tirelire9 tirelire, string noTirelire)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntête(tirelire.MontantTotal, noTirelire);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()));
            }
        }
        private static void MenuCompte1(this Compte1 tirelire, int numéro)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntêteCompte(numéro, tirelire.Titulaire, tirelire.MontantTotal);
                continuer = MenuUtil.TraiterMenuEtContinuer(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()));
            }
        }
        private static void MenuCompte2(this Compte2 tirelire, int numéro)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntêteCompte2(numéro, tirelire.Titulaire, tirelire.MontantTotal, tirelire.État);
                continuer = MenuUtil.TraiterMenuEtContinuerCompte2(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()),
                    () => MenuUtil.Fermer(() => tirelire.Fermer(), tirelire.MontantTotal),
                    () => MenuUtil.Réactiver(() => tirelire.Réactiver()));
            }
        }
        public static void MenuCompte3(this Compte3 tirelire, int numéro)
        {
            var continuer = true;
            while (continuer)
            {
                ConsolePlus.Clear();
                MenuUtil.AfficherEntêteCompte2(numéro, tirelire.Titulaire, tirelire.MontantTotal, tirelire.État);
                continuer = MenuUtil.TraiterMenuEtContinuerCompte3(
                    //Déposer
                    () => MenuUtil.Déposer(montant => tirelire.Déposer(montant)),
                    //Retirer
                    () => MenuUtil.Retirer(montant => tirelire.Retirer(montant)),
                    //Vider
                    () => MenuUtil.Vider(() => tirelire.Vider()),
                    () => MenuUtil.Fermer(() => tirelire.Fermer(), tirelire.MontantTotal),
                    () => MenuUtil.Réactiver(() => tirelire.Réactiver()),
                    () => MenuUtil.Geler(() => tirelire.Geler()),
                    () => MenuUtil.Verser(pourcentage => tirelire.VerserIntérêts(pourcentage)));
            }
        }
    }
}
