using Entidad;
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
    /// Interaction logic for frmCaja.xaml
    /// </summary>
    public partial class frmCaja : Window
    {
        public frmCaja()
        {
            InitializeComponent();
        }

        public void GenerarReporte(DataTable dt)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            ReporteCaja.Reset();
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReporteCaja.LocalReport.DataSources.Add(rd);



            ReporteCaja.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteCajaCierre.rdlc";

            ReporteCaja.RefreshReport();
        }
    }
}
