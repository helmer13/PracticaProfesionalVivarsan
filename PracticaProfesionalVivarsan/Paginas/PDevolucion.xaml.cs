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
    /// 

    public partial class PDevolucion : Page
    {

        LineaDetalleVentas lineaDetalle = new LineaDetalleVentas();
        private string error = "";

        public PDevolucion()
        {
            InitializeComponent();
        }

        private void btnBuscarFactura_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFactura.Text))
            {
                this.dialogo.IsOpen = true;
                this.txtTextBlockDialogo.Text = "# Factura requerido";
                txtFactura.Focus();
                return;
            }
            this.popup.IsOpen = true;
            Refrescar(Convert.ToInt32(txtFactura.Text));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.CargarCboBodegas();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lineaDetalle = (LineaDetalleVentas)dataGrid.SelectedCells[0].Item;

                txtCantidad.Text = lineaDetalle.Cantidad.ToString();
                txtid.Text = lineaDetalle.Id;
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

                if (lista.Count == 0)
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
            // LineaDetalleVentas lineaDetalle = new LineaDetalleVentas();
            FacturaVentasLogica logica = new FacturaVentasLogica();

            if (Validaciones() == true)
            {
                txtTextBlockDialogo2.Text = error;
                dialogoMENS.IsOpen = true;
                return;
            }
            else
            {
                var idDevolucion = Guid.NewGuid().ToString();
                Bodega bodega = (Bodega)cboBodegas.SelectedItem;

                Usuario usuarioGlobal = new Usuario();
                usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];

                logica.InsertarDevolucion(lineaDetalle, idDevolucion, fecha.SelectedDate.Value, Convert.ToInt32(txtCantidad.Text), cboTipo.Text, bodega.Id, usuarioGlobal.Empresa.IdEmpresa);
            }
        }

        private void btnEncontrarFactura_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CargarCboBodegas()
        {
            try
            {
                BodegaLogica logica = new BodegaLogica();
                List<Bodega> lista = new List<Bodega>();
                lista = logica.obtenerBodegas();
                cboBodegas.ItemsSource = lista;

                cboBodegas.DisplayMemberPath = "Nombre";
                cboBodegas.SelectedValuePath = "Id";
                cboBodegas.SelectedValue = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private Boolean Validaciones()
        {
            Boolean bandera = false;         
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                error = "Debe digitar la cantidad.";
                bandera = true;
            }
            if (string.IsNullOrEmpty(fecha.Text))
            {
                error = "Debe digitar la fecha.";
                bandera = true;
            }           
            return bandera;
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

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

    }
}
