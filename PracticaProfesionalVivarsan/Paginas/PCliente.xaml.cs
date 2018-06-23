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
namespace PracticaProfesionalVivarsan.Paginas
{
    /// <summary>
    /// Interaction logic for PCliente.xaml
    /// </summary>
    public partial class PCliente : Page
    {
        public PCliente()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                ClienteLogica logica = new ClienteLogica();

                cliente.Id = txtid.Text;
                if (cliente.Id == "")
                {
                    cliente.Id = Guid.NewGuid().ToString();
                }


                if (validaciones() == true)
                {
                    txtTextBlockDialogo.Text = "Debe ingresar todos los datos solicitados.";
                    dialogo.IsOpen = true;
                    return;
                }
                else
                {
                    cliente.Indentificacion = txtIndentificacion.Text;
                    cliente.Nombre = txtNombre.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Correo = txtCorreo.Text;
                    cliente.Telefono = int.Parse(txtTelefono.Text);

                    logica.InsertarActialiarCliente(cliente);
                    Refrescar();
                    txtTextBlockDialogo.Text = "Registro procesado";
                    dialogo.IsOpen = true;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
          
        }
        public Boolean validaciones()
        {
            if (string.IsNullOrEmpty(txtIndentificacion.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtDireccion.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrEmpty(txtTelefono.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Cliente c = (Cliente)dataGrid.SelectedCells[0].Item;
                txtCorreo.Text = c.Correo;
                txtid.Text = c.Id;
                txtDireccion.Text = c.Direccion;
                txtIndentificacion.Text = c.Indentificacion;
                txtNombre.Text = c.Nombre;
                txtTelefono.Text = c.Telefono.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
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

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {

            txtCorreo.Text = "";
            txtid.Text = "";
            txtDireccion.Text = "";
            txtIndentificacion.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Refrescar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockAyuda.Text = string.Format(" En el campo Identificación, se debe digitar la cédula del cliente a registrar.  \n En el campo Nombre, se debe digitar el nombre completo del cliente." +
                "\n En el campo Dirección, se debe digitar la dirección del cliente. \n En el campo Correo, se debe digitar el correo electrónico del cliente. \n En el campo Teléfono, se debe digitar el numero de teléfono del cliente.");
            ayuda.IsOpen = true;
        }

        public void SoloNumeros(TextCompositionEventArgs e)
        {
            //se convierte a Ascci del la tecla presionada 
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            //verificamos que se encuentre en ese rango que son entre el 0 y el 9 
            if (ascci >= 48 && ascci <= 57)
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }
    }
}
