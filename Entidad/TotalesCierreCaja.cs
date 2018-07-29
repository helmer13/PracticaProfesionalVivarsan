using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
   public class TotalesCierreCaja
    {
        string id;
        decimal gastos;
        decimal contado;
        decimal tarjeta;
        decimal mobtoApertura;
        DateTime fechaApertura;
        public decimal Gastos
        {
            get
            {
                return gastos;
            }

            set
            {
                gastos = value;
            }
        }

        public decimal Contado
        {
            get
            {
                return contado;
            }

            set
            {
                contado = value;
            }
        }

        public decimal Tarjeta
        {
            get
            {
                return tarjeta;
            }

            set
            {
                tarjeta = value;
            }
        }

        public decimal MobtoApertura
        {
            get
            {
                return mobtoApertura;
            }

            set
            {
                mobtoApertura = value;
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
