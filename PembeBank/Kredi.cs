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
    public partial class Kredi : Form
    {
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();
        NpgsqlCommand cmdgiris = new NpgsqlCommand();
        NpgsqlCommand cmdborcode = new NpgsqlCommand();
       

        public Kredi(int id, int kdeger)
        {
            InitializeComponent();
            kullanicigoruntule(id, kdeger);
            label19.Text = id.ToString();
            //sayfayı açan kullanıcının idsi
            label20.Text = kdeger.ToString();
            //banka degeri 

        }

        public void kullanicigoruntule(int id, int kdeger)
        {
            label3.Text = Convert.ToString(id);
            label17.Text = Convert.ToString(kdeger);


            cmd.CommandText = "select * from bankakullanicilari where kullaniciid= @id and deger=@kdeger";
            //bankakullanicilari view kullanıldı
            //paragonderilen procedure kullanılmak istendi
            cmd.Connection = con;

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@kdeger", kdeger);



            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                int deger = (int)dr["deger"];
                if (deger == 2)
                {
                    cmd.Parameters.Clear();
                    con.Close();
                    kurumsalgoruntule(id);
                    //kurumsalkredisayfa fonksiyonu çağırılmak istendi

                }
                if (deger == 1)
                {
                    cmd.Parameters.Clear();
                    con.Close();
                    bireyselgoruntule(id);
                    // bireyselkredisayfa fonksiyonu çağırılmak istendi

           
                }
            }
                dr.Close();
                con.Close();
                cmd.Parameters.Clear();
        }
        private void label12_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt16(label17.Text) == 1)
            {
                BireyselGirisSayfa bireyselGiris = new BireyselGirisSayfa(Convert.ToInt16(label3.Text));
                bireyselGiris.Show();
                this.Hide();
            }

            if (Convert.ToInt16(label17.Text) == 2)
            {
                KurumsalGirisSayfa kurumsalGirisSayfa = new KurumsalGirisSayfa(Convert.ToInt16(label3.Text));
                kurumsalGirisSayfa.Show();
                this.Hide();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();

            cmd.CommandText = "select * from bankakullanicilari where id= @id and deger= @kdeger ";

            //paragonderilen procedure kullanılmak istendi
            //cmd.Connection = con;

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", Convert.ToInt16(label3.Text));
            cmd.Parameters.AddWithValue("@kdeger", Convert.ToInt16(label17.Text));





            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read())
            {
                int deger = (int)dr["deger"];
                int id = Convert.ToInt16(dr["id"]);
                if (deger == 2)
                {

                    cmd.Parameters.Clear();
                    con.Close();
                    kurumsalborcode(id);
                    //kurumsalgoruntule fonksiyonu çağırılmak istendi


                }
                if (deger == 1)
                {
                    cmd.Parameters.Clear();
                    con.Close();
                    bireyselborcode(id);
                    //bireyselgoruntule fonksiyonu çağırılmak istendi



                }
            }
            dr.Close();
            kullanicigoruntule(Convert.ToInt16(label19.Text), Convert.ToInt16(label20.Text));
        }

        public void bireyselborcode(int id)
        {

            cmdborcode.Connection = con;

            cmdborcode.Parameters.Clear();

            cmdborcode.CommandText = "insert into bireyselkrediodeme(miktar,tarih,bireyselkrediid) values( @kmiktar, CURRENT_DATE , (select bireyselkredi.id from bireyselkredi join bireyselbanka on bireyselbanka.id = bireyselkredi.bireyselbankaid join bireyselkullanici on bireyselkullanici.id=bireyselbanka.bireyselkullaniciid where bireyselkullanici.id = @kid ))";
            //borcode procedure kullanılmak istendi

            cmdborcode.Parameters.AddWithValue("@kid", Convert.ToInt16(label3.Text));
            cmdborcode.Parameters.AddWithValue("@kmiktar", double.Parse(textBox1.Text));

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int sonuc = cmdborcode.ExecuteNonQuery();

                if (sonuc > 0)
                {

                    textBox1.Text = "";
                    //trigger sayesidnde hesaptan bakiye azalma kullanılabilirlimit güncelleme ve borc miktarı güncelleme işlemleri yapıldı

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
           

        }

        public void kurumsalborcode(int id)
        {
            cmdborcode.Connection = con;

            cmdborcode.Parameters.Clear();

            cmdborcode.CommandText = "insert into kurumsalkrediodeme(miktar,tarih,kurumsalkrediid) values( @kmiktar, CURRENT_DATE , (select kurumsalkredi.id from  kurumsalkredi join kurumsalbanka on kurumsalbanka.id =  kurumsalkredi.kurumsalbankaid join kurumsalkullanici on kurumsalkullanici.id=kurumsalbanka.kurumsalkullaniciid where kurumsalkullanici.id = @kid ))";
            //borcode procedure kullanılmak istendi

            cmdborcode.Parameters.AddWithValue("@kid", Convert.ToInt16(label3.Text));
            cmdborcode.Parameters.AddWithValue("@kmiktar", double.Parse(textBox1.Text));

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int sonuc = cmdborcode.ExecuteNonQuery();

                if (sonuc > 0)
                {

                    textBox1.Text = "";
                    //trigger sayesidnde hesaptan bakiye azalma kullanılabilirlimit güncelleme ve borc miktarı güncelleme işlemleri yapıldı

                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }



        }


        public void bireyselgoruntule(int id)
        {
            cmdgiris.Parameters.Clear();
            cmdgiris.CommandText = "select * from bireyselkredisayfa(@id)";
            //bireyselkredisayfa fonksiyonu kullanıldı
            cmdgiris.Connection = con;
            cmdgiris.CommandType = CommandType.Text;
            cmdgiris.Parameters.AddWithValue("@id", id);


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            NpgsqlDataReader dr = cmdgiris.ExecuteReader();
            while (dr.Read())
            {
                lblfaiz.Text = dr["faizmiktarı"].ToString();
                lblborc.Text = dr["borc"].ToString();
                lblkartno.Text = (string)dr["kartno"];
                lblkullanilebilirlimit.Text = dr["klimit"].ToString();
                lblslimit.Text = dr["slimit"].ToString();
                lblcvc.Text = (string)dr["cvc"];
                string tarih = dr["date"].ToString();
                lbldate.Text = tarih.Substring(0, 5);

            }

            con.Close();
            cmdgiris.Parameters.Clear();

        }

        public void kurumsalgoruntule(int id)
        {
            
            cmdgiris.Parameters.Clear();
            cmdgiris.CommandText = "select * from kurumsalkredisayfa(@id)";
            //kurumsalkredisayfa fonksiyonu kullanıldı
            cmdgiris.Connection = con;
            cmdgiris.CommandType = CommandType.Text;
            cmdgiris.Parameters.AddWithValue("@id", id);


            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            NpgsqlDataReader dr = cmdgiris.ExecuteReader();
            while (dr.Read())
            {
                lblfaiz.Text = dr["faizmiktarı"].ToString();
                lblborc.Text = dr["borc"].ToString();
                lblkartno.Text = (string)dr["kartno"];
                lblkullanilebilirlimit.Text = dr["klimit"].ToString();
                lblslimit.Text = dr["slimit"].ToString();
                lblcvc.Text = (string)dr["cvc"];
                string tarih = dr["date"].ToString();
                lbldate.Text = tarih.Substring(0, 5);


            }
            con.Close();
            cmdgiris.Parameters.Clear();


        }

        private void Kredi_Load(object sender, EventArgs e)
        {

        }
    }
}
