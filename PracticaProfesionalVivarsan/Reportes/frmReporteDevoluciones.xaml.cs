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

namespace PracticaProfesionalVivarsan.Reportes
{
    /// <summary>
    /// Interaction logic for frmReporteDevoluciones.xaml
    /// </summary>
    public partial class frmReporteDevoluciones : Window
    {
        private string error = "";
        public frmReporteDevoluciones()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
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
                DataTable dt = pl.ReporteDevoluciones(fechaInicio.SelectedDate.Value, fechaFin.SelectedDate.Value);

                ReporteDevoluciones.Reset();
                ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                ReporteDevoluciones.LocalReport.DataSources.Add(rd);

                DateTime f1 = fechaInicio.SelectedDate.Value;
                DateTime f2 = fechaFin.SelectedDate.Value;
                //Array que contendrá los parámetros
                ReportParameter[] parameters = new ReportParameter[3];
                //Establecemos el valor de los parámetros
                parameters[0] = new ReportParameter("paramUsuario", usuario.Nombre);
                parameters[1] = new ReportParameter("fechaInicio", f1.ToString());
                parameters[2] = new ReportParameter("fechaFin", f2.ToString());

                ReporteDevoluciones.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteDevoluciones.rdlc";
                ReporteDevoluciones.LocalReport.SetParameters(parameters);
                ReporteDevoluciones.RefreshReport();
            }
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
    }
}
