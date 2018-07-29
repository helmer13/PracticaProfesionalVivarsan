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



        public TotalesCierreCaja ObtenerTotalesCierrreCaja(DateTime fecha, string idUsuario)
        {
            DataSet ds = AccesoDatos.CajaDato.seleccionarTotalesCierreCaja(fecha,idUsuario);
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
    }
}
