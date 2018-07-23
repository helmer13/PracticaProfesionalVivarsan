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
    /// Interaction logic for PAbrirCaja.xaml
    /// </summary>
    public partial class PAbrirCaja : Page
    {
        public PAbrirCaja()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fecha.SelectedDate = DateTime.Now;

            Usuario usuario = new Usuario();
        

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            txtUsuario.Text = usuario.Nombre;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Caja caja = new Caja();
            Usuario usuario = new Usuario();
            CajaLogica logica = new CajaLogica();

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];

            caja.Id = Guid.NewGuid().ToString();
            caja.MontoApertura =Convert.ToDouble( txtMonto.Text);
            caja.FechaApertura = fecha.SelectedDate.Value;
            caja.Estado = "ABIERTA";
            caja.Usuario = usuario;

            logica.InsertarInsertaAbirCaja(caja);

            MessageBox.Show("SSe guardo con exito");
        }
    }
}
