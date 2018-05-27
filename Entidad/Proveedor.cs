using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   public class Proveedor
    {
        private string id;
        private string nombreProveedor;
        private string nombreContacto;
        private string correo;
        private int telefono;

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

        public string NombreProveedor
        {
            get
            {
                return nombreProveedor;
            }

            set
            {
                nombreProveedor = value;
            }
        }

        public string NombreContacto
        {
            get
            {
                return nombreContacto;
            }

            set
            {
                nombreContacto = value;
            }
        }

        public string Correo
        {
            get
            {
                return correo;
            }

            set
            {
                correo = value;
            }
        }

        public int Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }
    }
}
