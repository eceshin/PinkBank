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
    public partial class KGuncel : Form
    {
        public KGuncel()
        {
            InitializeComponent();
        }
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();

        public KGuncel(int id)
        {
            InitializeComponent();

            cmd.Parameters.Clear();


            cmd.CommandText = "select * from kurumsalkullanici where id=@id";
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

                txtid.Text = Convert.ToString(id);
                txtadi.Text = (string)dr["kurumadi"];
                txtno.Text = (string)dr["kurumno"];
                txtemail.Text = (string)dr["email"];
                txttelno.Text = (string)dr["telefonno"];
                txtsifre.Text = (string)dr["sifre"];
                txtadres.Text = dr["adres"].ToString();

            }

            dr.Close();


        }






        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();




            cmd.CommandText = "update kurumsalkullanici set kurumadi = @ad, email = @kemail, telefonno = @tel, sifre = @ksifre, adres = @kadres, kurumno = @krmno where id = @kid";
            //bireyselguncelle procedure kullanılmak istendi

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;



            cmd.Parameters.AddWithValue("@kid", int.Parse(txtid.Text));
            cmd.Parameters.AddWithValue("@ad", txtadi.Text);
            cmd.Parameters.AddWithValue("@krmno", txtno.Text);
            cmd.Parameters.AddWithValue("@kemail", txtemail.Text);
            cmd.Parameters.AddWithValue("@tel", txttelno.Text);
            cmd.Parameters.AddWithValue("@ksifre", txtsifre.Text);
            cmd.Parameters.AddWithValue("@kadres", txtadres.Text);


            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int sonuc = cmd.ExecuteNonQuery();


                if (sonuc > 0)
                {
                    MessageBox.Show("Güncelleme Gerçekleşti");


                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }
    }
}
