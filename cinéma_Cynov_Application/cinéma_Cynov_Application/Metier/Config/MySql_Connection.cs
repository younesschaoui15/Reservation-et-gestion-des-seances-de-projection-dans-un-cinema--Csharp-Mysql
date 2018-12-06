using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Cinema_Cynov_Application.Metier.Config
{
    class MySql_Connection
    {
        public MySqlConnection mySqlConn{ get; set;}
       
        /** Paramétres de la base de données **/
        const string server = "localhost";
        const string dataBase = "bd_cinema_cynov";
        const string user = "root";
        const string connexionString = "Data Source="+server+";Initial Catalog="+dataBase+";User Id="+user+";";

        
        /** Constructeurs & méthodes **/

        public MySql_Connection()
        {
            try
            {
                mySqlConn = new MySqlConnection(connexionString);
            }
            catch (Exception e)
            {
               Console.WriteLine("ERREUR x01 : Problème de connexion.\n");
            }
        }

        public bool openConnection()
        {
            try
            {
                mySqlConn.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERREUR x02 : Connexion interrompu à l'overture.\n");
                return false;
            }
            
        }

        public bool closeConnection()
        {
            try
            {
                mySqlConn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERREUR x03 : Connexion ne peut être fermée.\n");
                return false;
            }

        }

        public MySqlDataReader selectCommande(String sql)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand(sql, mySqlConn);
                MySqlDataReader dataReader = commande.ExecuteReader();
                return dataReader;
            }
            catch(Exception e)
            {
                Console.WriteLine("ERREUR x04 : Commande SQL de selection n'est pas passée.\n\nMessage d'erreur : " + e.Message);
                //Console.WriteLine("ERREUR x04 : Commande SQL de selection n'est pas passée.\n");
                return null;
            }            
        }

        public int executeCommande(String sql)
        {
            int count = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand(sql, mySqlConn);
                count = commande.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Console.WriteLine("ERREUR x05 : Ordre SQL n'a pas été exécuté.\n\nMessage Message d'erreur : " + e.Message);
                Console.WriteLine("ERREUR x05 : Ordre SQL n'a pas été exécuté.\n");
            }

            return count;
        }

    }

}
