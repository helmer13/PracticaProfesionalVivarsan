using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
  public  class Empresa
    {
        int idEmpresa;
        string nombreEmpresa;

        public int IdEmpresa
        {
            get
            {
                return idEmpresa;
            }

            set
            {
                idEmpresa = value;
            }
        }

        public string NombreEmpresa
        {
            get
            {
                return nombreEmpresa;
            }

            set
            {
                nombreEmpresa = value;
            }
        }
    }
}
