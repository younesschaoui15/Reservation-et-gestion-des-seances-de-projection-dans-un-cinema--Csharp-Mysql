using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cinema_Cynov_Application.Metier.Entites;
using Cinema_Cynov_Application.Metier.DAO;
using System.Security.Cryptography;
using Cinema_Cynov_Application.Metier.Services;

namespace cinéma_Cynov_Application
{
    class Program
    {
        
        static void Main(string[] args)
        {
            IServicesImpl services = IServicesImpl.getInstanceServices();
            
            Console.WriteLine("================================================================================");
            Console.WriteLine("=========================> Bienvenue au cinéma CYNOV <==========================");
            Console.WriteLine("================================================================================\n");

            char userType = services.getTypeUtilisateur();
            object x = services.authentification(userType);
            if (x == null)
            {
                Console.WriteLine("\n\nAu revoir à la prochaine, Merci."); Console.ReadKey();
                //Sortir de l'application
                System.Environment.Exit(1);
            }

            if (x is Admin)
            {
                Admin admin = (Admin)x;
                Console.WriteLine("\nBonjour Mr/Mme : " + admin.nom + " " + admin.prenom);
                Console.WriteLine("\n******************************** Administration ********************************\n\n");

                //le menu de l'administrateur
                services.administrer();
            }
            else
            {
                Visiteur visiteur = (Visiteur)x;
                Console.WriteLine("\nBonjour Mr/Mme : " + visiteur.nom + " " + visiteur.prenom + " dans votre espace cinéma CYNOV.");
                Console.WriteLine("\n******************************** Espace visiteur *******************************\n\n");
                
                //le menu du visiteur
                services.visiter(visiteur.id);
            }
        }
    }
}
