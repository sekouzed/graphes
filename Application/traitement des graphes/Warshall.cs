using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace traitement_des_graphes
{
   public class Warshall
    {
       public static bool[][] fermeture_transitive()
       {
           int k, i, j, n=GrapheBO.graphe.Nombre_de_noeud;
           bool[][] matrice_temporaire = new bool[n][];

           for (i = 0; i < n; i++)
           {
               matrice_temporaire[i] = new bool[n];
               for (j = 0; j < n; j++)
                 matrice_temporaire[i][j] = (GrapheBO.graphe.Matrice[i][j] > 0) ? true : false;
           }

           for (k = 0; k < n; k++)
               for (i = 0; i < n; i++)
                   for (j = 0; j < n; j++)
                       matrice_temporaire[i][j] = matrice_temporaire[i][j] || (matrice_temporaire[i][k] && matrice_temporaire[k][j]);
           return matrice_temporaire;
       }

    }
}
