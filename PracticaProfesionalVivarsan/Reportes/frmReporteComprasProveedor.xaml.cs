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
        public frmReporteComprasProveedor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rbProveedor.IsChecked = true;
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

        }

        private void Refrescar()
        {
            ProveedorLogica logica = new ProveedorLogica();
            List<Proveedor> lista = new List<Proveedor>();
            lista = logica.obtenerProveedores();
            dataGrid.ItemsSource = lista;
        }
    }
}
