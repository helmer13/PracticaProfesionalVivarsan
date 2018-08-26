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
        private int contador = 0;
        public MainWindow()
        {
            InitializeComponent();

            //Uri iconUri = new Uri("C:/Users/PC/Desktop/Background White.png", UriKind.RelativeOrAbsolute);
            //this.Icon = BitmapFrame.Create(iconUri);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            string nombreUsuario;
            string contrasena;

            UsuarioLogica usuarioLogica = new UsuarioLogica();

            if (txtUsuario.Text == "" || txtUsuario.Text == null)
            {
                txtTextBlockDialogo.Text = "Debe ingresar el nombre de usuario";
                dialogo.IsOpen = true;

            }
            else
            {
                if (txtContrasena.Password == "" || txtContrasena.Password == null)
                {
                    txtTextBlockDialogo.Text = "Debe ingresar la contraseña";
                    dialogo.IsOpen = true;

                }
                else
                {
                    nombreUsuario = txtUsuario.Text;
                    contrasena = txtContrasena.Password;
                    usuario = usuarioLogica.seleccionarUsuario(nombreUsuario, contrasena);
                    //variable local
                    App.Current.Properties["usuarioSesion"] = usuario;

                    if (!nombreUsuario.Equals(usuario.UsuarioGeneral) || !contrasena.Equals(usuario.Contrasena))
                    {
                        contador++;
                        txtTextBlockDialogo.Text = "Nombre de usuario o contraseña son incorrectos.\nIntentos disponibles:" + (3 - contador);
                        dialogo.IsOpen = true;
                        if (contador > 3)
                        {
                            Application.Current.Shutdown();
                        }
                    }
                    else
                    {
                        frmMenu prin = new frmMenu();
                        prin.Show();
                        this.Hide();
                        txtTextBlockDialogo.Text = "Iniciaste correctamente";
                        dialogo.IsOpen = false;
                        contador = 0;

                    }
                }
            }

        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Click(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
       //     txtUsuario.Focus();
        }
    }
}
