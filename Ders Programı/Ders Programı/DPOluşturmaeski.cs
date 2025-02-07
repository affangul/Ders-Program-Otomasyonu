using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Ders_Programı
{
    public partial class DPOluşturma : Form
    {
        Random random = new Random(); // daha sonra kullanmak üzere public random kütüphanesi oluşturuldu
        


        public DPOluşturma()
        {
            InitializeComponent();
            progressBar1.Minimum = 0; //progressbar değerleri tanımlandı
            progressBar1.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu işleme başladığınızda önceden yapılmış olan ders programı silinecektir devam etmek istiyor musunuz?", "Dikkat", MessageBoxButtons.YesNo); //uyarı mesajı
           if (result == DialogResult.Yes) // çıkan mesajda evete tıklarsa
           {
           int pbar = 0;  // progressbar için ayarlanmış bir değişken
                progressBar1.Value = pbar;  // tekrar başlanırsa diye progressbar sıfırlandı
                pbar++;  // progressbar değeri 1 arttı 
                tablosıfırlama();  
            int rsg = random.Next(100,1000);   // daha sonradan bazı verileri değiştirmek için rastgele bir sayı seçildi
            string[,] Sınıf = Sınıflar();  // sınıf tablosunda olan bilgilerin tutulduğu  dizi
                string[,] Ders = Dersler();    // ders tablosunda olan bilgilerin tutulduğu dizi                 
                string[,] Öğretmen = Öğretmenler();     // öğretmen tablosunda olan bilgilerin tutulduğu dizi         
                string[,] DerslikMüsait = Derslikler();         // dersliklerin  gün/saatlerinin tutulduğu dizi
                string[,] ÖğretmenMüsait = ÖğretmenMüsaitlik(); // öğretmenlerin gün/saatlerinin tutulduğu dizi
                string[,] SınıfMüsait = SınıfMüsaitlik();   // sınıfların   gün/saatlerinin tutulduğu dizi    
                int sınıfsayı = Sınıf.GetLength(0);   // toplam kaç sınıf olduğu kaydedildi  
                for (int x = 0; x < sınıfsayı;x++)  // sınıf sayısı kadar döngüye sokuldu
                {
                string sınıfmüfredat = Sınıf[x, 2]; // sınıfın müfredatının kaydedildiği eleman
                string sınıfisim = Sınıf[x, 1];     // sınıfın isminin kaydedildiği eleman
                string sınıfsaatbas = Sınıf[x, 4];  // sınıfın en erken saat kaçta dersi olabileceğinin kaydedildiği eleman
                string sınfsaatbit = Sınıf[x, 5];   // sınıfın en geç saat kaçta dersten çıkacağının kaydedildiği eleman
                int dersayı = Ders.GetLength(0);    // toplamda kaç tane dersin olduğu kaydedildi
                for (int y = 0;y < dersayı;y++)     // toplam ders sayısı kadar döngüye sokuldu
                {
                    string öğretmenisim = "";
                    if (Ders[y,3] == sınıfmüfredat) // müfredat aracılığı ile ders seçildi
                    {
                        int dikkat = 0; //potansiyel çakışmayı engellemek amaçlı araya önlem olarak eklenen bir değişken
                        string dersisim = Ders[y,1];    // dersin adı kaydedildi
                        int derstekrar = Convert.ToInt32(Ders[y, 2]); // dersin kaç tekrar yapılacağı kaydedildi
                        string dersliktür = Ders[y,4];  // dersin hangi türde dersliğe ihtiyacı olduğu kaydedildi
                        string öğretmenID = Ders[y, 5]; // dersi yapacak öğretmenin ID si kaydedildi
                        for (int z = 0; z <Öğretmen.GetLength(0) ; z++) // öğretmen ID si kullanılarak öğretmenin ad ve soyadı alındı
                        {
                            if (Öğretmen[z,0] == öğretmenID) //dersi verecek öğretmenin ID aracılığı ile bilgisi arandı
                            {
                                string ad = Öğretmen[z, 1]; //öğretmenin adı alındı
                                string soyadı = Öğretmen[z, 2]; // öğretmenin soyadı alındı
                                öğretmenisim = ad + " " + soyadı; // adı ve soyadı tek veride kaydedildi
                                break; // aynı ID ye sahip başka bir veri olmayacağından dolayı döngü bozularak işlem kısaltıldı
                            }
                        }
                        
                       // sınıf-öğretmen-derslik üçlüsünün aynı anda müsait olduğu zaman aranıyor
                        for (int i = 0; i < SınıfMüsait.GetLength(0); i++)   // sınıfın bilgilerinin alındığı kısım
                        {
                            string sınıfad = SınıfMüsait[i, 1]; // sql e kaydetmek üzere sınıfın ismi alındı
                            string saat1 = SınıfMüsait[i, 2]; // sınıfın derste olmadığı saat alındı
                            string gün1 = SınıfMüsait[i, 3];  // sınıfın derste olmadığı saatin günü alındı
                            for (int j = 0; j < ÖğretmenMüsait.GetLength(0); j++) // öğretmenin bilgilerinin alındığı kısım 
                            {
                                string öğretmenıd = ÖğretmenMüsait[j, 0]; // öğretmen ID mevcüt ders ile uyuştuğunu kontrol etmek üzere alındı
                                string saat2 = ÖğretmenMüsait[j, 2]; // öğretmenin derste olmadığı ve çalışma saati içinde olduğu bir zaman alındı
                                string gün2 = ÖğretmenMüsait[j, 3];  // öğretmenin derste olmadığı ve çalışma saati içinde olduğu bir zamanın günü alındı
                                if (saat1 == saat2 && gün1 == gün2 && sınıfisim == sınıfad && öğretmenID == öğretmenıd) // öğretmen ile sınıfın ortak bir saati arandı bulunursa sıradaki aşamaya geçildi
                                {
                                    for (int k = 0; k < DerslikMüsait.GetLength(0); k++) // derslik bilgileri alındığı kısım
                                    {
                                        string saat3 = DerslikMüsait[k, 2]; // derslikte ders işlenmeyen zaman alındı
                                        string gün3 = DerslikMüsait[k, 3]; // derslikte ders işlenmeyen zamanın günü alındı
                                        string tür = DerslikMüsait[k, 0];  // ders ile uyumlu olduğu kontrol etmek üzere türü alındı
                                        string derslikisim = DerslikMüsait[k, 1]; // sql e kaydetmek üzere dersliğin ismi alındı
                                        if (saat1 == saat3 && gün1 == gün3 && tür == dersliktür && dikkat < derstekrar) // sınıf ile derslik saatleri/günleri kıyaslandı, dersin işlenmesi gerektiği tür doğrulandı ve dersin haftada kaç defa tekrar edileceği oran doğrulandı
                                        {
                                            string saatbas = saat3; // sql e kaydetmek üzere saat kaydedildi
                                            DateTime saatzam = DateTime.ParseExact(saat3, "HH:mm:ss", null); // dersler 45dk olacak şekilde ayarlandı
                                            DateTime saatzama = saatzam.AddMinutes(45); 
                                            string saatbit = saatzama.ToString("HH:mm:ss"); // dersin bitiş saati sql kaydetmek üzere kaydedildi
                                            string sql = "INSERT INTO Final (Gün, Ders, Derslik, Öğretmen, Başlangıç, Bitiş, Sınıf) VALUES(@prm1,@prm2,@prm3,@prm4,@prm5,@prm6,@prm7)";  // sql komutu kaydedilir
                                            Program.bag.Open(); // veri tabanı bağlantısı açıldı
                                            SqlCommand komut = new SqlCommand(sql, Program.bag);  // sql komutu ile veri tabanı arası bağlantı birleştiriliyor
                                            komut.Parameters.AddWithValue("@prm1",gün1 );  // @prm işlemleri nereden alacağını tanımlar
                                            komut.Parameters.AddWithValue("@prm2",dersisim );
                                            komut.Parameters.AddWithValue("@prm3",derslikisim );
                                            komut.Parameters.AddWithValue("@prm4",öğretmenisim );
                                            komut.Parameters.AddWithValue("@prm5",saatbas);
                                            komut.Parameters.AddWithValue("@prm6",saatbit );
                                            komut.Parameters.AddWithValue("@prm7",sınıfisim );
                                            komut.ExecuteNonQuery();  // sql komudu çalıştırılır ve fark olup olmadığını etkilenenSatirSayisi kaydeder
                                            Program.bag.Close();  // veri tabanı bağlantısı kapatılır

                                                SınıfMüsait[i, 0] = "a"+rsg.ToString(); // kullanılan veriler tekrar çakışma olmaması için orantısız bir şekilde bozuldu
                                                rsg++;
                                                SınıfMüsait[i, 1] = "a"+rsg.ToString();
                                                rsg++;
                                                SınıfMüsait[i, 2] = "a" + rsg.ToString();
                                                rsg++;
                                                SınıfMüsait[i, 3] = "a" + rsg.ToString();
                                                rsg++;
                                                ÖğretmenMüsait[j,0] = "a"+rsg.ToString();
                                                rsg++;
                                                ÖğretmenMüsait[j, 1] = "a" + rsg.ToString();
                                                rsg++;
                                                ÖğretmenMüsait[j, 2] = "a" + rsg.ToString();
                                                rsg++;
                                                ÖğretmenMüsait[j, 3] = "a" + rsg.ToString();
                                                rsg++;
                                                DerslikMüsait[k, 0] = "a" + rsg.ToString();
                                                rsg++;
                                                DerslikMüsait[k, 1] = "a" + rsg.ToString();
                                                rsg++;
                                                DerslikMüsait[k, 2] = "a" + rsg.ToString();
                                                rsg++;
                                                DerslikMüsait[k, 3] = "a" + rsg.ToString();
                                                rsg++;
                                                dikkat++; // ders tekrarını kontrol etmek için dikkat verisi artırıldı
                                                if(pbar != 100) //progressbar verisi 100 olana kadar tekrar ederek ilerlemesi için şart eklendi veri 100 olursa ve daha işlem bitmemiş olursa 99 da kalması için if koyuldu
                                                {
                                                    progressBar1.Value = pbar;
                                                    pbar++;
                                                }
                                                
                                                break; // çakışma olmaması için başa sardırıldı
                                            }
                                    }
                                }
                            }
                        
                        }
                    }
                }
                

            }
                progressBar1.Value = 100; // işlem bittiğinde progressbar %100 görünmesi için atandı
                MessageBox.Show("İşlem bitmiştir"); // işlem bittiğini belli etmek için messagebox çıktı
           }
            else
            {
                MessageBox.Show("İşlem iptal edildi"); // hayıra basıldığını doğrulamak için messagebox çıkar
            }
        }
        private void tablosıfırlama() // daha önceden ders programı yapıldıysa çakışma olmaması için önceki verileri silen metot
        {
            string sql = "DELETE FROM Final"; // sql kodu
            SqlCommand komut = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            komut.ExecuteNonQuery(); // işlem yapıldı
            Program.bag.Close(); // veri tabanı bağlantısı kapatıldı
        }
        private string[,] Dersler() // ders tablosundan bilgileri çekip DerslerListe dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Ders"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgiler reader aracılığı ile okundu
            int rowCount = 0; // dizinin boyutunu ayarlamak üzere veri oluşturuldu
            while (reader.Read())
            {
                rowCount++; // veri başına rowcount bir artıldı
            }
            string[,] DerslerListe = new string[rowCount, reader.FieldCount]; // DerslerListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); // çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int row = 0; // dizide verinin yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read()) 
            {
                for (int col = 0; col < reader.FieldCount; col++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    DerslerListe[row, col] = reader[col].ToString(); // veri tabanından alınan bilgiler DerslerListeye eklendi
                }
                row++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı kapatıldı

            return DerslerListe; // button_click içinde çağırıldığı yere dizi verisi gönderildi
        }

        private string[,] Sınıflar() // sınıf tablosundan bilgileri çekip SınıflarListe dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Sınıf"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int rowCount = 0; // dizinin boyutunu ayarlamak üzere veri oluşturuldu
            while (reader.Read())
            {
                rowCount++; // veri başına rowcount bir artırıldı 
            }
            string[,] SınıflarListe = new string[rowCount, reader.FieldCount]; // SınıflarListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int row = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read()) 
            {
                for (int col = 0; col < reader.FieldCount; col++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    SınıflarListe[row, col] = reader[col].ToString(); // veri tabanından alınan bilgiler DerslerListeye eklendi
                } 
                row++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı kapatıldı

            return SınıflarListe; // button_click içinde çağırıldığı yere dizi verisi gönderildi
        }

        private string[,] Öğretmenler() // öğretmen tablosundan bilgileri çekip ÖğretmenListe dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Öğretmen"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int rowCount = 0; // dizinin boyutunu ayarlamak üzere veri oluşturuldu
            while (reader.Read())
            {
                rowCount++; // veri başına rowcount bir artırıldı 
            }
            string[,] ÖğretmenListe = new string[rowCount, reader.FieldCount]; // ÖğretmenListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int row = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int col = 0; col < reader.FieldCount; col++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    ÖğretmenListe[row, col] = reader[col].ToString(); // veri tabanından alınan bilgiler DerslerListeye eklendi
                }
                row++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı kapatıldı
            return ÖğretmenListe; // button_click içinde çağırıldığı yere dizi verisi gönderildi
        }
        // dizilere saat eklenecekken müsait olunup olunmadığı kontrol edilmek üzere saatler birden fazla yerde kullanılacaktı bu yüzden public kısma yazıldı
        TimeSpan sıfır = TimeSpan.Parse("00:00:00");
        TimeSpan bir = TimeSpan.Parse("01:00:00");
        TimeSpan iki = TimeSpan.Parse("02:00:00");
        TimeSpan üç = TimeSpan.Parse("03:00:00");
        TimeSpan dört = TimeSpan.Parse("04:00:00");
        TimeSpan beş = TimeSpan.Parse("05:00:00");
        TimeSpan altı = TimeSpan.Parse("06:00:00");
        TimeSpan yedi = TimeSpan.Parse("07:00:00");
        TimeSpan sekiz = TimeSpan.Parse("08:00:00");
        TimeSpan dokuz = TimeSpan.Parse("09:00:00");
        TimeSpan on = TimeSpan.Parse("10:00:00");
        TimeSpan onbir = TimeSpan.Parse("11:00:00");
        TimeSpan oniki = TimeSpan.Parse("12:00:00");
        TimeSpan onüç = TimeSpan.Parse("13:00:00");
        TimeSpan ondört = TimeSpan.Parse("14:00:00");
        TimeSpan onbeş = TimeSpan.Parse("15:00:00");
        TimeSpan onaltı = TimeSpan.Parse("16:00:00");
        TimeSpan onyedi = TimeSpan.Parse("17:00:00");
        TimeSpan onsekiz = TimeSpan.Parse("18:00:00");
        TimeSpan ondokuz = TimeSpan.Parse("19:00:00");
        TimeSpan yirmi = TimeSpan.Parse("20:00:00");
        TimeSpan yirmibir = TimeSpan.Parse("21:00:00");
        TimeSpan yirmiiki = TimeSpan.Parse("22:00:00");
        TimeSpan yirmiüç = TimeSpan.Parse("23:00:00");

        
        private string[,] ÖğretmenMüsaitlik() // öğretmen tablosundan bilgileri çekip Öğretmenmüsait dizisine ekleyen metot
        {
            TimeSpan [] saatler = { sıfır, bir, iki, üç, dört, beş, altı, yedi, sekiz, dokuz, on, onbir, oniki, onüç, ondört, onbeş, onaltı, onyedi, onsekiz, ondokuz, yirmi, yirmibir, yirmiiki};
            string sql = "SELECT * FROM Öğretmen"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int rowCount = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                rowCount++; // veri başına rowcount bir artırıldı 
            }
            string[,] ÖğretmenListe = new string[rowCount, reader.FieldCount]; // ÖğretmenListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int row = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int col = 0; col < reader.FieldCount; col++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    ÖğretmenListe[row, col] = reader[col].ToString(); // veri tabanından alınan bilgiler ÖğretmenListeye eklendi
                }
                row++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatıldı
            int satırsayıa = ÖğretmenListe.GetLength(0); // ÖğretmenListenin satır sayısı alındı
            int x = satırsayıa * 24 * 5; // Öğretmenmüsaitin alacağı satır sayısı için işlem gerçekleştirilip satırsayıa ya kaydedildi
            string[,] Öğretmenmüsait = new string[x, 4]; // Öğretmenmüsait adlı 2 boyutlu dizi alınan bilgiler ile oluşturuldu
            int y = 0; // satır sayısını tanımlamak için y verisi oluşturuldu
            for (int i = 0; i < satırsayıa; i++)
            {   
                TimeSpan PazartesiBaş = TimeSpan.Parse(ÖğretmenListe[i, 3]); //öğretmen tablosunda öğretmenlerin çalışma saatlerinin alındığı kısım
                TimeSpan PazartesiBit = TimeSpan.Parse(ÖğretmenListe[i, 4]);
                TimeSpan SalıBaş = TimeSpan.Parse(ÖğretmenListe[i, 5]);
                TimeSpan SalıBit = TimeSpan.Parse(ÖğretmenListe[i, 6]);
                TimeSpan ÇarşambaBaş = TimeSpan.Parse(ÖğretmenListe[i, 7]);
                TimeSpan ÇarşambaBit = TimeSpan.Parse(ÖğretmenListe[i, 8]);
                TimeSpan PerşembeBaş = TimeSpan.Parse(ÖğretmenListe[i, 9]);
                TimeSpan PerşembeBit = TimeSpan.Parse(ÖğretmenListe[i, 10]);
                TimeSpan CumaBaş = TimeSpan.Parse(ÖğretmenListe[i, 11]);
                TimeSpan CumaBit = TimeSpan.Parse(ÖğretmenListe[i, 12]);
                string ad = ÖğretmenListe[i, 1]; //Öğretmen adını ÖğretmenListeden alındı
                string soyadı = ÖğretmenListe[i, 2]; // Öğretmen soyadı ÖğretmenListeden alındı
                string öğretmenisim = ad + " " + soyadı; // Öğretmenin adı ve soyadı tek bir veride birleştirildi
                int z = 0; // haftanın hangi gününde olduğu seçilmek üzere z verisi oluşturuldu
                //string gün = "";  // haftanın gününü yazılı olarak tutulduğu kısım olarak gün verisi oluşturuldu

                /*
                 string x;
                CheckBox[] checkboxes = { checkbox1, checkbox2, checkbox3, // ... diğer checkbox'lar ... 
                checkbox23 };

            for (int i = 1; i <= 23; i++)
            {
                x = $"{i:D2}:00:00"; // Yeni saati oluştur
                Console.WriteLine(x); // Sonucu yazdır

                // İlgili checkbox'ı true yap
                checkboxes[i - 1].Checked = true;
            }
              */
                string gün = "";
                string saat = "00:00:00";
                for (int j = 0; j < 5; j++) // haftanın her günü bakılması için 5 tekrarlı bir for döngüsü yazıldı
                {

                    
                    if (z == 0) // haftanın hangi kısmında ise gün verisi o güne göre ayarlanması için if/else if kullanıldı
                    {
                        gün = "Pazartesi";
                    }
                    else if (z == 1)
                    {
                        gün = "Salı";
                    }
                    else if (z == 2)
                    {
                        gün = "Çarşamba";
                    }
                    else if (z == 3)
                    {
                        gün = "Perşembe";
                    }
                    else if (z == 4)
                    {
                        gün = "Cuma";
                    }

                    for (int ii = 1; ii <= 23; ii++)
                    {
                        
                        Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0]; // koşullar sağlanıyorsa Öğretmenmüsait dizisinin içine gerekli veriler eklendi
                        Öğretmenmüsait[y, 1] = öğretmenisim;
                        Öğretmenmüsait[y, 2] = saat;
                        Öğretmenmüsait[y, 3] = gün;
                        saat = $"{ii:D2}:00:00"; // Yeni saati oluştur


                    }

                    // hangi günde ise ona göre şartının kontorl edilmesi için if/else if içinde if şeklinde soruldu
                    if (gün == "Pazartesi")
                    { 
                        if (PazartesiBaş <= sıfır && sıfır < PazartesiBit)
                        { 
                        Öğretmenmüsait[y,0] = ÖğretmenListe[i,0]; // koşullar sağlanıyorsa Öğretmenmüsait dizisinin içine gerekli veriler eklendi
                        Öğretmenmüsait[y, 1] = öğretmenisim;
                        Öğretmenmüsait[y, 2] = "00:00:00";
                        Öğretmenmüsait[y, 3] = gün;
                        y++; // veri eklendikten sonra bir sornaki satıra geçmek için y verisi bir artıldı
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= sıfır && sıfır < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "00:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= sıfır && sıfır < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "00:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= sıfır && sıfır < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "00:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= sıfır && sıfır < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "00:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= bir && bir < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "01:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= bir && bir < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "01:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= bir && bir < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "01:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= bir && bir < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "01:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= bir && bir < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "01:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= iki && iki < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "02:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= iki && iki < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "02:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= iki && iki < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "02:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= iki && iki < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "02:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= iki && iki < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "02:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= üç && üç < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "03:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= üç && üç < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "03:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= üç && üç < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "03:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= üç && üç < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "03:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= üç && üç < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "03:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= dört && dört < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "04:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= dört && dört  < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "04:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= dört && dört < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "04:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= dört && dört < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "04:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= dört && dört < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "04:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= beş && beş < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "05:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= beş && beş < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "05:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= beş && beş < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "05:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= beş && beş < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "05:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= beş && beş < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "05:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= altı && altı < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "06:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= altı && altı < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "06:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= altı && altı < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "06:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= altı && altı < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "06:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= altı && altı < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "06:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= yedi && yedi < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "07:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= yedi && yedi < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "07:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= yedi && yedi < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "07:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= yedi && yedi < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "07:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= yedi && yedi < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "07:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= sekiz && sekiz < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "08:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= sekiz && sekiz < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "08:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= sekiz && sekiz < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "08:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= sekiz && sekiz < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "08:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= sekiz && sekiz < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "08:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= dokuz && dokuz < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "09:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= dokuz && dokuz < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "09:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= dokuz && dokuz < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "09:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= dokuz && dokuz < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "09:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= dokuz && dokuz < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "09:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= on && on < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "10:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= on && on < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "10:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= on && on < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "10:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= on && on < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "10:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= on && on < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "10:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= onbir && onbir < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "11:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= onbir && onbir < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "11:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= onbir && onbir < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "11:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= onbir && onbir < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "11:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= onbir && onbir < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "11:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= oniki && oniki < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "12:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= oniki && oniki < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "12:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= oniki && oniki < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "12:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= oniki && oniki < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "12:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= oniki && oniki < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "12:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= onüç && onüç < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "13:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= onüç && onüç < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "13:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= onüç && onüç < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "13:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= onüç && onüç < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "13:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= onüç && onüç < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "13:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= ondört && ondört < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "14:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= ondört && ondört < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "14:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= ondört && ondört < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "14:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= ondört && ondört < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "14:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= ondört && ondört < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "14:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= onbeş && onbeş < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "15:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= onbeş && onbeş < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "15:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= onbeş && onbeş < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "15:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= onbeş && onbeş < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "15:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= onbeş && onbeş < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "15:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= onaltı && onaltı < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "16:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= onaltı && onaltı < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "16:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= onaltı && onaltı < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "16:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= onaltı && onaltı < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "16:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= onaltı && onaltı < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "16:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= onyedi && onyedi < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "17:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= onyedi && onyedi < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "17:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= onyedi && onyedi < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "17:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= onyedi && onyedi < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "17:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= onyedi && onyedi < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "17:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= onsekiz && onsekiz < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "18:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= onsekiz && onsekiz < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "18:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= onsekiz && onsekiz < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "18:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= onsekiz && onsekiz < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "18:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= onsekiz && onsekiz < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "18:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= ondokuz && ondokuz < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "19:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= ondokuz && ondokuz < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "19:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= ondokuz && ondokuz < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "19:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= ondokuz && ondokuz < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "19:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= ondokuz && ondokuz < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "19:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= yirmi && yirmi < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "20:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= yirmi && yirmi < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "20:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= yirmi && yirmi < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "20:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= yirmi && yirmi < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "20:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= yirmi && yirmi < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "20:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= yirmibir && yirmibir < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "21:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= yirmibir && yirmibir < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "21:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= yirmibir && yirmibir < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "21:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= yirmibir && yirmibir < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "21:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= yirmibir && yirmibir < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "21:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= yirmiiki && yirmiiki < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "22:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= yirmiiki && yirmiiki < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "22:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= yirmiiki && yirmiiki < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "22:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= yirmiiki && yirmiiki < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "22:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= yirmiiki && yirmiiki < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "22:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    if (gün == "Pazartesi")
                    {
                        if (PazartesiBaş <= yirmiüç && yirmiüç < PazartesiBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "23:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Salı")
                    {
                        if (SalıBaş <= yirmiüç && yirmiüç < SalıBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "23:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Çarşamba")
                    {
                        if (ÇarşambaBaş <= yirmiüç && yirmiüç < ÇarşambaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "23:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Perşembe")
                    {
                        if (PerşembeBaş <= yirmiüç && yirmiüç < PerşembeBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "23:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    else if (gün == "Cuma")
                    {
                        if (CumaBaş <= yirmiüç && yirmiüç < CumaBit)
                        {
                            Öğretmenmüsait[y, 0] = ÖğretmenListe[i, 0];
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = "23:00:00";
                            Öğretmenmüsait[y, 3] = gün;
                            y++;
                        }
                    }
                    z++; // 1 günün tüm saatlerin bakıldıktan sonra z verisi 1 artırıldı

                }
            }
            return Öğretmenmüsait; // Tüm veriler kaydedildikten sonra metodun çağrıldığı yere Öğretmenmüsait dizisine geri gönderildi
        }

        private string[,] Derslikler() // Derslikİsim tablosundan bilgileri çekip MüsaitDerslikler dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Derslikİsim"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int rowCount = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                rowCount++; // veri başına rowcount bir artırıldı 
            }
            string[,] DerslikListe = new string[rowCount, reader.FieldCount];// DerslikListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int row = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int col = 0; col < reader.FieldCount; col++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    DerslikListe[row, col] = reader[col].ToString(); // veri tabanından alınan bilgiler ÖğretmenListeye eklendi
                }
                row++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatıldı
            int satırsayı = DerslikListe.GetLength(0); // DerslikListenin satır sayısı alındı
            int x = satırsayı * 24*5; // MüsaitDerslikler alacağı satır sayısı için işlem gerçekleştirilip satırsayıa ya kaydedildi
            string[,] MüsaitDerslikler = new string[x, 4]; // MüsaitDerslikler adlı 2 boyutlu dizi alınan bilgiler ile oluşturuldu
            int y = 0; // satır sayısını tanımlamak için y verisi oluşturuldu
            for (int i=0; i <satırsayı;i++)
            {
                 
                string gün = ""; // haftanın gününü yazılı olarak tutulduğu kısım olarak gün verisi oluşturuldu
                // haftanın hangi gününde olduğu seçilmek üzere z verisi oluşturuldu
                for (int z = 0; z < 5;) // haftanın her günü bakılması için 5 tekrarlı bir for döngüsü yazıldı
                {
                    int günkoşul = 1; // günlerin seçilip seçilmediğini kontrol etmek için bir veri atandı
                    if (z == 0) // haftanın hangi kısmında ise gün verisi o güne göre ayarlanması için if/else if kullanıldı 
                    {
                        gün = "Pazartesi";
                        if (checkBox25.Checked == true)
                        {
                            günkoşul = 0; // gün seçildiyse günkoşul 0 olacak şekilde ayarlandı
                        }
                    }
                    else if (z == 1)
                    {

                        gün = "Salı";
                        if (checkBox26.Checked == true)
                        {
                            günkoşul = 0;
                        }
                    }
                    else if (z == 2)
                    {
                        gün = "Çarşamba";
                        if (checkBox27.Checked == true)
                        {
                            günkoşul = 0;
                        }
                    }
                    else if (z == 3)
                    {
                        gün = "Perşembe";
                        if (checkBox28.Checked == true)
                        {
                            günkoşul = 0;
                        }
                    }
                    else if (z == 4)
                    {
                        gün = "Cuma";
                        if (checkBox29.Checked == true)
                        {
                            günkoşul = 0;
                        }
                        int sayı = 1;
                        
                    }
                    if (günkoşul == 0) // checkboxtan günün seçilip seçilmediği kontrol edilir
                    {
                        if (checkBox1.Checked == true) // ders yapılmasının istenip istenmediği derslikler üzerinden kontrol edilmek üzere if ile checkbox lar kontrol edildi
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2]; // MüsaitDerslikler için gerekli bilgiler kaydedildi
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "00:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++; // veri eklendikten sonra bir sonraki satıra geçmek için y verisi bir arttırıldı
                        }
                        if (checkBox2.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "01:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox3.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "02:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox4.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "03:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox5.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "04:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox6.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "05:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox7.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "06:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox8.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "07:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox9.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "08:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox10.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "09:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox11.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "10:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox12.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "11:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox13.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "12:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox14.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "13:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox15.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "14:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox16.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "15:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox17.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "16:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox18.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "17:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox19.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "18:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox20.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "19:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox21.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "20:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox22.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "21:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox23.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "22:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        if (checkBox24.Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = "23:00:00";
                            MüsaitDerslikler[y, 3] = gün;
                            y++;
                        }
                        
                    }
                 z++; // 1 günün tüm saatlerin bakıldıktan sonra z verisi 1 artırıldı
                }
            }
            return MüsaitDerslikler; // Tüm veriler kaydedildikten sonra metodun çağrıldığı yere MüsaitDerslikler dizisine geri gönderildi
        }
        private string[,] SınıfMüsaitlik() // Sınıf tablosundan bilgileri çekip SınıfMüsait dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Sınıf"; //sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int rowCount = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                rowCount++; // veri başına rowcount bir artırıldı 
            }
            string[,] SınıflarListe = new string[rowCount, reader.FieldCount]; // SınıflarListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int row = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int col = 0; col < reader.FieldCount; col++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    SınıflarListe[row, col] = reader[col].ToString(); // veri tabanından alınan bilgiler SınıflarListeye eklendi
                }
                row++; // öbür satıra geçmek için row verisi bir artırıldı
            }
            Program.bag.Close(); // veri tabanı bağlantısı kapatıldı
            int satırsayı = SınıflarListe.GetLength(0); // DerslikListenin satır sayısı alındı
            int x = satırsayı * 24 * 5;  // SınıfMüsait alacağı satır sayısı için işlem gerçekleştirilip satırsayı ya kaydedildi
            string[,] SınıfMüsait = new string[x, 4]; // SınıfMüsait adlı 2 boyutlu dizi alınan bilgiler ile oluşturuldu
            int y = 0; // satır sayısını tanımlamak için y verisi oluşturuldu

            for (int i = 0; i < satırsayı; i++)
            {
                TimeSpan saatbaş = TimeSpan.Parse(SınıflarListe[i, 4]); // sınıfların güne en erken derse kaçta başlayacağının verisi alındı
                TimeSpan saatbit = TimeSpan.Parse(SınıflarListe[i, 5]);
                int z = 0; // haftanın hangi gününde olduğu seçilmek üzere z verisi oluşturuldu
                string gün = ""; // haftanın gününü yazılı olarak tutulduğu kısım olarak gün verisi oluşturuldu
                for (int j = 0; j < 5; j++)
                {
                    if (z == 0) // haftanın hangi kısmında ise gün verisi o güne göre ayarlanması için if/else if kullanıldı 
                    {
                        gün = "Pazartesi";
                    }
                    else if (z == 1)
                    {
                        gün = "Salı";
                    }
                    else if (z == 2)
                    {
                        gün = "Çarşamba";
                    }
                    else if (z == 3)
                    {
                        gün = "Perşembe";
                    }
                    else if (z == 4)
                    {
                        gün = "Cuma";
                    }
                    if (saatbaş <= sıfır && sıfır < saatbit) // sınıfın derse başlama saati ile bitiş saati if ile sorgulandı
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0]; // SınıfMüsait için gerekli bilgiler kaydedildi
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "00:00:00";
                        SınıfMüsait[y, 3] = gün;
                    
                    y++; // veri eklendikten sonra bir sonraki satıra geçmek için y verisi bir arttırıldı
                    }
                    if (saatbaş <= bir && bir < saatbit)
                    { 
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "01:00:00";
                        SınıfMüsait[y, 3] = gün;
                    y++;
                    }
                    if (saatbaş <= iki && iki < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "02:00:00";
                        SınıfMüsait[y, 3] = gün;
                    y++;
                    }
                    if (saatbaş <= üç && üç < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "03:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= dört && dört < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "04:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= beş && beş < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "05:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= altı && altı < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "06:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= yedi && yedi < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "07:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= sekiz && sekiz < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "08:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= dokuz && dokuz < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "09:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= on && on < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "10:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= onbir && onbir < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "11:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= oniki && oniki < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "12:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= onüç && onüç < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "13:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= ondört && ondört < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "14:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= onbeş && onbeş < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "15:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= onaltı && onaltı < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "16:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= onyedi && onyedi < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "17:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= onsekiz && onsekiz < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "18:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= ondokuz && ondokuz < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "19:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= yirmi && yirmi < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "20:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= yirmibir && yirmibir < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "21:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= yirmiiki && yirmiiki < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "22:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    if (saatbaş <= yirmiüç && yirmiüç < saatbit)
                    {
                        SınıfMüsait[y, 0] = SınıflarListe[i, 0];
                        SınıfMüsait[y, 1] = SınıflarListe[i, 1];
                        SınıfMüsait[y, 2] = "23:00:00";
                        SınıfMüsait[y, 3] = gün;
                        y++;
                    }
                    z++;
                }
            }
            return SınıfMüsait; // Tüm veriler kaydedildikten sonra metodun çağrıldığı yere SınıfMüsait dizisine geri gönderildi
        }

        private void DPOluşturma_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true; //ders yapılacak saatler yapılmayacak saatlerden fazla olacağından dolayı en başta hepsi seçili olacak şekilde ayarlandı
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox12.Checked = true;
            checkBox13.Checked = true;
            checkBox14.Checked = true;
            checkBox15.Checked = true;
            checkBox16.Checked = true;
            checkBox17.Checked = true;
            checkBox18.Checked = true;
            checkBox19.Checked = true;
            checkBox20.Checked = true;
            checkBox21.Checked = true;
            checkBox22.Checked = true;
            checkBox23.Checked = true;
            checkBox24.Checked = true;

            checkBox25.Checked = true; //ders yapılacak günler yapılmayacak günlerden fazla olacağından dolayı en başta hepsi seçili olacak şekilde ayarlandı
            checkBox26.Checked = true;
            checkBox27.Checked = true;
            checkBox28.Checked = true;
            checkBox29.Checked = true;
            
        }

        private void button2_Click(object sender, EventArgs e) // formu kapatma tuşu
        {
            this.Close();
        }
    }
}
