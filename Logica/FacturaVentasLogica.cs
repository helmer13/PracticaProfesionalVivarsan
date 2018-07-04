using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class FacturaVentasLogica
    {
        public void GuardarFactura(FacturaVentas factura)
        {
            try
            {
                var xml = Utiles.toXML(factura);


                AccesoDatos.FacturaVentasDato.GuardarFactura(xml);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
