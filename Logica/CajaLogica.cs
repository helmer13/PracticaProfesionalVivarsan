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

    }
}
