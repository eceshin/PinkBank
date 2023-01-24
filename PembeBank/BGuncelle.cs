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
    public partial class BGuncelle : Form
    {
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();

        public BGuncelle(int id)
        {
            InitializeComponent();

            cmd.Parameters.Clear();
         



            cmd.CommandText = "select * from bireyselkullanici where id=@id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@id", id);

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            NpgsqlDataReader dr = cmd.ExecuteReader();



            txtid.Text = Convert.ToString(id);
            while (dr.Read())
            {
                
                txtadi.Text = (string)dr["adi"];
                txtsoyadi.Text = (string)dr["soyadi"];
                txtemail.Text = (string)dr["email"];
                txttel.Text = (string)dr["telefonno"];
                txtsifre.Text = (string)dr["sifre"];
                txtadres.Text = dr["ikametkahadresi"].ToString();

            }

            dr.Close();

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
           



            cmd.CommandText = "update bireyselkullanici set adi = @ad, soyadi = @soyad, email = @kemail, telefonno = @tel, sifre = @ksifre, ikametkahadresi = @kadres where id = @kid";

            //bireyselguncelle procedure kullanılmak istendi

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@kid", int.Parse(txtid.Text));

            cmd.Parameters.AddWithValue("@ad", txtadi.Text);
            cmd.Parameters.AddWithValue("@soyad", txtsoyadi.Text);
            cmd.Parameters.AddWithValue("@kemail", txtemail.Text);
            cmd.Parameters.AddWithValue("@tel", txttel.Text);
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
