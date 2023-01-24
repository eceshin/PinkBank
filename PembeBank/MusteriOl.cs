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
    public partial class MusteriOl : Form
    {
        public MusteriOl()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {  

           NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
            NpgsqlCommand cmd = new NpgsqlCommand();


          
           
            cmd.CommandText = "insert into bireyselkullanici(adi, soyadi, email, telefonno, dogumtarihi, uyeliktarihi, tckimlik, sifre, ikametkahadresi) values(@ad,@soyad,@kemail,@tel,@dtarih,@utarih,@ktc,@ksifre,@kadres);";
            //basvuruekle prosedürü çalıştırılmak istendi
            
            cmd.Parameters.Clear();

             cmd.Connection = con;;


             cmd.CommandType = CommandType.Text;


            cmd.Parameters.AddWithValue("@ad", txtAdi.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyadi.Text);
            cmd.Parameters.AddWithValue("@kemail", txtEposta.Text);
            cmd.Parameters.AddWithValue("@tel", txtTel.Text);
            cmd.Parameters.AddWithValue("@dtarih", dateTimePicker1.Value);
            cmd.Parameters.AddWithValue("@utarih", dateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@ktc", txtTc.Text);
            cmd.Parameters.AddWithValue("@ksifre", txtsifre.Text);
            cmd.Parameters.AddWithValue("@kadres", txtAdres.Text);

           
            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                int sonuc = cmd.ExecuteNonQuery();


               
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Başvurunuz başarıyla gerçekleştirildi 7-14 işgünü içerisinde sizlere dönüş yapacağız.");
                        txtAdi.Text = " ";
                        txtSoyadi.Text = " ";
                        txtAdres.Text = " ";
                        txtEposta.Text = " ";
                        txtSoyadi.Text = " ";
                        txtTel.Text = " ";
                        txtsifre.Text = " ";
                        txtAdres.Text = " ";
                        txtTc.Text = " ";

                    }
                
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

        private void label13_Click(object sender, EventArgs e)
        { 
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();

        }
    }
}
