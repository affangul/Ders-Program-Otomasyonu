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
    public partial class Ana : Form
    {

        public Ana()
        {
            InitializeComponent();
        }

        private void Ana_Load(object sender, EventArgs e)
        {
            DersVT();              // program ilk açıldığında veri tabanında olan bilgileri yükleyecek metotlar çalıştırıldı.
            DersID();
            Dersdersliktürcombox();
            Dersöğretmencombox();
            Dersmüfredatcombox(); 
            DerslikİsimVT();
            DerslikİsimID();
            Dersliktürcombobox();
            DerslikTürVT();
            DersliktürID();
            ÖğretmenVT();
            ÖğretmenID();
            SınıfVT();
            SınıfID();
            Sınıfmüfredatcombox();
            Sınıföğretmencombox();
            MüfredatVT();
            MüfredatID();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        /*
          Ders tablo
         */

        private void BilgiDerseklebutton_Click(object sender, EventArgs e)
        { 
 
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "insert into Ders (Ders_İsim,Ders_Saat,Müfredat_ID,Derslik_T_ID,Öğretmen_ID) values(@prm1,@prm2,@prm3,@prm4,@prm5)";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag);  // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", textBox1.Text);  // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
            komut.Parameters.AddWithValue("@prm2", textBox9.Text);
            komut.Parameters.AddWithValue("@prm3", comboBox1.SelectedValue);
            komut.Parameters.AddWithValue("@prm4", comboBox2.SelectedValue);
            komut.Parameters.AddWithValue("@prm5", comboBox4.SelectedValue);  
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
            Program.bag.Close();  // veri tabanı bağlantısı kapatılır
            if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra eklemenin başarılı olup olmadığı kontrol edilir
            {
                MessageBox.Show("Ders kaydı başarıyla yapıldı.");  // eklemenin başarılı olduğu mesajını verir
            }
            else
            {
                MessageBox.Show("Ders kaydı başarısız oldu.");    // eklemenin başarısız olduğunu mesajını verir
            }
          

            textBox1.Clear();  // textboxı temizler
            textBox9.Clear();  // textboxı temizler
            DersVT();  // yeni bilgileri ekrana yansıtmak için önceden yazılmış metotlar çalıştırılır
            DersID();
        }

        private void button17_Click(object sender, EventArgs e) //ders bilgi sil
        { 
            
            string sql = "DELETE FROM Ders WHERE Ders_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", comboBox3.SelectedValue); // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
            DialogResult result = MessageBox.Show("Seçili dersi silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);  // ekrana evet ya da hayır mesajı içeren messagebox çıkar
            if (result == DialogResult.Yes)  // evete tıklandığıysa if in içine girer
            {
                try
                {
                    Program.bag.Open();  //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder 
                    if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra silmenin başarılı olup olmadığı kontrol edilir
                    {
                        MessageBox.Show("Seçilen ders silindi.");  // silmenin başarılı olduğu mesajını verir
                        DersVT(); // yeni bilgileri ekrana yansıtmak için önceden yazılmış metotlar çalıştırılır
                        DersID();
                    }
                    else
                    {
                        MessageBox.Show("Ders silinemedi.");  // işlemin yapılamadığını bilgisini verir
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);   // işlem yapılırken hata olduğunun bilgisini verir
                }
                finally
                {
                    Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi.");  // hayıra tıklandığında işlemin iptal edildiği bilgisini verir
            }
        }


        private void BilgiDersdüzenlebutton_Click(object sender, EventArgs e)
        {
            
                
                Program.bag.Open();  //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                string sql = "UPDATE Ders SET Ders_İsim = @prm1, Ders_Saat = @prm2, Müfredat_ID = @prm3, Derslik_T_ID = @prm4, Öğretmen_ID = @prm5 WHERE Ders_ID = @prm6"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                komut.Parameters.AddWithValue("@prm1", textBox71.Text);  // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
                komut.Parameters.AddWithValue("@prm2", textBox17.Text);
                komut.Parameters.AddWithValue("@prm3", comboBox23.SelectedValue);
                komut.Parameters.AddWithValue("@prm4", comboBox25.SelectedValue);
                komut.Parameters.AddWithValue("@prm5", comboBox26.SelectedValue);
                komut.Parameters.AddWithValue("@prm6", comboBox24.SelectedValue);
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
                Program.bag.Close();  // veri tabanı bağlantısı kapatılır

                if (etkilenenSatirSayisi > 0) // komut çalıştıktan sonra düzenlemenin başarılı olup olmadığı kontrol edilir
                {
                    MessageBox.Show("Ders kaydı başarıyla güncellendi."); // düzenlemenin başarılı olduğu mesajını verir
                    DersVT();  
                }
                else
                {
                    MessageBox.Show("Ders kaydı güncellenemedi."); // işlemin yapılamadığını bilgisini verir
                }
           


        }

        private void BilgiDersVTbutton_Click(object sender, EventArgs e)
        {
            DersVT(); //yenile tuşuna basıldığında yenilenin basıldığı sayfada veri tabanından alınan bilgileri yenilenmesi için metotlar çalıştırılır
            DersID();
            Dersöğretmencombox();
            Dersmüfredatcombox();
            Dersdersliktürcombox();
        }

        private void DersVT()
        {
            
            string sql = "SELECT D.Ders_ID, D.Ders_İsim, D.Ders_Saat, M.Müfredat_Bölüm, T.Derslik_T_İsim, Ö.Öğretmen_Ad, Ö.Öğretmen_Soyad " +
                          "FROM Ders D " +
                          "JOIN Müfredat M ON (D.Müfredat_ID = M.Müfredat_ID) " +
                          "JOIN DerslikTür T ON (D.Derslik_T_ID = T.Derslik_T_ID) " +
                          "JOIN Öğretmen Ö ON (D.Öğretmen_ID = Ö.Öğretmen_ID) "; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView1.DataSource = dt;  // dt üzerine kaydedilmiş bilgiyi datagridview e aktarır
            dataGridView1.Columns[0].HeaderText = "Ders ID";  // data gridview de sütunların isimlerini kullanıcın gözüne hoş gözükecek şekilde değiştirdim
            dataGridView1.Columns[1].HeaderText = "Ders İsim";
            dataGridView1.Columns[2].HeaderText = "Ders Saat";
            dataGridView1.Columns[3].HeaderText = "Müfredat";
            dataGridView1.Columns[4].HeaderText = "Derslik Tür";
            dataGridView1.Columns[5].HeaderText = "Öğretmen Ad";
            dataGridView1.Columns[6].HeaderText = "Öğretmen Soyad";
        }
        private void Dersöğretmencombox()
        {
            Dersöğretmeneklecmbx();    //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Dersöğretmendüzenlecmbx();


        }

        private void Dersöğretmeneklecmbx()
        {
            
            string sql = "SELECT Öğretmen_ID, (Öğretmen_Ad +' '+ Öğretmen_Soyad) AS AdSoyad From Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox4.DisplayMember = "AdSoyad";  // görünecek olan bilgi
            comboBox4.ValueMember = "Öğretmen_ID"; //kod kısmında işlenen bilgi
            comboBox4.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
        private void Dersöğretmendüzenlecmbx()
        {
            
            string sql = "SELECT Öğretmen_ID, (Öğretmen_Ad +' '+ Öğretmen_Soyad) AS AdSoyad From Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox26.DisplayMember = "AdSoyad";  // görünecek olan bilgi
            comboBox26.ValueMember = "Öğretmen_ID"; //kod kısmında işlenen bilgi
            comboBox26.DataSource = dt;  // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void Dersmüfredatcombox()
        {
            Dersmüfredateklecmbx();   //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Dersmüfredatdüzenlecmbx();



        }

        private void Dersmüfredateklecmbx()
        {
            
            string sql = "SELECT Müfredat_ID, Müfredat_Bölüm AS Bölüm From Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox1.DisplayMember = "Bölüm"; // görünecek olan bilgi
            comboBox1.ValueMember = "Müfredat_ID"; //kod kısmında işlenen bilgi
            comboBox1.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
        private void Dersmüfredatdüzenlecmbx()
        {
            
            string sql = "SELECT Müfredat_ID, Müfredat_Bölüm AS Bölüm From Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox23.DisplayMember = "Bölüm"; // görünecek olan bilgi
            comboBox23.ValueMember = "Müfredat_ID"; //kod kısmında işlenen bilgi
            comboBox23.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void Dersdersliktürcombox()
        {
            Dersdersliktüreklecmbx();   //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Dersdersliktürdüzenlecmbx();
            
            
        }
        private void Dersdersliktüreklecmbx()
        {
            
            string sql = "SELECT Derslik_T_ID, Derslik_T_İsim AS Tür From DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable();  // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox2.DisplayMember = "Tür"; // görünecek olan bilgi
            comboBox2.ValueMember = "Derslik_T_ID"; //kod kısmında işlenen bilgi
            comboBox2.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
        private void Dersdersliktürdüzenlecmbx()
        {
            
            string sql = "SELECT Derslik_T_ID, Derslik_T_İsim AS Tür From DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox25.DisplayMember = "Tür"; // görünecek olan bilgi
            comboBox25.ValueMember = "Derslik_T_ID"; //kod kısmında işlenen bilgi
            comboBox25.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void DersID()
        {
            DerssilID();     //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            DersdüzenleID();



           /* if (comboBox3.Items.Count != null || comboBox24.Items.Count != null)  // silinebilecek veya düzenlenebilecek bir bilgi olmadığında tuşlar etkisiz hale getirildi
            {
                BilgiDerssilbutton.Enabled = true;
                BilgiDersdüzenlebutton.Enabled = true;
            }
            else
            {
                BilgiDerssilbutton.Enabled = false;
                BilgiDersdüzenlebutton.Enabled= false;
            }*/

        }
        private void DerssilID()
        {
            
            string sql = "SELECT Ders_ID FROM Ders"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox3.DisplayMember = "Ders_ID"; // görünecek olan bilgi
            comboBox3.ValueMember = "Ders_ID"; //kod kısmında işlenen bilgi
            comboBox3.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void DersdüzenleID()
        {
            
            string sql = "SELECT Ders_ID FROM Ders"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox24.DisplayMember = "Ders_ID"; // görünecek olan bilgi
            comboBox24.ValueMember = "Ders_ID"; //kod kısmında işlenen bilgi
            comboBox24.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
   
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)  // ders sil ıd
        {
            int secilenDersID = Convert.ToInt32(comboBox3.SelectedValue);  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
            Program.bag.Open();//veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT D.Ders_İsim, D.Ders_Saat, M.Müfredat_Bölüm, T.Derslik_T_İsim, CONCAT(Ö.Öğretmen_Ad, ' ', Ö.Öğretmen_Soyad) AS AdSoyad " +
                         "FROM Ders D " +
                         "JOIN Müfredat M ON (D.Müfredat_ID = M.Müfredat_ID) " +
                         "JOIN DerslikTür T ON (D.Derslik_T_ID = T.Derslik_T_ID) " +
                         "JOIN Öğretmen Ö ON (Ö.Öğretmen_ID = D.Öğretmen_ID)     " +
                         "WHERE D.Ders_ID = @prm1";   // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenDersID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
               textBox74.Text = reader.GetString(0);  // okunan veriye göre gelen bilgileri sırasıyla yansıtır
               textBox18.Text = reader.GetInt32(1).ToString();
               textBox26.Text = reader.GetString(2);
               textBox27.Text = reader.GetString(3);
               textBox28.Text = reader.GetString(4);
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void comboBox24_SelectedIndexChanged(object sender, EventArgs e) // ders düzenle ıd
        {
          
                  int secilenDersID = Convert.ToInt32(comboBox24.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersID ye tanımlanır
            
                  Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT D.Ders_İsim, D.Ders_Saat, M.Müfredat_Bölüm, T.Derslik_T_İsim, CONCAT(Ö.Öğretmen_Ad, ' ', Ö.Öğretmen_Soyad) AS AdSoyad " +
                               "FROM Ders D " +
                               "JOIN Müfredat M ON (D.Müfredat_ID = M.Müfredat_ID) " +
                               "JOIN DerslikTür T ON (D.Derslik_T_ID = T.Derslik_T_ID) " +
                               "JOIN Öğretmen Ö ON (Ö.Öğretmen_ID = D.Öğretmen_ID)     " +
                               "WHERE D.Ders_ID = @prm1";  // sql komutu kaydedilir
                  SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenDersID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader();  // reader aracılığı ile gelen veri okunur
                  while (reader.Read())
                  {
                    textBox71.Text = reader.GetString(0);  // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                    textBox17.Text = reader.GetInt32(1).ToString();
                    comboBox23.Text = reader.GetString(2);
                    comboBox25.Text = reader.GetString(3);
                    comboBox26.Text = reader.GetString(4);

                  }
                  Program.bag.Close();
        }


        /*
          Derslik İsim tablo
         */

        private void BilgiDerslikİsimeklebutton_Click(object sender, EventArgs e)
        {
          try
          {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "insert into Derslikİsim (Derslik_İ_İsim,Derslik_T_ID) values(@prm1,@prm2)"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                komut.Parameters.AddWithValue("@prm1", textBox2.Text);
            komut.Parameters.AddWithValue("@prm2", comboBox7.SelectedValue);
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
                Program.bag.Close();  // veri tabanı bağlantısı kapatılır
                if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra eklemenin başarılı olup olmadığı kontrol edilir
                {
                    MessageBox.Show("Derslik kaydı başarıyla yapıldı.");  // eklemenin başarılı olduğu mesajını verir
                }
                else
                {
                    MessageBox.Show("Derslik kaydı başarısız oldu.");    // eklemenin başarısız olduğunu mesajını verir
                }
          }
          catch (Exception ex)
          {
          MessageBox.Show("Hata oluştu: " + ex.Message);  // hata mesajını verir
          }
            textBox2.Clear();
            DerslikİsimVT();
            DerslikİsimID();
        }

        private void BilgiDerslikİsimsilbutton_Click(object sender, EventArgs e)
        {
            
            string sql = "DELETE FROM Derslikİsim WHERE Derslik_İ_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", comboBox5.SelectedValue);
            DialogResult result = MessageBox.Show("Seçili dersliği silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();
                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Seçilen derslik silindi.");
                        DerslikİsimVT();
                        DerslikİsimID();
                    }
                    else
                    {
                        MessageBox.Show("Derslik silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi.");
            }
        }

        private void BilgiDerslikİsimdüzenlebutton_Click(object sender, EventArgs e)
        {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "UPDATE Derslikİsim SET Derslik_İ_İsim = @prm1, Derslik_T_ID = @prm2 WHERE Derslik_İ_ID = @prm3"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", textBox15.Text);
            komut.Parameters.AddWithValue("@prm2", comboBox22.SelectedValue);
            komut.Parameters.AddWithValue("@prm3", comboBox21.SelectedValue);

            komut.ExecuteNonQuery();
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();
            if (etkilenenSatirSayisi > 0)
            {
                MessageBox.Show("Derslik bilgileri güncellenmiştir.");
                DerslikİsimVT();
            }
            else
            {
                MessageBox.Show("Derslik güncellenemedi.");
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void BilgiDerslikİsimVTbutton_Click(object sender, EventArgs e)
        {
            DerslikİsimVT();  //yenile tuşuna basıldığında yenilenin basıldığı sayfada veri tabanından alınan bilgileri yenilenmesi için metotlar çalıştırılır
            DerslikİsimID();
            Dersliktürcombobox();
        }

        private void DerslikİsimVT()
        {
            
            string sql = "SELECT K.Derslik_İ_ID, K.Derslik_İ_İsim, T.Derslik_T_İsim  FROM Derslikİsim K " +
                "JOIN DerslikTür T ON (K.Derslik_T_ID = T.Derslik_T_ID)"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView2.DataSource = dt;
            dataGridView2.Columns[0].HeaderText = "Derslik İsim ID"; // data gridview de sütunların isimlerini kullanıcın gözüne hoş gözükecek şekilde değiştirdim
            dataGridView2.Columns[1].HeaderText = "Derslik İsim";
            dataGridView2.Columns[2].HeaderText = "Derslik Tür";
        }
        private void DerslikİsimID()
        {
            DerslikİsimsilID();   //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            DerslikdüzenleID();
        }
        
        private void DerslikİsimsilID()
        {
            
            string sql = "SELECT Derslik_İ_ID FROM Derslikİsim"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox5.DisplayMember = "Derslik_İ_ID"; // görünecek olan bilgi
            comboBox5.ValueMember = "Derslik_İ_ID"; //kod kısmında işlenen bilgi
            comboBox5.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void DerslikdüzenleID()
        {
            
            string sql = "SELECT Derslik_İ_ID FROM Derslikİsim"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox21.DisplayMember = "Derslik_İ_ID"; // görünecek olan bilgi
            comboBox21.ValueMember = "Derslik_İ_ID"; //kod kısmında işlenen bilgi
            comboBox21.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
        
        private void Dersliktürcombobox()
        {
            Dersliktüreklecmbx();   //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Dersliktürdüzenlecmbx();
        }

        private void Dersliktüreklecmbx()
        {
            
            string sql = "SELECT Derslik_T_ID, Derslik_T_İsim AS Tür From DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable();  // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox7.DisplayMember = "Tür"; // görünecek olan bilgi
            comboBox7.ValueMember = "Derslik_T_ID"; //kod kısmında işlenen bilgi
            comboBox7.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void Dersliktürdüzenlecmbx()
        {
            
            string sql = "SELECT Derslik_T_ID, Derslik_T_İsim AS Tür From DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag);  // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox22.DisplayMember = "Tür"; // görünecek olan bilgi
            comboBox22.ValueMember = "Derslik_T_ID"; //kod kısmında işlenen bilgi
            comboBox22.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)  // derslik isim sil
        {
            int secilenDerslikisimID = Convert.ToInt32(comboBox5.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDerslikID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT K.Derslik_İ_İsim, T.Derslik_T_İsim " +
                         "FROM Derslikİsim K " +
                         "JOIN DerslikTür T ON (K.Derslik_T_ID = T.Derslik_T_ID ) " +
                         "WHERE K.Derslik_İ_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenDerslikisimID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox20.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                textBox29.Text = reader.GetString(1);

            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e) // derslik isim düzenle
        {
            int secilenDerslikisimID = Convert.ToInt32(comboBox21.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDerslikID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT K.Derslik_İ_İsim, T.Derslik_T_İsim " +
                         "FROM Derslikİsim K " +
                         "JOIN DerslikTür T ON (K.Derslik_T_ID = T.Derslik_T_ID ) " +
                         "WHERE K.Derslik_İ_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenDerslikisimID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox15.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                comboBox22.Text = reader.GetString(1);
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        /*
          Derslik Tür tablo
         */


        private void BilgiDerslikTüreklebutton_Click(object sender, EventArgs e)
        {
          try
          { 
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "insert into DerslikTür (Derslik_T_İsim) values(@prm1)"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                komut.Parameters.AddWithValue("@prm1", textBox3.Text);
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
            Program.bag.Close();  // veri tabanı bağlantısı kapatılır
            if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra eklemenin başarılı olup olmadığı kontrol edilir
            {
                MessageBox.Show("Derslik tür kaydı başarıyla yapıldı.");  // eklemenin başarılı olduğu mesajını verir
            }
            else
            {
                MessageBox.Show("Derslik tür kaydı başarısız oldu.");    // eklemenin başarısız olduğunu mesajını verir
            }
          }
          catch (Exception ex)
          {
          MessageBox.Show("Hata oluştu: " + ex.Message);  // hata mesajını verir
          }
            textBox3.Clear();
            DerslikTürVT();
            DersliktürID();
            Dersliktürcombobox();
            Dersdersliktürcombox();
        }

        private void BilgiDerslikTürsilbutton_Click(object sender, EventArgs e)  // düzeltilecek yanlış bilgiler
        {
            
            string sql = "DELETE FROM DerslikTür WHERE Derslik_T_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", comboBox6.SelectedValue);
            DialogResult result = MessageBox.Show("Seçili derslik türünü silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();
                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Seçilen derslik türü silindi.");
                        DerslikTürVT();
                        DersliktürID();
                        Dersliktürcombobox();
                        Dersdersliktürcombox();
                    }
                    else
                    {
                        MessageBox.Show("Derslik türü silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi.");
            }
        }

        private void BilgiDerslikTürdüzenlebutton_Click(object sender, EventArgs e)
        {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "UPDATE DerslikTür SET Derslik_T_İsim = @prm1 WHERE Derslik_T_ID = @prm2";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", textBox15.Text);
            komut.Parameters.AddWithValue("@prm2", comboBox20.SelectedValue);

            komut.ExecuteNonQuery();
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();
            if (etkilenenSatirSayisi > 0)
            {
                MessageBox.Show("Derslik bilgileri güncellenmiştir.");
                Dersliktürcombobox();
                Dersdersliktürcombox();
                DerslikTürVT();
            }
            else
            {
                MessageBox.Show("Derslik güncellenemedi.");
            }
            Program.bag.Close();
        }

        private void BilgiDerslikTürVTbutton_Click(object sender, EventArgs e)
        {
            DerslikTürVT();  //yenile tuşuna basıldığında yenilenin basıldığı sayfada veri tabanından alınan bilgileri yenilenmesi için metotlar çalıştırılır
            DersliktürID();
        }

        private void DerslikTürVT()
        {
            
            string sql = "select * from DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView6.DataSource = dt;
            dataGridView6.Columns[0].HeaderText = "Derslik Tür ID";
            dataGridView6.Columns[1].HeaderText = "Derslik Tür İsim";

        }
        private void DersliktürID()
        {
            DersliktürsilID();  //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            DersliktürdüzenleID();
        }

        private void DersliktürsilID()
        {
            
            string sql = "SELECT Derslik_T_ID FROM DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox6.DisplayMember = "Derslik_T_ID";  // görünecek olan bilgi
            comboBox6.ValueMember = "Derslik_T_ID"; //kod kısmında işlenen bilgi
            comboBox6.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        } 

        private void DersliktürdüzenleID()
        {
            
            string sql = "SELECT Derslik_T_ID FROM DerslikTür"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close();
            comboBox20.DisplayMember = "Derslik_T_ID";  // görünecek olan bilgi
            comboBox20.ValueMember = "Derslik_T_ID"; //kod kısmında işlenen bilgi
            comboBox20.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)  // derslik tür sil
        {
            int secilenDersliktürID = Convert.ToInt32(comboBox6.SelectedValue);  // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersliktürID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT Derslik_T_İsim FROM DerslikTür WHERE Derslik_T_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenDersliktürID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox19.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)  // derslik tür düzenle
        {
            int secilenDersliktürID = Convert.ToInt32(comboBox20.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenDersliktürID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT Derslik_T_İsim FROM DerslikTür WHERE Derslik_T_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenDersliktürID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox16.Text = reader.GetString(0);
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        /*
          Öğretmen tablo
         */

        private void BilgiÖğretmeneklebutton_Click(object sender, EventArgs e)
        {
          try
          {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "insert into Öğretmen (Öğretmen_Ad, Öğretmen_Soyad, Pzt_Bas, Pzt_Bit, Salı_Bas, Salı_Bit, Çarşamba_Bas, Çarşamba_Bit," +
                         " Perşembe_Bas, Perşembe_Bit, Cuma_Bas, Cuma_Bit)" +
                         " values(@prm1,@prm2,@prm3,@prm4,@prm5,@prm6,@prm7,@prm8,@prm9,@prm10,@prm11,@prm12)"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                komut.Parameters.AddWithValue("@prm1", textBox4.Text); // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
                komut.Parameters.AddWithValue("@prm2", textBox5.Text);
                komut.Parameters.AddWithValue("@prm3", textBox23.Text);
                komut.Parameters.AddWithValue("@prm4", textBox13.Text);
                komut.Parameters.AddWithValue("@prm5", textBox31.Text);
                komut.Parameters.AddWithValue("@prm6", textBox32.Text);
                komut.Parameters.AddWithValue("@prm7", textBox33.Text);
                komut.Parameters.AddWithValue("@prm8", textBox34.Text);
                komut.Parameters.AddWithValue("@prm9", textBox35.Text);
                komut.Parameters.AddWithValue("@prm10", textBox36.Text);
                komut.Parameters.AddWithValue("@prm11", textBox37.Text);
                komut.Parameters.AddWithValue("@prm12", textBox38.Text);
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
            Program.bag.Close();  // veri tabanı bağlantısı kapatılır
            if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra eklemenin başarılı olup olmadığı kontrol edilir
            {
                MessageBox.Show("Öğretmen kaydı başarıyla yapıldı.");  // eklemenin başarılı olduğu mesajını verir
            }
            else
            {
                MessageBox.Show("Öğretmen kaydı başarısız oldu.");    // eklemenin başarısız olduğunu mesajını verir
            }
          }
          catch (Exception ex)
          {
           MessageBox.Show("Hata oluştu: " + ex.Message);  // hata mesajını verir
          }
            textBox4.Clear();
            textBox5.Clear();
            textBox23.Clear();
            textBox13.Clear();
            textBox31.Clear();
            textBox32.Clear();
            textBox33.Clear();
            textBox34.Clear();
            textBox35.Clear();
            textBox36.Clear();
            textBox37.Clear();
            textBox38.Clear();
            ÖğretmenVT();
            ÖğretmenID();
            Dersöğretmencombox();
            Sınıföğretmencombox();
        }

        private void BilgiÖğretmensilbutton_Click(object sender, EventArgs e)
        {
            
            string sql = "DELETE FROM Öğretmen WHERE Öğretmen_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", comboBox8.SelectedValue);
            DialogResult result = MessageBox.Show("Seçili öğretmen bilgilerini silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();
                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Seçilen öğretmen bilgileri silindi.");
                        ÖğretmenVT();
                        ÖğretmenID();
                        Dersöğretmencombox();
                        Sınıföğretmencombox();
                    }
                    else
                    {
                        MessageBox.Show("Öğretmen bilgileri silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi.");
            }
        }

        private void BilgiÖğretmendüzenlebutton_Click(object sender, EventArgs e)
        {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "UPDATE Öğretmen SET Öğretmen_Ad = @prm1, Öğretmen_Soyad = @prm2, Pzt_Bas = @prm3, Pzt_Bit = @prm4, Salı_Bas = @prm5, Salı_Bit = @prm6," +
                " Çarşamba_Bas = @prm7, Çarşamba_Bit = @prm8, Perşembe_Bas = @prm9, Perşembe_Bit = @prm10, Cuma_Bas = @prm11, Cuma_Bit = @prm12 WHERE Öğretmen_ID = @prm13"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", textBox14.Text); // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
            komut.Parameters.AddWithValue("@prm2", textBox8.Text);
            komut.Parameters.AddWithValue("@prm3", textBox56.Text);
            komut.Parameters.AddWithValue("@prm4", textBox55.Text);
            komut.Parameters.AddWithValue("@prm5", textBox54.Text);
            komut.Parameters.AddWithValue("@prm6", textBox53.Text);
            komut.Parameters.AddWithValue("@prm7", textBox52.Text);
            komut.Parameters.AddWithValue("@prm8", textBox51.Text);
            komut.Parameters.AddWithValue("@prm9", textBox50.Text);
            komut.Parameters.AddWithValue("@prm10", textBox49.Text);
            komut.Parameters.AddWithValue("@prm11", textBox48.Text);
            komut.Parameters.AddWithValue("@prm12", textBox47.Text);
            komut.Parameters.AddWithValue("@prm13", comboBox19.SelectedValue);
            komut.ExecuteNonQuery();
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();
            if (etkilenenSatirSayisi > 0)
            {
                MessageBox.Show("Öğretmen bilgileri güncellenmiştir.");
                Dersöğretmencombox();
                Sınıföğretmencombox();
                ÖğretmenVT();
            }
            else
            {
                MessageBox.Show("Öğretmen güncellenemedi.");
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır

        }

        private void BilgiÖğretmenBilgiVTbutton_Click(object sender, EventArgs e)
        {
            ÖğretmenVT();  //yenile tuşuna basıldığında yenilenin basıldığı sayfada veri tabanından alınan bilgileri yenilenmesi için metotlar çalıştırılır
            ÖğretmenID();
        }
        private void ÖğretmenVT()
        {
            
            string sql = "select * from Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView3.DataSource = dt;
            dataGridView3.Columns[0].HeaderText = "Öğretmen ID"; // data gridview de sütunların isimlerini kullanıcın gözüne hoş gözükecek şekilde değiştirdim
            dataGridView3.Columns[1].HeaderText = "Öğretmen Ad";
            dataGridView3.Columns[2].HeaderText = "Öğretmen Soyad";
            dataGridView3.Columns[3].HeaderText = "Pazartesi Başlangıç";
            dataGridView3.Columns[4].HeaderText = "Pazartesi Bitiş";
            dataGridView3.Columns[5].HeaderText = "Salı Başlangıç";
            dataGridView3.Columns[6].HeaderText = "Salı Bitiş";
            dataGridView3.Columns[7].HeaderText = "Çarşamba Başlangıç";
            dataGridView3.Columns[8].HeaderText = "Çarşamba Bitiş";
            dataGridView3.Columns[9].HeaderText = "Perşembe Başlangıç";
            dataGridView3.Columns[10].HeaderText = "Perşembe Bitiş";
            dataGridView3.Columns[11].HeaderText = "Cuma Başlangıç";
            dataGridView3.Columns[12].HeaderText = "Cuma Bitiş";

        }
        private void ÖğretmenID()
        {
            ÖğretmensilID();    //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            ÖğretmendüzenleID();
        }

        private void ÖğretmensilID()
        {
            
            string sql = "SELECT Öğretmen_ID FROM Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open();//veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox8.DisplayMember = "Öğretmen_ID"; // görünecek olan bilgi
            comboBox8.ValueMember = "Öğretmen_ID"; //kod kısmında işlenen bilgi
            comboBox8.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void ÖğretmendüzenleID()
        {
            
            string sql = "SELECT Öğretmen_ID FROM Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox19.DisplayMember = "Öğretmen_ID"; // görünecek olan bilgi
            comboBox19.ValueMember = "Öğretmen_ID"; //kod kısmında işlenen bilgi
            comboBox19.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        } 

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)  // öğretmen sil ıd
        {
            int secilenÖğretmenID = Convert.ToInt32(comboBox8.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenÖğretmen e tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT * FROM Öğretmen WHERE Öğretmen_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenÖğretmenID);  // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {                                         // 0'dan değil de 1 den başlama sebebi * kullanıldığından ID de readerın içinde olacak
                textBox70.Text = reader.GetString(1); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                textBox69.Text = reader.GetString(2);
                textBox68.Text = reader.GetTimeSpan(3).ToString();
                textBox67.Text = reader.GetTimeSpan(4).ToString();
                textBox66.Text = reader.GetTimeSpan(5).ToString();
                textBox65.Text = reader.GetTimeSpan(6).ToString();
                textBox64.Text = reader.GetTimeSpan(7).ToString();
                textBox63.Text = reader.GetTimeSpan(8).ToString();
                textBox62.Text = reader.GetTimeSpan(9).ToString();
                textBox61.Text = reader.GetTimeSpan(10).ToString();
                textBox60.Text = reader.GetTimeSpan(11).ToString();
                textBox59.Text = reader.GetTimeSpan(12).ToString();
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void comboBox19_SelectedIndexChanged(object sender, EventArgs e) // öğretmen düzenle ıd
        {
            int secilenÖğretmenID = Convert.ToInt32(comboBox19.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenÖğretmen e tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT * FROM Öğretmen WHERE Öğretmen_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenÖğretmenID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {                                         // 0'dan değil de 1 den başlama sebebi * kullanıldığından ID de readerın içinde olacak
                textBox14.Text = reader.GetString(1); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                textBox8.Text = reader.GetString(2);
                textBox56.Text = reader.GetTimeSpan(3).ToString();
                textBox55.Text = reader.GetTimeSpan(4).ToString();
                textBox54.Text = reader.GetTimeSpan(5).ToString();
                textBox53.Text = reader.GetTimeSpan(6).ToString();
                textBox52.Text = reader.GetTimeSpan(7).ToString();
                textBox51.Text = reader.GetTimeSpan(8).ToString();
                textBox50.Text = reader.GetTimeSpan(9).ToString();
                textBox49.Text = reader.GetTimeSpan(10).ToString();
                textBox48.Text = reader.GetTimeSpan(11).ToString();
                textBox47.Text = reader.GetTimeSpan(12).ToString();
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        /*
          Sınıf tablo
         */

        private void BilgiSınıfeklebutton_Click(object sender, EventArgs e)
        {
          try
          { 
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "insert into Sınıf (Sınıf_İsim,Müfredat_ID,Öğretmen_ID,Sınıf_Bas,Sınıf_Bit) values(@prm1,@prm2,@prm3,@prm4,@prm5)"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                komut.Parameters.AddWithValue("@prm1", textBox7.Text); // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
                komut.Parameters.AddWithValue("@prm2", comboBox14.SelectedValue);
                komut.Parameters.AddWithValue("@prm3", comboBox17.SelectedValue);
                komut.Parameters.AddWithValue("@prm4", textBox76.Text);
                komut.Parameters.AddWithValue("@prm5", textBox75.Text);
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
                Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra eklemenin başarılı olup olmadığı kontrol edilir
            {
                MessageBox.Show("Sınıf kaydı başarıyla yapıldı.");  // eklemenin başarılı olduğu mesajını verir
            }
            else
            {
                MessageBox.Show("Sınıf kaydı başarısız oldu.");    // eklemenin başarısız olduğunu mesajını verir
            }
          }
          catch (Exception ex)
          {
          MessageBox.Show("Hata oluştu: " + ex.Message);  // hata mesajını verir
          }
            textBox7.Clear();
            textBox76.Clear();
            textBox75.Clear();
            SınıfVT();
            SınıfID();

        }

        private void BilgiSınıfsilbutton_Click(object sender, EventArgs e)
        {
            
            string sql = "DELETE FROM Sınıf WHERE Sınıf_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", comboBox9.SelectedValue);
            DialogResult result = MessageBox.Show("Seçili sınıfı silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();
                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Seçilen sınıf silindi.");
                        SınıfVT();
                        SınıfID();
                    }
                    else
                    {
                        MessageBox.Show("sınıf silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi.");
            }
        }

        private void BilgiSınıfdüzenlebutton_Click(object sender, EventArgs e)
        {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "UPDATE Sınıf SET Sınıf_İsim = @prm1, Müfredat_ID = @prm2, Öğretmen_ID = @prm3, Sınıf_Bas = @prm4, Sınıf_Bit = @prm5 WHERE Sınıf_ID = @prm6"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", textBox12.Text); // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
            komut.Parameters.AddWithValue("@prm2", comboBox27.SelectedValue);
            komut.Parameters.AddWithValue("@prm3", comboBox18.SelectedValue);
            komut.Parameters.AddWithValue("@prm4", textBox78.Text);
            komut.Parameters.AddWithValue("@prm5", textBox77.Text);
            komut.Parameters.AddWithValue("@prm6", comboBox16.SelectedValue);
            komut.ExecuteNonQuery();
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();
            if (etkilenenSatirSayisi > 0)
            {
                MessageBox.Show("Sınıf bilgileri güncellenmiştir.");
            }
            else
            {
                MessageBox.Show("Sınıf güncellenemedi.");
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void BilgiSınıfVTbutton_Click(object sender, EventArgs e)
        {
            SınıfVT();  //yenile tuşuna basıldığında yenilenin basıldığı sayfada veri tabanından alınan bilgileri yenilenmesi için metotlar çalıştırılır
            SınıfID();
            Sınıfmüfredatcombox();
            Sınıföğretmencombox();
        }

        private void SınıfVT()
        {
            
            string sql = "SELECT S.Sınıf_ID, S.Sınıf_İsim, M.Müfredat_Bölüm, Ö.Öğretmenadsoyad, S.Sınıf_Bas, S.Sınıf_Bit  FROM Sınıf S " +
                "JOIN Müfredat M ON (S.Müfredat_ID = M.Müfredat_ID) " +
                "JOIN (SELECT Öğretmen_ID, (Öğretmen_Ad +' '+ Öğretmen_Soyad) AS Öğretmenadsoyad FROM Öğretmen) Ö ON (S.Öğretmen_ID = Ö.Öğretmen_ID)"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView4.DataSource = dt; 
            dataGridView4.Columns[0].HeaderText = "Sınıf ID"; // data gridview de sütunların isimlerini kullanıcın gözüne hoş gözükecek şekilde değiştirdim
            dataGridView4.Columns[1].HeaderText = "Sınıf İsim";
            dataGridView4.Columns[2].HeaderText = "Müfredat";
            dataGridView4.Columns[3].HeaderText = "Sorumlu Öğretmen";
            dataGridView4.Columns[4].HeaderText = "Gün Başlangıç";
            dataGridView4.Columns[5].HeaderText = "Gün Bitiş";
        }
        private void SınıfID()
        {
            SınıfsilID();   //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Sınıfdüzenle();
        }

        private void SınıfsilID()
        {
            
            string sql = "SELECT Sınıf_ID FROM Sınıf"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable();  // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);
            Program.bag.Close(); 
            comboBox9.DisplayMember = "Sınıf_ID"; // görünecek olan bilgi
            comboBox9.ValueMember = "Sınıf_ID"; //kod kısmında işlenen bilgi
            comboBox9.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
        
        private void Sınıfdüzenle()
        {
            
            string sql = "SELECT Sınıf_ID FROM Sınıf"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close();
            comboBox16.DisplayMember = "Sınıf_ID"; // görünecek olan bilgi
            comboBox16.ValueMember = "Sınıf_ID"; //kod kısmında işlenen bilgi
            comboBox16.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }
        private void Sınıfmüfredatcombox()
        {
            Sınıfmüfredateklecmbx();    //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Sınıfmüfredatdüzenlecmbx();
        }
        private void Sınıfmüfredateklecmbx()
        {
            
            string sql = "SELECT Müfredat_ID, Müfredat_Bölüm AS Sınıfmüfredat From Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close();
            comboBox14.DisplayMember = "Sınıfmüfredat"; // görünecek olan bilgi
            comboBox14.ValueMember = "Müfredat_ID"; //kod kısmında işlenen bilgi
            comboBox14.DataSource = dt;  // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void Sınıfmüfredatdüzenlecmbx()
        {
            
            string sql = "SELECT Müfredat_ID, Müfredat_Bölüm AS Sınıfmüfredat From Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox27.DisplayMember = "Sınıfmüfredat"; // görünecek olan bilgi
            comboBox27.ValueMember = "Müfredat_ID"; //kod kısmında işlenen bilgi
            comboBox27.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void Sınıföğretmencombox()
        {
            Sınıföğretmeneklecmbx();    //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            Sınıföğretmendüzenlecmbx();
        }

        private void Sınıföğretmeneklecmbx()
        {
            
            string sql = "SELECT Öğretmen_ID, (Öğretmen_Ad +' '+ Öğretmen_Soyad) AS sAdSoyad From Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable();  // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox17.DisplayMember = "sAdSoyad"; // görünecek olan bilgi
            comboBox17.ValueMember = "Öğretmen_ID"; //kod kısmında işlenen bilgi
            comboBox17.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void Sınıföğretmendüzenlecmbx()
        {
            
            string sql = "SELECT Öğretmen_ID, (Öğretmen_Ad +' '+ Öğretmen_Soyad) AS sAdSoyad From Öğretmen"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);// da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox18.DisplayMember = "sAdSoyad"; // görünecek olan bilgi
            comboBox18.ValueMember = "Öğretmen_ID"; //kod kısmında işlenen bilgi
            comboBox18.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e) // sınıf sil ıd
        {
            int secilenSınıfID = Convert.ToInt32(comboBox9.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenSınıfID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT S.Sınıf_İsim, M.Müfredat_Bölüm,  CONCAT(Ö.Öğretmen_Ad, ' ', Ö.Öğretmen_Soyad) AS AdSoyad, S.Sınıf_Bas, S.Sınıf_Bit " +
                         "FROM Sınıf S " +
                         "JOIN Müfredat M ON (S.Müfredat_ID = M.Müfredat_ID) " +
                         "JOIN Öğretmen Ö ON (S.Öğretmen_ID = Ö.Öğretmen_ID) " +
                         "WHERE S.Sınıf_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıfID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox24.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                textBox6.Text = reader.GetString(1);
                textBox30.Text = reader.GetString(2);
                textBox73.Text = reader.GetTimeSpan(3).ToString();
                textBox72.Text = reader.GetTimeSpan(4).ToString();

            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e) // sınıf düzenle ıd
        {
            int secilenSınıfID = Convert.ToInt32(comboBox16.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenSınıfID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT S.Sınıf_İsim, M.Müfredat_Bölüm,  CONCAT(Ö.Öğretmen_Ad, ' ', Ö.Öğretmen_Soyad) AS AdSoyad, S.Sınıf_Bas, S.Sınıf_Bit " +
                         "FROM Sınıf S " +
                         "JOIN Müfredat M ON (S.Müfredat_ID = M.Müfredat_ID) " +
                         "JOIN Öğretmen Ö ON (S.Öğretmen_ID = Ö.Öğretmen_ID) " +
                         "WHERE S.Sınıf_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenSınıfID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read()) 
            {
                textBox12.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
                comboBox27.Text = reader.GetString(1);
                comboBox15.Text = reader.GetString(2);
                textBox78.Text = reader.GetTimeSpan(3).ToString();
                textBox77.Text = reader.GetTimeSpan(4).ToString();

            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }


        /*
         Müfredat Tablo
         */

        private void BilgiMüfredateklebutton_Click(object sender, EventArgs e)
        {
        try
        { 
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "insert into Müfredat (Müfredat_Bölüm) values(@prm1)"; // sql komutu kaydedilir
                SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                komut.Parameters.AddWithValue("@prm1", textBox11.Text);
                int etkilenenSatirSayisi = komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
                Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                if (etkilenenSatirSayisi > 0)  // komut çalıştıktan sonra eklemenin başarılı olup olmadığı kontrol edilir
                {
                    MessageBox.Show("Müfredat kaydı başarıyla yapıldı.");  // eklemenin başarılı olduğu mesajını verir
                }
                else
                {
                    MessageBox.Show("Müfredat kaydı başarısız oldu.");    // eklemenin başarısız olduğunu mesajını verir
                }
        }
        catch (Exception ex)
        {
        MessageBox.Show("Hata oluştu: " + ex.Message);  // hata mesajını verir
        }
            textBox11.Clear();
            MüfredatVT();
            MüfredatID();
            Dersmüfredatcombox();
            Sınıfmüfredatcombox();
        }

        private void BilgiMüfredatsilbutton_Click(object sender, EventArgs e)
        {
            
            string sql = "DELETE FROM Müfredat WHERE Müfredat_ID = @prm1"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", comboBox10.SelectedValue);
            DialogResult result = MessageBox.Show("Seçili müfredatı silmek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
                    int etkilenenSatirSayisi = komut.ExecuteNonQuery();
                    if (etkilenenSatirSayisi > 0)
                    {
                        MessageBox.Show("Seçilen müfredat silindi.");
                        MüfredatVT();
                        MüfredatID();
                        Dersmüfredatcombox();
                        Sınıfmüfredatcombox();
                    }
                    else
                    {
                        MessageBox.Show("Müfredat silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    Program.bag.Close(); // veri tabanı bağlantısı kapatılır
                }
            }
            else
            {
                MessageBox.Show("İşlem iptal edildi.");
            }

        }

        private void BilgiMüfredatdüzenlebutton_Click(object sender, EventArgs e)
        {
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "UPDATE Müfredat SET Müfredat_Bölüm = @prm1 WHERE Müfredat_ID = @prm2"; // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            komut.Parameters.AddWithValue("@prm1", textBox10.Text);
            komut.Parameters.AddWithValue("@prm2", comboBox15.SelectedValue);


            komut.ExecuteNonQuery();
            int etkilenenSatirSayisi = komut.ExecuteNonQuery();
            if (etkilenenSatirSayisi > 0)
            {
                MessageBox.Show("Müfredat bilgileri güncellenmiştir.");
                Dersmüfredatcombox();
                Sınıfmüfredatcombox();
                MüfredatVT();
            }
            else
            {
                MessageBox.Show("Müfredat güncellenemedi.");
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void BilgiMüfredatVTbutton_Click(object sender, EventArgs e)
        {
            MüfredatVT();  //yenile tuşuna basıldığında yenilenin basıldığı sayfada veri tabanından alınan bilgileri yenilenmesi için metotlar çalıştırılır
            MüfredatID();
        }

        private void MüfredatVT()
        {
            
            string sql = "SELECT * FROM Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            dataGridView5.DataSource = dt;
            dataGridView5.Columns[0].HeaderText = "Müfredat ID";
            dataGridView5.Columns[1].HeaderText = "Bölüm";

        }
        private void MüfredatID()
        {
            MüfredatsilID();     //comboboxtan birisini değiştirince öbürüde kendiliğinden değişmemesi için comboboxlar 2 farklı metotda yazıldı
            MüfredatdüzenleID();


        }
        private void MüfredatsilID()
        {
            
            string sql = "SELECT Müfredat_ID FROM Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
            comboBox10.DisplayMember = "Müfredat_ID"; // görünecek olan bilgi
            comboBox10.ValueMember = "Müfredat_ID"; //kod kısmında işlenen bilgi
            comboBox10.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void MüfredatdüzenleID()
        {
            
            string sql = "SELECT Müfredat_ID FROM Müfredat"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            da.Fill(dt);  // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir
            Program.bag.Close();  // veri tabanı bağlantısı kapatılır
            comboBox15.DisplayMember = "Müfredat_ID"; // görünecek olan bilgi
            comboBox15.ValueMember = "Müfredat_ID"; //kod kısmında işlenen bilgi
            comboBox15.DataSource = dt; // dt üzerinde olan bilgi comboboxa aktarılır
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e) // müfredat sil ıd
        {
            int secilenMüfredatID = Convert.ToInt32(comboBox10.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenMüfredatID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT Müfredat_Bölüm FROM Müfredat WHERE Müfredat_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenMüfredatID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox25.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e) // müfredat düzenle ıd
        {
            int secilenMüfredatID = Convert.ToInt32(comboBox15.SelectedValue); // comboboxdan seçilen bilginin kod kısmında atanmış değeri secilenMüfredatID ye tanımlanır
            
            Program.bag.Open(); //veri tabanına bağlantı sağlamak için veri alımını açılıyor
            string sql = "SELECT Müfredat_Bölüm FROM Müfredat WHERE Müfredat_ID = @prm1";  // sql komutu kaydedilir
            SqlCommand komut = new SqlCommand(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olur
            komut.Parameters.AddWithValue("@prm1", secilenMüfredatID); // değer sql kodu içinde olan ıd ile tanımlanır
            SqlDataReader reader = komut.ExecuteReader(); // reader aracılığı ile gelen veri okunur
            if (reader.Read())
            {
                textBox10.Text = reader.GetString(0); // okunan veriye göre gelen bilgileri sırasıyla yansıtır
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatılır
        }

        private void button1_Click(object sender, EventArgs e) // formu kapatma tuşu
        {
            this.Close();
        }
    }
}
