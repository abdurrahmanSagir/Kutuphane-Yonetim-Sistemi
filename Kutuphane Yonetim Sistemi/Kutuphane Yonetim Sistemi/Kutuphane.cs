using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane_Yonetim_Sistemi
{
    public partial class Kutuphane : Form
    {
        // server baglanti
        SqlConnection Conn = new SqlConnection(
        ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
        // girisForm boyutları
        int SimdikiWidth = 1280;
        int SimdikiHeight = 800;
      
        public Kutuphane()
        {
            InitializeComponent();
        }

        private void Kutuphane_Load(object sender, EventArgs e)
        {
            // from ekran  cözünürlüge göre boyut ayarlama
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            Rectangle ClientCozunurluk = new Rectangle();
            ClientCozunurluk = Screen.GetBounds(ClientCozunurluk);
            float OranWidth = ((float)ClientCozunurluk.Width / (float)SimdikiWidth);
            float OranHeight = ((float)ClientCozunurluk.Height / (float)SimdikiHeight);

            this.Scale(OranWidth, OranHeight);
        }

        private void tsbUyeEkle_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            grbUyeKayit.Visible = true;
           
        }

        private void tsbUyeListesi_Click(object sender, EventArgs e)
        {
            // tüm nesneler false yapılıyor
            FormNesneKontrol();
            try
            {
                grbUyeListesi.Visible = true;
                dgvUyeListesi.Visible = true;
                Conn.Open();
                string aktifUyeListesi = "select ad+' '+Soyad as [Ad Soyad] ,TC,TelNo as Telefon from Uye where aktifMi=1";
                SqlCommand cmd = new SqlCommand(aktifUyeListesi, Conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUyeListesi.DataSource = dt;
                Conn.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
          
            
        }

       

        private void tsbUyeArama_Click(object sender, EventArgs e)
        {
            
            FormNesneKontrol();
            grbUyeArama.Visible = true;
            lvUyeArama.Visible = true;
        }
        public void FormNesneKontrol()
        {
            // Form uzerinde bulunan nesnelerin  islem durumlarina göre visible ayarlanmasi
            grbUyeKayit.Visible = false;
            grbUyeArama.Visible = false;
            grbEmanet.Visible = false;
            grbKitapArama.Visible = false;
            grbKitapKayit.Visible = false;
            grbKitapListesi.Visible = false;
            grbIade.Visible = false;
            grbUyeListesi.Visible = false;
            grbEmanetListesi.Visible = false;
            grbEmanetArama.Visible = false;
            grbOdeme.Visible = false;
            dgvEmanetListesi.Visible = false;
            dgvKitapListesi.Visible = false;
            dgvUyeListesi.Visible = false;
            lvEmanetListesi.Visible = false;
            lvEmanetListesi.Clear();
            lvEmanetVerilecekKitap.Visible = false;
            lvEmanetVerilecekKitap.Clear();
            lvUyeArama.Visible = false;
            lvUyeArama.Clear();
            lvIade.Visible = false;
            lvIade.Clear();
            lvKitapAramaListesi.Visible = false;
            lvKitapAramaListesi.Clear();
            txtEmanetArama.Text = "";
            tctTcArama.Text = "";
            txtKitaplikArama.Text = "";
            tctTcArama.Text = "";
            
            
            


        }
        public void FormTextTemizle()
        {
            // form uzerindeti textbox temixleme
            txtTC.Text = "";
            txtSoyadi.Text = "";
            txtKitaplikArama.Text = "";
            txtKitapAdiKayit.Text = "";
            txtKayitBarkod.Text = "";
            txtIadeBarkod.Text = "";
            txtIadeTc.Text = "";
            txtEmanetTc.Text = "";
            txtEmanetBarkod.Text = "";
            txtEmanetArama.Text = "";
            txtAdi.Text = "";
            txtAdetKayit.Text = "";
            txtTelNo.Text = "";
            tctTcArama.Text = "";
            txtOdemeTc.Text = "";
            txtBasımYılı.Text = "";
            cbKategoriKayit.Text = "";
            cbyayinEviKayit.Text = "";
            cbKategoriKayit.Text = "";
            lvKitapAramaListesi.Clear();
            lvEmanetListesi.Clear();
            lvUyeArama.Clear();
        }

       

        private void btnAra_Click(object sender, EventArgs e)
        {
            // girilen  kitap adı , barkod , ad veya tc ye göre arama yapmak 
            try
            {
                if (txtEmanetArama.Text == "")
                {
                    MessageBox.Show("Lütfen Giris Yapiniz");
                }
                else
                {
                    lvEmanetListesi.Clear();
                    lvEmanetListesi.Columns.Add("Ad ", 0);
                    lvEmanetListesi.Columns.Add("Soyad", 0);
                    lvEmanetListesi.Columns.Add("Kitap", 0);
                    lvEmanetListesi.Columns.Add("Emanet Tarihi", 0);
                    lvEmanetListesi.Columns.Add("İade Tarihi", 0);



                    lvEmanetListesi.View = View.Details;
                    lvEmanetListesi.GridLines = true;

                    ListViewItem item = new ListViewItem();

                   
                    if (rbKitap.Checked == true)
                    {
                        Conn.Open();
                        // kitap veya barkod icin
                        string emanetListeArama = "select u.Ad,u.Soyad , k.kitapAdi ,e.verilisTarihi ,e.iadeTarihi  from Emanet e, Kitap k, Uye u " +
                            "where e.kitapID = k.kitapID and e.uyeID = u.uyeID and (k.barkod ='"+ txtEmanetArama.Text+"' or k.kitapAdi like '%"+txtEmanetArama.Text+"%')";
                        SqlCommand cmd = new SqlCommand(emanetListeArama, Conn);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            item = lvEmanetListesi.Items.Add(dr["Ad"].ToString());
                            item.SubItems.Add(dr["Soyad"].ToString());
                            item.SubItems.Add(dr["kitapAdi"].ToString());
                            item.SubItems.Add(dr["verilisTarihi"].ToString());
                            item.SubItems.Add(dr["iadeTarihi"].ToString());
                        }
                        lvEmanetListesi.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        lvEmanetListesi.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        Conn.Close();


                    }
                    else if(rbUye.Checked==true)
                    {
                        Conn.Open();
                        // uye adı veya tc icin
                        string emanetListeArama = "select u.Ad,u.Soyad , k.kitapAdi ,e.verilisTarihi ,e.iadeTarihi  from Emanet e, Kitap k, Uye u " +
                            "where e.kitapID = k.kitapID and e.uyeID = u.uyeID and (u.TC ='" + txtEmanetArama.Text + "' or u.ad like '%" + txtEmanetArama.Text + "%')";
                        SqlCommand cmd = new SqlCommand(emanetListeArama, Conn);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            item = lvEmanetListesi.Items.Add(dr["Ad"].ToString());
                            item.SubItems.Add(dr["Soyad"].ToString());
                            item.SubItems.Add(dr["kitapAdi"].ToString());
                            item.SubItems.Add(dr["verilisTarihi"].ToString());
                            item.SubItems.Add(dr["iadeTarihi"].ToString());
                        }
                        lvEmanetListesi.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        lvEmanetListesi.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        Conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Önce Secim Yapiniz");
                        
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
        }

        private void tsbEmanet_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            grbEmanet.Visible = true;
            lvEmanetVerilecekKitap.Visible = true;
        }

        private void tsbIade_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            grbIade.Visible = true;
            lvIade.Visible = true;
        }

        private void tsbEmanetListesi_Click(object sender, EventArgs e)
        {
            // iadesi yapılmayan emanetlerin listesi
            FormNesneKontrol();
            try
            {
                grbEmanetListesi.Visible = true;
                dgvEmanetListesi.Visible = true;
                Conn.Open();
                string iadesiYapilmayanEmanetler = "select u.Ad+' '+u.Soyad [Uye] , k.kitapAdi Kitap,e.verilisTarihi [Emanet Tarihi] from Emanet e, Kitap k,Uye u " +
                    "where e.kitapID = k.kitapID and e.uyeID = u.uyeID and iadeTarihi is null";
                SqlCommand cmd = new SqlCommand(iadesiYapilmayanEmanetler, Conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvEmanetListesi.DataSource = dt;
                Conn.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
            
        }

        private void tsbEmanetAra_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            grbEmanetArama.Visible = true;
            lvEmanetListesi.Visible = true;
        }

        private void tsbKitapEkle_Click(object sender, EventArgs e)
        {
            // yazar,kitap,yayınevleri getiriliyor
            FormNesneKontrol();
            grbKitapKayit.Visible = true;
            try
            {
                cbKategoriKayit.Items.Clear();
                cbyayinEviKayit.Items.Clear();
                cbYazarAdiKayit.Items.Clear();

                Conn.Open();
                SqlCommand cmd = new SqlCommand("select yazar from Yazar", Conn);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    cbYazarAdiKayit.Items.Add(dr["Yazar"].ToString());
                }
                
                SqlCommand cmd1 = new SqlCommand("select kategori from Kategori", Conn);
                cmd1.ExecuteNonQuery();

                DataTable dt1 = new DataTable();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(dt1);

                foreach (DataRow dr1 in dt1.Rows)
                {
                    cbKategoriKayit.Items.Add(dr1["kategori"].ToString());
                }


                SqlCommand cmd2 = new SqlCommand("select yayinEvi  from [Yayin Evi]", Conn);
                cmd2.ExecuteNonQuery();

                DataTable dt2 = new DataTable();
                SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                adp2.Fill(dt2);

                foreach (DataRow dr2 in dt2.Rows)
                {
                    cbyayinEviKayit.Items.Add(dr2["yayinEvi"].ToString());
                }
                cbYazarAdiKayit.SelectedIndex = 0;
                cbyayinEviKayit.SelectedIndex = 0;
                cbKategoriKayit.SelectedIndex = 0;
                Conn.Close();
                


            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }

        }

        private void tsbKitapListesi_Click(object sender, EventArgs e)
        {
            // stokda  bulunan Kitaplarin Listesi
            FormNesneKontrol();
            try
            {
                grbKitapListesi.Visible = true;
                dgvKitapListesi.Visible = true;

                Conn.Open();
                string iadesiYapilmayanEmanetler = "select k.kitapAdi Kitap,y.yazar Yazar,ka.kategori Kategori,ya.yayinEvi Yayinevi , k.basimYili [Basım Yılı]" +
                    " from  Kitap k,Yazar y,[Yayin Evi] ya,Kategori ka where " +
                    "k.yazarID = y.yazarID and k.yayinEviID = ya.yayinEviID and " +
                    "k.kategoriID = ka.kategoriID and k.adet > 0";
                SqlCommand cmd = new SqlCommand(iadesiYapilmayanEmanetler, Conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvKitapListesi.DataSource = dt;
                Conn.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show("", ex.Message);
            }

           

        }

        private void tsbKitapAra_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            grbKitapArama.Visible = true;
            lvKitapAramaListesi.Visible = true;

        }

        private void btnUyeAra_Click(object sender, EventArgs e)
        {
            // girilen TC ye göre Uye Arama 
            try
            {
                lvUyeArama.Clear();

                if (tctTcArama.Text == "" )
                {
                    MessageBox.Show("İp Ucu Giriniz");
                }
                else
                {
                    lvUyeArama.Columns.Add("Ad ", 0);
                    lvUyeArama.Columns.Add("Soyad", 0);
                    lvUyeArama.Columns.Add("TC", 0);
                    lvUyeArama.Columns.Add("Telefon", 0);


                    lvUyeArama.View = View.Details;
                    lvUyeArama.GridLines = true;
                    lvUyeArama.FullRowSelect = true;
                    lvUyeArama.MultiSelect = false;
                    lvUyeArama.HideSelection = false;


                    ListViewItem item = new ListViewItem();

                    Conn.Open();
                    string sqlstmt = "select ad,Soyad ,tc  ,telno  from Uye where  tc='"+tctTcArama.Text+ "' or ad like '%"+tctTcArama.Text+"%' or soyad like '%" + tctTcArama.Text + "%'";
                    SqlCommand cmd = new SqlCommand(sqlstmt, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        item = lvUyeArama.Items.Add(dr["ad"].ToString());
                        item.SubItems.Add(dr["Soyad"].ToString());
                        item.SubItems.Add(dr["tc"].ToString());
                        item.SubItems.Add(dr["telno"].ToString());




                    }
                    lvUyeArama.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lvUyeArama.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    Conn.Close();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
        }

        private void btnKitapAra_Click(object sender, EventArgs e)
        {
            // kitap,yazar,yayinevi,kategori veya barkod a göre arama yapılıyor
            try
            {
                if (txtKitaplikArama.Text=="")
                {
                    MessageBox.Show("Lütfen İp Ucu Giriniz");
                }
                else
                {
                    lvKitapAramaListesi.Clear();

                    lvKitapAramaListesi.Columns.Add("Barkod ", 0);
                    lvKitapAramaListesi.Columns.Add("Kitap", 0);
                    lvKitapAramaListesi.Columns.Add("Yazar", 0);
                    lvKitapAramaListesi.Columns.Add("Kategori", 0);
                    lvKitapAramaListesi.Columns.Add("Yayinevi", 0);
                    lvKitapAramaListesi.Columns.Add("Basım Yılı", 0);
                    lvKitapAramaListesi.Columns.Add("Adet", 0);


                    lvKitapAramaListesi.View = View.Details;
                    lvKitapAramaListesi.GridLines = true;
                    lvKitapAramaListesi.FullRowSelect = true;
                    lvKitapAramaListesi.MultiSelect = false;
                    lvKitapAramaListesi.HideSelection = false;

                    ListViewItem item = new ListViewItem();

                    
                    string sqlstmt = "select k.barkod,k.kitapAdi,y.yazar,ka.kategori,ya.yayinEvi,k.basimYili,k.adet from " +
                        "Kitap k, Yazar y,Kategori ka,[Yayin Evi] ya where " +
                        "k.yazarID = y.yazarID and k.kategoriID = ka.kategoriID and k.yayinEviID = ya.yayinEviID and" +
                        "(k.barkod ='"+txtKitaplikArama.Text+"' or k.kitapAdi like '%"+txtKitaplikArama.Text +"%' or" +
                        " y.yazar like '%"+txtKitaplikArama.Text+ "%' or ka.kategori like '%"+txtKitaplikArama.Text+ "%' or ya.yayinEvi like '%"+txtKitaplikArama.Text+"%')";
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlstmt, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        item = lvKitapAramaListesi.Items.Add(dr["barkod"].ToString());
                        item.SubItems.Add(dr["kitapAdi"].ToString());
                        item.SubItems.Add(dr["yazar"].ToString());
                        item.SubItems.Add(dr["kategori"].ToString());
                        item.SubItems.Add(dr["yayinEvi"].ToString());
                        item.SubItems.Add(dr["basimYili"].ToString());
                        item.SubItems.Add(dr["adet"].ToString());




                    }
                    lvKitapAramaListesi.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lvKitapAramaListesi.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    Conn.Close();

                }
            }
            catch (Exception ex )
            {

                MessageBox.Show("",ex.Message);
            }
        }

        private void Kutuphane_FormClosing(object sender, FormClosingEventArgs e)
        {
            // form exit basildiginda uyari veriyoruz
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                AnaSayfa anaSayfa = new AnaSayfa();
                this.Hide();
                anaSayfa.Show();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("Program kapatılmadı.");
                this.Show();
            }
        }

        private void btnUyeEkle_Click(object sender, EventArgs e)
        {
            // uye eklme veya guncelleme 
            try
            {
                if (txtAdi.Text!="" && txtSoyadi.Text !="" && txtTC.Text !="" && txtTelNo.Text !="" )
                {
                    if (btnUyeEkle.Text=="Kaydet")
                    {
                        Conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[dbo].[sp_uye_ekle]";
                        cmd.Parameters.AddWithValue("@ad", SqlDbType.NVarChar).Value = txtAdi.Text;
                        cmd.Parameters.AddWithValue("@soyad", SqlDbType.NVarChar).Value = txtSoyadi.Text;
                        cmd.Parameters.AddWithValue("@tc", SqlDbType.NVarChar).Value = txtTC.Text;
                        cmd.Parameters.AddWithValue("@telNo", SqlDbType.Int).Value = txtTelNo.Text;
                        cmd.Parameters.Add("@sonuc", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;
                        cmd.Connection = Conn;
                        cmd.ExecuteScalar();
                        MessageBox.Show(cmd.Parameters["@sonuc"].Value.ToString());
                        Conn.Close();
                        FormTextTemizle();
                        txtTC.Enabled = true;


                    }
                    else
                    {
                        Conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[dbo].[sp_uye_guncelle]";
                        cmd.Parameters.AddWithValue("@ad", SqlDbType.NVarChar).Value = txtAdi.Text;
                        cmd.Parameters.AddWithValue("@soyad", SqlDbType.NVarChar).Value = txtSoyadi.Text;
                        cmd.Parameters.AddWithValue("@tc", SqlDbType.NVarChar).Value = txtTC.Text;
                        cmd.Parameters.AddWithValue("@telNo", SqlDbType.Int).Value = txtTelNo.Text;
                        cmd.Parameters.Add("@sonuc", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;
                        cmd.Connection = Conn;
                        cmd.ExecuteScalar();
                        MessageBox.Show(cmd.Parameters["@sonuc"].Value.ToString());
                        Conn.Close();
                        FormTextTemizle();
                        txtTC.Enabled = true;

                    }
                    btnUyeEkle.Text = "Kaydet";


                }
                else
                {
                    MessageBox.Show("Bilgileri Eksiksiz Giriniz");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
                Conn.Close();
            }
        }

        private void btnUyeKayitIptal_Click(object sender, EventArgs e)
        {
            FormTextTemizle();
            btnUyeEkle.Text = "Kaydet";
            txtTC.Enabled = true;
            
        }

        private void tpEmanet_Click(object sender, EventArgs e)
        {

        }

        private void btnEmanet_Click(object sender, EventArgs e)
        {
            // emanet verme islemi
            try
            {
                if (txtEmanetBarkod.Text !="" && txtEmanetTc.Text!="")
                {
                   
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[sp_emanetver]";
                    cmd.Parameters.AddWithValue("@barkod", SqlDbType.NVarChar).Value = lvEmanetVerilecekKitap.SelectedItems[0].SubItems[3].Text;
                    cmd.Parameters.AddWithValue("@tc", SqlDbType.NVarChar).Value = lvEmanetVerilecekKitap.SelectedItems[0].SubItems[0].Text;
                    cmd.Parameters.AddWithValue("@personelId", SqlDbType.Int).Value = AnaSayfa.personelID;
                    cmd.Parameters.Add("@sonuc", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;
                    cmd.Connection = Conn;
                    cmd.ExecuteScalar();
                    MessageBox.Show(cmd.Parameters["@sonuc"].Value.ToString());
                    Conn.Close();
                    FormTextTemizle();


                }
                else
                {
                    MessageBox.Show("Bilgileri Eksiksiz Giriniz");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("", ex.Message);
                Conn.Close();
            }
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            FormTextTemizle();
        }
        
        private void txtEmanetBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar == 13)
            {
               
                txtEmanetTc.Focus();
                
            }
        }

        
        public void EmanetVeriKontrol()
        {
            // emanet verilmeden önce list icinde hang uye icin hangi kitap verilecegi gösteriliyor
            try
            {
                
                if (txtEmanetBarkod.Text.Length==13 && txtEmanetTc.Text.Length==11)
                {
                    lvEmanetVerilecekKitap.Clear();
                    lvEmanetVerilecekKitap.Columns.Add("TC", 0);
                    lvEmanetVerilecekKitap.Columns.Add("Ad ", 0);
                    lvEmanetVerilecekKitap.Columns.Add("Soyad", 0);
                    lvEmanetVerilecekKitap.Columns.Add("Barkod", 0);
                    lvEmanetVerilecekKitap.Columns.Add("Kitap", 0);
                    lvEmanetVerilecekKitap.Columns.Add("Yazar", 0);


                    lvEmanetVerilecekKitap.View = View.Details;
                    lvEmanetVerilecekKitap.GridLines = true;
                    lvEmanetVerilecekKitap.FullRowSelect = true;
                    lvEmanetVerilecekKitap.MultiSelect = false;
                    lvEmanetVerilecekKitap.HideSelection = false;

                    ListViewItem item = new ListViewItem();

                    Conn.Open();
                    string sqlstmt = "select u.TC,u.Ad,u.Soyad,k.barkod,k.kitapAdi,y.yazar from Kitap k, Uye u,Yazar y where k.yazarID = y.yazarID and" +
                        " k.barkod ='" + txtEmanetBarkod.Text + "' and u.TC ='"+txtEmanetTc.Text+"'";
                    SqlCommand cmd = new SqlCommand(sqlstmt, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        item = lvEmanetVerilecekKitap.Items.Add(dr["TC"].ToString());
                        item.SubItems.Add(dr["Ad"].ToString());
                        item.SubItems.Add(dr["Soyad"].ToString());
                        item.SubItems.Add(dr["barkod"].ToString());
                        item.SubItems.Add(dr["kitapAdi"].ToString());
                        item.SubItems.Add(dr["yazar"].ToString());




                    }
                    lvEmanetVerilecekKitap.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lvEmanetVerilecekKitap.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    Conn.Close();
                    

                }
                else
                {
                    MessageBox.Show("Eksik veya Hatali Giris Yaptınız.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("", ex.Message);
                Conn.Close();
            }

        }
        public void IadeVeriKontrol()
        {
            // Iade icin veri kontrolu saglanıyor
            try
            {

                if (txtIadeBarkod.Text.Length == 13 && txtIadeTc.Text.Length == 11)
                {
                    lvIade.Clear();
                    lvIade.Columns.Add("TC ", 0);
                    lvIade.Columns.Add("Ad ", 0);
                    lvIade.Columns.Add("Soyad", 0);
                    lvIade.Columns.Add("Barkod", 0);
                    lvIade.Columns.Add("Kitap", 0);
                    lvIade.Columns.Add("Yazar", 0);
                    lvIade.Columns.Add("Emanet Tarihi", 0);


                    lvIade.View = View.Details;
                    lvIade.GridLines = true;
                    lvIade.FullRowSelect = true;
                    lvIade.MultiSelect = false;
                    lvIade.HideSelection = false;

                    ListViewItem item = new ListViewItem();

                    Conn.Open();
                    string sqlstmt = "select u.TC,u.Ad,u.Soyad,k.barkod,k.kitapAdi,y.yazar ,e.verilisTarihi from Emanet e,Uye u ,Kitap k ,Yazar y where e.uyeID=u.uyeID and e.kitapID=k.kitapID " +
                        "and k.yazarID = y.yazarID and k.barkod = '"+txtIadeBarkod.Text+"' and e.iadeTarihi is null and u.TC = '"+txtIadeTc.Text+"'";
                    SqlCommand cmd = new SqlCommand(sqlstmt, Conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        item = lvIade.Items.Add(dr["TC"].ToString());
                        item.SubItems.Add(dr["Ad"].ToString());
                        item.SubItems.Add(dr["Soyad"].ToString());
                        item.SubItems.Add(dr["barkod"].ToString());
                        item.SubItems.Add(dr["kitapAdi"].ToString());
                        item.SubItems.Add(dr["yazar"].ToString());
                        item.SubItems.Add(dr["verilisTarihi"].ToString());




                    }
                    lvIade.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lvIade.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    Conn.Close();


                }
                else
                {
                    MessageBox.Show("Eksik veya Hatali Giris Yaptınız.");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("", ex.Message);
                Conn.Close();
            }


        }
        private void txtIadeBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                txtIadeTc.Focus();
            }
        }

        private void txtIadeTc_TextChanged(object sender, EventArgs e)
        {
            if (txtIadeTc.Text.Length==11)
            {
                IadeVeriKontrol();
            }
        }

        private void txtEmanetTc_TextChanged(object sender, EventArgs e)
        {
            if (txtEmanetTc.Text.Length==11)
            {
                EmanetVeriKontrol();
            }
        }

        private void btnIadeOnay_Click(object sender, EventArgs e)
        {
            // iade alma islemi -- borc hesabı ve odeme kontrol
            try
            {

                if (lvIade.SelectedItems.Count > 0)
                {

                    Conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[sp_iadeal]";
                    cmd.Parameters.AddWithValue("@barkod", SqlDbType.NVarChar).Value = (lvIade.SelectedItems[0].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("@tc", SqlDbType.NVarChar).Value = (lvIade.SelectedItems[0].SubItems[0].Text);
                    cmd.Parameters.AddWithValue("@personelId", SqlDbType.Int).Value = AnaSayfa.personelID;
                    cmd.Parameters.Add("@sonuc", SqlDbType.NVarChar, 500);
                    cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@emanetID", SqlDbType.Int);
                    cmd.Parameters["@emanetID"].Direction = ParameterDirection.Output;
                    cmd.Connection = Conn;
                    cmd.ExecuteScalar();
                    int emanetID =(int) cmd.Parameters["@emanetID"].Value;
                    string sonuc = (string)cmd.Parameters["@sonuc"].Value;
                    Conn.Close();
                    Conn.Open();
                   
                    string sqlstmt = "select uyeID,verilisTarihi,iadeTarihi from Emanet where emanetID="+emanetID+"";
                    
                    SqlCommand cmd1 = new SqlCommand(sqlstmt, Conn);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    int uye = 0;
                    DateTime emanetTarih, iadeTarih;
                    TimeSpan tarihFarkı;
                    int gunSayisi = 0;
                    double borc = 0;
                    
                    while (dr.Read())
                    {
                         uye =Convert.ToInt32( dr[0]);
                         emanetTarih = Convert.ToDateTime(dr[1]);
                         iadeTarih = Convert.ToDateTime(dr[2]);
                        tarihFarkı= (iadeTarih - emanetTarih);
                        gunSayisi =Convert.ToInt32( tarihFarkı.TotalDays);

                    }
                    Conn.Close();

                    if ((gunSayisi) > 10)
                    {
                        borc = Convert.ToDouble(gunSayisi * 0.25);
                        DialogResult odeme = new DialogResult();
                        odeme = MessageBox.Show("Gecikme Bedeli:"+borc+"tl  Odemek Ister Misiniz???", "Uyarı", MessageBoxButtons.YesNo);
                        if (odeme == DialogResult.Yes)
                        {
                            SqlCommand cmd2;
                            cmd2 = new SqlCommand("update Uye set emanetGecikmeBedeli +=@borc where uyeID=@uye", Conn);
                            cmd2.Parameters.Add("@borc", System.Data.SqlDbType.Float);
                            cmd2.Parameters["@borc"].Value = borc;
                            cmd2.Parameters.Add("@uye", System.Data.SqlDbType.Int);
                            cmd2.Parameters["@uye"].Value = uye;



                            Conn.Open();
                            cmd2.ExecuteNonQuery();
                            Conn.Close();

                        }
                        if (odeme == DialogResult.No)
                        {
                            MessageBox.Show("En kısa Zamanda Odeme Yapınız.");
                           

                        }

                       


                    }
                    else
                    {
                        MessageBox.Show("", sonuc);
                        Conn.Close();
                    }
                    
                    FormTextTemizle();
                    lvIade.Clear();





                }
                else
                {
                    MessageBox.Show("Secim Yapınız");
                }

            }
            catch (Exception ex)
            {
                Conn.Close();
                MessageBox.Show("",ex.Message);
                
            }

        }

        private void btnIadeIptal_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            FormTextTemizle();
        }

        private void txtKayitBarkod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                txtKitapAdiKayit.Focus();
            }
        }

        private void btnKitapKaydet_Click(object sender, EventArgs e)
        {
            // kitap kayit
            try
            {
              
                Conn.Close();
                if (txtKayitBarkod.Text!="" && txtKitapAdiKayit.Text!="" && cbYazarAdiKayit.Text!="" && cbyayinEviKayit.Text!="" && cbKategoriKayit.Text!="" && txtBasımYılı.Text!="" && txtAdetKayit.Text!="")
                {
                    if(btnKitapKaydet.Text=="Kaydet")
                    {
                        Conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[dbo].[sp_kitap_ekle]";
                        cmd.Parameters.AddWithValue("@barkod", SqlDbType.NVarChar).Value = txtKayitBarkod.Text;
                        cmd.Parameters.AddWithValue("@kitapAdi", SqlDbType.NVarChar).Value = txtKitapAdiKayit.Text;
                        cmd.Parameters.AddWithValue("@yazar", SqlDbType.NVarChar).Value = cbYazarAdiKayit.Text;
                        cmd.Parameters.AddWithValue("@yayinevi", SqlDbType.NVarChar).Value = cbyayinEviKayit.Text;
                        cmd.Parameters.AddWithValue("@kategori", SqlDbType.NVarChar).Value = cbKategoriKayit.Text;
                        cmd.Parameters.AddWithValue("@basimYili", SqlDbType.Int).Value = Convert.ToInt32(txtBasımYılı.Text);
                        cmd.Parameters.AddWithValue("@adet", SqlDbType.Int).Value = Convert.ToInt32(txtAdetKayit.Text);
                        cmd.Parameters.Add("@sonuc", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;

                        cmd.Connection = Conn;
                        cmd.ExecuteScalar();
                        MessageBox.Show(cmd.Parameters["@sonuc"].Value.ToString());
                        Conn.Close();
                        FormTextTemizle();
                        txtKayitBarkod.Enabled = true;

                    }
                    else
                    {
                        Conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "[dbo].[sp_kitap_guncelle]";
                        cmd.Parameters.AddWithValue("@barkod", SqlDbType.NVarChar).Value = txtKayitBarkod.Text;
                        cmd.Parameters.AddWithValue("@kitapAdi", SqlDbType.NVarChar).Value = txtKitapAdiKayit.Text;
                        cmd.Parameters.AddWithValue("@yazar", SqlDbType.NVarChar).Value = cbYazarAdiKayit.Text;
                        cmd.Parameters.AddWithValue("@yayinevi", SqlDbType.NVarChar).Value = cbyayinEviKayit.Text;
                        cmd.Parameters.AddWithValue("@kategori", SqlDbType.NVarChar).Value = cbKategoriKayit.Text;
                        cmd.Parameters.AddWithValue("@basimYili", SqlDbType.Int).Value = Convert.ToInt32(txtBasımYılı.Text);
                        cmd.Parameters.AddWithValue("@adet", SqlDbType.Int).Value = Convert.ToInt32(txtAdetKayit.Text);
                        cmd.Parameters.Add("@sonuc", SqlDbType.NVarChar, 500);
                        cmd.Parameters["@sonuc"].Direction = ParameterDirection.Output;

                        cmd.Connection = Conn;
                        cmd.ExecuteScalar();
                        MessageBox.Show(cmd.Parameters["@sonuc"].Value.ToString());
                        Conn.Close();
                        FormTextTemizle();
                        txtKayitBarkod.Enabled = true;
                    }

                    btnKitapKaydet.Text = "Kaydet";
                }
                else
                {
                    MessageBox.Show("Tüm Alanlari Eksikziz Doldurunuz..");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
        }

        private void btnKitapIptal_Click(object sender, EventArgs e)
        {
           
            FormTextTemizle();
            btnKitapKaydet.Text = "Kaydet";
            txtKayitBarkod.Enabled = true;
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            // secilen uye guncelleme  
            if (lvUyeArama.SelectedItems.Count>0)
            {
                txtAdi.Text = lvUyeArama.SelectedItems[0].SubItems[0].Text;
                txtSoyadi.Text = lvUyeArama.SelectedItems[0].SubItems[1].Text;
                txtTC.Text = lvUyeArama.SelectedItems[0].SubItems[2].Text;
                txtTelNo.Text = lvUyeArama.SelectedItems[0].SubItems[3].Text;
                txtTC.Enabled = false;
                btnUyeEkle.Text = "Guncelle";
                tsbUyeEkle.PerformClick();

            }
            else
            {
                MessageBox.Show("Lütfen Secim Yapınız.");
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lvUyeArama.SelectedItems.Count>0)
            {
                DialogResult uyeSil = new DialogResult();
                uyeSil = MessageBox.Show("Uye Silinsin Mi", "Uyarı", MessageBoxButtons.YesNo);
                if (uyeSil == DialogResult.Yes)
                {
                    SqlCommand cmd2;
                    cmd2 = new SqlCommand("update Uye set aktifMi=0 where TC=@tc", Conn);
                    cmd2.Parameters.Add("@tc", System.Data.SqlDbType.Float);
                    cmd2.Parameters["@tc"].Value = lvUyeArama.SelectedItems[0].SubItems[2].Text;

                    Conn.Open();
                    cmd2.ExecuteNonQuery();
                    Conn.Close();
                    FormTextTemizle();

                }
                if (uyeSil == DialogResult.No)
                {
                    MessageBox.Show("İslem İptal Edildi");


                }

            }
            else
            {
                MessageBox.Show("Lütfen Secim Yapınız.");
            }
                        
        }

        private void btnKitapGuncelle_Click(object sender, EventArgs e)
        {
            if (lvKitapAramaListesi.SelectedItems.Count>0)
            {
                txtKayitBarkod.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[0].Text;
                txtKitapAdiKayit.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[1].Text;
                cbYazarAdiKayit.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[2].Text;
                cbKategoriKayit.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[3].Text;
                cbyayinEviKayit.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[4].Text;
                txtBasımYılı.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[5].Text;
                txtAdetKayit.Text = lvKitapAramaListesi.SelectedItems[0].SubItems[6].Text;
                txtKayitBarkod.Enabled = false;
                btnKitapKaydet.Text = "Guncelle";
                tsbKitapEkle.PerformClick();
            }
            else
            {
                MessageBox.Show("Lütfen Secim Yapınız.");
            }
        }

        private void btnKitapSil_Click(object sender, EventArgs e)
        {
            if (lvKitapAramaListesi.SelectedItems.Count > 0)
            {
                DialogResult uyeSil = new DialogResult();
                uyeSil = MessageBox.Show("Kitap Silinsin Mi", "Uyarı", MessageBoxButtons.YesNo);
                if (uyeSil == DialogResult.Yes)
                {
                    SqlCommand cmd2;
                    cmd2 = new SqlCommand("delete Kitap where barkod='"+ lvKitapAramaListesi.SelectedItems[0].SubItems[0].Text+"'", Conn);
                   

                    Conn.Open();
                    cmd2.ExecuteNonQuery();
                    Conn.Close();
                    FormTextTemizle();

                }
                if (uyeSil == DialogResult.No)
                {
                    MessageBox.Show("İslem İptal Edildi");
                    FormTextTemizle();


                }

            }
            else
            {
                MessageBox.Show("Lütfen Secim Yapınız.");
            }
        }

        private void tsbOdeme_Click(object sender, EventArgs e)
        {
            FormNesneKontrol();
            grbOdeme.Visible = true;
        }

        private void txtOdemeTc_TextChanged(object sender, EventArgs e)
        {
            if (txtOdemeTc.Text.Length==11)
            {
                btnOdemeAra.PerformClick();

            }
        }

        private void btnOdemeAra_Click(object sender, EventArgs e)
        {
            
            try
            {
                double borc = 0;
                Conn.Open();
                string sqlstmt = "select emanetGecikmeBedeli from Uye where TC=" + txtOdemeTc.Text;
                SqlCommand cmd = new SqlCommand(sqlstmt, Conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                     borc = Convert.ToDouble(dr[0]);
                  


                }
                Conn.Close();
                Conn.Open();
                if (borc > 0)
                {
                    DialogResult odeme = new DialogResult();
                    odeme = MessageBox.Show("Toplam Borc:" + borc + "tl. Odemek İstiyor Musunuz ", "Odeme", MessageBoxButtons.YesNo);


                    if (odeme == DialogResult.Yes)
                    {
                        SqlCommand cmd6;
                        cmd6 = new SqlCommand("update Uye set emanetGecikmeBedeli =0 where TC=" + txtOdemeTc.Text, Conn);



                        cmd6.ExecuteNonQuery();
                        FormTextTemizle();


                    }
                    if (odeme == DialogResult.No)
                    {
                        MessageBox.Show("En Kısa Zamanda Odeme Yapınız");
                        FormTextTemizle();
                    }

                }
                else
                {
                    MessageBox.Show("Borc Bulunmamaktadır");
                    FormTextTemizle();
                }
                Conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }

        }
    }
}
