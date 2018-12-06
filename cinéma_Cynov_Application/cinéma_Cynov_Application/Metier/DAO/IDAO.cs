using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Cinema_Cynov_Application.Metier.Entites;
using Cinema_Cynov_Application.Metier.Config;

namespace Cinema_Cynov_Application.Metier.DAO
{
    public interface IDAO
    {
        bool isAdminExiste(string username, string password);
        bool isVisiteurExiste(string username, string password);
        bool isFilmExiste(int id_film);
        bool isSalleExiste(int id_salle);
        bool isSeanceExiste(string date);
        bool isFilmExisteInSeanceAndSalle(int id_film, string date, int id_salle);
        bool isFilmExisteInSeance(int id_film, string date);
        bool isDejaInscrit(int id_film, int id_visiteur, string date);

        Admin findAdminByEmail(string email);
        Visiteur findVisiteurByEmail(string email);
        Salle findSalleById(int id);

        /* Admin */
        bool creerCompteVisiteur(Visiteur x);
        bool creerCompteAdmin(Admin x);
        bool ajouterVisionnage(Visionnage v);
        bool creerSeanceProjection(Visionnage_Salle v_s);
        List<string> getListeSeancesProjection();
        List<Visionnage> getListeFilms();
        List<Salle> getListeSalles();

        /* Visiteur */
        bool ajouterIscriptionVisionnage(int id_film, int id_visiteur, string date_seance, int salle);
        List<string> getVisionnagesActuels(string attribut);
        bool isPlacesDisponible(int id_film, string date, int id_salle);
    }
}
