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
using CheckBox = System.Windows.Forms.CheckBox;

namespace Ders_Programı
{
    public partial class DPOluşturma : Form
    {
        


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
                string[,] Sınıf = Sınıflar();  // sınıf tablosunda olan bilgilerin tutulduğu  dizi
                string[,] Ders = Dersler();    // ders tablosunda olan bilgilerin tutulduğu dizi                 
                string[,] Öğretmen = Öğretmenler();     // öğretmen tablosunda olan bilgilerin tutulduğu dizi         
                string[,] ÖğretmenMüsait = ÖğretmenMüsaitlik(); // öğretmenlerin gün/saatlerinin tutulduğu dizi
                string[,] DerslikMüsait = DerslikMüsaitlik();         // dersliklerin  gün/saatlerinin tutulduğu dizi
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

        private string[,] Sınıflar() // sınıf tablosundan bilgileri çekip SınıflarListe dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Sınıf"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int satırsayı = 0; // dizinin boyutunu ayarlamak üzere veri oluşturuldu
            while (reader.Read())
            {
                satırsayı++; // veri başına rowcount bir artırıldı 
            }
            string[,] SınıflarListe = new string[satırsayı, reader.FieldCount]; // SınıflarListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int satır = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int sütun = 0; sütun < reader.FieldCount; sütun++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    SınıflarListe[satır, sütun] = reader[sütun].ToString(); // veri tabanından alınan bilgiler DerslerListeye eklendi
                }
                satır++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı kapatıldı

