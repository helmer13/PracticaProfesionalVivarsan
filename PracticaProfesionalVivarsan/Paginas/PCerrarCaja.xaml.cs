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
    /// Interaction logic for PCerrarCaja.xaml
    /// </summary>
    public partial class PCerrarCaja : Page
    {
        public PCerrarCaja()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            fechaCierre.SelectedDate = DateTime.Now;

            Usuario usuario = new Usuario();
            TotalesCierreCaja totales = new TotalesCierreCaja();

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            txtUsuario.Text = usuario.Nombre;

            CajaLogica logica = new CajaLogica();
            totales = logica.ObtenerTotalesCierrreCaja(fechaCierre.SelectedDate.Value, usuario.Id);
            txtefectivoSinBase.Text = totales.Contado.ToString();
            txtbase.Text = totales.MobtoApertura.ToString();
            txtTotalGastos.Text = totales.Gastos.ToString();

            fechaApertura.SelectedDate = totales.FechaApertura;
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
            List<object> lista = new List<object>();
            lista.Add(new { formaPago = "Efectivo", valorCajero = 0.00, valorSistema = 0.00 });

            dataGridDetallePago.ItemsSource = lista;
        }


    }
}
