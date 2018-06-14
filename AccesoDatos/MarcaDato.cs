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
    public class MarcaDato
    {
        public static DataSet seleccionarMarcas()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_MARCA_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDMARCA", 0);

            DataSet ds = db.ExecuteReader(comando, "Marca");
            return ds;

        }

        public static DataSet seleccionarMarca(int id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_MARCA_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDMARCA");
            comando.Parameters.AddWithValue("@IDMARCA", id);

            DataSet ds = db.ExecuteReader(comando, "Marca");
            return ds;

        }

        public static DataSet seleccionarMarcaNombre(string descripcion)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_MARCA_VISTAPORNOMBRE");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@DESCRIPCION", descripcion);

            DataSet ds = db.ExecuteReader(comando, "Marca");
            return ds;

        }
        

        public static void InsertarActualizarMarca(int id, string descripcion)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_MARCA");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@DESCRIPCION", descripcion);

            db.ExecuteNonQuery(comando);
        }
    }
}
