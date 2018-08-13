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
    /// Interaction logic for frmReporteGastos.xaml
    /// </summary>
    public partial class frmReporteGastos : Window
    {
        private string error = "";
        public frmReporteGastos()
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
                GastoLogica log = new GastoLogica();
                Usuario usuario = new Usuario();
                usuario = (Usuario)App.Current.Properties["usuarioSesion"];
                DataTable dt = log.ReporteGastos(fechaInicio.SelectedDate.Value, fechaFin.SelectedDate.Value);
                ReportGastos.Reset();
                ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                ReportGastos.LocalReport.DataSources.Add(rd);

                //Array que contendrá los parámetros
                ReportParameter[] parameters = new ReportParameter[3];
                //Establecemos el valor de los parámetros
                parameters[0] = new ReportParameter("paramUsuario", usuario.Nombre);
                parameters[1] = new ReportParameter("fechaInicio", fechaInicio.SelectedDate.Value.ToString());
                parameters[2] = new ReportParameter("fechaFin", fechaFin.SelectedDate.Value.ToString());

                ReportGastos.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteGastos.rdlc";
                ReportGastos.LocalReport.SetParameters(parameters);
                ReportGastos.RefreshReport();
            }
        }
    }
}
