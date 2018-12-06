using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Cinema_Cynov_Application.Metier.Entites;
using Cinema_Cynov_Application.Metier.Config;
using Cinema_Cynov_Application.Metier.DAO;

namespace Cinema_Cynov_Application.Metier.Services
{
    public class IServicesImpl : IServices
    {
        static DAOImpl dao = null;
        static IServicesImpl service = null;

        //Utilisation du patron de conception SINGLETON
        public static IServicesImpl getInstanceServices()
        {
            if (service == null)
                service = new IServicesImpl();
            return service;
        }

        private IServicesImpl()
        {
            if (dao == null)
                dao = new DAOImpl();
        }

        /** Implémentaion des fonctions **/

        /* Espace administrateur */

        public char getTypeUtilisateur()
        {
            char userType = '#';
            while (true)
            {
                Console.Write("Etes-vous un admin(a) ou un visiteur(v) ? ");
                userType = Console.ReadKey().KeyChar;
                if (userType == 'a' || userType == 'v')
                    return userType;
                Console.WriteLine();
            }
        }
        
        public object authentification(char userType)
        {
            object x = null;
            while (true)
            {
                Console.Write("\nVotre e-mail : ");
                string email = Console.ReadLine();
                Console.Write("Votre mot de passe : ");
                string password = Console.ReadLine();

                bool is_existe = false;
                if (userType == 'a')
                {
                    is_existe = dao.isAdminExiste(email, password);
                    if (is_existe)
                        x = dao.findAdminByEmail(email);
                }
                else
                {
                    is_existe = dao.isVisiteurExiste(email, password);
                    if (is_existe)
                        x = dao.findVisiteurByEmail(email);
                }

                if (x != null)
                {
                    Console.WriteLine("> Connecté.\n");
                    return x;
                }
                else
                {
                    Console.Write("\nNom d'utilisateur/Mot de passe incorrect.\nEssayer de nouveau Oui/Non(n) ? ");
                    if (Console.ReadKey().KeyChar == 'n')
                        return null;
                }
            }
        }

        public void creerCompteVisiteur()
        {
            Console.WriteLine("\n\n--- Création compte visiteur ---");
            Console.Write("\nNom : ");
            string nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();
            Console.Write("E-mail : ");
            string email = Console.ReadLine();
            Console.Write("Mot de passe : ");
            string password = Console.ReadLine();

            Visiteur v = new Visiteur(0, nom, prenom, email, password);
            bool is_cree = dao.creerCompteVisiteur(v);

            if (is_cree)
                Console.WriteLine("> Compte visiteur crée avec succes.");
            else
                Console.WriteLine("> Compte visiteur n'est pas crée.");
        }

        public void ajouterVisionnage()
        {
            Console.WriteLine("\n\n\t--- Ajouter un nouveau visionnage ---");
            Visionnage v = new Visionnage();
            Console.Write("\nNom du visionnage : ");
            v.nom = Console.ReadLine();
            Console.Write("Réalisateur : ");
            v.realisateur = Console.ReadLine();
            Console.Write("Producteur : ");
            v.producteur = Console.ReadLine();
            
            char t = '#';

            //Type du visionnage
            do
            {
                Console.Write("\nType du visionnage, Long métrage(1)/Court métrage(2)/Série(3) : ");
                t = Console.ReadKey().KeyChar;
            } while (t != '1' && t != '2' && t != '3');

            if (t=='1')
                v.type = "Long métrage";
            else if (t=='2')
                v.type = "Court métrage";
            else
                v.type = "Série";

            //si le visionnage est 3D ou non
            do
            {
                Console.Write("\nVisionnage 3D ? Oui(o)/Non(n) : ");
                t = Console.ReadKey().KeyChar;
            } while (t != 'o' && t != 'n');

            if (t == 'o')
                v.is_3d = true;
            else
                v.is_3d = false;

            //si le visionnage est original ou non
            do
            {
                t = '#';
                Console.Write("\nVisionnage original ? Oui(o)/Non(n) : ");
                t = Console.ReadKey().KeyChar;
            } while (t != 'o' && t != 'n');

            if (t == 'o')
                v.is_original = true;
            else
                v.is_original = false;



            bool is_cree = dao.ajouterVisionnage(v);

            if (is_cree)
                Console.WriteLine("\n> Visionnage a été ajouté avec succes.");
            else
                Console.WriteLine("\n> Visionnage n'a pas été ajouté.");
        }

