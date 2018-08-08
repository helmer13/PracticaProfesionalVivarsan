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
  public  class ProductoDato
    {

        public static DataSet seleccionarProductoTodo()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_PRODUCTO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "TODO");
            comando.Parameters.AddWithValue("@IDPRODUCTO", DBNull.Value);

            DataSet ds = db.ExecuteReader(comando, "producto");
            return ds;

        }


        public static DataSet seleccionarProductoPorID(string idProducto)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_PRODUCTO_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "IDPRODUCTO");
            comando.Parameters.AddWithValue("IDPRODUCTO", idProducto);

            DataSet ds = db.ExecuteReader(comando, "producto");
            return ds;

        }


        public static void InsertarActaulizarProducto(string idProducto, string nombre, int idmodelo, decimal precioCompra, decimal precioVenta)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_PRODUCTO");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@IDPRODUCTO", idProducto);
            comando.Parameters.AddWithValue("@NOMBRE", nombre);
            comando.Parameters.AddWithValue("@IDMODELO", idmodelo);
            comando.Parameters.AddWithValue("@PRECIOCOMPRA", precioCompra);
            comando.Parameters.AddWithValue("@PRECIOVENTA", precioVenta);

            db.ExecuteNonQuery(comando);
        }
        public static DataTable ReporteProductos()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTEPRODUCTOS");
            comando.CommandType = CommandType.StoredProcedure;

            DataSet ds = db.ExecuteReader(comando, "producto");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
    }
}
