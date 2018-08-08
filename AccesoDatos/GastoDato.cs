using AcessoDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class GastoDato
    {
        public static DataSet seleccionarGastoTodo()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_GASTO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDGASTO", 0);

            DataSet ds = db.ExecuteReader(comando, "Gasto");
            return ds;

        }

        public static DataTable ReporteGastos(DateTime fechaInicio, DateTime fechaFin)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_GASTOS_DEVOLUCION");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCOSULTA", "GASTOS");
            comando.Parameters.AddWithValue("@FECHAINICIO", fechaInicio);
            comando.Parameters.AddWithValue("@FECHAFIN", fechaFin);

            DataSet ds = db.ExecuteReader(comando, "Gasto");
            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }


        public static DataSet seleccionarGastoPorID(string id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_GASTO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDGASTO");
            comando.Parameters.AddWithValue("@IDGASTO", id);

            DataSet ds = db.ExecuteReader(comando, "Gasto");
            return ds;

        }


        public static void InsertarGasto(int id, string idUsuario, string descripcion, string tipo, DateTime fecha, double monto)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_GASTO");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@IDUSUARIO", idUsuario);
            comando.Parameters.AddWithValue("@DESCRIPCION", descripcion);
            comando.Parameters.AddWithValue("@TIPO", tipo);
            comando.Parameters.AddWithValue("@FECHA", fecha);
            comando.Parameters.AddWithValue("@MONTO", monto);

            db.ExecuteNonQuery(comando);
        }
    }
}
