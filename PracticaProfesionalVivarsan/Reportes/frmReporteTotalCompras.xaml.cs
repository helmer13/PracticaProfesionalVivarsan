using System;
using System.Collections.Generic;
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
using Entidad;
using Logica;
using Microsoft.Reporting.WinForms;
using System.Data;
namespace PracticaProfesionalVivarsan.Reportes
{
    /// <summary>
    /// Interaction logic for frmReporteTotalCompras.xaml
    /// </summary>
    public partial class frmReporteTotalCompras : Window
    {
        public frmReporteTotalCompras()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            FacturaComprasLogica pl = new FacturaComprasLogica();
            DataTable dt = pl.ReporteTotalCompras(fechaInicio.SelectedDate.Value,fechaFin.SelectedDate.Value);

            ReporteTotalCompras.Reset();
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReporteTotalCompras.LocalReport.DataSources.Add(rd);

         //   ReportParameter ReportParameter1 = new ReportParameter();
           // ReportParameter1.Name = "paramUsuario";
            //ReportParameter1.Values.Add(usuario.Nombre);

            ReporteTotalCompras.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteTotalCompras.rdlc";
           // ReporteTotalCompras.LocalReport.SetParameters(ReportParameter1);
            ReporteTotalCompras.RefreshReport();
        }
    }
}
