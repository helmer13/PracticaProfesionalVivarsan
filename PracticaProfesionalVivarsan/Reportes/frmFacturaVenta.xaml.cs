using Entidad;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaProfesionalVivarsan.Reportes
{
    /// <summary>
    /// Interaction logic for frmFacturaVenta.xaml
    /// </summary>
    public partial class frmFacturaVenta : Window
    {
        public frmFacturaVenta()
        {
            InitializeComponent();
        }
        public void GenerarReporte(double impuesto, FacturaVentas factura, DataTable dt2)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            ReporteFactura.Reset();
            ReportDataSource rd2 = new ReportDataSource("DataSet2", dt2);
            ReporteFactura.LocalReport.DataSources.Add(rd2);
            //Array que contendrá los parámetros
            ReportParameter[] parameters = new ReportParameter[6];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("paramImpuesto", impuesto.ToString());
            parameters[1] = new ReportParameter("paramCliente", factura.Cliente.Nombre);
            parameters[2] = new ReportParameter("paramFecha", factura.Fecha.ToString());            
            parameters[3] = new ReportParameter("paramTipo", factura.TipoPago);
            parameters[4] = new ReportParameter("paramTotal", factura.Total.ToString());
            parameters[5] = new ReportParameter("paramIdentificacion", factura.Cliente.Indentificacion.ToString());

            ReporteFactura.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteFactura.rdlc";
            ReporteFactura.LocalReport.SetParameters(parameters);
            ReporteFactura.RefreshReport();
        }
    }
}
