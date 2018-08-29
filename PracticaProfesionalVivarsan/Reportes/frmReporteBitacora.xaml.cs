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
    /// Interaction logic for frmReporteBitacora.xaml
    /// </summary>
    public partial class frmReporteBitacora : Window
    {
        private string error = "";
        public frmReporteBitacora()
        {
            InitializeComponent();
            Uri iconUri = new Uri("pack://application:,,,/PracticaProfesionalVivarsan;component/imagenes/Background White.png");
            this.Icon = BitmapFrame.Create(iconUri);
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
                UsuarioLogica log = new UsuarioLogica();
                Usuario usuario = new Usuario();
                usuario = (Usuario)App.Current.Properties["usuarioSesion"];
                DataTable dt = log.ReporteBitacora(fechaInicio.SelectedDate.Value);
                ReportGastos.Reset();
                ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                ReportGastos.LocalReport.DataSources.Add(rd);

                //Array que contendrá los parámetros
                ReportParameter[] parameters = new ReportParameter[1];
                //Establecemos el valor de los parámetros
                parameters[0] = new ReportParameter("paramUsuario", usuario.Nombre);
                //parameters[1] = new ReportParameter("fechaInicio", fechaInicio.SelectedDate.Value.ToString());

                ReportGastos.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteBitacora.rdlc";
                ReportGastos.LocalReport.SetParameters(parameters);
                ReportGastos.RefreshReport();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fechaInicio.SelectedDate = DateTime.Now.Date;
        }
        private Boolean Validaciones()
        {
            Boolean bandera = false;
            if (string.IsNullOrEmpty(fechaInicio.Text))
            {
                error = "Debe digitar la fecha.";
                bandera = true;
            }           
            return bandera;
        }
    }
}
