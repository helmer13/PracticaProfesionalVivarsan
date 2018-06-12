using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
  public class BodegaLogica
    {
        /// <summary>
        /// Selecciona toda la lista de bodegas
        /// </summary>
        /// <returns></returns>
        public List<Bodega> obtenerBodegas()
        {
            DataSet ds = AccesoDatos.BodegaDato.seleccionarBodegas();
            List<Bodega> resultado = new List<Bodega>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Bodega modelo = new Bodega();
                modelo.Id = Convert.ToInt32(row["ID"]);
                modelo.Nombre = row["Nombre"].ToString();
                modelo.Direccion = row["Direccion"].ToString();
                modelo.Telefono = Convert.ToInt32(row["Telefono"]);
              

                resultado.Add(modelo);
            }
            return resultado;
        }

    }
}
