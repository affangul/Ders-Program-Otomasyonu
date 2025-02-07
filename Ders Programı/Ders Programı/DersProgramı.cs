using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ders_Programı
{
    public partial class DersProgramı : Form
    {
        public DersProgramı()
        {
            InitializeComponent();
            
        }
        private void DersProgramı_Load(object sender, EventArgs e)
        {
            SınıfID();  // form ilk defa açıldığında comboboxların dolması için metotlar çalıştırıldı
            DerslikID();
            ÖğretmenID();
            this.BackColor = Color.Green;
            //this.BackColor = Color.FromArgb(166, 214, 8); //arka planı kivi rengine çevirir
        }

        private void button1_Click(object sender, EventArgs e) // geri dön tuşu
        {
            this.Close(); // önceki form bu form kapanana kadar görünmez kalacağından sadece bu formu kapatarak önceki forma ulaşıyoruz
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // sınıf göster combobox
        {
            sınıfPazartesi(); // combobox a veri tabanından aktarılmış olan sınıf isimlerinden seçilen sınıfın ders programını datagridview e aktarmak için metotlar çalıştırılır
            sınıfSalı();
            sınıfÇarşamba();
            sınıfPerşembe();
            sınıfCuma();
        }

        private void sınıfPazartesi()
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Başlangıç, Bitiş, Öğretmen FROM Final " +
            "WHERE Sınıf = @prm1 AND Gün = 'Pazartesi' " +
            "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView1.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void sınıfSalı()
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Başlangıç, Bitiş, Öğretmen FROM Final " +
            "WHERE Sınıf = @prm1 AND Gün = 'Salı'" +
            "ORDER BY Başlangıç "; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView2.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void sınıfÇarşamba()
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Başlangıç, Bitiş, Öğretmen FROM Final " +
            "WHERE Sınıf = @prm1 AND Gün = 'Çarşamba' " +
            "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView3.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void sınıfPerşembe()
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Başlangıç, Bitiş, Öğretmen FROM Final " +
            "WHERE Sınıf = @prm1 AND Gün = 'Perşembe'" +
            "ORDER BY Başlangıç "; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView4.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void sınıfCuma()
        {
            string secilenSınıf = comboBox1.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Başlangıç, Bitiş, Öğretmen FROM Final " +
            "WHERE Sınıf = @prm1 AND Gün = 'Cuma' " +
            "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıf); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView5.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) //sınıf
        {
            try
            {
                label4.Text = comboBox1.Text;

                string secilenSınıf = comboBox1.SelectedValue.ToString(); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenSınıfID ye tanımlanır
                
                Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                string sql = "SELECT CONCAT(Ö.Öğretmen_Ad , ' ', Ö.Öğretmen_Soyad) AS AdSoyad " +
                             "FROM Sınıf S " +
                             "JOIN Öğretmen Ö ON (S.Öğretmen_ID = Ö.Öğretmen_ID) " +
                             "WHERE S.Sınıf_İsim = @prm1"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag);
                komut.Parameters.AddWithValue("@prm1", secilenSınıf);
                SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
                if (reader.Read())
                {
                    label5.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                }
                Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                Font font = new Font("Arial", 10); // çıktıda gösterilcek yazının türü ve boyutu ayarlanıp font a atandı
                SolidBrush fırça = new SolidBrush(Color.Black); // çıktıda gösterilecek yazının rengi ayarlanıp fırça ya atandı 
                e.Graphics.DrawString("Sınıf: ", font, fırça, 25, 25);
                e.Graphics.DrawString(label4.Text, font, fırça, 125, 25);
                e.Graphics.DrawString("Danışman: ", font, fırça, 25, 55);
                e.Graphics.DrawString(label5.Text, font, fırça, 125, 55);
                int y = 150;
                e.Graphics.DrawString("Pazartesi ", font, fırça, 25, y); //pazartesi
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 25;
                foreach (DataGridViewRow row in dataGridView1.Rows)  // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı  
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Salı ", font, fırça, 25, y); //salı
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView2.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Çarşamba ", font, fırça, 25, y); //çarşamba
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView3.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Perşembe ", font, fırça, 25, y); //perşembe
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView4.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Cuma ", font, fırça, 25, y); //cuma
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView5.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
               
                

            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e) // sınıf çıktı
        {
            printPreviewDialog1.ShowDialog(); // çıktı alabilmek için daha önceden özelliklerden printdocumentin atandığı printpreviewdialog çalıştırıldı
        }

        private void button1_Click_1(object sender, EventArgs e) // sınıf kaydet tuş
        {
            Sınıfreport sr = new Sınıfreport();
            sr.ShowDialog(); // Sınıfreport formu açılır ve bu form ana form kapanana kadar beklemede kalır


        }

        private void SınıfID()
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // derslik göster combobox
        {
            derslikPazartesi();  // combobox a veri tabanından aktarılmış olan derslik isimlerinden seçilen dersliğin ders programını datagridview e aktarmak için metotlar çalıştırılır
            derslikSalı();
            derslikÇarşamba();
            derslikPerşembe();
            derslikCuma();

        }

        private void derslikPazartesi()
        {
            string secilenderslik = comboBox2.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenderslik ye tanımlanır
            
            string sql = "SELECT Ders, Sınıf , Başlangıç, Bitiş, Öğretmen FROM Final " +
                          "WHERE Derslik = @prm1 AND Gün = 'Pazartesi' " +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenderslik); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView8.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void derslikSalı()
        {
            string secilenderslik = comboBox2.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenderslik ye tanımlanır
            
            string sql = "SELECT Ders, Sınıf , Başlangıç, Bitiş, Öğretmen FROM Final " +
                          "WHERE Derslik = @prm1 AND Gün = 'Salı' " +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenderslik); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView9.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void derslikÇarşamba()
        {
            string secilenderslik = comboBox2.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenderslik ye tanımlanır
            
            string sql = "SELECT Ders, Sınıf , Başlangıç, Bitiş, Öğretmen FROM Final " +
                          "WHERE Derslik = @prm1 AND Gün = 'Çarşamba' " +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenderslik); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView10.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void derslikPerşembe()
        {
            string secilenderslik = comboBox2.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenderslik ye tanımlanır
            
            string sql = "SELECT Ders, Sınıf , Başlangıç, Bitiş, Öğretmen FROM Final " +
                          "WHERE Derslik = @prm1 AND Gün = 'Perşembe' " +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenderslik); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView11.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void derslikCuma()
        {
            string secilenderslik = comboBox2.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenderslik ye tanımlanır
            
            string sql = "SELECT Ders, Sınıf , Başlangıç, Bitiş, Öğretmen FROM Final " +
                          "WHERE Derslik = @prm1 AND Gün = 'Cuma' " +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenderslik); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView12.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }

        

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                label6.Text = comboBox2.Text;
                Font font = new Font("Arial", 10);   // çıktıda gösterilcek yazının türü ve boyutu ayarlanıp font a atandı
                SolidBrush fırça = new SolidBrush(Color.Black); // çıktıda gösterilecek yazının rengi ayarlanıp fırça ya atandı
                e.Graphics.DrawString("Derslik: ", font, fırça, 25, 25);
                e.Graphics.DrawString(label6.Text, font, fırça, 100, 25);
                int y = 125;
                e.Graphics.DrawString("Pazartesi ", font, fırça, 25, 100); //pazartesi
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Sınıf", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 25;
                foreach (DataGridViewRow row in dataGridView8.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Salı ", font, fırça, 25, y); //salı
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Sınıf", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView9.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Çarşamba ", font, fırça, 25, y); //çarşamba
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Sınıf", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView10.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Perşembe ", font, fırça, 25, y); //perşembe
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Sınıf", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView11.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Cuma ", font, fırça, 25, y); //cuma
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Sınıf", font, fırça, 125, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 225, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 325, y);
                e.Graphics.DrawString("Öğretmen Ad Soyadı", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView12.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
            }
            catch (Exception)
            {

            }

        }

        private void button5_Click(object sender, EventArgs e) // derslik çıktı
        {
            printPreviewDialog2.ShowDialog(); // çıktı alabilmek için daha önceden özelliklerden printdocumentin atandığı printpreviewdialog çalıştırıldı
        }

        private void button2_Click(object sender, EventArgs e) // derslik kaydetme
        {
            Derslikreport dr = new Derslikreport();
            dr.ShowDialog();
        }

        private void DerslikID()
        {
            
            string sql = "SELECT  Derslik_İ_İsim From Derslikİsim"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox2.DisplayMember = "Derslik_İ_İsim"; // görünecek olan bilgi
            comboBox2.ValueMember = "Derslik_İ_İsim"; //kod kısmında işlenen bilgi
            comboBox2.DataSource = dt;  // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            öğretmenPazartesi(); // combobox a veri tabanından aktarılmış olan sınıf isimlerinden seçilen sınıfın ders programını datagridview e aktarmak için metotlar çalıştırılır
            öğretmenSalı();
            öğretmenÇarşamba();
            öğretmenPerşembe();
            öğretmenCuma();
        }
        private void öğretmenPazartesi()
        {
            string secilenöğretmen = comboBox3.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenöğretmen ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Sınıf, Başlangıç, Bitiş FROM Final " +
                          "WHERE Öğretmen = @prm1 AND Gün = 'Pazartesi'" +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenöğretmen); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView15.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void öğretmenSalı()
        {
            string secilenöğretmen = comboBox3.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenöğretmen ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Sınıf, Başlangıç, Bitiş FROM Final " +
                          "WHERE Öğretmen = @prm1 AND Gün = 'Salı'" +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenöğretmen); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView16.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void öğretmenÇarşamba()
        {
            string secilenöğretmen = comboBox3.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenöğretmen ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Sınıf, Başlangıç, Bitiş FROM Final " +
                          "WHERE Öğretmen = @prm1 AND Gün = 'Çarşamba'" +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenöğretmen); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView17.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void öğretmenPerşembe()
        {
            string secilenöğretmen = comboBox3.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenöğretmen ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Sınıf, Başlangıç, Bitiş FROM Final " +
                          "WHERE Öğretmen = @prm1 AND Gün = 'Perşembe'" +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenöğretmen); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView18.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }
        private void öğretmenCuma()
        {
            string secilenöğretmen = comboBox3.SelectedValue.ToString();  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenöğretmen ye tanımlanır
            
            string sql = "SELECT Ders, Derslik, Sınıf, Başlangıç, Bitiş FROM Final " +
                          "WHERE Öğretmen = @prm1 AND Gün = 'Cuma'" +
                          "ORDER BY Başlangıç"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenöğretmen); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataAdapter da = new SqlDataAdapter(komut); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView19.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                label7.Text = comboBox3.Text;
                Font font = new Font("Arial", 10);   // çıktıda gösterilcek yazının türü ve boyutu ayarlanıp font a atandı
                SolidBrush fırça = new SolidBrush(Color.Black); // çıktıda gösterilecek yazının rengi ayarlanıp fırça ya atandı
                e.Graphics.DrawString("Öğretmen: ", font, fırça, 25, 25);
                e.Graphics.DrawString(label7.Text, font, fırça, 100, 25);
                int y = 125;
                e.Graphics.DrawString("Pazartesi ", font, fırça, 25, 100); //pazartesi
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi     // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Sınıf", font, fırça, 225, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 325, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 425, y);
                y += 25;
                foreach (DataGridViewRow row in dataGridView15.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Salı ", font, fırça, 25, y); //salı
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Sınıf", font, fırça, 225, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 325, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView16.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Çarşamba ", font, fırça, 25, y); //çarşamba
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Sınıf", font, fırça, 225, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 325, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView17.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Perşembe ", font, fırça, 25, y); //perşembe
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Sınıf", font, fırça, 225, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 325, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView18.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;
                e.Graphics.DrawString("Cuma ", font, fırça, 25, y); //cuma
                y += 25;
                e.Graphics.DrawString("Ders", font, fırça, 25, y);   // datagirdview den yansıtılan bilgilerin bakan herkesin anlayabilmesi için bilginin neyle alakalı olduğu gösterildi
                e.Graphics.DrawString("Derslik", font, fırça, 125, y);
                e.Graphics.DrawString("Sınıf", font, fırça, 225, y);
                e.Graphics.DrawString("Başlangıç", font, fırça, 325, y);
                e.Graphics.DrawString("Bitiş", font, fırça, 425, y);
                y += 50;
                foreach (DataGridViewRow row in dataGridView19.Rows) // veri tabanından alınıp datagirdview e aktarılmış bilgileri çekmek için foreach kullanıldı
                {
                    int x = 25;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string cellValue = cell.FormattedValue.ToString();
                        e.Graphics.DrawString(cellValue, font, fırça, x, y);
                        x += 100;
                    }
                    y += 25;
                }
                y += 25;


            }
            catch (Exception)
            {

            }

        }



        private void button6_Click(object sender, EventArgs e) // öğretmen çıktı
        {
            printPreviewDialog3.ShowDialog();  // çıktı alabilmek için daha önceden özelliklerden printdocumentin atandığı printpreviewdialog çalıştırıldı
        }

        private void button4_Click(object sender, EventArgs e) // öğretmen kaydet
        {
            Öğretmenreport ör = new Öğretmenreport();
            ör.ShowDialog();
        }

        private void ÖğretmenID()
        {
            
            string sql = "SELECT (Öğretmen_Ad +' '+ Öğretmen_Soyad) AS AdSoyad From Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // daha önceden sql ile Program.bag birleştirildiğinden dolayı buraya komut yazarak da ya @prm1 i tanıtmış oluyoruz
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox3.DisplayMember = "AdSoyad";  // görünecek olan bilgi
            comboBox3.ValueMember = "AdSoyad"; //kod kısmında işlenen bilgi
            comboBox3.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close(); // formu kapatır
        }
    }
}
        
