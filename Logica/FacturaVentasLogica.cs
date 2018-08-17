using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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


        public List<LineaDetalleVentas> obtenerLineaDetalleVentas(int idFactactaVentas)
        {


            DataSet ds = AccesoDatos.FacturaVentasDato.seleccionarLineaDetalleVentas(idFactactaVentas);

            List<LineaDetalleVentas> resultado = new List<LineaDetalleVentas>();
            ProductoLogica logicaProducto = new ProductoLogica();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                LineaDetalleVentas LineaDetalle = new LineaDetalleVentas();
                LineaDetalle.Id = row["ID"].ToString();
                LineaDetalle.IdFactura =Convert.ToInt32( row["IDRegistroVentas"].ToString());
                LineaDetalle.Producto = logicaProducto.obtenerProducto(row["IDProducto"].ToString());
                LineaDetalle.Cantidad = Convert.ToInt32( row["Cantidad"].ToString());
                LineaDetalle.SubTotal = Convert.ToDouble( row["SubTotal"].ToString());
              

                resultado.Add(LineaDetalle);
            }

            return resultado;
        }

        public FacturaVentas ObtenerContadorFacturas()
        {
            FacturaVentas factura = new FacturaVentas();

            DataSet ds = AccesoDatos.FacturaVentasDato.seleccionarContadorFacturas();


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                factura.Id = Convert.ToInt32(row["CONTADOR"].ToString());



            }

            return factura;
        }


        public void InsertarDevolucion(LineaDetalleVentas lineaDetalle,string idDevolucion,DateTime fechaDevolucion,int cantidad,string tipo,int idBodega,int idEmpresa)
        {
            AccesoDatos.FacturaVentasDato.InsertarDevolucion(idDevolucion,lineaDetalle.IdFactura,fechaDevolucion,0,
                lineaDetalle.Id,cantidad,tipo,idEmpresa,idBodega,lineaDetalle.Producto.IdProducto);
        }

        public DataTable ReporteTotalVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            return AccesoDatos.FacturaVentasDato.ReporteTotalVentas(fechaInicio, fechaFin);
        }

        public DataTable ReporteTotalVentasCl(DateTime fechaInicio, DateTime fechaFin, string id)
        {
            return AccesoDatos.FacturaVentasDato.ReporteTotalVentasCl(fechaInicio, fechaFin, id);
        }
        public DataTable ReporteDevoluciones(DateTime fechaInicio, DateTime fechaFin)
        {
            return AccesoDatos.FacturaVentasDato.ReporteDevoluciones(fechaInicio, fechaFin);
        }

        public FacturaVentas EncabezadoFactura(int idFacturaVentas)
        {


            DataSet ds = AccesoDatos.FacturaVentasDato.EncabezadoFactura(idFacturaVentas);

            FacturaVentas resultado = new FacturaVentas();
            UsuarioLogica uL = new UsuarioLogica();
            ClienteLogica cL = new ClienteLogica();
            foreach (DataRow row in ds.Tables[0].Rows)
            {               
                resultado.Id = Convert.ToInt32(row["ID"].ToString());
                resultado.Usuario = uL.seleccionarUsuario(row["IDUsuario"].ToString());
                resultado.Cliente = cL.obtenerCliente(row["IDCliente"].ToString());
                resultado.Fecha = Convert.ToDateTime(row["Fecha"].ToString());
                resultado.TipoPago = row["TipoPago"].ToString();
                resultado.Total = Convert.ToDouble(row["Total"].ToString());

            }

            return resultado;
        }
        public DataTable ReporteFactura2(int id)
        {
            return AccesoDatos.FacturaVentasDato.ReporteFactura2(id);
        }
    }
}
