namespace Kutuphane_Yonetim_Sistemi
{
    partial class AnaSayfa
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaSayfa));
            this.txtPersonelSifre = new System.Windows.Forms.TextBox();
            this.btnGiris = new System.Windows.Forms.Button();
            this.txtPersonelAd = new System.Windows.Forms.TextBox();
            this.grbGiris = new System.Windows.Forms.GroupBox();
            this.pbSifre = new System.Windows.Forms.PictureBox();
            this.pbKullaniciAdi = new System.Windows.Forms.PictureBox();
            this.btnAnaSayfaCikis = new System.Windows.Forms.Button();
            this.grbGiris.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSifre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKullaniciAdi)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPersonelSifre
            // 
            this.txtPersonelSifre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPersonelSifre.Location = new System.Drawing.Point(87, 97);
            this.txtPersonelSifre.Name = "txtPersonelSifre";
            this.txtPersonelSifre.PasswordChar = '*';
            this.txtPersonelSifre.Size = new System.Drawing.Size(142, 22);
            this.txtPersonelSifre.TabIndex = 2;
            this.txtPersonelSifre.Click += new System.EventHandler(this.txtPersonelSifre_Click);
            this.txtPersonelSifre.TextChanged += new System.EventHandler(this.txtPersonelSifre_TextChanged);
            // 
            // btnGiris
            // 
            this.btnGiris.BackgroundImage = global::Kutuphane_Yonetim_Sistemi.Properties.Resources.icons8_enter_100;
            this.btnGiris.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.Location = new System.Drawing.Point(6, 125);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(142, 117);
            this.btnGiris.TabIndex = 3;
            this.btnGiris.Text = "Giris";
            this.btnGiris.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGiris.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // txtPersonelAd
            // 
            this.txtPersonelAd.Location = new System.Drawing.Point(87, 64);
            this.txtPersonelAd.Name = "txtPersonelAd";
            this.txtPersonelAd.Size = new System.Drawing.Size(142, 22);
            this.txtPersonelAd.TabIndex = 1;
            this.txtPersonelAd.Click += new System.EventHandler(this.txtPersonelAd_Click);
            this.txtPersonelAd.TextChanged += new System.EventHandler(this.txtPersonelAd_TextChanged);
            // 
            // grbGiris
            // 
            this.grbGiris.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grbGiris.BackColor = System.Drawing.Color.Transparent;
            this.grbGiris.Controls.Add(this.btnAnaSayfaCikis);
            this.grbGiris.Controls.Add(this.pbSifre);
            this.grbGiris.Controls.Add(this.txtPersonelAd);
            this.grbGiris.Controls.Add(this.pbKullaniciAdi);
            this.grbGiris.Controls.Add(this.btnGiris);
            this.grbGiris.Controls.Add(this.txtPersonelSifre);
            this.grbGiris.Location = new System.Drawing.Point(214, 38);
            this.grbGiris.Name = "grbGiris";
            this.grbGiris.Size = new System.Drawing.Size(353, 266);
            this.grbGiris.TabIndex = 4;
            this.grbGiris.TabStop = false;
            this.grbGiris.Visible = false;
            // 
            // pbSifre
            // 
            this.pbSifre.Image = global::Kutuphane_Yonetim_Sistemi.Properties.Resources.Iconsmind_Outline_Password;
            this.pbSifre.Location = new System.Drawing.Point(49, 97);
            this.pbSifre.Name = "pbSifre";
            this.pbSifre.Size = new System.Drawing.Size(32, 22);
            this.pbSifre.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSifre.TabIndex = 5;
            this.pbSifre.TabStop = false;
            // 
            // pbKullaniciAdi
            // 
            this.pbKullaniciAdi.Image = global::Kutuphane_Yonetim_Sistemi.Properties.Resources.Mahm0udwally_All_Flat_User;
            this.pbKullaniciAdi.Location = new System.Drawing.Point(49, 64);
            this.pbKullaniciAdi.Name = "pbKullaniciAdi";
            this.pbKullaniciAdi.Size = new System.Drawing.Size(32, 22);
            this.pbKullaniciAdi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbKullaniciAdi.TabIndex = 5;
            this.pbKullaniciAdi.TabStop = false;
            // 
            // btnAnaSayfaCikis
            // 
            this.btnAnaSayfaCikis.BackgroundImage = global::Kutuphane_Yonetim_Sistemi.Properties.Resources.icons8_exit_64;
            this.btnAnaSayfaCikis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAnaSayfaCikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAnaSayfaCikis.Location = new System.Drawing.Point(183, 125);
            this.btnAnaSayfaCikis.Name = "btnAnaSayfaCikis";
            this.btnAnaSayfaCikis.Size = new System.Drawing.Size(142, 117);
            this.btnAnaSayfaCikis.TabIndex = 6;
            this.btnAnaSayfaCikis.Text = "Cıkış";
            this.btnAnaSayfaCikis.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAnaSayfaCikis.UseVisualStyleBackColor = true;
            this.btnAnaSayfaCikis.Click += new System.EventHandler(this.btnAnaSayfaCikis_Click);
            // 
            // AnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::Kutuphane_Yonetim_Sistemi.Properties.Resources.silhouette_1632912_1920;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(736, 487);
            this.Controls.Add(this.grbGiris);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnaSayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hos Geldiniz";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnaSayfa_FormClosing);
            this.Load += new System.EventHandler(this.frmGiris_Load);
            this.grbGiris.ResumeLayout(false);
            this.grbGiris.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSifre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKullaniciAdi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtPersonelSifre;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.TextBox txtPersonelAd;
        private System.Windows.Forms.GroupBox grbGiris;
        private System.Windows.Forms.PictureBox pbKullaniciAdi;
        private System.Windows.Forms.PictureBox pbSifre;
        private System.Windows.Forms.Button btnAnaSayfaCikis;
    }
}

