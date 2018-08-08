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
  public  class ClienteLogica
    {


        public List< Cliente> obtenerClientes()
        {
           

            DataSet ds = AccesoDatos.ClienteDato.seleccionarClienteTodo();

            List<Cliente> resultado = new List<Cliente>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Cliente Cliente = new Cliente();
                Cliente.Id = row["ID"].ToString();
                Cliente.Indentificacion = row["Identificacion"].ToString();
                Cliente.Nombre = row["Nombre"].ToString();
                Cliente.Correo = row["Correo"].ToString();
                Cliente.Direccion = row["Direccion"].ToString();
                Cliente.Telefono =int.Parse( row["Telefono"].ToString());

                resultado.Add(Cliente);
            }

            return resultado;
        }


        public Cliente obtenerCliente(string idCliente)
        {
            Cliente Cliente = new Cliente();

            DataSet ds = AccesoDatos.ClienteDato.seleccionarClientePorID(idCliente);

          
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Cliente.Id = row["ID"].ToString();
                Cliente.Indentificacion = row["Identificacion"].ToString();
                Cliente.Nombre = row["Nombre"].ToString();
                Cliente.Correo = row["Correo"].ToString();
                Cliente.Direccion = row["Direccion"].ToString();
                Cliente.Telefono = int.Parse(row["Telefono"].ToString());

               
            }

            return Cliente;
        }

        public void InsertarActialiarCliente(Cliente cliente)
        {
            AccesoDatos.ClienteDato.InsertarActaulizarCliente(cliente.Id, cliente.Indentificacion, cliente.Nombre, cliente.Correo, cliente.Direccion, cliente.Telefono);
        }
        public DataTable ReporteClientes()
        {
            return AccesoDatos.ClienteDato.ReporteClientes();
        }

    }
}
