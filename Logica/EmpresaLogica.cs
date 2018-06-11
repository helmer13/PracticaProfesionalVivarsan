using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entidad;
using AcessoDatos;
using System.Data;
namespace Logica
{
  public  class EmpresaLogica
    {
        public Empresa obtenerEmpresa(int idEmpresa)
        {
            Empresa empresa = new Empresa();
           
            DataSet ds = AccesoDatos.EmpresaDato.seleccionarEmpresa(idEmpresa);


            foreach (DataRow row in ds.Tables[0].Rows)
            {
                empresa.IdEmpresa = Convert.ToInt32( row["ID"].ToString());
                empresa.NombreEmpresa = row["NombreEmpresa"].ToString();
            }

            return empresa;
        }



    }
}
