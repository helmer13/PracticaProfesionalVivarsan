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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

          //  Uri iconUri = new Uri("C:/Users/PC/Desktop/key_102281.ico", UriKind.RelativeOrAbsolute);
           // this.Icon = BitmapFrame.Create(iconUri);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            string nombreUsuario;
            string contrasena;

            UsuarioLogica usuarioLogica = new UsuarioLogica();

            if (txtUsuario.Text == "" || txtUsuario.Text == null)
            {
                MessageBox.Show("Debe ingresar el nombre de usuario.", "Error");
            }
            else
            {
                if (txtContrasenna.Password == "" || txtContrasenna.Password == null)
                {
                    MessageBox.Show("Debe ingresar la contraseña.","Error");
                }
                else
                {
                    nombreUsuario = txtUsuario.Text;
                    contrasena = txtContrasenna.Password;
                    usuario= usuarioLogica.seleccionarUsuario(nombreUsuario, contrasena);

                    if (!nombreUsuario.Equals(usuario.UsuarioGeneral) || !contrasena.Equals(usuario.Contrasena))
                    {
                        MessageBox.Show("Nombre de usuario o contraseña son incorrectos.", "Advertencia");
                    }
                    else
                    {
                        //Principal prin = Principal();
                        //prin.Show();

                        //Quitar esto despues
                        MessageBox.Show("Correcto.", "Advertencia");
                    }
                }
            }           

        }
    }
}
