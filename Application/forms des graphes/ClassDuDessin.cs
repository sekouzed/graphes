using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using traitement_des_graphes;
using System.Drawing.Drawing2D;

namespace forms_des_graphes
{
    public class ClassDuDessin
    {
        public static void dessiner_sommet(Graphics g,Color couleur, sommet s)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(new SolidBrush(couleur), s.X, s.Y, 20, 20);
            if (s.Numero < 10)
                g.DrawString(s.Numero.ToString(), new Font("Microsoft Sans Serif", 7, FontStyle.Bold), new SolidBrush(Color.White), new PointF(s.X + 5, s.Y + 5));
            else
            {
                if (s.Numero >= 10 && s.Numero < 100)
                    g.DrawString(s.Numero.ToString(), new Font("Microsoft Sans Serif", 7, FontStyle.Bold), new SolidBrush(Color.White), new PointF(s.X + 2, s.Y + 5));
                else
                    g.DrawString(s.Numero.ToString(), new Font("Microsoft Sans Serif", 7, FontStyle.Bold), new SolidBrush(Color.White), new PointF(s.X - 1, s.Y + 5));
            }
         }

        public static Point milieu(Point P1, Point P2)
        {
            return new Point((P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2);
        }
        public static Double distance(Point P1, Point P2)
        {
            return Math.Sqrt((P1.X - P2.X) * (P1.X - P2.X) + (P1.Y - P2.Y) * (P1.Y - P2.Y)); ;
        }

        public static void dessiner_liaison(Graphics g,Color couleur, sommet s, sommet l,int cout)
        {
            Point P1 = new Point(s.X, s.Y)+ new Size(10, 10);//sommet s
            Point P2 = new Point(l.X, l.Y)+ new Size(10, 10) ;//sommet lier à s par l
            Point I1 = milieu(P1,P2);//milieu entre le sommet s et le sommet lier à s par l
            Point I2 = milieu(P1,I1);
            Point I = milieu(P1, P2);
            Double d_min = distance(I, P2); ;
            while (d_min > 40)
            {
                I = milieu(P1, I);
                d_min = distance(P1, I);
            }
            g.SmoothingMode = SmoothingMode.HighQuality; 
            Color Text_couleur = Color.White;
            Pen pen = new Pen(couleur, 1);
            Pen pen2 = new Pen(Color.Black, 3);
            pen2.EndCap = LineCap.ArrowAnchor;
                    
            if (s == l)//liaison boucle 
            {
                g.DrawEllipse(new Pen(couleur, 1), s.X, s.Y, 40, 40);
            }
            else
            {

                if (GrapheBO.graphe.Matrice[s.Numero][l.Numero] > 0 && GrapheBO.graphe.Matrice[l.Numero][s.Numero] > 0)
                {
                    if (l.Marquer == true)
                    {
                        g.DrawBezier(pen, P1, P1 + new Size(-10, -10), P2 + new Size(-10, -10), P2);
                        if(GrapheBO.graphe.Graphe_orienter)
                             g.DrawLine(pen2, P1, I + new Size(-10, -10));
                        g.DrawString(cout.ToString(), new Font("Microsoft Sans Serif", 7, FontStyle.Bold), new SolidBrush(Text_couleur), new PointF(I1.X, I1.Y) + new Size(-10, -10));
                    }
                    else
                    {
                        g.DrawBezier(pen, P1, P1 + new Size(10, 10), P2 + new Size(10, 10), P2);
                        if (GrapheBO.graphe.Graphe_orienter)
                            g.DrawLine(pen2, P1, I + new Size(10, 10));
                        g.DrawString(cout.ToString(), new Font("Microsoft Sans Serif", 7, FontStyle.Bold), new SolidBrush(Text_couleur), new PointF(I1.X, I1.Y));
                    }
                    s.Marquer = true;
                }
                else
                {
                    g.DrawLine(pen, P1, P2);
                    g.DrawString(cout.ToString(), new Font("Microsoft Sans Serif", 7, FontStyle.Bold), new SolidBrush(Text_couleur), new PointF(I1.X, I1.Y));
                    if (GrapheBO.graphe.Graphe_orienter)
                        g.DrawLine(pen2, P1, I2);
                }
            }

        }
    }
}