        public void administrer()
        {
            char choix = '#';
            do
            {
                Console.WriteLine("\n\t  --- Menu d'administration ---\n");
                Console.WriteLine("\t1) Créer un nouveau compte admin.");
                Console.WriteLine("\t2) Créer un nouveau compte visiteur.");
                Console.WriteLine("\t3) Ajouter un nouveau visionnage.");
                Console.WriteLine("\t4) Gérer les séances de projection.");
                Console.WriteLine("\n\t0) Quitter l'application.");
                Console.Write("\n\nVotre choix : ");
                choix = Console.ReadKey().KeyChar;
                switch (choix)
                {
                    case '0':
                        System.Environment.Exit(1); break;
                    case '1':
                        creerCompteVisiteur(); break;
                    case '2':
                        creerCompteVisiteur(); break;
                    case '3':
                        ajouterVisionnage(); break;
                    case '4':
                        gererSeancesProjection(); break;

                    default:
                        Console.WriteLine("\n> ATTENTION: Choix hors liste! essayer de nouveau.\n\n");
                        choix = '#';
                        break;
                }
            }
            while(choix=='#');

            administrer();
        }

        public void gererSeancesProjection()
        {
            char choix = '#';
            do
            {
                Console.WriteLine("\n\n\t  --- Gestion des séances de projection ---\n");
                Console.WriteLine("\t1) Liste des séances.");
                Console.WriteLine("\t2) Liste des films.");
                Console.WriteLine("\t3) Liste des salles.");
                Console.WriteLine("\t4) Ajouter une séance de projection.");
                Console.WriteLine("\t5) Ajouter un film à une séance de projection.");
                Console.WriteLine("\n\t0) Retour.");
                Console.Write("\n\nVotre choix : ");
                choix = Console.ReadKey().KeyChar;
                switch (choix)
                {
                    case '0':
                        return;
                    case '1':
                        afficherListeSeancesProjection(); break;
                    case '2':
                        afficherListeFilms(); break;
                    case '3':
                        afficherListeSalles(); break;
                    case '4':
                        ajouterSeanceProjection(); break;
                    case '5':
                        ajouterFilmAuSeanceProjection(); break;

                    default:
                        Console.WriteLine("\n> ATTENTION: Choix hors liste! essayer de nouveau.\n\n");
                        choix = '#';
                        break;
                }
            }
            while (choix == '#');

            gererSeancesProjection();
        }

        public void afficherListeSeancesProjection()
        {
            List<string> liste = dao.getListeSeancesProjection();
            if (liste == null || liste.Count == 0)
                Console.WriteLine("\n> La liste est vide ! ");
            else
            {
                int c = 1;
                Console.WriteLine("\n");
                foreach(string date in liste)
                {
                    Console.WriteLine("Séance "+c+"- "+date);
                    c++;
                }
            }
        }

        public void afficherListeFilms()
        {
            List<Visionnage> liste = dao.getListeFilms();
            if (liste == null || liste.Count == 0)
                Console.WriteLine("> La liste est vide ! ");
            else
            {
                int c = 1;
                Console.WriteLine("\n");
                foreach (Visionnage v in liste)
                {
                    Console.WriteLine(c + "- " + v.ToString());
                    c++;
                }
            }
        }

