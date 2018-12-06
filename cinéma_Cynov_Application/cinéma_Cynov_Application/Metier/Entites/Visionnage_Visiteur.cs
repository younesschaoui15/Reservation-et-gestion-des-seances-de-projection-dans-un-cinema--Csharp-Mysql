using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema_Cynov_Application.Metier.Entites
{
    public class Visionnage_Visiteur
    {
        public int id_visionnage { get; set; }
        public int id_visiteur { get; set; }
        public DateTime date { get; set; }

        public Visionnage_Visiteur() { }
        public Visionnage_Visiteur(int id_visionnage, int id_visiteur, DateTime date)
        {
            this.id_visionnage = id_visionnage;
            this.id_visiteur = id_visiteur;
            this.date = date;
        }
    }
}
