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
        private string error = "";
        public frmReporteTotalCompras()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        private Boolean Validaciones()
        {
            Boolean bandera = false;
            if (string.IsNullOrEmpty(fechaInicio.Text))
            {
                error = "Debe digitar la fecha de inicio.";
                bandera = true;
            }
            if (string.IsNullOrEmpty(fechaFin.Text))
            {
                error = "Debe digitar la fecha final.";
                bandera = true;
            }
            return bandera;
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (Validaciones() == true)
            {
                txtTextBlockDialogo.Text = error;
                dialogoMENS.IsOpen = true;
                return;
            }
            else
            {
                FacturaComprasLogica pl = new FacturaComprasLogica();
                Usuario usuario = new Usuario();
                usuario = (Usuario)App.Current.Properties["usuarioSesion"];
                DataTable dt = pl.ReporteTotalCompras(fechaInicio.SelectedDate.Value, fechaFin.SelectedDate.Value);

                ReporteTotalCompras.Reset();
                ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                ReporteTotalCompras.LocalReport.DataSources.Add(rd);

                ReportParameter ReportParameter1 = new ReportParameter();
                ReportParameter1.Name = "paramUsuario";
                ReportParameter1.Values.Add(usuario.Nombre);

                ReporteTotalCompras.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteTotalCompras.rdlc";
                ReporteTotalCompras.LocalReport.SetParameters(ReportParameter1);
                ReporteTotalCompras.RefreshReport();
            }
        }
    }
}
