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
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "Ventas");


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
    }
}
