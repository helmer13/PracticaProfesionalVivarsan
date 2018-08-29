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
            Uri iconUri = new Uri("pack://application:,,,/PracticaProfesionalVivarsan;component/imagenes/Background White.png");
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fechaInicio.SelectedDate = DateTime.Now.Date;
            fechaFin.SelectedDate = DateTime.Now.Date;
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

                //Array que contendrá los parámetros
                ReportParameter[] parameters = new ReportParameter[3];
                //Establecemos el valor de los parámetros
                parameters[0] = new ReportParameter("paramUsuario", usuario.Nombre);
                parameters[1] = new ReportParameter("fechaInicio", fechaInicio.SelectedDate.Value.ToString());
                parameters[2] = new ReportParameter("fechaFin", fechaFin.SelectedDate.Value.ToString());

                ReporteTotalVentas.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteTotalVentas.rdlc";
                ReporteTotalVentas.LocalReport.SetParameters(parameters);
                ReporteTotalVentas.RefreshReport();
            }
        }
    }
}
