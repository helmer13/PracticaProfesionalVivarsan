﻿using System;
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

        public static DataSet seleccionarValidarNumeroFactura(int numueroFactura,string idProveedor)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");

            SqlCommand comando = new SqlCommand("SPR_VALIDA_NUMEROFACTURACOMPRAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@NUMEROFACTURA", numueroFactura);
            comando.Parameters.AddWithValue("@IDPROVEEDOR", idProveedor);


            DataSet ds = db.ExecuteReader(comando, "factura");
            return ds;

        }
        public static DataTable ReporteTotalCompras(DateTime fecha1, DateTime fecha2)
        {

            Database db = DatabaseFactory.CreateDatabase("Default");
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("SPR_REPORTE_TOTALCOMPRAS");
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@FECHAINICIO", fecha1);
            comando.Parameters.AddWithValue("@FECHAFIN", fecha2);
            DataSet ds = db.ExecuteReader(comando, "producto");

            SqlDataAdapter adp = new SqlDataAdapter(comando);
            adp.Fill(dt);
            return dt;

        }
    }
}
