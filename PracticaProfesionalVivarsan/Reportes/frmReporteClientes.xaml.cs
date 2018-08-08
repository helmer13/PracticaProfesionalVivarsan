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
    /// Interaction logic for frmReporteClientes.xaml
    /// </summary>
    public partial class frmReporteClientes : Window
    {
        public frmReporteClientes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClienteLogica log = new ClienteLogica();
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            DataTable dt = log.ReporteClientes();
            ReportClientes.Reset();
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportClientes.LocalReport.DataSources.Add(rd);

            ReportParameter ReportParameter1 = new ReportParameter();
            ReportParameter1.Name = "paramUsuario";
            ReportParameter1.Values.Add(usuario.Nombre);

            ReportClientes.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteClientes.rdlc";
            ReportClientes.LocalReport.SetParameters(ReportParameter1);
            ReportClientes.RefreshReport();
        }
    }
}
