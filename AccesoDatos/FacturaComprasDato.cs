using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using AcessoDatos;

namespace AccesoDatos
{
   public class FacturaComprasDato
    {

        public static DataSet seleccionarContadorFacturas()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CONTADOR_FACTURAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "COMPRAS");
           

            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }


        public static void GuardarFactura(string XML)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_FACTURA_COMPRA");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@XML", XML);
           

            db.ExecuteNonQuery(comando);
        }

        public static DataSet seleccionarValidarNumeroFactura(int numueroFactura,string idProveedor)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_VALIDA_NUMEROFACTURACOMPRAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NUMEROFACTURA", numueroFactura);
            comando.Parameters.AddWithValue("@IDPROVEEDOR", idProveedor);


            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }
        public static DataTable ReporteTotalCompras(DateTime fecha1, DateTime fecha2)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_TOTALCOMPRAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            DataSet ds = db.ExecuteReader(comando, "producto");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }

        public static DataTable ReporteTotalComprasProv(DateTime fecha1, DateTime fecha2, string id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_COMPRAS_PROVEEDOR");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            comando.Parameters.AddWithValue("@IDPROVEEDOR", id);
            DataSet ds = db.ExecuteReader(comando, "factura");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }


        public static void BackUpUsuario()
        {

            string comandoBackUp = "BACKUP DATABASE[BDProyectoCCV] TO DISK = N'C:\\backupCCV\\backUp_backupCCV.bak' WITH NOFORMAT, NOINIT, NAME = N'BDProyectoCCV-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand(comandoBackUp);

            db.ExecuteNonQuery(comando);
        }

        public static void RestoreBD(string ruta)
        {
            string str = "use master";
            string str1 = "alter database BDProyectoCCV set SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            string str2 = "RESTORE DATABASE BDProyectoCCV FROM DISK = '"+ ruta +"' WITH REPLACE";

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand(str);
            SqlCommand comando1 = new SqlCommand(str1);
            SqlCommand comando2 = new SqlCommand(str2);

            db.ExecuteNonQuery(comando);
            db.ExecuteNonQuery(comando1);
            db.ExecuteNonQuery(comando2);
        }

    }
}
