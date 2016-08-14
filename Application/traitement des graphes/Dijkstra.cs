using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace traitement_des_graphes
{
   public class Dijkstra
    {
        static List<sommet> les_stations=new List<sommet>();
      
        //ouverture du Sommet de cout minimum parmis les Sommets non visites
        public static sommet ouvre_min(List<sommet> liste)
        {
            sommet res = new sommet();
            res.Cout=int.MaxValue;
            foreach (sommet s in liste)
                {
                    if (s.Cout <= res.Cout)
                        res = s;
                }
            liste.Remove(res);
            return res;
        }
        //l'algo de Dijkstra proprement dit
        public static List<sommet> plus_court_chemin(sommet depart, sommet destination)
        {
           for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)    
            {
                les_stations.Add( GrapheBO.graphe.Table_de_sommets[i]);
            }

            List<sommet> chemin = new List<sommet>();
            List<sommet> Sommets_a_ouvrir = new List<sommet>();
            sommet Sommet_ouvert = new sommet(); ;
            //on initialise les couts à l'infini pour tous les Sommets 
            foreach (sommet sta in les_stations)
                {
                    sta.Cout=int.MaxValue;
                    sta.precedant=null;
                    Sommets_a_ouvrir.Add(sta);
                }
            depart.Cout=0;//evidemment
            Sommet_ouvert = ouvre_min(Sommets_a_ouvrir);
            //on verifie qu'il existe au moins un arc liant le Sommet ouvert aux Sommets restants et que l'on n'est pas à l'arrivee
            
            while (Sommet_ouvert != destination)
            {
                foreach (sommet s in Sommets_a_ouvrir)
                    {
                        int val=int.MaxValue;    
                        foreach (liaison l1 in Sommet_ouvert.Liaisons_sortants)
                                if (l1.Sommet_lier.Numero == s.Numero)
                                   val = Sommet_ouvert.Cout +l1.cout;
                       
                        if (s.Cout > val)
                            {
                               s.Cout=val;
                                 s.precedant=Sommet_ouvert;
                        }
                    }
                    Sommet_ouvert = ouvre_min(Sommets_a_ouvrir);

            }
            Sommets_a_ouvrir.Clear();
            if (destination.Cout == int.MaxValue)
            {
                //le graphe n'est pas connexe, il n y a pas de chemin
                 return null;
            }
            else
            //construction du chemin
            {
                while (Sommet_ouvert != depart)
                {
                    chemin.Add(Sommet_ouvert);
                    Sommet_ouvert = Sommet_ouvert.precedant;
                }


            }
            chemin.Add(depart);
            chemin.Reverse();
            return chemin;
        }
    }
}
