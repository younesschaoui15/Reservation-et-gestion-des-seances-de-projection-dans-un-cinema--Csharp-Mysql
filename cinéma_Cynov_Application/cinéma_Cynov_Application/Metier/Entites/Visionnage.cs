using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Cinema_Cynov_Application.Metier.Entites
{
    public class Visionnage
    {
        public int id{get; set;}
        public string nom { get; set; }
        public string realisateur { get; set; }
        public string producteur { get; set; }
        public string type { get; set; }
        public bool is_3d { get; set; }
        public bool is_original { get; set; }

        public Visionnage() { }
        public Visionnage(int id, string nom, string realisateur=null, string producteur=null, string type="Long métrage", bool is_3d=false, bool is_original=true)
        {
            this.id = id;
            this.nom = nom;
            this.realisateur = realisateur;
            this.producteur = producteur;
            this.type = type;
            this.is_3d = is_3d;
            this.is_original = is_original;
        }

        public override string ToString()
        {
            string _3d = "NON", _org = "NON";
            if (is_3d) _3d = "OUI";
            if (is_original) _org = "OUI";
            return nom+", Réalisateur: "+realisateur+", Producteur: "+producteur+" ("+type+", 3D:"+_3d+", Original:"+_org+")";
        }
    }
}
