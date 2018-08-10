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
using Logica;
using Microsoft.Reporting.WinForms;
using System.Data;
using Entidad;

namespace PracticaProfesionalVivarsan.Reportes
{
    /// <summary>
    /// Interaction logic for frmReporteTotalVentas.xaml
    /// </summary>
    public partial class frmReporteTotalVentas : Window
    {
        private string error = "";
        public frmReporteTotalVentas()
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
                FacturaVentasLogica pl = new FacturaVentasLogica();
                Usuario usuario = new Usuario();
                usuario = (Usuario)App.Current.Properties["usuarioSesion"];
                DataTable dt = pl.ReporteTotalVentas(fechaInicio.SelectedDate.Value, fechaFin.SelectedDate.Value);

                ReporteTotalVentas.Reset();
                ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                ReporteTotalVentas.LocalReport.DataSources.Add(rd);

                ReportParameter ReportParameter1 = new ReportParameter();
                ReportParameter1.Name = "paramUsuario";
                ReportParameter1.Values.Add(usuario.Nombre);

                ReporteTotalVentas.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteTotalVentas.rdlc";
                ReporteTotalVentas.LocalReport.SetParameters(ReportParameter1);
                ReporteTotalVentas.RefreshReport();
            }
        }
    }
}
