using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class GastoLogica
    {
        public List<Gasto> seleccionarGastoTodo()
        {


            DataSet ds = AccesoDatos.GastoDato.seleccionarGastoTodo();
            UsuarioLogica uLogica = new UsuarioLogica(); 
            List<Gasto> resultado = new List<Gasto>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Gasto gasto = new Gasto();
                gasto.Id = Convert.ToInt32(row["IDProducto"]);
                gasto.Descripcion = row["Descripcion"].ToString();
                gasto.Usuario = uLogica.seleccionarUsuario(row["IDUsuario"].ToString());
                gasto.Fecha = Convert.ToDateTime(row["Fecha"].ToString());
                gasto.Monto = Convert.ToDouble(row["Monto"]);

                resultado.Add(gasto);
            }

            return resultado;
        }


        public Gasto seleccionarGastoPorID(string idGasto)
        {
            Gasto gasto = new Gasto();
            UsuarioLogica uLogica = new UsuarioLogica();
            DataSet ds = AccesoDatos.GastoDato.seleccionarGastoPorID(idGasto);


            foreach (DataRow row in ds.Tables[0].Rows)
            {               
                gasto.Id = Convert.ToInt32(row["IDProducto"]);
                gasto.Descripcion = row["Descripcion"].ToString();
                gasto.Usuario = uLogica.seleccionarUsuario(row["IDUsuario"].ToString());
                gasto.Fecha = Convert.ToDateTime(row["Fecha"].ToString());
                gasto.Monto = Convert.ToDouble(row["Monto"]);


            }

            return gasto;
        }


        public void InsertarGasto(Gasto gasto)
        {
            AccesoDatos.GastoDato.InsertarGasto(gasto.Id, gasto.Usuario.Id, gasto.Descripcion, gasto.Tipo, gasto.Fecha, gasto.Monto);
        }
    }
}
