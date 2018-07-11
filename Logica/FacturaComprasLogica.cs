﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidad;
using AcessoDatos;
using System.Data;
namespace Logica
{
  public  class FacturaComprasLogica
    {
        public FacturaCompras ObtenerContadorFacturas()
        {
            FacturaCompras factura = new FacturaCompras();

            DataSet ds = AccesoDatos.FacturaComprasDato.seleccionarContadorFacturas();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                factura.Id =Convert.ToInt32( row["CONTADOR"].ToString());
               


            }

            return factura;
        }


        public void GuardarFactura(FacturaCompras factura)
        {
            try
            {
                var xml = Utiles.toXML(factura);


                AccesoDatos.FacturaComprasDato.GuardarFactura(xml);
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }
    }
}