using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Modelo
    {
        int id;
        string descripcion;
        int anno;
        Marca marca;
        string nombreCompleto;

        public int Id
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

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }



        public int Anno
        {
            get
            {
                return anno;
            }

            set
            {
                anno = value;
            }
        }

        public Marca Marca
        {
            get
            {
                return marca;
            }

            set
            {
                marca = value;
            }
        }

        public string NombreCompleto
        {
            get
            {
                return descripcion + " " + anno;
            }

            set
            {
                descripcion = value;
            }
        }
        public override string ToString()
        {
            return Descripcion + " " + Anno;
        }
    }
}
