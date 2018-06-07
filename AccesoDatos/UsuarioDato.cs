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

        public static DataSet seleccionarUsuario(String usuario, String contrasenna)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INICIARSESION");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@USUARIO", usuario);
            comando.Parameters.AddWithValue("@CONTRASENA", contrasenna);

            DataSet ds = db.ExecuteReader(comando, "usuario");
            return ds;

        }
        public static void InsertarActualizarUsuario(string id, string nombre, string usuarioGeneral, string contrasena, string tipo)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_USUARIO");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@NOMBRE", nombre);
            comando.Parameters.AddWithValue("@USUARIO", usuarioGeneral);
            comando.Parameters.AddWithValue("@CONTRASENA", contrasena);
            comando.Parameters.AddWithValue("@TIPO", tipo);

            db.ExecuteNonQuery(comando);
        }




    }
}
