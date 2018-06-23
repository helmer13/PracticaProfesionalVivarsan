using Entidad;
using Logica;
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

namespace PracticaProfesionalVivarsan.Paginas
{
    /// <summary>
    /// Interaction logic for PUsuario.xaml
    /// </summary>
    public partial class PUsuario : Page
    {
        public PUsuario()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }

        private void dataGridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Usuario c = (Usuario)dataGridUsuarios.SelectedCells[0].Item;
            txtIndentificacion.Text = c.Id;
            txtNombre.Text = c.Nombre;
            txtContrasena.Password = c.Contrasena;
            txtUsuarioGeneral.Text = c.UsuarioGeneral;
            cboTipo.Text = c.Tipo;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = new Usuario();
            UsuarioLogica logica = new UsuarioLogica();
            Usuario usuarioGlobal = new Usuario();

            if (validaciones() == true)
            {
                txtTextBlockDialogo.Text = "Debe ingresar todos los datos solicitados.";
                dialogo.IsOpen = true;
                return;
            }
            else
            {
                usu.Id = txtIndentificacion.Text;
                usu.Nombre = txtNombre.Text;
                usu.UsuarioGeneral = txtUsuarioGeneral.Text;
                usu.Contrasena = txtContrasena.Password;
                usu.Tipo = cboTipo.Text;

                //usuarioLogueado
                usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
                usu.Empresa = usuarioGlobal.Empresa;
                logica.InsertarActualizarUsuario(usu);
                Refrescar();
                txtTextBlockDialogo.Text = "Registro procesado";
                dialogo.IsOpen = true;
            }
            
        }
        public Boolean validaciones()
        {
            if (string.IsNullOrEmpty(txtIndentificacion.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUsuarioGeneral.Text) || string.IsNullOrEmpty(txtContrasena.Password))
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
            txtIndentificacion.Text = "";
            txtNombre.Text = "";
            txtUsuarioGeneral.Text = "";
            txtContrasena.Password = "";
            cboTipo.Text = "Administrador";
        }

        private void Refrescar()
        {
            UsuarioLogica logica = new UsuarioLogica();
            List<Usuario> lista = new List<Usuario>();
            lista = logica.obtenerUsuarios();
            dataGridUsuarios.ItemsSource = lista;
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockAyuda.Text = string.Format(" En el campo Identificación, se debe digitar la cédula del usuario a registrar.  \n En el campo Nombre, se debe digitar el nombre completo del usuario." +
                "\n En el campo Nombre de Usuario, se debe digitar el usuario que va utilizar el usuario para acceder al sistema. \n En el campo Contraseña, se debe digitar la contraseña temporal que va usar el usuario para acceder al sistema." +
                "\n En el combo box Tipo, se debe seleccionar el perfil del usuario.");
            ayuda.IsOpen = true;
        }
    }
}
