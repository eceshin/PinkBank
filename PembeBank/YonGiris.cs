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
    public partial class YonGiris : Form
    {
        public YonGiris()
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
            {
                cmd.CommandText = "select * from yoneticigiris(@yno,@ysifre)";


                cmd.Parameters.AddWithValue("@yno", textBox1.Text);
                cmd.Parameters.AddWithValue("@ysifre", textBox2.Text);


                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {


                    YonSayfa yonSayfa = new YonSayfa();
                    yonSayfa.Show();
                    this.Hide();

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
