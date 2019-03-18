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
    
    public partial class AnaSayfa : Form
    {
        // server 
        SqlConnection Conn = new SqlConnection(
        ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);
        // personel ıd  Kutuphane Form da kullanılacak
        public static int personelID = 0;
        // girisForm boyutları
        int SimdikiWidth = 1280;
        int SimdikiHeight = 800;
        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void frmGiris_Load(object sender, EventArgs e)
        {
            // from ekran  cözünürlüge göre boyut ayarlama
            this.Location =new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            Rectangle ClientCozunurluk = new Rectangle();
            ClientCozunurluk = Screen.GetBounds(ClientCozunurluk);
            float OranWidth = ((float)ClientCozunurluk.Width / (float)SimdikiWidth);
            float OranHeight = ((float)ClientCozunurluk.Height / (float)SimdikiHeight);

            this.Scale(OranWidth, OranHeight);

            grbGiris.Visible = true;

        }

       
        private void txtPersonelSifre_TextChanged(object sender, EventArgs e)
        {
            txtPersonelSifre.ForeColor = Color.Black;
        }

      

        private void setdefault()
        {
            txtPersonelAd.Text = "Kullanici Adi";
            txtPersonelSifre.Text = "Password";
            txtPersonelAd.ForeColor = Color.Gray;
            txtPersonelSifre.ForeColor = Color.Gray;
        }

        private void txtPersonelSifre_Click(object sender, EventArgs e)
        {
            setdefault();
            if (txtPersonelSifre.Text == "Password")
            {
                txtPersonelSifre.Text = "";
            }
        }

        private void txtPersonelAd_TextChanged(object sender, EventArgs e)
        {
            txtPersonelAd.ForeColor = Color.Black;
        }

        private void txtPersonelAd_Click(object sender, EventArgs e)
        {
            setdefault();
            if (txtPersonelAd.Text == "Kullanici Adi")
            {
                txtPersonelAd.Text = "";
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            // Ana sayfada giris icin personel ad ve sifre alınıyor  veritabanında kontrol ediliyor 
            // dogru ise girs yapılıyor 
            try
            {

                Conn.Open();
                string sqlstmt = "select *from dbo.Personel where personel ='" + txtPersonelAd.Text + "' and sifre='" + txtPersonelSifre.Text + "'";
                SqlCommand cmd7 = new SqlCommand(sqlstmt, Conn);
                SqlDataReader dr = cmd7.ExecuteReader();
                while (dr.Read())
                {
                    personelID = Convert.ToInt32( dr["personelID"].ToString());
                }
            
                Conn.Close();

                if (personelID >0)
                {
                    Kutuphane kutuphaneForm = new Kutuphane();
                    kutuphaneForm.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Sifre veya Personel Adı Yanlış");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("",ex.Message);
            }
           
        }

        private void btnAnaSayfaCikis_Click(object sender, EventArgs e)
        {
            // cıkıs icin uyarı veriyor 
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("Program kapatılmadı.");
            }
        }

        private void AnaSayfa_FormClosing(object sender, FormClosingEventArgs e)
        {
            // form exit basıldıgı zaman önce uyarı veriyoruz
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Devam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
                Application.Exit();
            }
            if (cikis == DialogResult.No)
            {
                MessageBox.Show("Program kapatılmadı.");
                this.Show();
            }
        }
    }
}
