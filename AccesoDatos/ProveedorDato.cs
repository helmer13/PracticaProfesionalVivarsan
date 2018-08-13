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
  public  class ProveedorDato
    {


        public static DataSet seleccionarProveedorTodo()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_PROVEEDOR_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDPROVEEDOR", DBNull.Value);

            DataSet ds = db.ExecuteReader(comando, "proveedor");
            return ds;

        }

        public static DataTable ReporteProveedores()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();

            SqlCommand comando = new SqlCommand("SPR_PROVEEDOR_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDPROVEEDOR", DBNull.Value);

            DataSet ds = db.ExecuteReader(comando, "proveedor");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }


        public static DataSet seleccionarProveedorPorID(string idProveedor)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_PROVEEDOR_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDPROVEEDOR");
            comando.Parameters.AddWithValue("@IDPROVEEDOR", idProveedor);

            DataSet ds = db.ExecuteReader(comando, "proveedor");
            return ds;

        }


        public static void InsertarActaulizarProveedor(string id, string nombreProveedor, string nombreContacto, string correo, int telefono)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_PROVEEDOR");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@NOMBREPROVEEDOR", nombreProveedor);
            comando.Parameters.AddWithValue("@NOMBRECONTACTO", nombreContacto);
            comando.Parameters.AddWithValue("@CORREO", correo);
            comando.Parameters.AddWithValue("@TELEFONO", telefono);

            db.ExecuteNonQuery(comando);
        }
    }
}
