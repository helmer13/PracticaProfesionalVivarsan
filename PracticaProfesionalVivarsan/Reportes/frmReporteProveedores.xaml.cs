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
    /// Interaction logic for frmReporteProveedores.xaml
    /// </summary>
    public partial class frmReporteProveedores : Window
    {
        public frmReporteProveedores()
        {
            InitializeComponent();
            Uri iconUri = new Uri("pack://application:,,,/PracticaProfesionalVivarsan;component/imagenes/Background White.png");
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProveedorLogica log = new ProveedorLogica();
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            DataTable dt = log.ReporteProveedores();
            ReportProveedores.Reset();
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportProveedores.LocalReport.DataSources.Add(rd);

            ReportParameter ReportParameter1 = new ReportParameter();
            ReportParameter1.Name = "paramUsuario";
            ReportParameter1.Values.Add(usuario.Nombre);

            ReportProveedores.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteProveedores.rdlc";
            ReportProveedores.LocalReport.SetParameters(ReportParameter1);
            ReportProveedores.RefreshReport();
        }
    }
}
