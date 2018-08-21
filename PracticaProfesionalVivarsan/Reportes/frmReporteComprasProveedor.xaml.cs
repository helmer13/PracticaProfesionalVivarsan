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
namespace PracticaProfesionalVivarsan.Reportes
{
    /// <summary>
    /// Interaction logic for frmReporteComprasProveedor.xaml
    /// </summary>
    public partial class frmReporteComprasProveedor : Window
    {
        private string error = "";
        public frmReporteComprasProveedor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rbProveedor.IsChecked = true;
            fechaInicio.SelectedDate = DateTime.Now.Date;
            fechaFin.SelectedDate = DateTime.Now.Date;
            Refrescar();
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;

            List<Proveedor> lista = new List<Proveedor>();
            List<Proveedor> listaAxiliar = new List<Proveedor>();
            lista = (List<Proveedor>)dataGrid.ItemsSource;

            if (filter == "")
            {
                Refrescar();
            }
            else
            {
                if (rbProveedor.IsChecked.Value)
                {
                    txtBuscar.Focus();
                    listaAxiliar = lista.Where(p => p.NombreProveedor.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
                if (rbCorreo.IsChecked.Value)
                {
                    txtBuscar.Focus();
                    listaAxiliar = lista.Where(p => p.Correo.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Proveedor m = (Proveedor)dataGrid.SelectedCells[0].Item;
                txtId.Text = m.Id;


            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                frmComprasProveedor frm = new frmComprasProveedor();
                FacturaComprasLogica log = new FacturaComprasLogica();
                ProveedorLogica pl = new ProveedorLogica();
                Proveedor p = pl.obtenerProveedor(txtId.Text);
                DateTime inicio = fechaInicio.SelectedDate.Value;
                DateTime fin = fechaFin.SelectedDate.Value;
                frm.GenerarReporte(p.NombreProveedor, log.ReporteTotalComprasProv(inicio, fin, p.Id), inicio, fin);
                frm.Show();
            }
                
        }

        private void Refrescar()
        {
            ProveedorLogica logica = new ProveedorLogica();
            List<Proveedor> lista = new List<Proveedor>();
            lista = logica.obtenerProveedores();
            dataGrid.ItemsSource = lista;
            
        }

        private Boolean Validaciones()
        {
            Boolean bandera = false;
            if (string.IsNullOrEmpty(txtId.Text))
            {
                error = "Debe elegir un proveedor de la lista.";
                bandera = true;
            }
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
