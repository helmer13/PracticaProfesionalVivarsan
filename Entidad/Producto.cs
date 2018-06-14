using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public   class Producto
    {
        string idProducto;
        string nombre;
        Modelo modelo;
        decimal precioCompra;
        decimal precioVenta;

        public string IdProducto
        {
            get
            {
                return idProducto;
            }

            set
            {
                idProducto = value;
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

        public Modelo Modelo
        {
            get
            {
                return modelo;
            }

            set
            {
                modelo = value;
            }
        }

        public decimal PrecioCompra
        {
            get
            {
                return precioCompra;
            }

            set
            {
                precioCompra = value;
            }
        }

        public decimal PrecioVenta
        {
            get
            {
                return precioVenta;
            }

            set
            {
                precioVenta = value;
            }
        }
    }
}
