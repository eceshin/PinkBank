﻿namespace PembeBank
{
    partial class KHesaplar
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.id = new System.Windows.Forms.ColumnHeader();
            this.kurumadi = new System.Windows.Forms.ColumnHeader();
            this.KartNo = new System.Windows.Forms.ColumnHeader();
            this.cvc = new System.Windows.Forms.ColumnHeader();
            this.date = new System.Windows.Forms.ColumnHeader();
            this.kurumno = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.kurumadi,
            this.KartNo,
            this.cvc,
            this.date,
            this.kurumno});
            this.listView1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listView1.ForeColor = System.Drawing.Color.Navy;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 35);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(445, 457);
            this.listView1.TabIndex = 17;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.Text = "Id";
            this.id.Width = 50;
            // 
            // kurumadi
            // 
            this.kurumadi.Text = "Kurum Adı";
            this.kurumadi.Width = 80;
            // 
            // KartNo
            // 
            this.KartNo.Text = "Kart No";
            this.KartNo.Width = 150;
            // 
            // cvc
            // 
            this.cvc.Text = "CVC";
            // 
            // date
            // 
            this.date.Text = "Date";
            // 
            // kurumno
            // 
            this.kurumno.Text = "Kurum No";
            // 
            // KHesaplar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.ClientSize = new System.Drawing.Size(469, 523);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "KHesaplar";
            this.Text = "KHesaplar";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader kurumadi;
        private System.Windows.Forms.ColumnHeader KartNo;
        private System.Windows.Forms.ColumnHeader cvc;
        private System.Windows.Forms.ColumnHeader date;
        private System.Windows.Forms.ColumnHeader kurumno;
    }
}