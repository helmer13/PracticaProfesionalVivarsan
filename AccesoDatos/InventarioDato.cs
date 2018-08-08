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
    public class InventarioDato
    {
        public static DataSet seleccionarInventario(int empresa)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INVENTARIO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDEMPRESA", empresa);
            DataSet ds = db.ExecuteReader(comando, "Inventario");
            return ds;

        }

        public static DataTable ReporteInventario(int empresa)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTEINVENTARIO");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@IDEMPRESA", empresa);
            DataSet ds = db.ExecuteReader(comando, "Inventario");
            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }

        public static DataSet seleccionarInventarioPorPRODEMPBOD(int empresa)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INVENTARIO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDEMPRESA", empresa);
            DataSet ds = db.ExecuteReader(comando, "Inventario");
            return ds;

        }
    }
}
