using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace traitement_des_graphes
{
    [Serializable]
    public class Graphe
    {
        private bool _graphe_orienter;

        public bool Graphe_orienter
        {
            get { return _graphe_orienter; }
            set { _graphe_orienter = value; }
        }

        private int _Nombre_de_noeud;
        public  int Nombre_de_noeud
        {
            get { return _Nombre_de_noeud; }
            set { _Nombre_de_noeud = value; }
        }

        private sommet[] _table_de_sommets;//liste d'adjacence
        public  sommet[] Table_de_sommets
        {
            get { return _table_de_sommets; }
            set { _table_de_sommets = value; }
        }

        int[][] _matrice;//matrice d'adjacence

        public int[][] Matrice
        {
            get { return _matrice; }
            set { _matrice = value; }
        }

        public Graphe()
        {
 
        }
    }
}
