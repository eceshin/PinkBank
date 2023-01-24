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
    public partial class YonSayfa : Form
    {
        NpgsqlConnection con = new NpgsqlConnection("server=localHost; port=5432; Database=pbank; user Id=postgres; password=123456");
        NpgsqlCommand cmd = new NpgsqlCommand();

        public YonSayfa()
        {
            InitializeComponent();

            //cmd.Parameters.Clear();

            //cmd.CommandText = "select * from kullanicilar";
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.Text;



            //try {
            //    if (con.State != ConnectionState.Open)
            //    {
            //        con.Open();

            //    }

            //        DataTable dt = new DataTable();

            //  NpgsqlDataReader dr = cmd.ExecuteReader();

            //        dt.Load(dr);
            //        dataGridView1.DataSource = dt;


            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //}

            dataGridView1.Visible = false;

            dataGridView2.Visible = false;

            dataGridView3.Visible = false;
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtadi.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtsoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtemail.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtno.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtdogumtarihi.Text=dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtuyelik.Text=dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txttc.Text=dataGridView1.CurrentRow.Cells[7].Value.ToString();
             txtsifre.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtadres.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            txtonaydrumu.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();




        }
   private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtkrmid.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtkrmadi.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtkrmemail.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtxkrmtel.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            txtkrmadres.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            txtkrmuyeliktarihi.Text= dataGridView2.CurrentRow.Cells[5].Value.ToString();    
            txtkrmsifre.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            txtkrmno.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();

        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            bireyselkullanicigoruntule();
            kurumsalkullanici.Visible = false;
            bireyselkullanici.Visible = true;
          
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;


        }

        public void bireyselkullanicigoruntule()
        {

            cmd.Parameters.Clear();

            cmd.CommandText = "select * from bireyselkullanici order by id";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;



            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }


                DataTable dt = new DataTable();

                NpgsqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
                dataGridView1.DataSource = dt;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            con.Close();
        }
       

        public void kurumsalkullanicigoruntule()
        {
            cmd.Parameters.Clear();

            cmd.CommandText = "select * from kurumsalkullanici order by id";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;



            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }


                DataTable dt = new DataTable();

                NpgsqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
                dataGridView2.DataSource = dt;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            kurumsalkullanicigoruntule();
            bireyselkullanici.Visible = false;
            kurumsalkullanici.Visible = true;
        

            dataGridView2.Visible = true;
            dataGridView1.Visible = false;

            dataGridView3.Visible = false;

        }

       private void btnkrmguncelle_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();




            cmd.CommandText = "update kurumsalkullanici set kurumadi = @ad, email = @kemail, telefonno = @tel,adres = @kadres,uyeliktarihi=@uyetarih ,sifre = @ksifre,kurumno=@kno where id = @kid";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@kid",Convert.ToInt16( txtkrmid.Text));
            cmd.Parameters.AddWithValue("@ad", txtkrmadi.Text);
            cmd.Parameters.AddWithValue("@kemail", txtkrmemail.Text);
            cmd.Parameters.AddWithValue("@tel", txtxkrmtel.Text);
            cmd.Parameters.AddWithValue("@ksifre", txtkrmsifre.Text);
            cmd.Parameters.AddWithValue("@kadres", txtkrmadres.Text);

            cmd.Parameters.AddWithValue("@uyetarih",Convert.ToDateTime( txtkrmuyeliktarihi.Text));

            cmd.Parameters.AddWithValue("@kno", txtkrmno.Text);


            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int sonuc = cmd.ExecuteNonQuery();


                if (sonuc > 0)
                {
                    txtkrmuyeliktarihi.Text = "";
                    txtkrmid.Text = "";
                    txtkrmadi.Text = "";
                    txtkrmadres.Text = "";
                    txtkrmemail.Text = "";
                    txtkrmno.Text = "";
                    txtkrmsifre.Text = "";
                    txtkrmuyeliktarihi.Text = "";
                    txtxkrmtel.Text = "";
                    kurumsalkullanicigoruntule();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void btnkrmsil_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();




            cmd.CommandText = "delete from kurumsalkullanici where id = @kid";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@kid",Convert.ToInt16( txtkrmid.Text));



            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int sonuc = cmd.ExecuteNonQuery();


                if (sonuc > 0)
                {
                    txtkrmuyeliktarihi.Text = "";
                    txtkrmid.Text = "";
                    txtkrmadi.Text = "";
                    txtkrmadres.Text = "";
                    txtkrmemail.Text = "";
                    txtkrmno.Text = "";
                    txtkrmsifre.Text = "";
                    txtkrmuyeliktarihi.Text = "";
                    txtxkrmtel.Text = "";


                    kurumsalkullanicigoruntule();

                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        
        

        private void btnbireyselguncelle_Click(object sender, EventArgs e)
        {  
            cmd.Parameters.Clear();

                cmd.CommandText = "update bireyselkullanici set adi = @ad, soyadi = @soyad, email = @kemail, telefonno = @tel, sifre = @ksifre, ikametkahadresi = @kadres where tckimlik = @tc";


                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;


                cmd.Parameters.AddWithValue("@tc", txttc.Text);
                cmd.Parameters.AddWithValue("@ad", txtadi.Text);
                cmd.Parameters.AddWithValue("@soyad", txtsoyadi.Text);
                cmd.Parameters.AddWithValue("@kemail", txtemail.Text);
                cmd.Parameters.AddWithValue("@tel", txtno.Text);
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
                    txtno.Text = "";
                txtdogumtarihi.Text = "";
                txtadres.Text = "";
                txtadi.Text = "";
                txtemail.Text = "";
                txtonaydrumu.Text = "";
                txtsifre.Text = "";
                txtsoyadi.Text = "";
                txttc.Text = "";
                txtuyelik.Text = "";
                {
                        bireyselkullanicigoruntule();

                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            
        }

      
        private void btnbekle_Click(object sender, EventArgs e)
        {
            cmd.CommandText = "insert into bireyselkullanici(adi, soyadi, email, telefonno,dogumtarihi,uyeliktarihi, tckimlik, sifre, ikametkahadresi,onaydurumu) values(@ad,@soyad,@kemail,@tel,@dogumtarihi,@uyeliktarihi,@ktc,@ksifre,@kadres,@onaydurumu);";
            cmd.Parameters.Clear();

            cmd.Connection = con; ;


            cmd.CommandType = CommandType.Text;


            cmd.Parameters.AddWithValue("@ad", txtadi.Text);
            cmd.Parameters.AddWithValue("@soyad", txtsoyadi.Text);
            cmd.Parameters.AddWithValue("@kemail", txtemail.Text);
            cmd.Parameters.AddWithValue("@tel", txtno.Text);
            cmd.Parameters.AddWithValue("@dogumtarihi", Convert.ToDateTime(txtdogumtarihi.Text));
            cmd.Parameters.AddWithValue("@uyeliktarihi", Convert.ToDateTime(txtuyelik.Text));

            cmd.Parameters.AddWithValue("@ktc", txttc.Text);
            cmd.Parameters.AddWithValue("@ksifre", txtsifre.Text);
            cmd.Parameters.AddWithValue("@kadres", txtadres.Text);

            cmd.Parameters.AddWithValue("@onaydurumu",Convert.ToInt16( txtonaydrumu.Text));


            try
            {

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }


                int sonuc = cmd.ExecuteNonQuery();



                if (sonuc > 0)
                {
                    txtno.Text = "";
                    txtdogumtarihi.Text = "";
                    txtadres.Text = "";
                    txtadi.Text = "";
                    txtemail.Text = "";
                    txtonaydrumu.Text = "";
                    txtsifre.Text = "";
                    txtsoyadi.Text = "";
                    txttc.Text = "";
                    txtuyelik.Text = "";

                    bireyselkullanicigoruntule();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {  
            cmd.CommandText = "insert into kurumsalkullanici(kurumadi, email, telefonno,uyeliktarihi, adres, sifre, kurumno) values(@ad,@kemail,@tel,@uyeliktarihi,@kadres,@ksifre,@kurumno);";
                cmd.Parameters.Clear();

                cmd.Connection = con; ;


                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@ad", txtkrmadi.Text);
                cmd.Parameters.AddWithValue("@kemail", txtkrmemail.Text);
                cmd.Parameters.AddWithValue("@tel", txtxkrmtel.Text);

            cmd.Parameters.AddWithValue("@uyeliktarihi",Convert.ToDateTime( txtkrmuyeliktarihi.Text));
            cmd.Parameters.AddWithValue("@kadres", txtkrmadres.Text);
                cmd.Parameters.AddWithValue("@ksifre", txtkrmsifre.Text);
                cmd.Parameters.AddWithValue("@kurumno", txtkrmno.Text);


            try
                {

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }


                    int sonuc = cmd.ExecuteNonQuery();



                    if (sonuc > 0)
                    {
                    txtkrmuyeliktarihi.Text = "";
                    txtkrmid.Text = "";
                    txtkrmadi.Text = "";
                    txtkrmadres.Text = "";
                    txtkrmemail.Text = "";
                    txtkrmno.Text = "";
                    txtkrmsifre.Text = "";
                    txtkrmuyeliktarihi.Text = "";
                    txtxkrmtel.Text = "";

                        kurumsalkullanicigoruntule();
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();

            cmd.CommandText = "delete from bireyselkullanici where tckimlik = @tc";


            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            cmd.Parameters.AddWithValue("@tc", txttc.Text);


            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int sonuc = cmd.ExecuteNonQuery();


                if (sonuc > 0)
                {
                    txtno.Text = "";
                    txtdogumtarihi.Text = "";
                    txtadres.Text = "";
                    txtadi.Text = "";
                    txtemail.Text = "";
                    txtonaydrumu.Text = "";
                    txtsifre.Text = "";
                    txtsoyadi.Text = "";
                    txttc.Text = "";
                    txtuyelik.Text = "";

            bireyselkullanicigoruntule();
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            bireyselkullanici.Visible = false;
            kurumsalkullanici.Visible = false;


            dataGridView2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView3.Visible = true;
            

                cmd.Parameters.Clear();

                cmd.CommandText = "select * from kullanicilar order by id";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;



                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();

                    }

                    

                    DataTable dt = new DataTable();

                    NpgsqlDataReader dr = cmd.ExecuteReader();

                    dt.Load(dr);
                    dataGridView3.DataSource = dt;

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                con.Close();

        }

        private void label18_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            bireyselkullanici.Visible = false;
            kurumsalkullanici.Visible = false;


            dataGridView2.Visible = false;
            dataGridView1.Visible = false;
            dataGridView3.Visible = true;

            cmd.Parameters.Clear();

            cmd.CommandText = "select * from krediodemeleri ";
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;



            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();

                }



                DataTable dt = new DataTable();

                NpgsqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);
                dataGridView3.DataSource = dt;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            con.Close();
        }
    }
       
        
    }

