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
  public  class ProveedorLogica
    {


        public List<Proveedor> obtenerProveedores()
        {


            DataSet ds = AccesoDatos.ProveedorDato.seleccionarProveedorTodo();

            List<Proveedor> resultado = new List<Proveedor>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Proveedor proveedor = new Proveedor();
                proveedor.Id = row["ID"].ToString();
                proveedor.NombreProveedor = row["NombreProveedor"].ToString();
                proveedor.NombreContacto = row["NombreContacto"].ToString();
                proveedor.Correo = row["Correo"].ToString();
                proveedor.Telefono = int.Parse(row["Telefono"].ToString());

                resultado.Add(proveedor);
            }

            return resultado;
        }


        public Proveedor obtenerProveedor(string idCliente)
        {
            Proveedor proveedor = new Proveedor();

            DataSet ds = AccesoDatos.ProveedorDato.seleccionarProveedorPorID(idCliente);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                proveedor.Id = row["ID"].ToString();
                proveedor.NombreProveedor = row["NombreProveedor"].ToString();
                proveedor.NombreContacto = row["NombreContacto"].ToString();
                proveedor.Correo = row["Correo"].ToString();
                proveedor.Telefono = int.Parse(row["Telefono"].ToString());


            }

            return proveedor;
        }

        public void InsertarActialiarProveedor(Proveedor proveedor)
        {
            AccesoDatos.ProveedorDato.InsertarActaulizarProveedor(proveedor.Id, proveedor.NombreProveedor, proveedor.NombreContacto, proveedor.Correo, proveedor.Telefono);
        }
    }
}
