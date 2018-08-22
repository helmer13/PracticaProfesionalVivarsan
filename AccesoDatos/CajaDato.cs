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
   public class CajaDato
    {


        public static void InsertarCajaabrir( string id, DateTime fechaApertura, 
             double montoApertuta, string descripcion, string estado, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_CAJA");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@TIPOCONSULTA", "ABRIRCAJA");
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@FECHAAPERTURA", fechaApertura);
            comando.Parameters.AddWithValue("@FECHACIERRE", DBNull.Value);
            comando.Parameters.AddWithValue("@MONTOAPERTURA", montoApertuta);
            comando.Parameters.AddWithValue("@MONTOCIERRE", DBNull.Value);
            comando.Parameters.AddWithValue("@DESCRIPCION", descripcion);
            comando.Parameters.AddWithValue("@ESTADO", estado);
            comando.Parameters.AddWithValue("@IDUSUARIO", idUsuario);
            db.ExecuteNonQuery(comando);
        }


        public static void ActualizarCerrarCaja(string id, DateTime fechaCierre,double montoCierre, string estado, string idUsuario, double montoEfectivoSinBase,
            double montoGastos, double montoEfectivoSistema,string mensaje)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_CAJA");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@TIPOCONSULTA", "CIERRECAJA");
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@FECHAAPERTURA", DBNull.Value);
            comando.Parameters.AddWithValue("@FECHACIERRE", fechaCierre);
            comando.Parameters.AddWithValue("@MONTOAPERTURA", DBNull.Value);
            comando.Parameters.AddWithValue("@MONTOCIERRE", montoCierre);
            comando.Parameters.AddWithValue("@DESCRIPCION", DBNull.Value);
            comando.Parameters.AddWithValue("@ESTADO", estado);
            comando.Parameters.AddWithValue("@IDUSUARIO", idUsuario);

            comando.Parameters.AddWithValue("@MONTOEFECTIVOSINBASE", montoEfectivoSinBase);
            comando.Parameters.AddWithValue("@MONTOGASTOS", montoGastos);
            comando.Parameters.AddWithValue("@EFECTIVOSISTEMA", montoEfectivoSistema);
            comando.Parameters.AddWithValue("@MENSAJE", mensaje);
            db.ExecuteNonQuery(comando);
        }



        public static DataSet seleccionarTotalesCierreCaja(DateTime fecha,DateTime fechaCierre, string idUsuario)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CAJAVISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TOTALES");
            comando.Parameters.AddWithValue("@FECHAAPERTURA", fecha);
            comando.Parameters.AddWithValue("@FECHACIERRE", fechaCierre);
            comando.Parameters.AddWithValue("@IDUSUARIO", idUsuario);


            DataSet ds = db.ExecuteReader(comando, "caja");
            return ds;

        }



        public static DataSet SeleccionarCajaAbierta( string idUsuario)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CAJAVISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "CAJAABIERTA");
            comando.Parameters.AddWithValue("@FECHAAPERTURA", DBNull.Value);
            comando.Parameters.AddWithValue("@FECHACIERRE", DBNull.Value);
            comando.Parameters.AddWithValue("@IDUSUARIO", idUsuario);


            DataSet ds = db.ExecuteReader(comando, "caja");
            return ds;

        }
        public static DataTable ReporteCajas(DateTime fecha1, DateTime fecha2)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_CAJA_FECHA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            DataSet ds = db.ExecuteReader(comando, "cajas");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
        public static DataTable ReporteCajaID(string id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_CAJA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@ID", id);
            DataSet ds = db.ExecuteReader(comando, "cajas");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
    }
}
