using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ders_Programı
{
    internal static class Program
    {

        static public SqlConnection bag = new SqlConnection(" Bağlantı kodu buraya yazılacak "); //bütün metotlarda kullanmak üzere public sql bağlantısı tanımlandı
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Giris());
        }
    }
}
