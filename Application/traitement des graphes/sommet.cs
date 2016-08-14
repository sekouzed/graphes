using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace traitement_des_graphes
{
    [Serializable]
    public class sommet
        {
            private int _numero;
            public int Numero
            {
                get { return _numero; }
                set { _numero = value; }
            }

            private int _x, _y;
            public int X
            {
                get { return _x; }
                set { _x = value; }
            }
            public int Y
            {
                get { return _y; }
                set { _y = value; }
            }

            private int _cout;
            public int Cout
            {
                get { return _cout; }
                set { _cout = value; }
            }

            public sommet precedant;
            private bool _marquer = false;
            public bool Marquer
            {
                get { return _marquer; }
                set { _marquer = value; }
            }

            private List<liaison> _Liaisons_sortants;
            public List<liaison> Liaisons_sortants
            {
                get { return _Liaisons_sortants; }
                set { _Liaisons_sortants = value; }
            }

            private List<liaison> _Liaisons_entrants;
            public List<liaison> Liaisons_entrants
            {
                get { return _Liaisons_entrants; }
                set { _Liaisons_entrants = value; }
            }

            public sommet()
            {
                _x = 0; _y = 0; _numero = 0; _cout = -1;
                _Liaisons_sortants = new List<liaison>();
                _Liaisons_entrants = new List<liaison>();
            }
            public sommet(int _numero1, int _x1, int _y1, int _cout1)
            {
                _x = _x1; _y = _y1; _numero = _numero1; _cout = _cout1;
                _Liaisons_sortants = new List<liaison>();
                _Liaisons_entrants = new List<liaison>();
            }
        }
  
}
