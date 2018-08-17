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
    public class FacturaVentasDato
    {
        public static DataSet seleccionarContadorFacturas()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CONTADOR_FACTURAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "VENTAS");


            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }


        public static void GuardarFactura(string XML)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_FACTURA_VENTA");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@XML", XML);


            db.ExecuteNonQuery(comando);
        }


        public static DataSet seleccionarLineaDetalleVentas(int idFacturaVentas)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_FACTURAVENTAS_VISTA");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "LINEADETALLE");
            comando.Parameters.AddWithValue("@IDFACTURAVENTAS", idFacturaVentas);

            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }


        public static void InsertarDevolucion(string id, int idRegistroVentas, DateTime fechaDevolucion, double monto, string idLineaDetalleVentas, int cantidad,
            string tipo,int idEmpresa, int idBodega, string idProducto)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_ACTUALIZAR_DEVOLUCION");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@IDREGISTROVENTAS", idRegistroVentas);
            comando.Parameters.AddWithValue("@FECHADEVOLUCION", fechaDevolucion);
            comando.Parameters.AddWithValue("@MONTO", monto);
            comando.Parameters.AddWithValue("@IDLINEADETALLEVENTAS", idLineaDetalleVentas);
            comando.Parameters.AddWithValue("@CANTIDAD", cantidad);
            comando.Parameters.AddWithValue("@TIPO", tipo);
            comando.Parameters.AddWithValue("@IDEMPRESA", idEmpresa);
            comando.Parameters.AddWithValue("@IDBODEGA", idBodega);
            comando.Parameters.AddWithValue("@IDPRODUCTO", idProducto);
            db.ExecuteNonQuery(comando);
        }

        public static DataTable ReporteTotalVentas(DateTime fecha1, DateTime fecha2)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_TOTALVENTAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            DataSet ds = db.ExecuteReader(comando, "producto");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
        public static DataTable ReporteTotalVentasCl(DateTime fecha1, DateTime fecha2, string id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_VENTAS_CLIENTE");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            comando.Parameters.AddWithValue("@IDCLIENTE", id);
            DataSet ds = db.ExecuteReader(comando, "facturas");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }

        public static DataTable ReporteDevoluciones(DateTime fecha1, DateTime fecha2)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTEDEVOLUCIONES");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            DataSet ds = db.ExecuteReader(comando, "facturas");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
        public static DataSet EncabezadoFactura(int idFacturaVentas)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_DETALLEFACTURA_VENTAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NUMEROFACTURA", idFacturaVentas);

            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }

        public static DataTable ReporteFactura2(int id)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_DETALLEFACTURA2_VENTAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NUMEROFACTURA", id);
            DataSet ds = db.ExecuteReader(comando, "facturas");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
    }
}
