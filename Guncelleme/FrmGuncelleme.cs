using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FaysConcept.Update
{
    public partial class FrmGuncelleme : DevExpress.XtraEditors.XtraForm
    {
        WebClient dosyaIndir = new WebClient();

        // diğer açık olan exe ler tespit etme
        public static bool IsRunning(string ProgramAdi)
        {
            // 0 dan büyük ise çalışıyor demektir.
            return Process.GetProcessesByName(ProgramAdi).Length > 0;
        }

        public FrmGuncelleme()
        {
            InitializeComponent();           
        }

        private void FrmGuncelleme_Load(object sender, EventArgs e)
        {

        }

        private void btnIndir_Click(object sender, EventArgs e)
        {// temp klasörünü kontrol et yok ise oluştur
            if (!Directory.Exists(Application.StartupPath + "\\temp"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\temp");
            }
            dosyaIndir.DownloadProgressChanged += (DownloadProgressChangedEventHandler)IndirmeDurumu;
            // DownloadFileAsync kullanılır ise indirmeyi beklememek için kullanılır.
            dosyaIndir.DownloadFileCompleted += (AsyncCompletedEventHandler)IndirmeBitti;
            dosyaIndir.DownloadFileAsync(new Uri("http://www.fayscrm.com/Download/Tpic/FLG/Update.zip"), Application.StartupPath + "\\temp\\Update.zip");
        }

        private void IndirmeBitti(object sender, AsyncCompletedEventArgs e)
        {
            

            try
            {
                ZipFile.ExtractToDirectory(Application.StartupPath + "\\temp\\Update.zip", Application.StartupPath + "\\temp");
                XElement Dosyalar = XElement.Load(Application.StartupPath + "\\temp\\Liste.xml");
                foreach (var veriler in Dosyalar.Elements().ToList())
                {
                    if (File.Exists(Application.StartupPath + veriler.Element("YuklenecegiKonum").Value))
                    {
                        File.Delete(Application.StartupPath + veriler.Element("YuklenecegiKonum").Value);
                    }
                    File.Copy(Application.StartupPath + "\\temp\\" + veriler.Element("DosyaAdi").Value, Application.StartupPath + veriler.Element("YuklenecegiKonum").Value);
                }

                Directory.Delete(Application.StartupPath + "\\temp", true);
                MessageBox.Show("Güncelleme İşlemi Tamamlandı.");
                if (MessageBox.Show("Program tekrar açılsın mı ?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start($"{Application.StartupPath}\\FLG.exe");
                }
            }
            catch (Exception ex)
            {
                if (Directory.Exists(Application.StartupPath + "\\temp"))
                {
                    Directory.Delete(Application.StartupPath + "\\temp", true);
                }
                MessageBox.Show(ex.Message);
            }
            this.Close();
                
      
        }
        
        public void IndirmeDurumu(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Properties.Maximum = (int)e.TotalBytesToReceive;
            //progressbarın hareketini sağlayacak olan kısım text kısmı bytesrecevied
            progressBar.Text = Convert.ToString(e.BytesReceived);
            lblIndirmeBoyutu.Text = $"{(Convert.ToDecimal(e.BytesReceived) / 1024 / 1024).ToString("N2")} MB\\{(Convert.ToDecimal(e.TotalBytesToReceive) / 1024 / 1024).ToString("N2")} MB";
        }
    }
}
