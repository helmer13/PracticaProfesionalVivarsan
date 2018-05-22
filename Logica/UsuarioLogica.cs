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
  public  class UsuarioLogica
    {

        public Usuario obtenerUsuarios()
        {
            Usuario usuario1 = new Usuario();
           
            DataSet ds = AccesoDatos.UsuarioDato.seleccionarUsuarios();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                usuario1.Id = row["ID"].ToString();
                usuario1.UsuarioGeneral = row["Usuario"].ToString();
                usuario1.Tipo = int.Parse( row["Tipo"].ToString());
                usuario1.Nombre = row["Nombre"].ToString();
                usuario1.Contrasena = row["Contrasena"].ToString();
            }
            return usuario1;
        }

    }
}
