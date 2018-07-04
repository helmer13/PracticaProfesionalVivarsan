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