        public void ajouterSeanceProjection()
        {
            //récuperation de la date
            string date = "";
            do
            {
                do
                {
                    Console.Write("\nDate de la séance (ex: 2018-11-8) : ");
                    date = Console.ReadLine();
                    if (new Regex("^\\d{4}-\\d{1,2}-\\d{1,2}$").IsMatch(date))
                        break;
                    else
                        Console.WriteLine("> ERREUR: La format de la date est invalide.\n");
                } while (true);

                //vérification si la séance existe déjà
                if (dao.isSeanceExiste(date))
                {
                    Console.WriteLine("\n> ATTENTION: La séance choisi existe déjà,\njuste essayer d'ajouter des films à la séance existante.");
                    return;
                }
                else
                    break;
            } while (true);

            //récuperation du film
            int id_film;
            do
            {
                do
                {
                    Console.Write("\nLe film (ex: 15) : ");
                    if (Int32.TryParse(Console.ReadLine(), out id_film))
                        break;
                    else
                        Console.WriteLine("> ERREUR: les données saisi ne sont pas un chiffre valid.\n");

                } while (true);

                //vérification si le film existe déjà
                if (!dao.isFilmExiste(id_film))
                    Console.WriteLine("\n> ATTENTION: Le film choisi n'existe pas!");
                else
                    break;
            } while (true);


            //récuperation de la salle
            int id_salle;
            do
            {
                do
                {
                    Console.Write("\nLa salle (ex: 2) : ");
                    if (Int32.TryParse(Console.ReadLine(), out id_salle))
                        break;
                    else
                        Console.WriteLine("> ERREUR: les données saisi ne sont pas un chiffre valid.\n");

                } while (true);
                //vérification si la salle existe deja
                if (!dao.isSalleExiste(id_salle))
                    Console.WriteLine("\n> ATTENTION: La salle choisi n'existe pas!");
                else
                    break;
            }while(true);

            //récuperation de l'horaire
            string horaire = "";
            do
            {
                Console.Write("\nHoraire du film (ex: 19:30) : ");
                horaire = Console.ReadLine();
                if (new Regex("^\\d{1,2}:\\d{1,2}$").IsMatch(horaire))
                {
                    horaire+=":00";
                    break;
                }
                else
                    Console.WriteLine("> ERREUR: La format de l'horaire est invalide.\n");
            } while (true);

            //Créer la séance de projection
            if (dao.creerSeanceProjection(new Visionnage_Salle(id_film, id_salle, date, horaire, dao.findSalleById(id_salle).nbPlaces)))
                Console.WriteLine("> La séance de projection est crée avec succes.\n");
            else
                Console.WriteLine("> La séance de projection n'est pas crée!\n");
        }

        public void ajouterFilmAuSeanceProjection()
        {
            //récuperation de la date
            string date = "";
            do
            {
                do
                {
                    Console.Write("\nLa séance du (ex: 2018-11-8) : ");
                    date = Console.ReadLine();
                    if (new Regex("^\\d{4}-\\d{1,2}-\\d{1,2}$").IsMatch(date))
                        break;
                    else
                        Console.WriteLine("> ERREUR: La format de la date est invalide.\n");
                } while (true);

                //vérification si la séance existe déjà
                if (!dao.isSeanceExiste(date))
                    Console.WriteLine("\n> ATTENTION: La séance choisi n'existe pas!");
                else
                    break;
            } while (true);

            //récuperation du film
            int id_film;
            do
            {
                do
                {
                    Console.Write("\nLe film (ex: 15) : ");
                    if (Int32.TryParse(Console.ReadLine(), out id_film))
                        break;
                    else
                        Console.WriteLine("> ERREUR: les données saisi ne sont pas un chiffre valid.\n");

                } while (true);

                //vérification si le film existe déjà
                if (!dao.isFilmExiste(id_film))
                    Console.WriteLine("\n> ATTENTION: Le film choisi n'existe pas!");
                else
                    break;
            } while (true);


            //récuperation de la salle
            int id_salle;
            do
            {
                do
                {
                    Console.Write("\nLa salle (ex: 2) : ");
                    if (Int32.TryParse(Console.ReadLine(), out id_salle))
                        break;
                    else
                        Console.WriteLine("> ERREUR: les données saisi ne sont pas un chiffre valid.\n");

                } while (true);

                //vérification si la salle existe deja
                if (!dao.isSalleExiste(id_salle))
                    Console.WriteLine("\n> ATTENTION: La salle choisi n'existe pas!");
                else
                    break;
            } while (true);

            //récuperation de l'horaire
            string horaire = "";
            do
            {
                Console.Write("\nHoraire du film (ex: 19:30) : ");
                horaire = Console.ReadLine();
                if (new Regex("^\\d{1,2}:\\d{1,2}$").IsMatch(horaire))
                {
                    horaire += ":00";
                    break;
                }
                else
                    Console.WriteLine("> ERREUR: La format de l'horaire est invalide.\n");
            } while (true);

            //Ajout du film à la séance de projection
            if (dao.creerSeanceProjection(new Visionnage_Salle(id_film, id_salle, date, horaire, dao.findSalleById(id_salle).nbPlaces)))
                Console.WriteLine("> Le visionnage a été ajouté à la séance de projection avec succes.\n");
            else
                Console.WriteLine("> ATTENTION: Ce visionnage dans cette salle est déjà affécté à cette séance de projection,\nessayer avec d'autres films, salles ou séances de projections.");
        }
          
