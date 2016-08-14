using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using traitement_des_graphes;
using System.IO;

namespace forms_des_graphes
{
    public partial class FormRapport : Form
    {
        public FormRapport()
        {
            InitializeComponent();
        }

        private void FormRapport_Load(object sender, EventArgs e)
        {
            if(GrapheBO.graphe.Graphe_orienter)
                 richTextBox1.Text = "TYPE : GRAPHE ORIENTE";
            else
                richTextBox1.Text = "TYPE : GRAPHE NON ORIENTE";

            richTextBox1.Text += "\n\nLE NOMBRE DE NOEUD : " + GrapheBO.graphe.Nombre_de_noeud;
            richTextBox1.Text += "\n\nMATRICE D'ADJACENCE :\n\n";
            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud - 1; i++)
            {
                for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud - 1; j++)
                {
                    richTextBox1.Text += GrapheBO.graphe.Matrice[i][j] + " ";
                }
                richTextBox1.Text += GrapheBO.graphe.Matrice[i][GrapheBO.graphe.Nombre_de_noeud - 1] + "\n";
            }

            for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud - 1; j++)
            {
                richTextBox1.Text += GrapheBO.graphe.Matrice[GrapheBO.graphe.Nombre_de_noeud - 1][j] + " ";
            }
            richTextBox1.Text += GrapheBO.graphe.Matrice[GrapheBO.graphe.Nombre_de_noeud - 1][GrapheBO.graphe.Nombre_de_noeud - 1];

            richTextBox1.Text += "\n\nLISTE D'ADJACENCE  :\n";
            richTextBox1.Text += "\n---Liaison sortant---------------------------------------------------\n";
            for (int k = 0; k < GrapheBO.graphe.Nombre_de_noeud; k++)
            {
                richTextBox1.Text += k + "  : ";
                foreach (liaison N in GrapheBO.graphe.Table_de_sommets[k].Liaisons_sortants)
                {
                    richTextBox1.Text += " => " + N.Sommet_lier.Numero + "[" + N.cout + "]";
                }
                richTextBox1.Text += "\n";
            }

            richTextBox1.Text += "\n---Liaison entrant---------------------------------------------------\n";
            for (int k = 0; k < GrapheBO.graphe.Nombre_de_noeud; k++)
            {
                richTextBox1.Text += k + "  : ";
                foreach (liaison N in GrapheBO.graphe.Table_de_sommets[k].Liaisons_entrants)
                {
                    richTextBox1.Text += " => " + N.Sommet_lier.Numero + "[" + N.cout + "]";
                }
                richTextBox1.Text += "\n";
            }

            richTextBox1.Text += "\nPARCOURS EN PROFONDEUR :\n\n";
            List<sommet> DFS = new List<sommet>();
            parcours.en_profondeur(ref DFS, GrapheBO.graphe.Table_de_sommets[0]);
            foreach (sommet s in DFS)
            {
                richTextBox1.Text += " => " + s.Numero;
            }
            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)    
            {    
                GrapheBO.graphe.Table_de_sommets[i].Marquer=false;
            }

            richTextBox1.Text += "\n\nPARCOURS EN LARGEUR :\n\n";
            List<sommet> BFS = new List<sommet>();
            parcours.en_largeur(ref BFS, GrapheBO.graphe.Table_de_sommets[0]);
            foreach (sommet s in BFS)
            {
                richTextBox1.Text += " => " + s.Numero;
            }
               for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)    
            {    
                GrapheBO.graphe.Table_de_sommets[i].Marquer=false;
            }


            richTextBox1.Text += "\n\nMATRICE DE LA FERMETURE TRANSITIVE :\n\n";
            bool[][] nouvo_matrice = Warshall.fermeture_transitive();
            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
            {
                for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud; j++)
                {
                    if (nouvo_matrice[i][j])
                        richTextBox1.Text += "1 ";
                    else
                        richTextBox1.Text += "0 ";
                 }
                richTextBox1.Text += "\n";
            }

            richTextBox1.Text += "\n\nLES PLUS COURTS CHEMINS ENTRE LES NOEUDS:\n\n";
            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
            {
                for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud; j++)
                {
                    List<sommet> chemin_touver = new List<sommet>();
                    chemin_touver = Dijkstra.plus_court_chemin( GrapheBO.graphe.Table_de_sommets[i],GrapheBO.graphe.Table_de_sommets[j]);
                    richTextBox1.Text += "De " + GrapheBO.graphe.Table_de_sommets[i].Numero + " à " + GrapheBO.graphe.Table_de_sommets[j].Numero + " on a :";
                    foreach(sommet p in chemin_touver)
                        richTextBox1.Text += " => "+p.Numero;
                    richTextBox1.Text += "\n";
                } 
                richTextBox1.Text += "\n";
            }



        }

        private void bt_enregistrer_Click(object sender, EventArgs e)
        {

            saveFileDialog1.ShowDialog();
            StreamWriter ligne = new StreamWriter(saveFileDialog1.FileName);

            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                ligne.WriteLine(richTextBox1.Lines[i]);
            }
            ligne.Close();
            MessageBox.Show("Le detail été bien sauvegardé dans " + saveFileDialog1.FileName);
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}
