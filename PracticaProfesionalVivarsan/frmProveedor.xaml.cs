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
namespace PracticaProfesionalVivarsan
{
    /// <summary>
    /// Interaction logic for frmProveedor.xaml
    /// </summary>
    public partial class frmProveedor : Window
    {
        public frmProveedor()
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
            MessageBox.Show("Correcto.", "Advertencia");
           
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtID.Text="";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
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
    }
}
