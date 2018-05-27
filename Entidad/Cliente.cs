using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
  public  class Cliente
    {
        string id;
        string indentificacion;
        string nombre;
        string correo;
        string direccion;
        int telefono;

     
        public string Indentificacion
        {
            get
            {
                return indentificacion;
            }

            set
            {
                indentificacion = value;
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

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
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
    }
}
