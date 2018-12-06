using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema_Cynov_Application.Metier.Entites
{
    public class Visionnage_Salle
    {
        public int id_visionnage { get; set; }
        public int id_salle { get; set; }
        public string date { get; set; }
        public string horaire { get; set; }
        public int nbPlacesDispo { get; set; }

        public Visionnage_Salle() { }
        public Visionnage_Salle(int id_visionnage, int id_salle, string date, string horaire, int nbPlacesDispo)
        {
            this.id_visionnage = id_visionnage;
            this.id_salle = id_salle;
            this.date = date;
            this.horaire = horaire;
            this.nbPlacesDispo = nbPlacesDispo;
        }
    }
}
