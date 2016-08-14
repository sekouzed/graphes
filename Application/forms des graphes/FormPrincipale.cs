using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using traitement_des_graphes;
using System.Drawing.Drawing2D;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace forms_des_graphes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }      

        //----------------------------------------------------------------------------------------------
        #region shemas

        private Graphics g;
        private int Noeud,dep=-1;
        private bool deplacer = false;
        private bool lier = false;
        private Button bt_terminer;
        private Form form_Dialog;
        private Button bt_ok;
        private Button bt_Annuler;
        private ComboBox comboBox1_depart;
        private ComboBox comboBox1_destination;
        private CheckedListBox checkedListBox;
        private TextBox txt_lecout;
        private Label lab;
        private Label lab2;
        private CheckBox check_carte;

        private void form_initialise(int type)// si ajouter cou:type=0;si parcours hauteur:type=1;si parcours en largeur:type=2;
        {

            bt_ok = new Button();
            bt_Annuler = new Button();
            lab = new Label();

            lab.AutoSize = true;
            lab.BackColor = System.Drawing.Color.Transparent;
            lab.Location = new System.Drawing.Point(21, 33);
            lab.Size = new System.Drawing.Size(87, 13);
            lab.TabIndex = 0;

            bt_ok.Location = new System.Drawing.Point(215, 9);
            bt_ok.Size = new System.Drawing.Size(75, 61);
            bt_ok.TabIndex = 1;
            bt_ok.Text = "OK";
            bt_ok.UseVisualStyleBackColor = true;
            bt_ok.TabIndex = 1;
            bt_ok.DialogResult = DialogResult.OK;

            bt_Annuler.Location = new System.Drawing.Point(215, 76);
            bt_Annuler.Size = new System.Drawing.Size(75, 33);
            bt_Annuler.TabIndex = 2;
            bt_Annuler.Text = "Annuler";
            bt_Annuler.UseVisualStyleBackColor = true;
            bt_Annuler.DialogResult = DialogResult.Cancel;

            form_Dialog.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            form_Dialog.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form_Dialog.BackgroundImage = global::forms_des_graphes.Properties.Resources.Image231;
            form_Dialog.StartPosition = FormStartPosition.CenterScreen;
            form_Dialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            form_Dialog.ClientSize = new System.Drawing.Size(302, 118);
            form_Dialog.MaximumSize = new System.Drawing.Size(312, 150);
            form_Dialog.MinimumSize = new System.Drawing.Size(312, 150);
            form_Dialog.Controls.Add(bt_Annuler);
            form_Dialog.Controls.Add(bt_ok);
            form_Dialog.Controls.Add(lab);
            form_Dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            
            switch (type)
            {
                case 0:
                    txt_lecout = new TextBox();
                    txt_lecout.Location = new System.Drawing.Point(24, 50);
                    txt_lecout.Size = new System.Drawing.Size(84, 20);
                    txt_lecout.Text = "1";
                    lab.Text = "Cout de la liaison";
                    form_Dialog.Controls.Add(txt_lecout);
                    form_Dialog.Text = "Ajoute cout";
                    break;
                case 1:
                    form_Dialog.Text = "Parcours en hauteur";
                    lab.Text = "Sommet de Départ";
                    comboBox1_depart = new ComboBox();
                    comboBox1_depart.Focus();
                    comboBox1_depart.FormattingEnabled = true;
                    comboBox1_depart.Location = new System.Drawing.Point(24, 50);
                    comboBox1_depart.Size = new System.Drawing.Size(84, 20);
                    comboBox1_depart.Size = new System.Drawing.Size(lab.Width, 20);
                    for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                        comboBox1_depart.Items.Add(GrapheBO.graphe.Table_de_sommets[i].Numero);
                    comboBox1_depart.SelectedItem = comboBox1_depart.Items[0];
                    form_Dialog.Controls.Add(comboBox1_depart);
                    break;

                case 2:
                    form_Dialog.Text = "Parcours en largeur";
                    lab.Text = "Sommet de Départ";
                    comboBox1_depart = new ComboBox();
                    comboBox1_depart.FormattingEnabled = true;
                    comboBox1_depart.Location = new System.Drawing.Point(24, 50);
                    comboBox1_depart.Size = new System.Drawing.Size(84, 20);
                    for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                        comboBox1_depart.Items.Add(GrapheBO.graphe.Table_de_sommets[i].Numero);
                    comboBox1_depart.SelectedItem = comboBox1_depart.Items[0];
                    form_Dialog.Controls.Add(comboBox1_depart);

                    break;

                case 4: 
                    form_Dialog.Text = "Chemin optimal";
                    lab2 = new Label();
                    lab.Text = "Départ";
                    lab2.AutoSize = true;
                    lab2.BackColor = System.Drawing.Color.Transparent;
                    lab2.Location = new System.Drawing.Point(24+84, 33);
                    lab2.Size = new System.Drawing.Size(87, 13);
                    lab2.Text = "Arrivée";
                    form_Dialog.Controls.Add(lab2);
                    comboBox1_depart = new ComboBox();
                    comboBox1_depart.FormattingEnabled = true;
                    comboBox1_depart.Location = new System.Drawing.Point(24, 50);
                    comboBox1_depart.Size = new System.Drawing.Size(84, 20);
                    comboBox1_destination = new ComboBox();
                    comboBox1_destination.FormattingEnabled = true;
                    comboBox1_destination.Location = new System.Drawing.Point(24+84, 50);
                    comboBox1_destination.Size = new System.Drawing.Size(84, 20);

                    for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                    {
                        comboBox1_depart.Items.Add(GrapheBO.graphe.Table_de_sommets[i].Numero);
                        comboBox1_destination.Items.Add(GrapheBO.graphe.Table_de_sommets[i].Numero);
                    }
                    comboBox1_depart.SelectedItem = comboBox1_depart.Items[0];
                    comboBox1_destination.SelectedItem = comboBox1_depart.Items[0];
                    form_Dialog.Controls.Add(comboBox1_depart);
                    form_Dialog.Controls.Add(comboBox1_destination);
                    break;

                case 5:
                    form_Dialog.Text = "Suppression des Noeuds";
                    lab.Text = "Noeuds à supprimers";
                    lab.Location = new System.Drawing.Point(21, bt_ok.Location.Y);
                    checkedListBox = new CheckedListBox();
                    checkedListBox.FormattingEnabled = true;
                    checkedListBox.Location = new System.Drawing.Point(24, 24);
                    checkedListBox.Size = new System.Drawing.Size(lab.Width, 93);
                    for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                        checkedListBox.Items.Add("sommet "+GrapheBO.graphe.Table_de_sommets[i].Numero);
                    checkedListBox.SelectedItem = checkedListBox.Items[0];
                    form_Dialog.Controls.Add(checkedListBox);
                    break;

                case 6:
                    form_Dialog.Text = "Choix du type de graphe";
                    lab.Text = "Type de graphe";
                    check_carte = new CheckBox(); 
                    check_carte.AutoSize = true;
                    check_carte.BackColor = System.Drawing.Color.Transparent;
                    check_carte.Location = new System.Drawing.Point(27, 85);
                    check_carte.Size = new System.Drawing.Size(93, 17);
                    check_carte.TabIndex = 3;
                    check_carte.Text = "Carte itinéraire";
                    check_carte.UseVisualStyleBackColor = false;
                    form_Dialog.Controls.Add(check_carte);
                    comboBox1_depart = new ComboBox();
                    comboBox1_depart.Focus();
                    comboBox1_depart.FormattingEnabled = true;
                    comboBox1_depart.Location = new System.Drawing.Point(24, 50);
                    comboBox1_depart.Items.Add("Graphe orienté");
                    comboBox1_depart.Items.Add("Graphe non orienté");
                    comboBox1_depart.SelectedItem = comboBox1_depart.Items[0];
                    form_Dialog.Controls.Add(comboBox1_depart);
                    break;
                default: return;
            }
        }
        private void bt_terminer_Click(object sender, EventArgs e) 
        {
            this.panel1.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.lier_MouseClick);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);
            this.bt_terminer.Click -= new System.EventHandler(this.bt_terminer_Click);
            lab_Nombre_de_Noeud.Text = GrapheBO.graphe.Nombre_de_noeud + " sommet(s) ajouté(s)";
            panel1.Controls.Remove(bt_terminer);
            bt_terminer.Dispose();
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                if (item.Name == "tool_bt_actualiser")
                    continue;
                item.Enabled = true;
            }
            panel1.ContextMenuStrip = contextMenuStrip1;
            tool_bt_actualiser.PerformClick();
        }

        private void schemas_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            g = panel1.CreateGraphics(); richTextBox1.Text = "";
            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
            {
                foreach (liaison N in GrapheBO.graphe.Table_de_sommets[i].Liaisons_sortants)
                {
                    richTextBox1.Text += " ( " + GrapheBO.graphe.Table_de_sommets[i].Numero + " , " + N.Sommet_lier.Numero + " ) = " + N.cout + "\n";
                    ClassDuDessin.dessiner_liaison(g, Color.Black, GrapheBO.graphe.Table_de_sommets[i], N.Sommet_lier, N.cout);
                }
            }

            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
            {
                ClassDuDessin.dessiner_sommet(g, Color.Black, GrapheBO.graphe.Table_de_sommets[i]);
                GrapheBO.graphe.Table_de_sommets[i].Marquer = false;
            }

            panel1.Paint -= new System.Windows.Forms.PaintEventHandler(schemas_Paint);
        }
        private void Place_noeud_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                sommet s = new sommet(GrapheBO.graphe.Nombre_de_noeud, e.X, e.Y, -1);
                GrapheBO.liste_de_sommets.Add(s);
                GrapheBO.graphe.Nombre_de_noeud++;

                g = panel1.CreateGraphics();
                ClassDuDessin.dessiner_sommet(g, Color.Black, s);
                lab_Nombre_de_Noeud.Text = "Faites \"CLICK GAUCHE\" pour placer des Noeuds et \"CLICK DROIT\" pour finir.";
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.panel1.Cursor = Cursors.Default;
                lab_Nombre_de_Noeud.Text = GrapheBO.graphe.Nombre_de_noeud + " sommet(s) ajouté(s)";
                
                GrapheBO.graphe.Table_de_sommets = new sommet[GrapheBO.graphe.Nombre_de_noeud]; int k = 0;
                foreach (sommet i in GrapheBO.liste_de_sommets)
                {
                    GrapheBO.graphe.Table_de_sommets[k] = new sommet(i.Numero, i.X, i.Y, -1);
                    k++;
                }


                GrapheBO.graphe.Matrice = new int[GrapheBO.graphe.Nombre_de_noeud][];
                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud;i++ )
                    GrapheBO.graphe.Matrice[i] = new int[GrapheBO.graphe.Nombre_de_noeud];

                this.panel1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.Place_noeud_MouseDown);
                this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);
                panel1.ContextMenuStrip = contextMenuStrip1;
                //this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lier_MouseClick);

                //bt_terminer = new Button();
                //bt_terminer.Location = new System.Drawing.Point(8, 40);
                //bt_terminer.Size = new System.Drawing.Size(100, 50);
                //bt_terminer.Text = "Terminer";
                //bt_terminer.UseVisualStyleBackColor = true;
                //panel1.Controls.Add(bt_terminer);
                //this.bt_terminer.Click += new System.EventHandler(this.bt_terminer_Click);

            }
            
        }
        private void Ajouter_noeud(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                sommet s = new sommet(GrapheBO.graphe.Nombre_de_noeud, e.X, e.Y, -1);
                GrapheBO.liste_de_sommets.Add(s);
                GrapheBO.graphe.Nombre_de_noeud++;

                g = panel1.CreateGraphics();
                ClassDuDessin.dessiner_sommet(g, Color.Black, s);
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.panel1.Cursor = Cursors.Default;
                lab_Nombre_de_Noeud.Text = GrapheBO.graphe.Nombre_de_noeud + " sommet(s) ajouté(s)";

                GrapheBO.graphe.Table_de_sommets = new sommet[GrapheBO.graphe.Nombre_de_noeud]; int k = 0;
                foreach (sommet i in GrapheBO.liste_de_sommets)
                {
                    GrapheBO.graphe.Table_de_sommets[k] = i;
                    k++;
                }

                GrapheBO.graphe.Matrice = new int[GrapheBO.graphe.Nombre_de_noeud][];
                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                    GrapheBO.graphe.Matrice[i] = new int[GrapheBO.graphe.Nombre_de_noeud];

                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                {
                    foreach (liaison b in GrapheBO.graphe.Table_de_sommets[i].Liaisons_sortants)
                    {
                        for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud; j++)
                        {
                            if (b.Sommet_lier.Numero == GrapheBO.graphe.Table_de_sommets[j].Numero)
                                GrapheBO.graphe.Matrice[i][j] = b.cout;
                        }
                    }
                }
                this.panel1.Cursor = Cursors.Default;
                this.panel1.MouseDown -= new System.Windows.Forms.MouseEventHandler(this.Ajouter_noeud);
                this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);

            }
        }                
        private void deplacer_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (deplacer)
                { 
                    if (Noeud != -1)
                    {
                        for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                        {
                            if (Noeud == GrapheBO.graphe.Table_de_sommets[i].Numero)
                            {
                                GrapheBO.graphe.Table_de_sommets[i].X = e.X;
                                GrapheBO.graphe.Table_de_sommets[i].Y = e.Y;
                                Noeud = -1;
                                panel1.Invalidate();
                                panel1.Paint += new System.Windows.Forms.PaintEventHandler(schemas_Paint);
                                break;
                            }
                        }
                    }
                    
                    this.panel1.Cursor = Cursors.Default;  
                    deplacer = false;
                }
                else
                {
                    Noeud = -1;
                   for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)  
                    {
                        Rectangle r1 = new Rectangle( GrapheBO.graphe.Table_de_sommets[i].X,  GrapheBO.graphe.Table_de_sommets[i].Y, 20, 20);
                        if (r1.Contains(e.Location))
                        {
                            Noeud =  GrapheBO.graphe.Table_de_sommets[i].Numero; 
                            deplacer = true;
                            this.panel1.Cursor = Cursors.NoMove2D;
                            break;
                        }
                    } 
                }     
        }
        private void lier_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (lier)
            {
                if (dep != -1)
                {
                    for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)  
                    {
                        Rectangle r1 = new Rectangle( GrapheBO.graphe.Table_de_sommets[i].X,  GrapheBO.graphe.Table_de_sommets[i].Y, 20, 20);
                        if (r1.Contains(e.Location))
                        {

                            form_Dialog = new Form();
                            form_initialise(0);
                            form_Dialog.ShowDialog();

                            if (form_Dialog.DialogResult == DialogResult.OK)
                            {
                                int cout = 0;
                                try
                                {
                                    cout = int.Parse(txt_lecout.Text);
                                    if (cout != 0)
                                    {
                                        GrapheBO.graphe.Table_de_sommets[dep].Liaisons_sortants.Add(new liaison(ref GrapheBO.graphe.Table_de_sommets[GrapheBO.graphe.Table_de_sommets[i].Numero], cout));
                                        GrapheBO.graphe.Table_de_sommets[ GrapheBO.graphe.Table_de_sommets[i].Numero].Liaisons_entrants.Add(new liaison(ref GrapheBO.graphe.Table_de_sommets[dep], cout));
                                        GrapheBO.graphe.Matrice[dep][ GrapheBO.graphe.Table_de_sommets[i].Numero] = cout;
                                        panel1.Invalidate();
                                        panel1.Paint += new System.Windows.Forms.PaintEventHandler(schemas_Paint);
                                    }
                                    form_Dialog.Dispose();
                                }
                                catch (Exception) { MessageBox.Show("Ce cout n'est pas incorrect"); }
                            }
                            else
                            {
                                form_Dialog.Dispose();
                            }

                            break;
                        }
                    }
                }
                lier = false;
                this.panel1.Cursor = Cursors.Default;
            }
            else
            {
               foreach (sommet p in GrapheBO.graphe.Table_de_sommets)
                {
                    Rectangle r1 = new Rectangle(p.X, p.Y, 20, 20);
                    if (r1.Contains(e.Location))
                    {
                        dep = p.Numero;
                        lier = true;
                        this.panel1.Cursor = Cursors.Hand;
                        break;
                    }
                }
            }
        }
        
        //------------------------------------------------------------------------------------------------------------------------------
        #endregion shemas
        //------------------------------------------------------------------------------------------------------------------------------
        #region bare de Memu

        private void RaffraichirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                lab_Nombre_de_Noeud.Text = GrapheBO.graphe.Nombre_de_noeud + " sommet(s) ajouté(s)";
                panel1.Invalidate();
                panel1.Paint += new System.Windows.Forms.PaintEventHandler(schemas_Paint);
            }

        }

        private void ajusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                Random rd = new Random();
                int valX, valY;
               for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++) 
                {
                    valX = rd.Next(GrapheBO.graphe.Table_de_sommets[i].X - 10, GrapheBO.graphe.Table_de_sommets[i].X + 10);
                    valY = rd.Next(GrapheBO.graphe.Table_de_sommets[i].Y - 10, GrapheBO.graphe.Table_de_sommets[i].Y + 10);

                    if (panel1.DisplayRectangle.Contains(new Point(valX, valY)))
                    {
                        GrapheBO.graphe.Table_de_sommets[i].X = valX;
                        GrapheBO.graphe.Table_de_sommets[i].Y = valY;
                    }
                }
                actualiserToolStripMenuItem.PerformClick();
            }
        }

        private void faireUneLiaisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                foreach (ToolStripItem item in menuStrip1.Items)
                {
                    if (item.Name == "tool_bt_actualiser")
                        continue;
                    item.Enabled = false;
                }
                panel1.ContextMenuStrip = null;
                lab_Nombre_de_Noeud.Text = "cliquez sur un \"Noeud de départ\" puis sur un \"Noeud d'arrivé\" pour faire la liaison.";
                this.panel1.MouseClick -= new MouseEventHandler(deplacer_MouseClick);
                this.panel1.MouseClick += new MouseEventHandler(lier_MouseClick);

                bt_terminer = new Button();
                bt_terminer.Location = new System.Drawing.Point(8, 40);
                bt_terminer.Size = new System.Drawing.Size(100, 50);
                bt_terminer.Text = "Terminer";
                bt_terminer.UseVisualStyleBackColor = true;
                panel1.Controls.Add(bt_terminer);
                this.bt_terminer.Click += new System.EventHandler(this.bt_terminer_Click);
            }
        }

        private void nouveauToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            form_Dialog = new Form();

            form_initialise(6);

            form_Dialog.ShowDialog();

            if (form_Dialog.DialogResult == DialogResult.OK)
            {
                if (check_carte.Checked)
                {
                    panel1.BackgroundImage = global::forms_des_graphes.Properties.Resources.carte_du_monde;
                    panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                }

                if (comboBox1_depart.SelectedItem == "Graphe orienté")
                    GrapheBO.graphe.Graphe_orienter = true;
                else
                    GrapheBO.graphe.Graphe_orienter = false;
                this.Text = "projet en théorie des graphe|DIOUBATE SEKOU_LSI-1 , Nouveau schémas";
                GrapheBO.graphe.Nombre_de_noeud = 0;
                richTextBox1.Text = "";
                GrapheBO.liste_de_sommets.Clear(); panel1.Invalidate();
                lab_Nombre_de_Noeud.Text = "Faites \"CLICK GAUCHE\" pour placer des Noeuds et \"CLICK DROIT\" pour finir.";
                this.panel1.Cursor = Cursors.Hand;
                panel1.ContextMenuStrip = null;
                this.panel1.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);
                this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Place_noeud_MouseDown);
                form_Dialog.Dispose();
            }
            else
            {
                form_Dialog.Dispose();
            }
        }

        private void ajouterUneCarteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = global::forms_des_graphes.Properties.Resources.carte_du_monde;
            panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            panel1.Paint += new System.Windows.Forms.PaintEventHandler(schemas_Paint);
        }

        private void ajouterUnNoeudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                this.panel1.Cursor = Cursors.Hand;
                GrapheBO.liste_de_sommets.Clear();
                for (int k = 0; k < GrapheBO.graphe.Nombre_de_noeud; k++)
                {
                    GrapheBO.liste_de_sommets.Add(GrapheBO.graphe.Table_de_sommets[k]);
                }

                lab_Nombre_de_Noeud.Text = "Faites \"CLICK GAUCHE\" pour placer des Noeuds et \"CLICK DROIT\" pour finir.";
                this.panel1.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);
                this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ajouter_noeud);
            }
        }

        private void supprimerUnNoeudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {

                form_Dialog = new Form();

                form_initialise(5);

                form_Dialog.ShowDialog();

                if (form_Dialog.DialogResult == DialogResult.OK)
                {
                  foreach(int i in checkedListBox.CheckedIndices)
                    {
                        GrapheBO.supprimer(i);
                        actualiserToolStripMenuItem.PerformClick();
                    }
                    form_Dialog.Dispose();
                }
                else
                {
                    form_Dialog.Dispose();
                }
            }
        }

        private void matriceAdjacenteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                FormMatriceAdjacente = new Form();
                Matrice_InitializeComponent();
                FormMatriceAdjacente_Load();
                FormMatriceAdjacente.ShowDialog();
                if (FormMatriceAdjacente.DialogResult == DialogResult.OK)
                {
                    FormMatriceAdjacente.Dispose();
                    actualiserToolStripMenuItem.PerformClick();
                }
            }

        } 

        private void listeAdjacenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                FormListeAdjacente = new Form();
                Liste_InitializeComponent();
                FormListeAdjacente_Load();
                FormListeAdjacente.ShowDialog();
            }

        } 

        private void aperçuavantimpressionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRapport rapport = new FormRapport();
            rapport.Show();
        } 

        private void ouvrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            try
            {
                if (openFileDialog1.FileName != null)
                {
                    this.Text = "projet en théorie des graphe | LSI-1 , "+openFileDialog1.FileName;
                    GrapheBO.deserialiser(openFileDialog1.FileName);
                    panel1.ContextMenuStrip = contextMenuStrip1;
                    actualiserToolStripMenuItem.PerformClick();
                    this.panel1.MouseClick -= new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);
                    this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.deplacer_MouseClick);
                }
            }
            catch (Exception d) { MessageBox.Show(d.Message); }

        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            try
            {
                GrapheBO.serialiser(saveFileDialog1.FileName);
                this.Text = "projet en théorie des graphe | LSI-1 , " + saveFileDialog1.FileName;
            }
            catch (Exception d) { MessageBox.Show(d.Message); }
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAide aide = new FormAide();
            aide.Show();
        }

        #endregion bare de Memu
        //----------------------------------------------------------------------------------------------------------------------------
        #region menu contextuel
        
        private void profondeur_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enProfondeurToolStripMenuItem.PerformClick();
        }

        private void largeur_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enLargeursToolStripMenuItem.PerformClick();
        }

        private void cheminOptimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cheminOptimalToolStripMenuItem1.PerformClick();
        }

        private void fermeture_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fermetureTransitiveToolStripMenuItem.PerformClick();
        }

        private void ajuster_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ajusterToolStripMenuItem.PerformClick();
        }

        private void Raffraichir_raToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actualiserToolStripMenuItem.PerformClick();
        }

        private void faireUneLiaisonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            faireUneLiaisonToolStripMenuItem.PerformClick();
        }
      
        private void ajouterUnNoeudToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            ajouterUnNoeudToolStripMenuItem.PerformClick();
        }

        private void supprimerUnNoeudToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            supprimerUnNoeudToolStripMenuItem.PerformClick();
        }

        #endregion menu contextuel
        //-------------------------------------------------------------------------------------------------------------   
        #region Algo

        private void enProfondeurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {

                form_Dialog = new Form();

                form_initialise(1);

                form_Dialog.ShowDialog();

                if (form_Dialog.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        List<sommet> DFS = new List<sommet>();
                        parcours.en_profondeur(ref DFS, GrapheBO.graphe.Table_de_sommets[(int)comboBox1_depart.SelectedItem]);
                        lab_info.Text = "Parcour_en_profondeur : ";
                        foreach (sommet s in DFS)
                        {
                            ClassDuDessin.dessiner_sommet(g, Color.Green, s);
                            s.Marquer = false;
                            System.Threading.Thread.Sleep(1000);
                            lab_info.Text += " => " + s.Numero;
                        }
                        if (DFS.Count != GrapheBO.graphe.Nombre_de_noeud)
                            MessageBox.Show("Ce graphe n'est pas fortement connexe, tous les sommets ne peuvent êtres parcourus");
                        actualiserToolStripMenuItem.PerformClick();

                        form_Dialog.Dispose();
                    }
                    catch (Exception ex) { MessageBox.Show("Ce depart n'est pas incorrect"); }
                }
                else
                {
                    form_Dialog.Dispose();
                }
            }
        }
       
        private void enLargeursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                form_Dialog = new Form();

                form_initialise(2);

                form_Dialog.ShowDialog();

                if (form_Dialog.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        List<sommet> BFS = new List<sommet>();
                        parcours.en_largeur(ref BFS, GrapheBO.graphe.Table_de_sommets[(int)comboBox1_depart.SelectedItem]);
                        lab_info.Text = "Parcour_en_largeur : ";
                        foreach (sommet s in BFS)
                        {
                            ClassDuDessin.dessiner_sommet(g, Color.Green, s);
                            s.Marquer = false;
                            System.Threading.Thread.Sleep(1000);
                            lab_info.Text += " => " + s.Numero;
                        }
                        if (BFS.Count != GrapheBO.graphe.Nombre_de_noeud)
                            MessageBox.Show("Ce graphe n'est pas fortement connexe, tous les sommets ne peuvent êtres parcourus");
                        actualiserToolStripMenuItem.PerformClick();

                        form_Dialog.Dispose();
                    }
                    catch (Exception ex) { MessageBox.Show("Ce depart n'est pas incorrect"); }
                }
                else
                {
                    form_Dialog.Dispose();
                }
            }
        }

        private void fermetureTransitiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                panel1.Refresh();
                Point P1 = new Point();
                Point P2 = new Point();
                float[] dashValues = { 5, 7 };
                Pen pen1 = new Pen(Color.Black, 1);
                pen1.DashPattern = dashValues;

                bool[][] nouvo_matrice = Warshall.fermeture_transitive();
                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                {
                    for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud; j++)
                        if (nouvo_matrice[i][j])
                        {
                            P1.X = GrapheBO.graphe.Table_de_sommets[i].X + 10; P1.Y = GrapheBO.graphe.Table_de_sommets[i].Y + 10;
                            P2.X = GrapheBO.graphe.Table_de_sommets[j].X + 10; P2.Y = GrapheBO.graphe.Table_de_sommets[j].Y + 10;

                            if (GrapheBO.graphe.Table_de_sommets[i] == GrapheBO.graphe.Table_de_sommets[j])//liaison boucle 
                            {
                                g.DrawEllipse(pen1, P1.X - 10, P1.Y - 10, 40, 40);
                            }
                            else
                            {
                                g.DrawLine(pen1, P1, P2);
                            }
                        }
                }
                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)  
                {
                    foreach (liaison l in GrapheBO.graphe.Table_de_sommets[i].Liaisons_sortants)
                        ClassDuDessin.dessiner_liaison(g, Color.Black, GrapheBO.graphe.Table_de_sommets[i], l.Sommet_lier, l.cout);
                }
                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                {
                    ClassDuDessin.dessiner_sommet(g, Color.Black, GrapheBO.graphe.Table_de_sommets[i]); 
                    GrapheBO.graphe.Table_de_sommets[i].Marquer = false;
                }
                lab_Nombre_de_Noeud.Text = "Vous pouvez actualiser le schemas pour revenir à l'initial.";
            }
        }

        private void cheminOptimalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (GrapheBO.graphe.Nombre_de_noeud == 0)
            {
                MessageBox.Show("Le graphe est vide! Veillez placer d'abord les noeuds.");
            }
            else
            {
                form_Dialog = new Form();
                form_initialise(4);
                form_Dialog.ShowDialog();

                if (form_Dialog.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        sommet depart = null;
                        sommet arrivee = null;
                        panel1.Refresh();
                        for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)   
                        {
                            GrapheBO.graphe.Table_de_sommets[i].precedant = null; GrapheBO.graphe.Table_de_sommets[i].Cout = -1;
                            if (GrapheBO.graphe.Table_de_sommets[i].Numero == (int)comboBox1_depart.SelectedItem)
                                depart = GrapheBO.graphe.Table_de_sommets[i];
                            if (GrapheBO.graphe.Table_de_sommets[i].Numero == (int)comboBox1_destination.SelectedItem)
                                arrivee = GrapheBO.graphe.Table_de_sommets[i];
                            foreach (liaison l in GrapheBO.graphe.Table_de_sommets[i].Liaisons_sortants)
                            {
                                ClassDuDessin.dessiner_liaison(g, Color.Black, GrapheBO.graphe.Table_de_sommets[i], l.Sommet_lier, l.cout);
                            }
                        }
                        for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)   
                        {
                            ClassDuDessin.dessiner_sommet(g, Color.Black, GrapheBO.graphe.Table_de_sommets[i]); 
                            GrapheBO.graphe.Table_de_sommets[i].Marquer = false;
                        }

                        if (depart == null && arrivee == null)
                            MessageBox.Show("Verifier \"le depart\" et \"l'arrivée\"");
                        else
                        {
                            List<sommet> chemin_touver = new List<sommet>();
                            chemin_touver = Dijkstra.plus_court_chemin(depart, arrivee);

                            if (chemin_touver == null)
                                MessageBox.Show("Le chemin que vous avez demandés est trouvable.");
                            else
                            {
                                lab_info.Text = "le chemin optimal entre " + depart.Numero + " et " + arrivee.Numero + " : ";
                                foreach (sommet s in chemin_touver)
                                {
                                    if (s != depart)
                                    {
                                        Point P1 = new Point(s.X, s.Y) + new Size(10, 10);
                                        Point P2 = new Point(s.precedant.X, s.precedant.Y) + new Size(10, 10);
                                        g.DrawLine(new Pen(Color.Red, 1), P1, P2);
                                    }
                                    lab_info.Text += " => " + s.Numero;
                                }
                                foreach (sommet p in chemin_touver)
                                {
                                    System.Threading.Thread.Sleep(1000);
                                    ClassDuDessin.dessiner_sommet(g, Color.Red, p); p.Marquer = false;
                                }
                            }


                        }
                    }

                    catch (Exception) { MessageBox.Show("Une erreur est survenue lors de l'execution de Djikstra!"); }
                    form_Dialog.Dispose();
                }
                else form_Dialog.Dispose();
            }
        }

        #endregion Algo 
        //--------------------------------------------------------------------------------------------------------------------- 
        #region FormMatriceAdjacente

        private Form FormMatriceAdjacente;
        private RichTextBox matrice_richTextBox1;
        private TableLayoutPanel matrice_tableLayoutPanel1;
        private Button matrice_bt_ajouter;
        private Label matrice_label1;

        private void Matrice_InitializeComponent()
        {
            matrice_richTextBox1 = new RichTextBox();
            matrice_tableLayoutPanel1 = new TableLayoutPanel();
            matrice_bt_ajouter = new Button();
            matrice_label1 = new Label();
            matrice_tableLayoutPanel1.SuspendLayout();
            FormMatriceAdjacente.SuspendLayout();
            // 
            // matrice_richTextBox1
            // 
            matrice_richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            matrice_richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            matrice_richTextBox1.Location = new System.Drawing.Point(3, 33);
            matrice_richTextBox1.Name = "matrice_richTextBox1";
            matrice_richTextBox1.Size = new System.Drawing.Size(457, 398);
            matrice_richTextBox1.TabIndex = 0;
            matrice_richTextBox1.Text = "";
            // 
            // matrice_tableLayoutPanel1
            // 
            matrice_tableLayoutPanel1.ColumnCount = 1;
            matrice_tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            matrice_tableLayoutPanel1.Controls.Add(matrice_richTextBox1, 0, 1);
            matrice_tableLayoutPanel1.Controls.Add(matrice_bt_ajouter, 0, 2);
            matrice_tableLayoutPanel1.Controls.Add(matrice_label1, 0, 0);
            matrice_tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            matrice_tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            matrice_tableLayoutPanel1.Name = "matrice_tableLayoutPanel1";
            matrice_tableLayoutPanel1.RowCount = 3;
            matrice_tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            matrice_tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            matrice_tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            matrice_tableLayoutPanel1.Size = new System.Drawing.Size(463, 467);
            matrice_tableLayoutPanel1.TabIndex = 1;
            // 
            // matrice_bt_ajouter
            // 
            matrice_bt_ajouter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            matrice_bt_ajouter.Dock = System.Windows.Forms.DockStyle.Right;
            matrice_bt_ajouter.FlatAppearance.BorderSize = 0;
            matrice_bt_ajouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            matrice_bt_ajouter.Location = new System.Drawing.Point(351, 437);
            matrice_bt_ajouter.Name = "matrice_bt_ajouter";
            matrice_bt_ajouter.Size = new System.Drawing.Size(109, 27);
            matrice_bt_ajouter.TabIndex = 25;
            matrice_bt_ajouter.Text = "OK";
            matrice_bt_ajouter.UseVisualStyleBackColor = true;
            matrice_bt_ajouter.Click += new System.EventHandler(this.bt_ajouter_matrice_Click);
            // 
            // matrice_label1
            // 
            matrice_label1.AutoSize = true;
            matrice_label1.Dock = System.Windows.Forms.DockStyle.Fill;
            matrice_label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            matrice_label1.Location = new System.Drawing.Point(3, 0);
            matrice_label1.Name = "matrice_label1";
            matrice_label1.Size = new System.Drawing.Size(457, 30);
            matrice_label1.TabIndex = 26;
            matrice_label1.Text = "Veillez saisir la matrice d\'adjacence";
            matrice_label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMatriceAdjacente
            // 
            FormMatriceAdjacente.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormMatriceAdjacente.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormMatriceAdjacente.BackColor = System.Drawing.SystemColors.Control;
            FormMatriceAdjacente.ClientSize = new System.Drawing.Size(463, 467);
            FormMatriceAdjacente.Controls.Add(matrice_tableLayoutPanel1);
            FormMatriceAdjacente.Name = "FormMatriceAdjacente";
            FormMatriceAdjacente.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormMatriceAdjacente.Text = "Matrice Adjacente";
            //Load += new System.EventHandler(this.FormMatriceAdjacente_Load);
            matrice_tableLayoutPanel1.ResumeLayout(false);
            matrice_tableLayoutPanel1.PerformLayout();
            FormMatriceAdjacente.ResumeLayout(false);

        }

        private void FormMatriceAdjacente_Load()
        {
            for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud - 1; i++)
            {
                for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud - 1; j++)
                {
                    matrice_richTextBox1.Text += GrapheBO.graphe.Matrice[i][j] + " ";
                }
                matrice_richTextBox1.Text += GrapheBO.graphe.Matrice[i][GrapheBO.graphe.Nombre_de_noeud - 1] + "\n";
            }

            for (int j = 0; j < GrapheBO.graphe.Nombre_de_noeud - 1; j++)
            {
                matrice_richTextBox1.Text += GrapheBO.graphe.Matrice[GrapheBO.graphe.Nombre_de_noeud - 1][j] + " ";
            }
            matrice_richTextBox1.Text += GrapheBO.graphe.Matrice[GrapheBO.graphe.Nombre_de_noeud - 1][GrapheBO.graphe.Nombre_de_noeud - 1];
        }

        public bool verifierMatrice()
        {
            if (GrapheBO.graphe.Nombre_de_noeud != matrice_richTextBox1.Lines.Length)
            {
                MessageBox.Show("Vous devez donner " + GrapheBO.graphe.Nombre_de_noeud + " ligne(s) et " + GrapheBO.graphe.Nombre_de_noeud + " colonne(s) \nVerifiez le nombre de ligne !");
                return false;
            }
            else
            {
                for (int i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                {
                    if (matrice_richTextBox1.Lines[i].Length != GrapheBO.graphe.Nombre_de_noeud * 2 - 1)
                    {
                        MessageBox.Show("Vous devez donner " + GrapheBO.graphe.Nombre_de_noeud + " ligne(s) et " + GrapheBO.graphe.Nombre_de_noeud + " colonne(s)\n Verifiez la ligne " + i);
                        return false;
                    }
                }
                foreach (sommet p in GrapheBO.graphe.Table_de_sommets)
                {
                    p.Liaisons_sortants.Clear();
                    p.Liaisons_entrants.Clear();
                }
                return true;
            }
        }

        private void bt_ajouter_matrice_Click(object sender, EventArgs e)
        {
            int i, j;
            int cout_liaison;
            string[] ligne = new string[GrapheBO.graphe.Nombre_de_noeud];
            if (verifierMatrice())
            {
                try
                {
                    for (i = 0; i < GrapheBO.graphe.Nombre_de_noeud; i++)
                    {
                        ligne = matrice_richTextBox1.Lines[i].Split(' ');

                        for (j = 0; j < GrapheBO.graphe.Nombre_de_noeud; j++)
                        {
                            cout_liaison = int.Parse(ligne[j]);
                            GrapheBO.graphe.Matrice[i][j] = cout_liaison;

                            if (cout_liaison > 0)
                            {
                                GrapheBO.graphe.Table_de_sommets[i].Liaisons_sortants.Add(new liaison(ref GrapheBO.graphe.Table_de_sommets[j], cout_liaison));
                                GrapheBO.graphe.Table_de_sommets[j].Liaisons_entrants.Add(new liaison(ref GrapheBO.graphe.Table_de_sommets[i], cout_liaison));
                            }
                        }
                    }
                    matrice_bt_ajouter.DialogResult = DialogResult.OK;
                }
                catch (Exception v) { MessageBox.Show(v.Message); };
            }
        }

        #endregion FormMatriceAdjacente
        //--------------------------------------------------------------------------------------------------------------------- 
        #region FormListeAdjacente

        private Form FormListeAdjacente;
        private RichTextBox liste_adjacent_richTextBox;

        private void Liste_InitializeComponent()
        {
            liste_adjacent_richTextBox = new RichTextBox();
            FormListeAdjacente.SuspendLayout();
            // 
            // liste_adjacent_richTextBox
            // 
            liste_adjacent_richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            liste_adjacent_richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            liste_adjacent_richTextBox.Location = new System.Drawing.Point(0, 0);
            liste_adjacent_richTextBox.Name = "liste_adjacent_richTextBox";
            liste_adjacent_richTextBox.ReadOnly = true;
            liste_adjacent_richTextBox.Size = new System.Drawing.Size(592, 360);
            liste_adjacent_richTextBox.TabIndex = 0;
            liste_adjacent_richTextBox.Text = "";
            // 
            // FormListeAdjacente
            // 
            FormListeAdjacente.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            FormListeAdjacente.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            FormListeAdjacente.BackColor = System.Drawing.SystemColors.Control;
            FormListeAdjacente.ClientSize = new System.Drawing.Size(592, 360);
            FormListeAdjacente.Controls.Add(liste_adjacent_richTextBox);
            FormListeAdjacente.Name = "FormListeAdjacente";
            FormListeAdjacente.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            FormListeAdjacente.ResumeLayout(false);

        }

        private void FormListeAdjacente_Load()
        {
            liste_adjacent_richTextBox.Text = "\n---Liaison sortant---------------------------------------------------\n";
            for (int k = 0; k < GrapheBO.graphe.Nombre_de_noeud; k++)
            {
                liste_adjacent_richTextBox.Text += GrapheBO.graphe.Table_de_sommets[k].Numero + "  =>  { ";
                foreach (liaison N in GrapheBO.graphe.Table_de_sommets[k].Liaisons_sortants)
                {
                    liste_adjacent_richTextBox.Text +=  N.Sommet_lier.Numero + "[" + N.cout + "] , ";
                }
                liste_adjacent_richTextBox.Text += "}\n";
            }

            liste_adjacent_richTextBox.Text += "\n---Liaison entrant---------------------------------------------------\n";
            for (int k = 0; k < GrapheBO.graphe.Nombre_de_noeud; k++)
            {
                liste_adjacent_richTextBox.Text += GrapheBO.graphe.Table_de_sommets[k].Numero + "  <=  { ";
                foreach (liaison N in GrapheBO.graphe.Table_de_sommets[k].Liaisons_entrants)
                {
                    liste_adjacent_richTextBox.Text += N.Sommet_lier.Numero + "[" + N.cout + "] , ";
                }
                liste_adjacent_richTextBox.Text += "}\n";
            }
        }
        
        #endregion FormListeAdjacente
    }
}
