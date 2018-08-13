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
    /// Interaction logic for frmVentasCliente.xaml
    /// </summary>
    public partial class frmVentasCliente : Window
    {
        public frmVentasCliente()
        {
            InitializeComponent();
        }

        public void GenerarReporte(string nombre, DataTable dt, DateTime f1, DateTime f2)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            ReporteVC.Reset();
            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReporteVC.LocalReport.DataSources.Add(rd);

            //Array que contendrá los parámetros
            ReportParameter[] parameters = new ReportParameter[4];
            //Establecemos el valor de los parámetros
            parameters[0] = new ReportParameter("paramUsuario", usuario.Nombre);
            parameters[1] = new ReportParameter("paramCliente", nombre);
            parameters[2] = new ReportParameter("fechaInicio", f1.ToString());
            parameters[3] = new ReportParameter("fechaFin", f2.ToString());

            ReporteVC.LocalReport.ReportEmbeddedResource = "PracticaProfesionalVivarsan.Reportes.ReporteVentasCliente.rdlc";
            ReporteVC.LocalReport.SetParameters(parameters);
            ReporteVC.RefreshReport();
        }
    }
}
