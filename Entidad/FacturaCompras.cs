using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
  public  class FacturaCompras
    {
        int id;
        Usuario usuario;
        string idProveedor;
        DateTime fecha;
        double total;
        string tipoPago;

        List<LineaDetalleCompras> lineasDetalleCompras = new List<LineaDetalleCompras>();

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

        public Usuario  Usuario
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

        public string IdProveedor
        {
            get
            {
                return idProveedor;
            }

            set
            {
                idProveedor = value;
            }
        }

        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {
                fecha = value;
            }
        }

        public double Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public string TipoPago
        {
            get
            {
                return tipoPago;
            }

            set
            {
                tipoPago = value;
            }
        }

        public List<LineaDetalleCompras> LineasDetalleCompras
        {
            get
            {
                return lineasDetalleCompras;
            }

            set
            {
                lineasDetalleCompras = value;
            }
        }
    }
}
