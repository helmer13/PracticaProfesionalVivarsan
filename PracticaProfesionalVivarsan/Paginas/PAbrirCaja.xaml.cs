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
            if (string.IsNullOrEmpty(txtMonto.Text))
            {
                dialogo.IsOpen = true;
                txtTextBlockDialogo.Text = "Debe digitar el monto de la caja";
                return;
            }

            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            CajaLogica logica = new CajaLogica();
            Caja cajaValidar = new Caja();
            cajaValidar = logica.ObtenerCajaAbierta(usuario.Id);

            if (cajaValidar.Estado == "ABIERTA")
            {
                dialogo.IsOpen = true;
                txtTextBlockDialogo.Text = "La caja ya se encuentra abierta";
                return;
            }



                Caja caja = new Caja();
            caja.Id = Guid.NewGuid().ToString();
            caja.MontoApertura = Convert.ToDouble(txtMonto.Text);
            caja.FechaApertura = fecha.SelectedDate.Value;
            caja.Estado = "ABIERTA";
            caja.Usuario = usuario;

            logica.InsertarInsertaAbirCaja(caja);

            txtMonto.Text = string.Empty;

            dialogo.IsOpen = true;
            txtTextBlockDialogo.Text = "Registro procesado";
        }


        private void txtMonto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ",")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
            {
                e.Handled = true;
            }

        }

        private void txtMonto_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {


                if (!string.IsNullOrEmpty(txtMonto.Text))
                {
                    if (txtMonto.Text.Substring(0, 1) == ".")
                    {
                        var v = txtMonto.Text.Remove(0, 1);
                        txtMonto.Text = v;
                    }

                    txtMonto.Text = string.Format("{0:N2}", Convert.ToDecimal(txtMonto.Text));
                }
            }
        }


    

        private void txtMonto_LostFocus(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(txtMonto.Text))
            {
                if (txtMonto.Text.Substring(0, 1) == ".")
                {
                    var v = txtMonto.Text.Remove(0, 1);
                    txtMonto.Text = v;
                }

                txtMonto.Text = string.Format("{0:N2}", Convert.ToDecimal(txtMonto.Text));
            }


        }
    }
}

