using Entidad;
using Logica;
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

namespace PracticaProfesionalVivarsan
{
    /// <summary>
    /// Interaction logic for frmReporteProd.xaml
    /// </summary>
    public partial class frmReporteProd : Window
    {
        public frmReporteProd()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ProductoLogica pl = new ProductoLogica();
            DataTable dt = pl.ReporteProductos();
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            DemoReporte.Reset();
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            DemoReporte.LocalReport.DataSources.Add(rd);

            ReportParameter ReportParameter1 = new ReportParameter();
            ReportParameter1.Name = "paramUsuario";
            ReportParameter1.Values.Add(usuario.Nombre);

            DemoReporte.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteProductos.rdlc";
            DemoReporte.LocalReport.SetParameters(ReportParameter1);
            DemoReporte.RefreshReport();

        }

    }
}
