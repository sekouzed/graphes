namespace forms_des_graphes
{
    partial class FormRapport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bt_enregistrer = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_OK = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.FileName = "rapport du graphe";
            // 
            // bt_enregistrer
            // 
            this.bt_enregistrer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_enregistrer.Location = new System.Drawing.Point(288, 3);
            this.bt_enregistrer.Name = "bt_enregistrer";
            this.bt_enregistrer.Size = new System.Drawing.Size(94, 22);
            this.bt_enregistrer.TabIndex = 0;
            this.bt_enregistrer.Text = "Sauvegarder";
            this.bt_enregistrer.UseVisualStyleBackColor = true;
            this.bt_enregistrer.Click += new System.EventHandler(this.bt_enregistrer_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bt_enregistrer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_OK, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 465);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 28);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // bt_OK
            // 
            this.bt_OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bt_OK.Location = new System.Drawing.Point(388, 3);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(94, 22);
            this.bt_OK.TabIndex = 1;
            this.bt_OK.Text = "OK";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(485, 465);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // FormRapport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 493);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormRapport";
            this.Text = "Detail sur le graphe";
            this.Load += new System.EventHandler(this.FormRapport_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button bt_enregistrer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}