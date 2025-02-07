using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ders_Programı
{
    public partial class Sınıfreport : Form
    {
        public Sınıfreport()
        {
            InitializeComponent();
            sınıfcombo(); // formda ilk combobox doldurma metodu çalıştırılıyor
        }

        private void Sınıfreport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e) // formu kapatma tuşu
        {
            this.Close();
        }

        private void sınıfcombo() // sınıf ismini combobox1 de göstermek için metot
        {
            
            string sql = "SELECT Sınıf_İsim From Sınıf"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close();  // veri tabanı bağlantısı kapatılır
            comboBox1.DisplayMember = "Sınıf_İsim"; // görünecek olan bilgi
            comboBox1.ValueMember = "Sınıf_İsim"; //kod kısmında işlenen bilgi
            comboBox1.DataSource = dt;  // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void report()
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenSınıf ye tanımlanır
            
            Program.bag.Open();
            string sql = "SELECT Gün, Ders, Derslik, Öğretmen, Başlangıç, Bitiş FROM Final " +
                          "WHERE Sınıf = @prm1 " +
                          "ORDER BY Gün, Başlangıç;"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // veri tabanından alınan bilgile da değişkenine kaydedilir
            DataTable dt = new DataTable(); // alınacak bilgileri bir yere yüklemek için önce datatable la eklenir
            da.Fill(dt); // veile dt nin içine kaydedilir
            ReportDataSource ds = new ReportDataSource("DataSet1", dt); //reportviewerın dataset1 ile bağlantısı sağlanır ve dt verisi ekleni 
            reportViewer1.LocalReport.DataSources.Clear(); // çakışma olmaması için önceki veriler temizlenir
            reportViewer1.LocalReport.DataSources.Add(ds); // repotviewer1 in içine ds değişkenine yüklenmiş bilgiler aktarılır
            reportViewer1.LocalReport.Refresh(); // reportviewer1 yeniler
            reportViewer1.RefreshReport(); // reportviewer1 yeniler
            Program.bag.Close();
            ekbilgi(); //reportviewer da olan textbox doldurmak için metot çalıştırıldı

        }
        private void ekbilgi() // word veya pdf çevirilirken çevrilen sınıfın ismini ve danışman öğretmenin bilgisini göstermek için metot
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString(); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenSınıfID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT CONCAT(Ö.Öğretmen_Ad , ' ', Ö.Öğretmen_Soyad) AS AdSoyad " +
                         "FROM Sınıf S " +
                         "JOIN Öğretmen Ö ON (S.Öğretmen_ID = Ö.Öğretmen_ID) " +
                         "WHERE S.Sınıf_İsim = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // @prm işleminin ne olduğu tanımlanıyor
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            string öğretmen = "Danışman: "; //sınıf danışmanını göstermek için en başta danışman kısmını içeren veri oluşturuldu
            if (reader.Read())
            {
                öğretmen += reader.GetString(0); // sql den gelen veri ile daha önceden yazılmış olan veri birleştirildi
            }
            string sınıfisim = "Sınıf: " + secilenSınıf; // ismi secilensınıf dan çekildi
            ReportParameterCollection rprm = new ReportParameterCollection(); //parameterları tanımlamak için değişken tanımlandı
            rprm.Add(new ReportParameter("Sınıfisim", sınıfisim)); //daha önceden oluşturulmuş parameter ismi ile eklenecek veri eşleştirildi
            rprm.Add(new ReportParameter("SınıfÖğretmen", öğretmen)); //daha önceden oluşturulmuş parameter ismi ile eklenecek veri eşleştirildi
            reportViewer1.LocalReport.SetParameters(rprm); // parameter işlemi yapıldı
            this.reportViewer1.RefreshReport(); // işlemin görülmesi için sayfa yenilendi
        }

        private void button2_Click(object sender, EventArgs e) // reportviewerı yenileme tuşu
        {
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            report(); // form açıldığında reportviewerın çalışması işin metot çalışır
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            report(); // combobox1 de bir sınıf değiştiğinde yeni sınıfın bilgilerini göstermek için indexchanged içine metot yazılır
            this.reportViewer1.RefreshReport();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
