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
   public class FacturaComprasDato
    {

        public static DataSet seleccionarContadorFacturas()
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_CONTADOR_FACTURAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@TIPOCONSULTA", "COMPRAS");
           

            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }


        public static void GuardarFactura(string XML)
        {
            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_INSERTAR_FACTURA_COMPRA");
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@XML", XML);
           

            db.ExecuteNonQuery(comando);
        }


    }
}
