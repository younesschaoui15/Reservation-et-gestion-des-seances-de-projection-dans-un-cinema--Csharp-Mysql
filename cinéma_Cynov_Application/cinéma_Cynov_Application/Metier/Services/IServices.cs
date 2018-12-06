using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.IO;
using System.Data;
using Cinema_Cynov_Application.Metier.Config;
using Cinema_Cynov_Application.Metier.Entites;
using System.Security.Cryptography;

namespace Cinema_Cynov_Application.Metier.Services
{
    public interface IServices
    {
        /** Les fonctions **/

        void administrer();
        char getTypeUtilisateur();
        object authentification(char userType);
        void creerCompteVisiteur();
        void gererSeancesProjection();
        void ajouterFilmAuSeanceProjection();
        void ajouterSeanceProjection();
        void afficherListeSalles();
        void afficherListeFilms();
        void afficherListeSeancesProjection();

        void visiter(int id_visiteur);
        void trouverVisionnage();
        void iscriptionVisionnage(int id_visiteur);

    }

}
