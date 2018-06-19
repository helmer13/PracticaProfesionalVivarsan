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


            //  cliente.Id = Guid.NewGuid().ToString();
            proveedor.NombreContacto = txtNombreContacto.Text;
            proveedor.NombreProveedor = txtNombreProveedor.Text;
            proveedor.Correo = txtCorreo.Text;
            proveedor.Telefono = int.Parse(txtTelefono.Text);

            logica.InsertarActialiarProveedor(proveedor);
            Refrescar();
            txtTextBlockDialogo.Text = "Registro procesado";
            dialogo.IsOpen = true;

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text = "";
            txtNombreContacto.Text = "";
            txtNombreProveedor.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockAyuda.Text = string.Format("My Text \n Your Text");
            ayuda.IsOpen = true;
        }
    }
}
