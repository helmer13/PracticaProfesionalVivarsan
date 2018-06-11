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
   public class EmpresaDato
    {
        public static DataSet seleccionarEmpresa(int idEmpresa)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_EMPRESA_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "EMPRESA");
            comando.Parameters.AddWithValue("@IDEMPRESA", idEmpresa);

            DataSet ds = db.ExecuteReader(comando, "Empresa");
            return ds;

        }


    }
}
