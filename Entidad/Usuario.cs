using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   public class Usuario
    {
        string id;
        string nombre;
        string usuarioGeneral;
        string contrasena;
        string tipo;
        Empresa empresa;
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

      

        public string Contrasena
        {
            get
            {
                return contrasena;
            }

            set
            {
                contrasena = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public string UsuarioGeneral
        {
            get
            {
                return usuarioGeneral;
            }

            set
            {
                usuarioGeneral = value;
            }
        }

        public Empresa Empresa
        {
            get
            {
                return empresa;
            }

            set
            {
                empresa = value;
            }
        }
    }
}