            return SınıflarListe; // button_click içinde çağırıldığı yere dizi verisi gönderildi
        }

        private string[,] Dersler() // ders tablosundan bilgileri çekip DerslerListe dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Ders"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgiler reader aracılığı ile okundu
            int satırsayı = 0; // dizinin boyutunu ayarlamak üzere veri oluşturuldu
            while (reader.Read())
            {
                satırsayı++; // veri başına rowcount bir artıldı
            }
            string[,] DerslerListe = new string[satırsayı, reader.FieldCount]; // DerslerListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); // çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int satır = 0; // dizide verinin yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int sütun = 0; sütun < reader.FieldCount; sütun++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    DerslerListe[satır, sütun] = reader[sütun].ToString(); // veri tabanından alınan bilgiler DerslerListeye eklendi
                }
                satır++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı kapatıldı

            return DerslerListe; // button_click içinde çağırıldığı yere dizi verisi gönderildi
        }

        

        private string[,] Öğretmenler() // öğretmen tablosundan bilgileri çekip ÖğretmenListe dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Öğretmen"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int satırsayı = 0; // dizinin boyutunu ayarlamak üzere veri oluşturuldu
            while (reader.Read())
            {
                satırsayı++; // veri başına rowcount bir artırıldı 
            }
            string[,] ÖğretmenListe = new string[satırsayı, reader.FieldCount]; // ÖğretmenListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int satır = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int sütun = 0; sütun < reader.FieldCount; sütun++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    ÖğretmenListe[satır, sütun] = reader[sütun].ToString(); // veri tabanından alınan bilgiler DerslerListeye eklendi
                }
                satır++; // öbür satıra geçmek için row verisi 1 artırıldı
            }
            Program.bag.Close(); // veri tabanı kapatıldı
            return ÖğretmenListe; // button_click içinde çağırıldığı yere dizi verisi gönderildi
        }
        // dizilere saat eklenecekken müsait olunup olunmadığı kontrol edilmek üzere saatler birden fazla yerde kullanılacaktı bu yüzden public kısma yazıldı
       
        static TimeSpan sıfır = TimeSpan.Parse("00:00:00");
        static TimeSpan bir = TimeSpan.Parse("01:00:00");
        static TimeSpan iki = TimeSpan.Parse("02:00:00");
        static TimeSpan üç = TimeSpan.Parse("03:00:00");
        static TimeSpan dört = TimeSpan.Parse("04:00:00");
        static TimeSpan beş = TimeSpan.Parse("05:00:00");
        static TimeSpan altı = TimeSpan.Parse("06:00:00");
        static TimeSpan yedi = TimeSpan.Parse("07:00:00");
        static TimeSpan sekiz = TimeSpan.Parse("08:00:00");
        static TimeSpan dokuz = TimeSpan.Parse("09:00:00");
        static TimeSpan on = TimeSpan.Parse("10:00:00");
        static TimeSpan onbir = TimeSpan.Parse("11:00:00");
        static TimeSpan oniki = TimeSpan.Parse("12:00:00");
        static TimeSpan onüç = TimeSpan.Parse("13:00:00");
        static TimeSpan ondört = TimeSpan.Parse("14:00:00");
        static TimeSpan onbeş = TimeSpan.Parse("15:00:00");
        static TimeSpan onaltı = TimeSpan.Parse("16:00:00");
        static TimeSpan onyedi = TimeSpan.Parse("17:00:00");
        static TimeSpan onsekiz = TimeSpan.Parse("18:00:00");
        static TimeSpan ondokuz = TimeSpan.Parse("19:00:00");
        static TimeSpan yirmi = TimeSpan.Parse("20:00:00");
        static TimeSpan yirmibir = TimeSpan.Parse("21:00:00");
        static TimeSpan yirmiiki = TimeSpan.Parse("22:00:00");
        static TimeSpan yirmiüç = TimeSpan.Parse("23:00:00");

       TimeSpan[] saatler = { sıfır, bir, iki, üç, dört, beş, altı, yedi, sekiz, dokuz, on, onbir, oniki, onüç, ondört, onbeş, onaltı, onyedi, onsekiz, ondokuz, yirmi, yirmibir, yirmiiki, yirmiüç };

        private string[,] ÖğretmenMüsaitlik() // öğretmen tablosundan bilgileri çekip Öğretmenmüsait dizisine ekleyen metot
        {

            string[,] Öğretmen = Öğretmenler();

            int satırsayıa = Öğretmen.GetLength(0); // ÖğretmenListenin satır sayısı alındı
            int x = satırsayıa * 24 * 5; // Öğretmenmüsaitin alacağı satır sayısı için işlem gerçekleştirilip satırsayıa ya kaydedildi
            string[,] Öğretmenmüsait = new string[x, 4]; // Öğretmenmüsait adlı 2 boyutlu dizi alınan bilgiler ile oluşturuldu
            int y = 0; // satır sayısını tanımlamak için y verisi oluşturuldu
            for (int i = 0; i < satırsayıa; i++)
            {
                TimeSpan PazartesiBaş = TimeSpan.Parse(Öğretmen[i, 3]); //öğretmen tablosunda öğretmenlerin çalışma saatlerinin alındığı kısım
                TimeSpan PazartesiBit = TimeSpan.Parse(Öğretmen[i, 4]);
                TimeSpan SalıBaş = TimeSpan.Parse(Öğretmen[i, 5]);
                TimeSpan SalıBit = TimeSpan.Parse(Öğretmen[i, 6]);
                TimeSpan ÇarşambaBaş = TimeSpan.Parse(Öğretmen[i, 7]);
                TimeSpan ÇarşambaBit = TimeSpan.Parse(Öğretmen[i, 8]);
                TimeSpan PerşembeBaş = TimeSpan.Parse(Öğretmen[i, 9]);
                TimeSpan PerşembeBit = TimeSpan.Parse(Öğretmen[i, 10]);
                TimeSpan CumaBaş = TimeSpan.Parse(Öğretmen[i, 11]);
                TimeSpan CumaBit = TimeSpan.Parse(Öğretmen[i, 12]);
                string ad = Öğretmen[i, 1]; //Öğretmen adını ÖğretmenListeden alındı
                string soyadı = Öğretmen[i, 2]; // Öğretmen soyadı ÖğretmenListeden alındı
                string öğretmenisim = ad + " " + soyadı; // Öğretmenin adı ve soyadı tek bir veride birleştirildi
                int z = 0; // haftanın hangi gününde olduğu seçilmek üzere z verisi oluşturuldu
                int dizigün = 0;
                TimeSpan[] öğretmengün = { PazartesiBaş, PazartesiBit, SalıBaş, SalıBit, ÇarşambaBaş, ÇarşambaBit, PerşembeBaş, PerşembeBit, CumaBaş, CumaBit };

                string gün = "";
                for (int j = 0; j < 5; j++) // haftanın her günü bakılması için 5 tekrarlı bir for döngüsü yazıldı
                {
                    string saat = "00:00:00"; // saat 00:00:00 olarak başladı ve her gün döngüsünde geri 00:00:00 olmasına ayarlandı

                    if (z == 0) // haftanın hangi kısmında ise gün verisi o güne göre ayarlanması için if/else if kullanıldı
                    {
                        gün = "Pazartesi";
                        dizigün = 0;
                    }
                    else if (z == 1)
                    {
                        gün = "Salı";
                        dizigün = 2;
                    }
                    else if (z == 2)
                    {
                        gün = "Çarşamba";
                        dizigün = 4;
                    }
                    else if (z == 3)
                    {
                        gün = "Perşembe";
                        dizigün = 6;
                    }
                    else if (z == 4)
                    {
                        gün = "Cuma";
                        dizigün = 8;
                    }

                    for (int ii = 0; ii < 24; ii++)
                    {
                        saat = $"{ii:D2}:00:00"; //saat güncellendi
                        if (öğretmengün[dizigün] <= saatler[ii] && saatler[ii] < öğretmengün[dizigün + 1])
                        {
                            Öğretmenmüsait[y, 0] = Öğretmen[i, 0]; // koşullar sağlanıyorsa Öğretmenmüsait dizisinin içine gerekli veriler eklendi
                            Öğretmenmüsait[y, 1] = öğretmenisim;
                            Öğretmenmüsait[y, 2] = saat;
                            Öğretmenmüsait[y, 3] = gün;
                            y++; // veri eklendikten sonra bir sornaki satıra geçmek için y verisi bir artıldı                         
                        }


                    }

                    z++; // gün artırma
                }
            }
            return Öğretmenmüsait; // Tüm veriler kaydedildikten sonra metodun çağrıldığı yere Öğretmenmüsait dizisine geri gönderildi
        }

        private string [,] DerslikMüsaitlik() // Derslikİsim tablosundan bilgileri çekip MüsaitDerslikler dizisine ekleyen metot
        {
            string sql = "SELECT * FROM Derslikİsim"; // sql kodu
            SqlCommand cmd = new SqlCommand(sql, Program.bag); // sql kodu ile veri tabanı bağlantısı birleştirildi
            Program.bag.Open(); // veri tabanı bağlantısı açıldı
            SqlDataReader reader = cmd.ExecuteReader(); // veri tabanından gelen bilgileri reader aracılığı ile okundu
            int satırsayıa = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                satırsayıa++; // veri başına rowcount bir artırıldı 
            }
            string[,] DerslikListe = new string[satırsayıa, reader.FieldCount];// DerslikListe adında 2 boyutlu dizi oluşturuldu uzunluğu önceden alınan veriler ile ayarlandı
            reader.Close(); //çakışma olmaması için reader kapatıldı
            reader = cmd.ExecuteReader(); // reader baştan okuması için tekrar açıldı
            int satır = 0; // dizide veririn yerleşeceği satırı seçmek için veri oluşturuldu
            while (reader.Read())
            {
                for (int sütun = 0; sütun < reader.FieldCount; sütun++) //dizide verinin yerleşeceği sütunu seçmek için for döngüsü içinde col verisi kullanıldı
                {
                    DerslikListe[satır, sütun] = reader[sütun].ToString(); // veri tabanından alınan bilgiler ÖğretmenListeye eklendi
                }
                satır++; // öbür satıra geçmek için row verisi 1 artırıldı
            }

            Program.bag.Close(); // veri tabanı bağlantısı kapatıldı
            int satırsayı = DerslikListe.GetLength(0); // DerslikListenin satır sayısı alındı
            int x = satırsayı * 24 * 5; // MüsaitDerslikler alacağı satır sayısı için işlem gerçekleştirilip satırsayı ya kaydedildi
            string[,] MüsaitDerslikler = new string[x, 4]; // MüsaitDerslikler adlı 2 boyutlu dizi alınan bilgiler ile oluşturuldu
            int y = 0; // satır sayısını tanımlamak için y verisi oluşturuldu
            CheckBox[] checkboxes = { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7, checkBox8, checkBox9, checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15, checkBox16, checkBox17, checkBox18, checkBox19, checkBox20, checkBox21, checkBox22, checkBox23, checkBox24 };
            for (int i = 0; i < satırsayı; i++)
            {


               
                string gün = ""; // haftanın gününü yazılı olarak tutulduğu kısım olarak gün verisi oluşturuldu
                // haftanın hangi gününde olduğu seçilmek üzere z verisi oluşturuldu
                for (int z = 0; z < 5;) // haftanın her günü bakılması için 5 tekrarlı bir for döngüsü yazıldı
                {
                    string saat = "00:00:00";
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


                    }



                    for (int ii = 0; ii < 24; ii++)
                    {
                        saat = $"{ii:D2}:00:00"; // bir saat sonraya alındı
                        if (checkboxes[ii].Checked == true)
                        {
                            MüsaitDerslikler[y, 0] = DerslikListe[i, 2];
                            MüsaitDerslikler[y, 1] = DerslikListe[i, 1];
                            MüsaitDerslikler[y, 2] = saat;
                            MüsaitDerslikler[y, 3] = gün;
                            y++; // veri eklendikten sonra bir sornaki satıra geçmek için y verisi bir artıldı                         
                        }


                    }
                    z++; // 1 günün tüm saatlerin bakıldıktan sonra z verisi 1 artırıldı

                }


            }
            return MüsaitDerslikler; // Tüm veriler kaydedildikten sonra metodun çağrıldığı yere MüsaitDerslikler dizisine geri gönderildi
        }
        private string[,] SınıfMüsaitlik() // Sınıf tablosundan bilgileri çekip SınıfMüsait dizisine ekleyen metot
        {
            string[,] Sınıf = Sınıflar();
            int satırsayı = Sınıf.GetLength(0); // DerslikListenin satır sayısı alındı
            int x = satırsayı * 24 * 5;  // SınıfMüsait alacağı satır sayısı için işlem gerçekleştirilip satırsayı ya kaydedildi
            string[,] SınıfMüsait = new string[x, 4]; // SınıfMüsait adlı 2 boyutlu dizi alınan bilgiler ile oluşturuldu
            int y = 0; // satır sayısını tanımlamak için y verisi oluşturuldu

            for (int i = 0; i < satırsayı; i++)
            {
                TimeSpan saatbaş = TimeSpan.Parse(Sınıf[i, 4]); // sınıfların güne en erken derse kaçta başlayacağının verisi alındı
                TimeSpan saatbit = TimeSpan.Parse(Sınıf[i, 5]);
                int z = 0; // haftanın hangi gününde olduğu seçilmek üzere z verisi oluşturuldu
                string gün = ""; // haftanın gününü yazılı olarak tutulduğu kısım olarak gün verisi oluşturuldu
                for (int j = 0; j < 5; j++)
                {
                    string saat = "00:00:00";
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
                    for (int ii = 0; ii < 24; ii++)
                    {
                        if (saatbaş <= saatler[ii] && saatler[ii] < saatbit)
                        {
                            saat = $"{ii:D2}:00:00"; // bir saat sonraya alındı
                            SınıfMüsait[y, 0] = Sınıf[i, 0];
                            SınıfMüsait[y, 1] = Sınıf[i, 1];
                            SınıfMüsait[y, 2] = saat;
                            SınıfMüsait[y, 3] = gün;
                            y++; // veri eklendikten sonra bir sornaki satıra geçmek için y verisi bir artıldı
                        }
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
