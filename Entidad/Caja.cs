using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
  public  class Caja
    {

        string id;
        DateTime fechaApertura;
        DateTime fechaCierre;
        double montoApertura;
        double montoCierre;
        string descripcion;
        string estado;
        Usuario usuario;

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

        public DateTime FechaApertura
        {
            get
            {
                return fechaApertura;
            }

            set
            {
                fechaApertura = value;
            }
        }

        public DateTime FechaCierre
        {
            get
            {
                return fechaCierre;
            }

            set
            {
                fechaCierre = value;
            }
        }

        public double MontoApertura
        {
            get
            {
                return montoApertura;
            }

            set
            {
                montoApertura = value;
            }
        }

        public double MontoCierre
        {
            get
            {
                return montoCierre;
            }

            set
            {
                montoCierre = value;
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

        public string Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public Usuario Usuario
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }
    }
}
