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
    public partial class KHesaplar : Form
    {
        public KHesaplar(int id)
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
            NpgsqlCommand cmd = new NpgsqlCommand();



            cmd.CommandText = "(select KurumsalKullanici.id,KurumsalKullanici.kurumadi,KurumsalKullanici.kurumno,Kart.kartNo,Kart.cvc,Kart.date from KurumsalKullanici join  KurumsalBanka on KurumsalKullanici.id = KurumsalBanka.KurumsalKullaniciid join Kart on Kart.id = KurumsalBanka.kartId where KurumsalKullanici.id = @kid) union(select KurumsalKullanici.id, KurumsalKullanici.kurumadi, KurumsalKullanici.kurumno, Kart.kartNo, Kart.cvc, Kart.date from KurumsalKullanici join KurumsalBanka on KurumsalKullanici.id = KurumsalBanka.KurumsalKullaniciid join KurumsalKredi on KurumsalBanka.id = KurumsalKredi.KurumsalBankaid join Kart on Kart.id = KurumsalKredi.kartId where KurumsalKullanici.id = @kid)";
            
            //kurumsalhesapgoruntule function kullanılmak istendi

            cmd.Parameters.AddWithValue("@kid", id);
            cmd.Connection = con;

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                NpgsqlDataReader dr = cmd.ExecuteReader();
                listView1.Items.Clear();

                while (dr.Read())
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dr["id"].ToString();
                    lvi.SubItems.Add(dr["kurumadi"].ToString());
                    lvi.SubItems.Add(dr["kartno"].ToString());
                    lvi.SubItems.Add(dr["cvc"].ToString());
                    lvi.SubItems.Add(dr["date"].ToString());
                    lvi.SubItems.Add(dr["kurumno"].ToString());
                    listView1.Items.Add(lvi);

                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
