using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class MarcaLogica
    {
        public List<Marca> obtenerMarcas()
        {


            DataSet ds = AccesoDatos.MarcaDato.seleccionarMarcas();
            List<Marca> resultado = new List<Marca>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Marca marca = new Marca();
                marca.Id = Convert.ToInt32(row["ID"]);
                marca.Descripcion = row["Descripcion"].ToString();

                resultado.Add(marca);
            }
            return resultado;
        }

        public Marca seleccionarMarca(int id)
        {
            Marca marca = new Marca();
            DataSet ds = AccesoDatos.MarcaDato.seleccionarMarca(id);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                marca.Id = Convert.ToInt32(row["ID"]);
                marca.Descripcion = row["Descripcion"].ToString();
            }
            return marca;
        }

        public Marca seleccionarMarcaNombre(string descripcion)
        {
            Marca marca = new Marca();
            DataSet ds = AccesoDatos.MarcaDato.seleccionarMarcaNombre(descripcion);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                marca.Id = Convert.ToInt32(row["ID"]);
                marca.Descripcion = row["Descripcion"].ToString();
            }
            return marca;
        }
        public void InsertarActualizarMarca(Marca marca)
        {
            AccesoDatos.MarcaDato.InsertarActualizarMarca(marca.Id, marca.Descripcion);
        }
    }
}
