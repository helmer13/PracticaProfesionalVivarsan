using Entidad;
using Logica;
using PracticaProfesionalVivarsan.Reportes;
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
    /// Interaction logic for PCerrarCaja.xaml
    /// </summary>
    public partial class PCerrarCaja : Page
    {
        public PCerrarCaja()
        {
            InitializeComponent();
        }

        Caja caja;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fechaCierre.SelectedDate = DateTime.Now;

            Usuario usuario = new Usuario();
            TotalesCierreCaja totales = new TotalesCierreCaja();

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            txtUsuario.Text = usuario.Nombre;
            CajaLogica logica = new CajaLogica();

            caja = logica.ObtenerCajaAbierta(usuario.Id);

            fechaApertura.SelectedDate = caja.FechaApertura;



            totales = logica.ObtenerTotalesCierrreCaja(fechaApertura.SelectedDate.Value, fechaCierre.SelectedDate.Value, usuario.Id);
            txtefectivoSinBase.Text = totales.Contado.ToString();
           // txtbase.Text = totales.MobtoApertura.ToString();
            txtbase.Text= string.Format("{0:N2}", Convert.ToDecimal(totales.MobtoApertura));
            txtTotalGastos.Text = totales.Gastos.ToString();


            var totalEfectivoSistema = (totales.MobtoApertura + totales.Contado) - totales.Gastos;
            txtTotalEfectivoSistema.Text = totalEfectivoSistema.ToString();


            LlenarDataGrid();


        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTotalEfectivoCaja.Text))
                {
                    dialogo.IsOpen = true;
                    txtTextBlockDialogo.Text = "Debe de insertar el total del efectivo de la caja";
                    txtTotalEfectivoCaja.Focus();
                    return;
                }

                Usuario usuario = new Usuario();
                usuario = (Usuario)App.Current.Properties["usuarioSesion"];
                CajaLogica logica = new CajaLogica();
                Caja caja = new Caja();
                caja = logica.ObtenerCajaAbierta(usuario.Id);

                if (caja.Estado == null)
                {
                    dialogo.IsOpen = true;
                    txtTextBlockDialogo.Text = "La caja ya se encuentra cerrada";
                    return;
                }


                Caja cajaCerrar = new Caja();


                TotalesCierreCaja totales = new TotalesCierreCaja();


                txtUsuario.Text = usuario.Nombre;


                totales = logica.ObtenerTotalesCierrreCaja(fechaCierre.SelectedDate.Value, fechaCierre.SelectedDate.Value, usuario.Id);

                cajaCerrar.Id = totales.Id;
                cajaCerrar.FechaCierre = fechaCierre.SelectedDate.Value;
                cajaCerrar.MontoCierre = Convert.ToDouble(txtTotalEfectivoCaja.Text);
                cajaCerrar.Usuario = usuario;
                cajaCerrar.Estado = "CERRADO";
                cajaCerrar.FechaApertura = fechaApertura.SelectedDate.Value;
                cajaCerrar.MontoApertura = Convert.ToDouble(txtbase.Text);

                //if (Convert.ToDouble( txtTotalEfectivoCaja.Text)== Convert.ToDouble(txtTotalEfectivoSistema.Text))
                //{
                //    cajaCerrar.Mensaje = "Monto correcto";
                //}
                //if (Convert.ToDouble(txtTotalEfectivoCaja.Text) > Convert.ToDouble(txtTotalEfectivoSistema.Text))
                //{
                //    cajaCerrar.Mensaje = "Sobrante";
                //}
                //if (Convert.ToDouble(txtTotalEfectivoCaja.Text) < Convert.ToDouble(txtTotalEfectivoSistema.Text))
                //{
                //    var monto = Convert.ToDouble(txtTotalEfectivoCaja.Text) - Convert.ToDouble(txtTotalEfectivoSistema.Text);

                //    cajaCerrar.Mensaje = "Falta: "+monto;
                //}
                var monto = Convert.ToDouble(txtTotalEfectivoCaja.Text) - Convert.ToDouble(txtTotalEfectivoSistema.Text);
                if (Convert.ToDouble(txtTotalEfectivoCaja.Text) == Convert.ToDouble(txtTotalEfectivoSistema.Text))
                {
                    cajaCerrar.Mensaje = "Caja cerrada correctamente.";
                }
                if (0 < monto)
                {
                    cajaCerrar.Mensaje = "Caja cerrada con un sobrante de dinero.";
                }
                if (0 > monto)
                {
                    var monto2 = Convert.ToDouble(txtTotalEfectivoCaja.Text) - Convert.ToDouble(txtTotalEfectivoSistema.Text);

                    cajaCerrar.Mensaje = "Hay un faltante de dinero por un monto de: " + monto;
                }
                logica.ActualizarCerrarCaja(cajaCerrar, Convert.ToDouble(txtefectivoSinBase.Text),
                    Convert.ToDouble(txtTotalGastos.Text), Convert.ToDouble(txtTotalEfectivoSistema.Text));

                //MessageBox.Show("Inserto");
                dialogo.IsOpen = true;
                txtTextBlockDialogo.Text = "Registro procesado";

                frmCaja frm = new frmCaja();
                frm.GenerarReporte(logica.ReporteCajaID(cajaCerrar.Id));
                frm.Show();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dataGridDetallePago_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void dataGridDetallePago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGridDetallePago_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }


        public void LlenarDataGrid()
        {
            Usuario usuario = new Usuario();
            TotalesCierreCaja totales = new TotalesCierreCaja();

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            txtUsuario.Text = usuario.Nombre;

            CajaLogica logica = new CajaLogica();
            totales = logica.ObtenerTotalesCierrreCaja(fechaApertura.SelectedDate.Value, fechaCierre.SelectedDate.Value, usuario.Id);

            List<object> lista = new List<object>();
            lista.Add(new { formaPago = "Efectivo", valorCajero = 0.00, valorSistema = txtTotalEfectivoSistema.Text });
            lista.Add(new { formaPago = "Crédito", valorCajero = 0.00, valorSistema = totales.Tarjeta });

            dataGridDetallePago.ItemsSource = lista;
        }

        private void txtTotalEfectivoCaja_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Usuario usuario = new Usuario();
            //TotalesCierreCaja totales = new TotalesCierreCaja();

            //usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            //txtUsuario.Text = usuario.Nombre;

            //CajaLogica logica = new CajaLogica();
            //totales = logica.ObtenerTotalesCierrreCaja(fechaCierre.SelectedDate.Value,fechaCierre.SelectedDate.Value, usuario.Id);

            //List<object> lista = new List<object>();
            //lista.Add(new { formaPago = "Efectivo", valorCajero = txtTotalEfectivoCaja.Text, valorSistema = txtTotalEfectivoSistema.Text });
            //lista.Add(new { formaPago = "Crédito", valorCajero = 0.00, valorSistema = totales.Tarjeta });

            //dataGridDetallePago.ItemsSource = lista;
        }



        private void txtTotalEfectivoCaja_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtTotalEfectivoCaja_LostFocus(object sender, RoutedEventArgs e)
        {
            

            if (!string.IsNullOrEmpty(txtTotalEfectivoCaja.Text))
            {
                if (txtTotalEfectivoCaja.Text.Substring(0, 1) == ".")
                {
                    var v = txtTotalEfectivoCaja.Text.Remove(0, 1);
                    txtTotalEfectivoCaja.Text = v;
                }

                txtTotalEfectivoCaja.Text = string.Format("{0:N2}", Convert.ToDecimal(txtTotalEfectivoCaja.Text));
            }
        }

        private void txtTotalEfectivoCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                

                if (!string.IsNullOrEmpty(txtTotalEfectivoCaja.Text))
                {
                    if (txtTotalEfectivoCaja.Text.Substring(0, 1) == ".")
                    {
                        var v = txtTotalEfectivoCaja.Text.Remove(0, 1);
                        txtTotalEfectivoCaja.Text = v;
                    }

                    txtTotalEfectivoCaja.Text = string.Format("{0:N2}", Convert.ToDecimal(txtTotalEfectivoCaja.Text));
                }
            }
        }


    }
}
