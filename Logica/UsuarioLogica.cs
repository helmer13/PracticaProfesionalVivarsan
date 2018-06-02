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

        public List<Usuario> obtenerUsuarios()
        {
            Usuario usuario1 = new Usuario();
           
            DataSet ds = AccesoDatos.UsuarioDato.seleccionarUsuarios();
            List<Usuario> resultado = new List<Usuario>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                usuario1.Id = row["ID"].ToString();
                usuario1.UsuarioGeneral = row["Usuario"].ToString();
                usuario1.Tipo = row["Tipo"].ToString();
                usuario1.Nombre = row["Nombre"].ToString();
                usuario1.Contrasena = row["Contrasena"].ToString();

                resultado.Add(usuario1);
            }
            return resultado;
        }

        public Usuario seleccionarUsuario(String usuario, String contrasenna)
        {
            Usuario usuario1 = new Usuario();
            DataSet ds = AccesoDatos.UsuarioDato.seleccionarUsuario(usuario, contrasenna);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                usuario1.Id = row["ID"].ToString();
                usuario1.UsuarioGeneral = row["Usuario"].ToString();
                usuario1.Tipo = row["Tipo"].ToString();
                usuario1.Nombre = row["Nombre"].ToString();
                usuario1.Contrasena = row["Contrasena"].ToString();
            }
            return usuario1;
        }
        public void InsertarActualizarUsuario(Usuario usu)
        {
            AccesoDatos.UsuarioDato.InsertarActualizarUsuario(usu.Id, usu.Nombre, usu.UsuarioGeneral, usu.Contrasena, usu.Tipo);
        }
    }
}
