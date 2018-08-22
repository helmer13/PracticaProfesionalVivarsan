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
    /// Interaction logic for frmReporteVentasCliente.xaml
    /// </summary>
    public partial class frmReporteVentasCliente : Window
    {
        private string error = "";
        public frmReporteVentasCliente()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                rbIndentificacion.IsChecked = true;
                fechaInicio.SelectedDate = DateTime.Now;
                fechaFin.SelectedDate = DateTime.Now;
                Refrescar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Cliente m = (Cliente)dataGrid.SelectedCells[0].Item;
                txtId.Text = m.Id;
               

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;

            List<Cliente> lista = new List<Cliente>();
            List<Cliente> listaAxiliar = new List<Cliente>();
            lista = (List<Cliente>)dataGrid.ItemsSource;

            if (filter == "")
            {
                Refrescar();
            }
            else
            {


                if (rbIndentificacion.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Indentificacion.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
                if (rbNombre.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
                if (rbCorreo.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Correo.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
            }
        }

        private void Refrescar()
        {
            try
            {
                ClienteLogica logica = new ClienteLogica();
                List<Cliente> lista = new List<Cliente>();
                lista = logica.obtenerClientes();
                dataGrid.ItemsSource = lista;
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
                frmVentasCliente frm = new frmVentasCliente();
                FacturaVentasLogica log = new FacturaVentasLogica();
                ClienteLogica pl = new ClienteLogica();
                Cliente c = pl.obtenerCliente(txtId.Text);
                DateTime inicio = fechaInicio.SelectedDate.Value;
                DateTime fin = fechaFin.SelectedDate.Value;
                frm.GenerarReporte(c.Nombre, log.ReporteTotalVentasCl(inicio, fin, c.Id), inicio, fin);
                frm.Show();
            }
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
