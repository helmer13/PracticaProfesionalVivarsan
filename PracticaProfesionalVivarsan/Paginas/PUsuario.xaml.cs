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

            //usu.Id = txtid.Text;
            //if (usu.Id == "")
            //{
            //    usu.Id = Guid.NewGuid().ToString();
            //}


            //  cliente.Id = Guid.NewGuid().ToString();
            usu.Id = txtIndentificacion.Text;
            usu.Nombre = txtNombre.Text;
            usu.UsuarioGeneral = txtUsuarioGeneral.Text;
            usu.Contrasena = txtContrasena.Password;
            usu.Tipo = cboTipo.Text;

            logica.InsertarActualizarUsuario(usu);
            Refrescar();
            txtTextBlockDialogo.Text = "Registro procesado";
            dialogo.IsOpen = true;
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
    }
}
