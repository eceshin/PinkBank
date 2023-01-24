using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PembeBank
{
    public partial class KurumsalGirisSayfa : Form
    {
        public KurumsalGirisSayfa(int id)
        {
            InitializeComponent();
            lblid.Text= id.ToString();
            kullanicigoruntule(id);

        }

        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();
     

        public void kullanicigoruntule(int id)
        {

            cmd.Parameters.Clear();
           

            cmd.CommandText = "select * from kgirissayfa(@kid)";

            //bGieissayfa fonksiyonu kullanılmak istendi

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@kid", id);


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                label2.Text = dr["kdeger"].ToString();
                label10.Text = dr["adi"].ToString();
                label3.Text = (string)dr["kartno"];
                label1.Text = (string)dr["iban"];
                label7.Text = dr["bakiye"].ToString();
                label4.Text = (string)dr["cvc"];
                string tarih = dr["date"].ToString();
                label6.Text = tarih.Substring(0, 5);

            }


            dr.Close();

            cmd.Parameters.Clear();


            cmd.CommandText = "select * from doviz";
            cmd.Connection = con;

            NpgsqlDataReader drdoviz = cmd.ExecuteReader();



            while (drdoviz.Read())
            {
                int dovizId = (int)drdoviz["id"];
                switch (dovizId)
                {
                    case 4:
                        label17.Text = drdoviz["miktarı"].ToString();
                        break;
                    case 1:
                        label14.Text = drdoviz["miktarı"].ToString();
                        break;
                    case 2:
                        label15.Text = drdoviz["miktarı"].ToString();
                        break;
                    case 3:
                        label16.Text = drdoviz["miktarı"].ToString();
                        break;
                }

            }
            con.Close();
            cmd.Parameters.Clear();
        }


        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {

                button5.PerformClick();
                Clipboard.SetText(label1.Text);


            }

        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            kullanicigoruntule(Convert.ToInt16(lblid.Text));

            Kredi kredi = new Kredi(Convert.ToInt16(lblid.Text), Convert.ToInt16(label2.Text));
            kredi.Show(); 
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kullanicigoruntule(Convert.ToInt16(lblid.Text));
            dataGridView2.Controls.Clear();
            ParaTransferi paraTransferi = new ParaTransferi(Convert.ToInt16(lblid.Text), Convert.ToInt16(label2.Text)) { TopLevel = false, TopMost = true };
            dataGridView2.Controls.Add(paraTransferi); ;
            paraTransferi.Show();
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            kullanicigoruntule(Convert.ToInt16(lblid.Text));
            dataGridView2.Controls.Clear();
            KHesaplar kHesaplar = new KHesaplar(Convert.ToInt16(lblid.Text)) { TopLevel = false, TopMost = true };
            dataGridView2.Controls.Add(kHesaplar); ;
            kHesaplar.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kullanicigoruntule(Convert.ToInt16(lblid.Text));
            dataGridView2.Controls.Clear();
            KGuncel kGuncel = new KGuncel(Convert.ToInt16(lblid.Text)) { TopLevel = false, TopMost = true };
            dataGridView2.Controls.Add(kGuncel); ;
            kGuncel.Show();
        }
    }
}
