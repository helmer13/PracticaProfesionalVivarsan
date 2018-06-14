using Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ModeloLogica
    {
        public List<Modelo> obtenerModelos()
        {
            DataSet ds = AccesoDatos.ModeloDato.seleccionarModelos();
            MarcaLogica mlogica = new MarcaLogica();
            List<Modelo> resultado = new List<Modelo>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Modelo modelo = new Modelo();
                modelo.Id = Convert.ToInt32(row["ID"]);
                modelo.Descripcion = row["Descripcion"].ToString();
                modelo.Anno = Convert.ToInt32(row["Anno"]);
                modelo.Marca = mlogica.seleccionarMarca(Convert.ToInt32(row["IDMarca"]));

                resultado.Add(modelo);
            }
            return resultado;
        }

        public Modelo seleccionarModelo(int id)
        {
            Modelo modelo = new Modelo();
            MarcaLogica mlogica = new MarcaLogica();
            DataSet ds = AccesoDatos.ModeloDato.seleccionarModelo(id);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                modelo.Id = Convert.ToInt32(row["ID"]);
                modelo.Descripcion = row["Descripcion"].ToString();
                modelo.Anno = Convert.ToInt32(row["Anno"]);
                modelo.Marca = mlogica.seleccionarMarca(Convert.ToInt32(row["IDMarca"]));
            }
            return modelo;
        }
        public void InsertarActualizarModelo(Modelo modelo)
        {
            AccesoDatos.ModeloDato.InsertarActualizarModelo(modelo.Id, modelo.Descripcion, modelo.Anno, modelo.Marca.Id);
        }
    }
}
