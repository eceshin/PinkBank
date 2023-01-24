using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PembeBank
{
    public partial class Form1 : Form
    {
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();

        public Form1()
        {
            InitializeComponent();


            cmd.CommandText = "select * from doviz";
            cmd.Connection = con;


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            NpgsqlDataReader drdoviz = cmd.ExecuteReader();

            while (drdoviz.Read())
            {
                int dovizId = (int)drdoviz["id"];
                switch (dovizId)
                {
                    case 1:
                        label13.Text = drdoviz["miktarı"].ToString();
                        break;
                    case 2:
                        label14.Text = drdoviz["miktarı"].ToString();
                        break;
                    case 3:
                        label15.Text = drdoviz["miktarı"].ToString();
                        break;
                    case 4:
                        label16.Text = drdoviz["miktarı"].ToString();
                        break;
                }





            }
        }
        BireyselGiris bireyselKullaniciGiris = new BireyselGiris() { TopLevel = false, TopMost = true };
        KurumsalGiris kurumsalGiris = new KurumsalGiris() { TopLevel = false, TopMost = true };
        YonGiris yonGiris = new YonGiris() { TopLevel = false, TopMost = true };
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Controls.Add(bireyselKullaniciGiris);
            bireyselKullaniciGiris.Show();
            pictureBox1.Hide();
            kurumsalGiris.Hide();
            yonGiris.Hide();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Controls.Add(kurumsalGiris);
            kurumsalGiris.Show();
            pictureBox1.Hide();
            bireyselKullaniciGiris.Hide();
            yonGiris.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MusteriOl musteriOl = new MusteriOl();
            this.Hide();
            musteriOl.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Controls.Add(yonGiris);
            yonGiris.Show();
            pictureBox1.Hide();
            kurumsalGiris.Hide();
            bireyselKullaniciGiris.Hide();



        }
    }
}