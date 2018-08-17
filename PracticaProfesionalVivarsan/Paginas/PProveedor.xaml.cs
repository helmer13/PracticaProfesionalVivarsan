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
namespace PracticaProfesionalVivarsan.Paginas
{
    /// <summary>
    /// Interaction logic for PProveedor.xaml
    /// </summary>
    public partial class PProveedor : Page
    {
        public PProveedor()
        {
            InitializeComponent();
        }
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor proveedor = new Proveedor();
            ProveedorLogica logica = new ProveedorLogica();

            proveedor.Id = txtID.Text;
            if (proveedor.Id == "")
            {
                proveedor.Id = Guid.NewGuid().ToString();
            }

            if (validaciones() == true)
            {
                txtTextBlockDialogo.Text = "Debe ingresar todos los datos solicitados.";
                dialogo.IsOpen = true;
                return;
            }
            else
            {
                //  cliente.Id = Guid.NewGuid().ToString();
                proveedor.NombreContacto = txtNombreContacto.Text;
                proveedor.NombreProveedor = txtNombreProveedor.Text;
                proveedor.Correo = txtCorreo.Text;
                proveedor.Telefono = int.Parse(txtTelefono.Text);

                logica.InsertarActialiarProveedor(proveedor);
                Refrescar();
                txtTextBlockDialogo.Text = "Registro procesado";
                dialogo.IsOpen = true;
                btnVolver_Click(sender, e);
            }
            

        }

        public Boolean validaciones()
        {
            if (string.IsNullOrEmpty(txtNombreContacto.Text) || string.IsNullOrEmpty(txtNombreProveedor.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtNombreProveedor.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            gridForm.Visibility = Visibility.Visible;
            gridTabla.Visibility = Visibility.Collapsed;
        }
        private void Refrescar()
        {
            ProveedorLogica logica = new ProveedorLogica();
            List<Proveedor> lista = new List<Proveedor>();
            lista = logica.obtenerProveedores();
            dataGrid.ItemsSource = lista;
        }


        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proveedor p = (Proveedor)dataGrid.SelectedCells[0].Item;
            txtCorreo.Text = p.Correo;
            txtID.Text = p.Id;
            txtNombreContacto.Text = p.NombreContacto;
            txtNombreProveedor.Text = p.NombreProveedor;
            txtTelefono.Text = p.Telefono.ToString();

            gridForm.Visibility = Visibility.Visible;
            gridTabla.Visibility = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            rbProveedor.IsChecked = true;
            Refrescar();
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockAyuda.Text = string.Format(" En el campo Nombre Proveedor, se debe digitar el nombre del proveedor a registrar.  \n En el campo Nombre Contacto, se debe digitar el nombre de la persona encargada de la empresa proveedora." +
                "\n En el campo Correo, se debe digitar el correo electrónico del proveedor. \n En el campo Teléfono, se debe digitar el numero de teléfono del proveedor.");
            ayuda.IsOpen = true;
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
                    listaAxiliar = lista.Where(p => p.NombreProveedor.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
                if (rbCorreo.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Correo.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGrid.ItemsSource = listaAxiliar;
                }
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = string.Empty;
            txtNombreContacto.Text = string.Empty;
            txtNombreProveedor.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            gridForm.Visibility = Visibility.Collapsed;
            gridTabla.Visibility = Visibility.Visible;
        }
    }
}
