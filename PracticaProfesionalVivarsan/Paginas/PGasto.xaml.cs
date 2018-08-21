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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticaProfesionalVivarsan.Paginas
{
    /// <summary>
    /// Interaction logic for PGasto.xaml
    /// </summary>
    public partial class PGasto : Page
    {
        public PGasto()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Gasto gasto = new Gasto();
                GastoLogica logica = new GastoLogica();
                Usuario usuario = new Usuario();

                if (validaciones() == true)
                {
                    txtTextBlockDialogo.Text = "Debe completar todos los campos solicitados";
                    dialogo.IsOpen = true;
                    return;
                }
                else
                {
                    usuario = (Usuario)App.Current.Properties["usuarioSesion"];
                    gasto.Id = logica.seleccionarGastoTodo().Count + 1;
                    gasto.Tipo = txtTipo.Text;
                    gasto.Descripcion = txtDescripcion.Text;
                    gasto.Usuario = usuario;
                    gasto.Fecha = fecha.SelectedDate.Value;
                    gasto.Monto = Convert.ToDouble(txtMonto.Text);

                    logica.InsertarGasto(gasto);
                    txtTextBlockDialogo.Text = "Registro procesado";
                    dialogo.IsOpen = true;
                    Refrescar();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ",")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
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

        public Boolean validaciones()
        {
            if (string.IsNullOrEmpty(txtTipo.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtMonto.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Refrescar()
        {
            txtTipo.Text = "";
            txtDescripcion.Text = "";
            txtMonto.Text = "";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fecha.SelectedDate = DateTime.Now;
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
