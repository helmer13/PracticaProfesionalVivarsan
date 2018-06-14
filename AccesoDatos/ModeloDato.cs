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
    public class ModeloDato
    {
        public static DataSet seleccionarModelos()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_MODELO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDMODELO", 0);
            comando.Parameters.AddWithValue("@IDMARCA", DBNull.Value);

            DataSet ds = db.ExecuteReader(comando, "Modelo");
            return ds;

        }

        public static DataSet seleccionarModelo(int id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_MODELO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDMODELO");
            comando.Parameters.AddWithValue("@IDMODELO", id);
            comando.Parameters.AddWithValue("@IDMARCA", 0);

            DataSet ds = db.ExecuteReader(comando, "Modelo");
            return ds;

        }

        public static DataSet seleccionarModeloPorMarca(int id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_MODELO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDMARCA");
            comando.Parameters.AddWithValue("@IDMODELO", DBNull.Value);
            comando.Parameters.AddWithValue("@IDMARCA", id);

            DataSet ds = db.ExecuteReader(comando, "Modelo");
            return ds;

        }
        public static void InsertarActualizarModelo(int id, string descripcion, int anno, int idmarca)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_MODELO");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@DESCRIPCION", descripcion);
            comando.Parameters.AddWithValue("@ANNO", anno);
            comando.Parameters.AddWithValue("@IDMARCA", idmarca);

            db.ExecuteNonQuery(comando);
        }
    }
}
