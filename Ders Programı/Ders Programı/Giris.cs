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

namespace Ders_Programı
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // giriş tuşu
        {
            giris(); // tuşa basıldığında giris metotu çalıştırılır
        }

        private void giris()
        {
            
            string sql = "select * from kullanıcı where Ad = @prm1 and Şifre = @prm2"; // sql komutu kaydedilir
            SqlDataAdapter da = new SqlDataAdapter(sql, Program.bag); //veri tabanı ile kodun arasında bağlantı kurup gerekli işlemleri yapmaya hazır olunur
            da.SelectCommand.Parameters.AddWithValue("@prm1", textBox1.Text); // @prm işlemleri hangi textbox veya comboboxtan olduğu tanımlanıyor
            da.SelectCommand.Parameters.AddWithValue("@prm2", textBox2.Text);
            DataTable dt = new DataTable(); // bilgileri tutmak üzere dt oluşturulur
            da.Fill(dt); // da üzerinde daha önceden yapılan bağlantı aracalığı ile dt üzerine sql komutundan gelen bilgiler eklenir


            if (dt.Rows.Count > 0) // eşleşen sonuç olup olmadığı kontrol edilir
            {
                Arasayfa arasayfa = new Arasayfa();
                this.Hide(); // bu form saklanır
                arasayfa.ShowDialog(); // arasayfa formu açılır ve bu form ana form kapanana kadar beklemede kalır
                this.Close(); // bu form kapanır
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış"); //kullanıcıya hatalı giriş yaptığının mesajını verir
            }
        }
        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)  // kullanıcı adı
        {
            if (e.KeyChar == (char)Keys.Enter)  // kullanıcı adı textboxunda entera basıldığında metot çalışır
            {
                giris();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) // şifre
        {
            if (e.KeyChar == (char)Keys.Enter)  // kullanıcı adı textboxunda entera basıldığında metot çalışır
            {
                giris();
            }
        }
    }
}
