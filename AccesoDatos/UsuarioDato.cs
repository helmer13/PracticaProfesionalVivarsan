using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using AcessoDatos;

//using PracticaProfesionalVivarsan.
namespace AccesoDatos
{
  public  class UsuarioDato
    {
       public  static DataSet seleccionarUsuarios()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_VISTA_USUARIOS");
            comando.CommandType = CommandType.StoredProcedure;
        //    comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            

            DataSet ds = db.ExecuteReader(comando, "Usuario");
            return ds;

        }




    }
}
