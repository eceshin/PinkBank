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
    public partial class BireyselGirisSayfa : Form
    {


        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();
        public BireyselGirisSayfa(int id)
        {
            InitializeComponent();

            kullaniciGoruntule(id);

        }

        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.C  )
            {

                button5.PerformClick();
                Clipboard.SetText(label1.Text);


            }
       



     }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Controls.Clear();
             int id = Convert.ToInt16(lblid.Text);
            
            ParaTransferi ParaTransferi = new ParaTransferi(id,Convert.ToUInt16(label13.Text)) { TopLevel = false, TopMost = true };

            dataGridView1.Controls.Add(ParaTransferi);
            ParaTransferi.Show();



            kullaniciGoruntule(id);


        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Controls.Clear();
            int id = Convert.ToInt16(lblid.Text);



            BGuncelle BGuncelle = new BGuncelle(id) { TopLevel = false, TopMost = true };

            dataGridView1.Controls.Add(BGuncelle);
            BGuncelle.Show();

            kullaniciGoruntule(id);




        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Controls.Clear();
            int id = Convert.ToInt16(lblid.Text);
            BHesaplar BHesaplar = new BHesaplar(id) { TopLevel = false, TopMost = true };

            dataGridView1.Controls.Add(BHesaplar);
            BHesaplar.Show();

            kullaniciGoruntule(id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Controls.Clear();
            int id = Convert.ToInt16(lblid.Text);
            Kredi bireyselKredi = new Kredi(id, Convert.ToUInt16(label13.Text));
            bireyselKredi.Show();
            this.Hide();

            kullaniciGoruntule(id);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            form1.Show();
            this.Hide();
        }



        public void kullaniciGoruntule(int id)
        {
            cmd.Parameters.Clear();


            lblid.Text = Convert.ToString(id);

            cmd.CommandText = "select * from bGirisSayfa(@id)";

            //bGirissayfa fonksiyonu kullanılmak istendi

            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@id", id);


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                label13.Text = dr["bdeger"].ToString();
                label10.Text = (string)dr["adi"];
                label11.Text = (string)dr["soyadi"];
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
                cmd.Parameters.Clear();
                con.Close();
        }
    }
}