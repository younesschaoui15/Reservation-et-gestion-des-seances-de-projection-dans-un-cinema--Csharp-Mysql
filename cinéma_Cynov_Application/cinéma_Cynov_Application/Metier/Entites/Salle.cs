using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cinema_Cynov_Application.Metier.Entites
{
    public class Salle
    {
        public int id { get; set; }
        public int nbPlaces { get; set; }

        public Salle() { }
        public Salle(int id, int nb)
        {
            this.id = id;
            this.nbPlaces = nb;
        }

    }
}