        public void afficherListeSalles()
        {
            List<Salle> liste = dao.getListeSalles();
            if (liste == null || liste.Count == 0)
                Console.WriteLine("> La liste est vide ! ");
            else
            {
                int c = 1;
                Console.WriteLine("\n");
                foreach (Salle s in liste)
                {
                    Console.WriteLine("No:"+s.id+" - Nombre de places : " + s.nbPlaces);
                    c++;
                }
            }
        }

        
        /* Espace visiteur */

        public void visiter(int id_visiteur)
        {
            char choix = '#';
            do
            {
                Console.WriteLine("\n\t  --- Menu visiteur ---\n");
                Console.WriteLine("\t1) Trouver un visionnage.");
                Console.WriteLine("\t2) S'inscrire à un visionnage.");
                Console.WriteLine("\n\t0) Quitter l'application.");
                Console.Write("\n\nVotre choix : ");
                choix = Console.ReadKey().KeyChar;
                switch (choix)
                {
                    case '0':
                        System.Environment.Exit(1); break;
                    case '1':
                        trouverVisionnage(); break;
                    case '2':
                        iscriptionVisionnage(id_visiteur); break;

                    default:
                        Console.WriteLine("\n> ATTENTION: Choix hors liste! essayer de nouveau.\n\n");
                        choix = '#';
                        break;
                }
            }
            while (choix == '#');

            visiter(id_visiteur);
        }

        public void trouverVisionnage()
        {
            Console.Write("\nTapper une fraction de son nom, réalisateur ou producteur : ");
            string nom = Console.ReadLine();
            List<string> liste = dao.getVisionnagesActuels(nom);

            if (liste == null || liste.Count == 0)
                Console.WriteLine("> La liste est vide ! ");
            else
                foreach (string s in liste)
                    Console.WriteLine(s+"\n");
        }

        public void iscriptionVisionnage(int id_visiteur)
        {
            //la séance choisi
            string date = "";
            do
            {
                do
                {
                    Console.Write("\nLa date de la séance choisi (ex: 2018-11-8) : ");
                    date = Console.ReadLine();
                    if (new Regex("^\\d{4}-\\d{1,2}-\\d{1,2}$").IsMatch(date))
                        break;
                    else
                        Console.WriteLine("> ERREUR: La format de la date est invalide.\n");
                } while (true);

                //vérification si la séance existe déjà
                if (!dao.isSeanceExiste(date))
                    Console.WriteLine("\n> ATTENTION: La séance choisi n'existe pas!");
                else
                    break;
            } while (true);

            //le film choisi
            int id_film;
            do
            {
                do
                {
                    Console.Write("\nLe film choisi (ex: 15) : ");
                    if (Int32.TryParse(Console.ReadLine(), out id_film))
                        break;
                    else
                        Console.WriteLine("> ERREUR: les données saisi ne sont pas un chiffre valid.\n");

                } while (true);

                //vérification si le film existe déjà
                if (!dao.isFilmExisteInSeance(id_film, date))
                    Console.WriteLine("\n> ATTENTION: Le film n'existe pas dans la séance choisi!");
                else
                    break;
            } while (true);

            //la salle choisi
            int id_salle;
            do
            {
                do
                {
                    Console.Write("\nLa salle (ex: 2) : ");
                    if (Int32.TryParse(Console.ReadLine(), out id_salle))
                        break;
                    else
                        Console.WriteLine("> ERREUR: les données saisi ne sont pas un chiffre valid.\n");

                } while (true);

                //vérification si la salle existe déjà
                if (!dao.isFilmExisteInSeanceAndSalle(id_film, date, id_salle))
                    Console.WriteLine("\n> ATTENTION: Le film n'existe pas dans la salle choisi!");
                else
                    break;
            } while (true);

            //Vérification des places dispo
            if (!dao.isPlacesDisponible(id_film, date, id_salle))
                Console.WriteLine("> Nous sommes désolé, les tickets pour ce film sont épuisés.");
            //Vérification si déjà inscrit
            else if (dao.isDejaInscrit(id_film, id_visiteur, date))
                Console.WriteLine("> ATTENTION: Vous-êtes déjà inscrit à ce film.");
            else
            {
                //Inscription
                if (dao.ajouterIscriptionVisionnage(id_film, id_visiteur, date, id_salle))
                    Console.WriteLine("> Inscription réussie.\n");
                else
                    Console.WriteLine("> ATTENTION: Inscription échouée!");
            }
            
        }
    }
}
