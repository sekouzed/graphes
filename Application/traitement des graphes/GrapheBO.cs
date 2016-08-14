using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace traitement_des_graphes
{
    public class GrapheBO
    {
        public static List<sommet> liste_de_sommets = new List<sommet>();
        public static Graphe graphe = new Graphe();

        public static void supprimer(int numero)
        {
            int indice_A_supprimer = -1;

            for (int i = 0; i < graphe.Nombre_de_noeud; i++)
            {
                foreach (liaison p in graphe.Table_de_sommets[i].Liaisons_sortants)
                    if (p.Sommet_lier.Numero == numero)
                    {
                        graphe.Table_de_sommets[i].Liaisons_entrants.Remove(p); break;
                    }

                foreach (liaison q in graphe.Table_de_sommets[i].Liaisons_sortants)
                    if (q.Sommet_lier.Numero == numero)
                    {
                        graphe.Table_de_sommets[i].Liaisons_sortants.Remove(q); break;
                    }

                if (graphe.Table_de_sommets[i].Numero == numero)
                {
                    indice_A_supprimer = i;
                    graphe.Table_de_sommets[i].Liaisons_sortants.Clear();
                    graphe.Table_de_sommets[i].Liaisons_entrants.Clear();
                }
            }
            graphe.Nombre_de_noeud--;
            for (int i = indice_A_supprimer; i < graphe.Nombre_de_noeud; i++)
            {
                graphe.Table_de_sommets[i] = graphe.Table_de_sommets[i + 1];
                graphe.Matrice[i] = graphe.Matrice[i + 1];
            }

            for (int i = 0; i < graphe.Nombre_de_noeud; i++)
            {
                for (int j = indice_A_supprimer; j < graphe.Nombre_de_noeud; j++)
                {
                    graphe.Matrice[i][j] = graphe.Matrice[i][j + 1];
                }
            }
        }

        public static void serialiser(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, graphe);
            stream.Close();
        }

        public static void deserialiser(string filename)
        {
            Graphe mygraf;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            mygraf = (Graphe)bFormatter.Deserialize(stream);
            stream.Close();
            graphe.Graphe_orienter = mygraf.Graphe_orienter;
            graphe.Nombre_de_noeud = mygraf.Nombre_de_noeud;
            graphe.Table_de_sommets = mygraf.Table_de_sommets;
            graphe.Matrice = mygraf.Matrice;
        }
    }
}
