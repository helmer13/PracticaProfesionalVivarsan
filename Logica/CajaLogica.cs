using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidad;
using AcessoDatos;
using System.Data;

namespace Logica
{
   public class CajaLogica
    {

        public void InsertarInsertaAbirCaja(Caja caja)
        {
            AccesoDatos.CajaDato.InsertarCajaabrir(caja.Id, caja.FechaApertura, caja.MontoApertura, caja.Descripcion, caja.Estado, caja.Usuario.Id);
        }


        public void ActualizarCerrarCaja(Caja caja, double montoEfectivoSinBase,
            double montoGastos, double montoEfectivoSistema)
        {
            AccesoDatos.CajaDato.ActualizarCerrarCaja(caja.Id, caja.FechaCierre, caja.MontoCierre, caja.Estado, caja.Usuario.Id,montoEfectivoSinBase,
              montoGastos,montoEfectivoSistema,caja.Mensaje  );
        }



        public TotalesCierreCaja ObtenerTotalesCierrreCaja(DateTime fecha,DateTime fechaCierre, string idUsuario)
        {
            DataSet ds = AccesoDatos.CajaDato.seleccionarTotalesCierreCaja(fecha, fechaCierre, idUsuario);
            TotalesCierreCaja totales = new TotalesCierreCaja();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                totales.Id =row["ID"].ToString();
                totales.FechaApertura = Convert.ToDateTime(row["FechaApertura"]);
                totales.MobtoApertura = Convert.ToDecimal(row["MontoApertura"]);
            
            }

            foreach (DataRow row in ds.Tables[1].Rows)
            {            
                totales.Gastos = Convert.ToDecimal(row["GASTOS"]); 
            }

            foreach (DataRow row in ds.Tables[2].Rows)
            {

                totales.Contado = Convert.ToDecimal(row["CONTADO"].ToString());
                totales.Tarjeta = Convert.ToDecimal( row["TARJETA"].ToString());
            }

            return totales;
        }


        public Caja ObtenerCajaAbierta( string idUsuario)
        {
            DataSet ds = AccesoDatos.CajaDato.SeleccionarCajaAbierta(idUsuario);
            Caja caja = new Caja();
            UsuarioLogica logica = new UsuarioLogica();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                caja.Id = row["ID"].ToString();
                caja.FechaApertura = Convert.ToDateTime(row["FechaApertura"]);
                caja.FechaCierre = Convert.ToDateTime(row["FechaCierre"]);
                caja.MontoApertura = Convert.ToDouble(row["MontoApertura"].ToString());
                caja.MontoCierre = Convert.ToDouble(row["MontoCierreCaja"].ToString());
                caja.Descripcion = row["Descripcion"].ToString();
                caja.Estado = row["Estado"].ToString();
                caja.Usuario = logica.seleccionarUsuario( row["IDUsuario"].ToString());
            }

           

            return caja;
        }
        public DataTable ReporteCajas(DateTime fechaInicio, DateTime fechaFin)
        {
            return AccesoDatos.CajaDato.ReporteCajas(fechaInicio, fechaFin);
        }
        public DataTable ReporteCajaID(string id)
        {
            return AccesoDatos.CajaDato.ReporteCajaID(id);
        }
    }
}
