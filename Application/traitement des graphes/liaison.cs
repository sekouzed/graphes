using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace traitement_des_graphes
{
    [Serializable]
    public class liaison
    {
        public int cout;
        private sommet _sommet_lier;
        public sommet Sommet_lier
        {
            get { return _sommet_lier; }
            set { _sommet_lier = value; }
        }
        public liaison()
        {
            _sommet_lier = null; cout = -1;
        }
        public liaison(ref sommet _sommet_suivant1, int _cout)
        {
            _sommet_lier = _sommet_suivant1; cout = _cout;
        }
    }
}
