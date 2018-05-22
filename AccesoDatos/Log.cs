using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessoDatos
{
    public class Log
    {

        public static void Write(String metodo, String mensaje)
        {

            String fecha = DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss");

            String archivo = "C:\\TEMP\\ErrorBaseDatos" + DateTime.Now.ToString("dd/MM/yyyy") + ".txt";

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(@archivo, true);
                file.WriteLine("------------------------------------------");
                file.WriteLine("Método :" + metodo);
                file.WriteLine("Fecha  :" + fecha);
                file.WriteLine("Detalle");
                file.WriteLine("\n");
                file.WriteLine("Message " + mensaje);
                file.WriteLine("\n");
                file.WriteLine("------------------------------------------");
                file.WriteLine("\n");
                file.Close();
            }
            catch
            {
            }

        }

        public static void Write(String metodo, Exception ex)
        {

            String fecha = DateTime.Now.ToString("dd/MM/yyyy hh:MM:ss");

            String archivo = "C:\\TEMP\\ErrorBaseDatos" + DateTime.Now.ToString("dd/MM/yyyy") + ".txt";
            try
            {

                System.IO.StreamWriter file = new System.IO.StreamWriter(@archivo, true);
                file.WriteLine("------------------------------------------");
                file.WriteLine("Método :" + metodo);
                file.WriteLine("Fecha  :" + fecha);
                file.WriteLine("Detalle");
                file.WriteLine("\n");
                file.WriteLine("Message " + ex.Message);
                file.WriteLine("\n");
                file.WriteLine("StackTrace " + ex.StackTrace);
                file.WriteLine("\n");
                file.WriteLine("Source " + ex.Source);
                file.WriteLine("\n");
                file.WriteLine("ToString() " + ex.ToString());
                file.WriteLine("------------------------------------------");
                file.WriteLine("\n");
                file.Close();
            }
            catch { }
        }
    }
}
