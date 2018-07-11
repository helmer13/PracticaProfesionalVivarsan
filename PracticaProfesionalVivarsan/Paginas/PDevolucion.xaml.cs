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
    /// Interaction logic for PDevolucion.xaml
    /// </summary>
    public partial class PDevolucion : Page
    {
        public PDevolucion()
        {
            InitializeComponent();
        }

        private void btnBuscarFactura_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty( txtFactura.Text))
            {
                this.dialogo.IsOpen = true;
                this.txtTextBlockDialogo.Text = "# Factura requerido";
                return;
            }
            this.popup.IsOpen = true;
            Refrescar(Convert.ToInt32( txtFactura.Text));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LineaDetalleVentas l = (LineaDetalleVentas)dataGrid.SelectedCells[0].Item;

                txtCantidad.Text = l.Cantidad.ToString();
                txtid.Text = l.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Refrescar(int idFactura)
        {
            try
            {
                FacturaVentasLogica logica = new FacturaVentasLogica();
                List<LineaDetalleVentas> lista = new List<LineaDetalleVentas>();

                lista = logica.obtenerLineaDetalleVentas(idFactura);

                if (lista.Count==0)
                {
                    this.dialogo.IsOpen = true;
                    this.txtTextBlockDialogo.Text = "El # Factura no existe";
                    this.popup.IsOpen = false;
                    return;
                }

                dataGrid.ItemsSource = lista;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEncontrarFactura_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
