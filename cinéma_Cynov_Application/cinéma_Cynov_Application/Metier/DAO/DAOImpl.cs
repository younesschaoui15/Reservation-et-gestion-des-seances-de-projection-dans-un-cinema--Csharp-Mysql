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

namespace Cinema_Cynov_Application.Metier.DAO
{
    public class DAOImpl : IDAO
    {
        static MySql_Connection mySqlConn;
        
        /** Les fonctions **/

        public bool isAdminExiste(string email, string password)
        {
            bool is_existe = false;
            StringBuilder md5_password = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                for (int i = 0; i < hash.Length; i++)
                    md5_password.Append(hash[i].ToString("x2"));
            }
            string sql = "SELECT COUNT(*) FROM admin WHERE email ='" + email.Replace("'", "''") + "' AND password='" + md5_password.ToString().Replace("'", "''") + "'";
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            
            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count == 1)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }
        public bool isVisiteurExiste(string email, string password)
        {
            bool is_existe = false;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();

            StringBuilder md5_password = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < hash.Length; i++)
                    md5_password.Append(hash[i].ToString("x2"));
            }
            string sql = "SELECT COUNT(*) FROM Visiteur WHERE email ='" + email.Replace("'", "''") + "' AND password='" + md5_password.ToString().Replace("'", "''") + "'";
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count == 1)
                        is_existe = true;
                }
            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }
        
        public Admin findAdminByEmail(string email)
        {
            Admin admin = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT * FROM admin WHERE email ='" + email.Replace("'", "''") + "';";
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader.Read())
                {
                    admin = new Admin();

                    admin.id = dataReader.GetInt32(0);
                    admin.nom = dataReader[1].ToString().ToUpper();
                    admin.prenom = char.ToUpper(dataReader[2].ToString().First()) + dataReader[2].ToString().Substring(1).ToLower();
                    admin.email = dataReader[3].ToString();
                    admin.motDePasse = dataReader[4].ToString();
                }
            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return admin;
        }
        public Visiteur findVisiteurByEmail(string email)
        {
            Visiteur visiteur = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT * FROM Visiteur WHERE email ='" + email.Replace("'", "''") + "';";
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader.Read())
                {
                    visiteur = new Visiteur();

                    visiteur.id = dataReader.GetInt32(0);
                    visiteur.nom = dataReader[1].ToString().ToUpper();
                    visiteur.prenom = char.ToUpper(dataReader[2].ToString().First()) + dataReader[2].ToString().Substring(1).ToLower();
                    visiteur.email = dataReader[3].ToString();
                    visiteur.motDePasse = dataReader[4].ToString();
                }
            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return visiteur;
        }
        public Salle findSalleById(int id)
        {
            Salle salle = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT * FROM salle WHERE id = " + id;
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader.Read())
                {
                    salle = new Salle();

                    salle.id = dataReader.GetInt32(0);
                    salle.nbPlaces = dataReader.GetInt32(1);
                }
            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return salle;
        }

        /* Admin */
        public bool creerCompteVisiteur(Visiteur e)
        {
            mySqlConn = new MySql_Connection();
            int count = 0;

            string sql = "INSERT INTO Visiteur VALUES (NULL, '" + e.nom + "', '" + e.prenom + "', '" + e.email + "', '" + e.motDePasse + "');";

            mySqlConn.openConnection();
            count = mySqlConn.executeCommande(sql);
            mySqlConn.closeConnection();

            if (count > 0)
                return true;
            else
                return false;
        }
        
        public bool creerCompteAdmin(Admin e)
        {
            mySqlConn = new MySql_Connection();
            int count = 0;

            string sql = "INSERT INTO Admin VALUES (NULL, '" + e.nom + "', '" + e.prenom + "', '" + e.email + "', '" + e.motDePasse + "');";

            mySqlConn.openConnection();
            count = mySqlConn.executeCommande(sql);
            mySqlConn.closeConnection();

            if (count > 0)
                return true;
            else
                return false;
        }
        
        public bool ajouterVisionnage(Visionnage x)
        {
            mySqlConn = new MySql_Connection();
            int count = 0;

            string sql = "INSERT INTO Visionnage VALUES (NULL, '" + x.nom + "', '" + x.realisateur+ "', '" + x.producteur + "', '" + x.type + "', "+x.is_3d+", "+x.is_original+");";

            mySqlConn.openConnection();
            count = mySqlConn.executeCommande(sql);
            mySqlConn.closeConnection();

            if (count > 0)
                return true;
            else
                return false;
        }

        public List<string> getListeSeancesProjection()
        {
            List<string> liste = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT distinct date FROM Visionnage_Salle where date >= CURDATE();";
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader != null)
                    liste = new List<string>();
                while (dataReader.Read())
                {
                    string date = dataReader[0].ToString().Substring(0, 10);

                    liste.Add(date);
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }

            return liste;
        }

        public List<Visionnage> getListeFilms()
        {
            List<Visionnage> liste = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT * FROM Visionnage;";
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader != null)
                    liste = new List<Visionnage>();
                while (dataReader.Read())
                {
                    Visionnage v = new Visionnage();
                    v.id = dataReader.GetInt32(0);
                    v.nom = dataReader.GetString(1);
                    v.realisateur = dataReader.GetString(2);
                    v.producteur = dataReader.GetString(3);
                    v.type = dataReader.GetString(4);
                    v.is_3d = dataReader.GetBoolean(5);
                    v.is_original = dataReader.GetBoolean(6);

                    liste.Add(v);
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }

            return liste;
        }

        public List<Salle> getListeSalles()
        {
            List<Salle> liste = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT * FROM Salle;";
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader != null)
                    liste = new List<Salle>();
                while (dataReader.Read())
                {
                    Salle v = new Salle();
                    v.id = dataReader.GetInt32(0);
                    v.nbPlaces = dataReader.GetInt32(1);

                    liste.Add(v);
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }

            return liste;
        }

        public bool isFilmExiste(int id_film)
        {
            bool is_existe = false;
            string sql = "SELECT COUNT(*) FROM visionnage WHERE id = " + id_film;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count >0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }

        public bool isSeanceExiste(string date)
        {
            bool is_existe = false;
            string sql = "SELECT COUNT(*) FROM visionnage_salle WHERE date = '" + date+"';";
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count > 0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }

        public bool isSalleExiste(int id_salle)
        {
            bool is_existe = false;
            string sql = "SELECT COUNT(*) FROM salle WHERE id = "+id_salle;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count > 0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }

        public bool creerSeanceProjection(Visionnage_Salle v_s)
        {
            mySqlConn = new MySql_Connection();
            int count = 0;

            string sql = "INSERT INTO Visionnage_salle VALUES (" + v_s.id_visionnage + ", " + v_s.id_salle + ", '" + v_s.date + "', '" + v_s.horaire + "', "+v_s.nbPlacesDispo+");";

            mySqlConn.openConnection();
            count = mySqlConn.executeCommande(sql);
            mySqlConn.closeConnection();

            if (count > 0)
                return true;
            else
                return false;
        }

        
        /* Visiteur */

        public List<string> getVisionnagesActuels(string att)
        {
            List<string> liste = null;
            mySqlConn = new MySql_Connection();
            string sql = "SELECT * FROM Visionnage v, visionnage_salle vv where v.id = vv.id_visionnage "+
                         "and vv.date>=CURRENT_DATE and (v.nom like '%"+att+"%' or v.realisateur like '%"+att+"%' or v.producteur like '%"+att+"%') "+
                         "ORDER BY vv.date;";
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);
            try
            {
                if (dataReader != null)
                    liste = new List<string>();
                while (dataReader.Read())
                {
                    //id nom realisateur producteur type is_3d is_original id_visionnage id_salle date horaire nbPlacesDispo
                    string s;
                    s = "-> No film: " + dataReader.GetInt32(0) + ", Titre: " + dataReader.GetString(1)+"\n";
                    s += "   Par: " + dataReader.GetString(2) + ", Prod: " + dataReader.GetString(3)+"\n";
                    s += "   ( " + dataReader.GetString(4);
                    if (dataReader.GetBoolean(5))
                        s += ", 3D:OUI";
                    else
                        s += ", 3D:NON";
                    if (dataReader.GetBoolean(6))
                        s += ", Original:OUI )\n";
                    else
                        s += ", Original:NON )\n";
                    s += "   Séance: " + dataReader.GetDateTime(9).ToShortDateString() + " à " + dataReader.GetString(10) + "\n";

                    s += "   Salle: " + dataReader.GetInt32(8) + " ( Places disponibles: " + dataReader.GetInt32(11)+" )";

                    liste.Add(s);
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }

            return liste;
        }

        public bool ajouterIscriptionVisionnage(int id_film, int id_visiteur, string date_seance, int salle)
        {
            mySqlConn = new MySql_Connection();
            int count = 0;

            string sql = "INSERT INTO Visionnage_visiteur VALUES (" + id_film + "," + id_visiteur + ", '" + date_seance + "');";
            string sql2 = "update visionnage_salle set nbPlacesDispo = nbPlacesDispo-1 where id_visionnage = "+id_film+" and date = '"+date_seance+"' and id_salle = "+salle+";";
            mySqlConn.openConnection();
            count = mySqlConn.executeCommande(sql);

            if (count > 0)
            {
                count = mySqlConn.executeCommande(sql2);
                if (count > 0)
                {
                    mySqlConn.closeConnection();
                    return true;
                }
                else
                {
                    mySqlConn.closeConnection();
                    return false;
                }
            }
            else
            {
                mySqlConn.closeConnection();
                return false;
            }
        }

        public bool isFilmExisteInSeance(int id_film, string date)
        {
            bool is_existe = false;
            string sql = "SELECT COUNT(*) FROM visionnage_salle WHERE date = '" + date + "' and id_visionnage = "+id_film;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count > 0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }

        public bool isFilmExisteInSeanceAndSalle(int id_film, string date, int id_salle)
        {
            bool is_existe = false;
            string sql = "SELECT COUNT(*) FROM visionnage_salle WHERE date = '" + date + "' and id_visionnage = " + id_film+" and id_salle = "+id_salle;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count > 0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }

        public bool isPlacesDisponible(int id_film, string date, int id_salle)
        {
            bool is_existe = false;
            string sql = "SELECT nbPlacesDispo FROM visionnage_salle WHERE date = '" + date + "' and id_visionnage = " + id_film + " and id_salle = " + id_salle;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count > 0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }

        public bool isDejaInscrit(int id_film, int id_visiteur, string date)
        {
            bool is_existe = false;
            string sql = "SELECT COUNT(*) FROM visionnage_visiteur WHERE date = '" + date + "' and id_visionnage = " + id_film+" and id_visiteur= "+id_visiteur;
            mySqlConn = new MySql_Connection();
            mySqlConn.openConnection();
            MySqlDataReader dataReader = mySqlConn.selectCommande(sql);

            try
            {
                if (dataReader.Read())
                {
                    int count = dataReader.GetInt32(0);
                    if (count >0)
                        is_existe = true;
                }

            }
            finally
            {
                mySqlConn.closeConnection();
            }
            return is_existe;
        }
    }

}
