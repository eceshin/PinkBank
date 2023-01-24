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
    public partial class BHesaplar : Form
    {
        public BHesaplar(int id)
        {
            InitializeComponent();
            NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
            NpgsqlCommand cmd = new NpgsqlCommand();



            cmd.CommandText = "(select BireyselKullanici.id,BireyselKullanici.adi,BireyselKullanici.soyadi,Kart.kartNo,Kart.cvc,Kart.date from BireyselKullanici join  BireyselBanka on BireyselKullanici.id = BireyselBanka.BireyselKullaniciid join Kart on Kart.id = BireyselBanka.kartId where BireyselKullanici.id = @kid) union (select BireyselKullanici.id, BireyselKullanici.adi,BireyselKullanici.soyadi,Kart.kartNo,Kart.cvc, Kart.date from BireyselKullanici join  BireyselBanka on BireyselKullanici.id = BireyselBanka.BireyselKullaniciid join BireyselKredi on BireyselBanka.id = BireyselKredi.Bireyselbankaid join Kart on Kart.id = BireyselKredi.kartId where BireyselKullanici.id = @kid)";
            //bireyselhesapgoruntule function kullanılmak istendi

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
                    lvi.SubItems.Add(dr["adi"].ToString());
                    lvi.SubItems.Add(dr["soyadi"].ToString());
                    lvi.SubItems.Add(dr["kartno"].ToString());
                    lvi.SubItems.Add(dr["cvc"].ToString());
                    lvi.SubItems.Add(dr["date"].ToString());

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
