using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Cinema_Cynov_Application.Metier.Entites
{
    public class Admin
    {
        public int id{get; set;}
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string motDePasse { get; set; }

        public Admin() { }
        public Admin(int id, string nom, string prenom, string email, string password)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.email = email;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder md5_password = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                    md5_password.Append(hash[i].ToString("x2"));
                this.motDePasse = md5_password.ToString();
            }
        }
    }
}
