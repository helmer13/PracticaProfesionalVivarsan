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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Entidad;
using Logica;
namespace PracticaProfesionalVivarsan
{
    /// <summary>
    /// Interaction logic for frmCliente.xaml
    /// </summary>
    public partial class frmCliente : Window
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            ClienteLogica logica = new ClienteLogica();

            cliente.Id = txtid.Text;
            if (cliente.Id == "")
            {
                cliente.Id = Guid.NewGuid().ToString();
            }
          

          //  cliente.Id = Guid.NewGuid().ToString();
            cliente.Indentificacion = txtIndentificacion.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.Direccion = txtDireccion.Text;
            cliente.Correo = txtCorreo.Text;
            cliente.Telefono =int.Parse( txtTelefono.Text);

            logica.InsertarActialiarCliente(cliente);
            MessageBox.Show("Correcto.", "Advertencia");
            Refrescar();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Cliente c = (Cliente)dataGrid.SelectedCells[0].Item;
            txtCorreo.Text = c.Correo;
            txtid.Text = c.Id;
            txtDireccion.Text = c.Direccion;
            txtIndentificacion.Text = c.Indentificacion;
            txtNombre.Text = c.Nombre;
            txtTelefono.Text = c.Telefono.ToString();
            
        }

        private void Refrescar()
        {
            ClienteLogica logica = new ClienteLogica();
            List<Cliente> lista = new List<Cliente>();
            lista = logica.obtenerClientes();
            dataGrid.ItemsSource = lista;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {

            txtCorreo.Text ="";
            txtid.Text = "";
            txtDireccion.Text = "";
            txtIndentificacion.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }
    }
}
