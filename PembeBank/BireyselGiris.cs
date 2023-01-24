using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PembeBank
{
    public partial class BireyselGiris : Form
    {
        public BireyselGiris()
        {
            InitializeComponent();
            
        }
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;

            cmd.Parameters.Clear();

          try
            {   cmd.CommandText = "select * from bireyselgiris(@btc,@bsifre)";
                //bireyselgiris fonksiyonu kullanılmak istendi
            cmd.Parameters.AddWithValue("@btc", txttc.Text);
            cmd.Parameters.AddWithValue("@bsifre", txtsifre.Text);



           

                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    int onaydurumu = (int)dr["bonaydurumu"];

                    int id = (int)dr["bid"];


                    if (onaydurumu == 1)
                    {

                        BireyselGirisSayfa bireyselGiris = new BireyselGirisSayfa(id);
                        bireyselGiris.Show();
                        this.Hide();

                    }
                }
            }

            catch
            {

                MessageBox.Show("YANLIŞ GİRİŞ");
                con.Close();
                cmd.Parameters.Clear();
            }
                }
            }

            
        }
    
    