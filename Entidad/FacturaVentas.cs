using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class FacturaVentas
    {
        int id;
        Usuario usuario;
        Cliente cliente;
        DateTime fecha;
        double total;
        string tipoPago;

        List<LineaDetalleVentas> lineaDetalleVentas = new List<LineaDetalleVentas>();

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

        public Cliente Cliente
        {
            get
            {
                return cliente;
            }

            set
            {
                cliente = value;
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

        public List<LineaDetalleVentas> LineaDetalleVentas
        {
            get
            {
                return lineaDetalleVentas;
            }

            set
            {
                lineaDetalleVentas = value;
            }
        }
    }
}
