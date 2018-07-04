using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class InventarioLogica
    {
        public List<Inventario> obtenerInventario(int empresa)
        {
            DataSet ds = AccesoDatos.InventarioDato.seleccionarInventario(empresa);
            ProductoLogica plogica = new ProductoLogica();
            BodegaLogica bLogica = new BodegaLogica();
            EmpresaLogica eLogica = new EmpresaLogica();
            List<Inventario> resultado = new List<Inventario>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Inventario invent = new Inventario();
                invent.Producto = plogica.obtenerProducto(row["IDProducto"].ToString());
                invent.Bodega = bLogica.obtenerBodega(Convert.ToInt32(row["IDBodega"].ToString()));
                invent.Empresa = eLogica.obtenerEmpresa(Convert.ToInt32(row["IDEmpresa"].ToString()));
                invent.Cantidad = (Convert.ToInt32(row["Cantidad"]));

                resultado.Add(invent);
            }
            return resultado;
        }
    }
}
