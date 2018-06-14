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
   public class ProductoLogica
    {


        public List<Producto> obtenerProductos()
        {


            DataSet ds = AccesoDatos.ProductoDato.seleccionarProductoTodo();
            MarcaLogica mlogica = new MarcaLogica();
            List<Producto> resultado = new List<Producto>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Producto producto = new Producto();
                producto.IdProducto = row["IDProducto"].ToString();
                producto.Nombre = row["Nombre"].ToString();
                producto.Marca = mlogica.seleccionarMarca(Convert.ToInt32(row["Marca"]));
                producto.PrecioCompra = decimal.Parse( row["PrecioCompra"].ToString());
                producto.PrecioVenta = decimal.Parse(row["PrecioVenta"].ToString());

                resultado.Add(producto);
            }

            return resultado;
        }


        public Producto obtenerProducto(string idProducto)
        {
            Producto producto = new Producto();
            MarcaLogica mlogica = new MarcaLogica();
            DataSet ds = AccesoDatos.ProductoDato.seleccionarProductoPorID(idProducto);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                producto.IdProducto = row["IDProducto"].ToString();
                producto.Nombre = row["Nombre"].ToString();
                producto.Marca = mlogica.seleccionarMarca(Convert.ToInt32(row["Marca"]));
                producto.PrecioCompra = decimal.Parse(row["PrecioCompra"].ToString());
                producto.PrecioVenta = decimal.Parse(row["PrecioVenta"].ToString());


            }

            return producto;
        }


        public void InsertarActialiarProducto(Producto producto)
        {
            AccesoDatos.ProductoDato.InsertarActaulizarProducto(producto.IdProducto, producto.Nombre, producto.Marca.Id, producto.PrecioCompra, producto.PrecioVenta);
        }
    }
}
