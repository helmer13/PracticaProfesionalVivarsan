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
   public class ClienteDato
    {


        public static DataSet seleccionarClienteTodo()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CLIENTE_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDCLIENTE", DBNull.Value);

            DataSet ds = db.ExecuteReader(comando, "cliente");
            return ds;

        }

        public static DataSet seleccionarClientePorID(string idCliente)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CLIENTE_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDCLIENTE");
            comando.Parameters.AddWithValue("@IDCLIENTE", idCliente);

            DataSet ds = db.ExecuteReader(comando, "cliente");
            return ds;

        }


    }
}
