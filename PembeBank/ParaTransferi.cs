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
    public partial class ParaTransferi : Form
    {
        public ParaTransferi(int id,int kdeger)
        {
            InitializeComponent();

            label5.Text = id.ToString();
            //sayfayı açan kişinin kullanici idsi
            label7.Text = kdeger.ToString();
            //bireysel banka mı kurumsal banka mı olduğu


        }
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");


        NpgsqlCommand cmd = new NpgsqlCommand();
        NpgsqlCommand cmdgonderilen = new NpgsqlCommand();
        NpgsqlCommand cmdgonderen = new NpgsqlCommand();

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();

            cmd.CommandText = "select * from bankakullanicilari where iban= @kiban ";

            //paragonderilen procedure kullanılmak istendi
            cmd.Connection = con;

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@kiban", txtIban.Text);



            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                int deger = (int)dr["deger"];
                if (deger == 1)
                {

                    cmd.Parameters.Clear();
                    con.Close();
                    bireyselgondeerilen(Convert.ToUInt16(label5.Text), Convert.ToUInt16(label7.Text));

                    

                }
                if (deger == 2)
                {
                    cmd.Parameters.Clear();
                    con.Close();
                    kurumsalgonderilen(Convert.ToUInt16(label5.Text), Convert.ToUInt16(label7.Text));

                }


            }

            con.Close();
            cmd.Parameters.Clear();
        }


       public void bireyselgondeerilen(int id,int kdeger)
        {
           

            cmdgonderilen.CommandText = "update BireyselBanka set bakiye = (@kmiktar+bakiye) where iban= @kiban ";

            //paragonderilen procedure kullanılmak istendi
            cmdgonderilen.Connection = con;

            cmdgonderilen.CommandType = CommandType.Text;
            cmdgonderilen.Parameters.AddWithValue("@kiban", txtIban.Text);
            cmdgonderilen.Parameters.AddWithValue("@kmiktar", double.Parse(txtMiktar.Text));



            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                int sonuc = cmdgonderilen.ExecuteNonQuery();

                if (sonuc > 0)
                {
                    if (kdeger == 1)
                    {

                        cmdgonderilen.Parameters.Clear();
                        con.Close();
                        bireyselgonderen(id);

                    }
                    if(kdeger == 2)
                    {
                        cmdgonderilen.Parameters.Clear();
                        con.Close();
                        kurumsalgonderen(id);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            con.Close();
            cmdgonderilen.Parameters.Clear();

        }
        public void bireyselgonderen(int id)
        {
            cmdgonderen.Parameters.Clear();

            cmdgonderen.CommandText = "update BireyselBanka set bakiye = (bakiye - @kmiktar) where id = (select bireyselbanka.id from bireyselbanka join bireyselkullanici on bireyselbanka.bireyselkullaniciid = bireyselkullanici.id where bireyselkullanici.id = @kid )";
            //paragonderilen procedure kullanılmak istendi 

            cmdgonderen.Connection = con;
            cmdgonderen.CommandType = CommandType.Text;
            cmdgonderen.Parameters.AddWithValue("@kid", id);
            cmdgonderen.Parameters.AddWithValue("@kmiktar", float.Parse(txtMiktar.Text));

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                int sonuc = cmdgonderen.ExecuteNonQuery();

                if (sonuc > 0)
                {
                    MessageBox.Show("İşleminiz başarıyla gerçekleştirildi");
                    txtAdiGönderilen.Text = " ";
                    txtIban.Text = " ";
                    txtMiktar.Text = " ";
                    txtNot.Text = " ";
                    txtAdiGönderen.Text = " ";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            con.Close();
            cmdgonderen.Parameters.Clear();
        }

        public void kurumsalgonderilen(int id,int kdeger)
            {

           

            cmdgonderilen.CommandText = "update KurumsalBanka set kartbakiye = (@kmiktar+kartbakiye) where iban= @kiban ";

            //paragonderilen procedure kullanılmak istendi
            cmdgonderilen.Connection = con;

            cmdgonderilen.CommandType = CommandType.Text;
            cmdgonderilen.Parameters.AddWithValue("@kiban", txtIban.Text);
            cmdgonderilen.Parameters.AddWithValue("@kmiktar", double.Parse(txtMiktar.Text));



                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                    con.Open();
                    }
                    int sonuc = cmdgonderilen.ExecuteNonQuery();

                if (sonuc > 0)
                {
                    if (kdeger == 1)
                    {
                        cmdgonderilen.Parameters.Clear();
                        con.Close();
                        bireyselgonderen(id);

                    }
                    if (kdeger == 2)
                    {
                        cmdgonderilen.Parameters.Clear();
                        con.Close();
                        kurumsalgonderen(id);
                    }
                }
                con.Close();
            }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }


            con.Close();
            cmdgonderilen.Parameters.Clear();

        }

        public void kurumsalgonderen(int id)
        {
            cmdgonderen.Parameters.Clear();

            cmdgonderen.CommandText = "update KurumsalBanka set kartbakiye = (kartbakiye - @kmiktar) where id = (select kurumsalbanka.id from kurumsalbanka join kurumsalkullanici on kurumsalbanka.kurumsalkullaniciid = kurumsalkullanici.id where kurumsalkullanici.id = @kid )";
            //paragonderilen procedure kullanılmak istendi 

            cmdgonderen.Connection = con;
            cmdgonderen.CommandType = CommandType.Text;
            cmdgonderen.Parameters.AddWithValue("@kid", id);
            cmdgonderen.Parameters.AddWithValue("@kmiktar", float.Parse(txtMiktar.Text));

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                int sonuc = cmdgonderen.ExecuteNonQuery();

                if (sonuc > 0)
                {
                    MessageBox.Show("İşleminiz başarıyla gerçekleştirildi");
                    txtAdiGönderilen.Text = "";
                    txtIban.Text = "";
                    txtMiktar.Text = "";
                    txtNot.Text = "";
                    txtAdiGönderen.Text = "";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            cmdgonderen.Parameters.Clear();
            con.Close();
        }



    }
}
