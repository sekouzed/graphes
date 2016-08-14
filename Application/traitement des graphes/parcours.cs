using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace traitement_des_graphes
{
   public class parcours
    {
       
        public static void en_profondeur(ref List<sommet> DFS, sommet s)
        {
            s.Marquer = true;
            DFS.Add(s);
            foreach (liaison v in s.Liaisons_sortants)
            {
                if (!v.Sommet_lier.Marquer)
                    en_profondeur(ref DFS, v.Sommet_lier);
            }

        }

        public static void en_largeur(ref List<sommet> BFS, sommet s)
        {
            Queue<sommet> maFile =new Queue<sommet>();
            s.Marquer = true;
            maFile.Enqueue(s);
            while (maFile.Count != 0)
            {
                sommet x = new sommet();
                x = maFile.Dequeue();
                BFS.Add(x);
                foreach (liaison v in x.Liaisons_sortants)
                {
                    if (!v.Sommet_lier.Marquer)
                    {
                        v.Sommet_lier.Marquer = true;
                        maFile.Enqueue(v.Sommet_lier);
                    }
                }

            }
        }
    
    }
}
