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
    /// Interaction logic for PProducto.xaml
    /// </summary>
    public partial class PProducto : Page
    {
        public PProducto()
        {
            InitializeComponent();
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Producto c = (Producto)dataGridProductos.SelectedCells[0].Item;
                txtCodigo.Text = c.IdProducto;
                //txtid.Text = c.Id;
                txtMarca.Text = c.Marca;
                txtNombre.Text = c.Nombre;
                txtPrecioCompra.Text = c.PrecioCompra.ToString();
                txtPrecioVenta.Text = c.PrecioVenta.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Producto prod = new Producto();
                ProductoLogica logica = new ProductoLogica();
                prod.IdProducto = txtCodigo.Text;
                prod.Nombre = txtNombre.Text;
                prod.Marca = txtMarca.Text;
                prod.PrecioCompra = 0; // Convert.ToDecimal(txtPrecioCompra.Text);
                prod.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);

                logica.InsertarActialiarProducto(prod);
                Refrescar();
                txtTextBlockDialogo.Text = "Registro procesado";
                dialogo.IsOpen = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = "";
            txtid.Text = "";
            txtMarca.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";
            txtCodigo.Text = "";
        }

        private void Refrescar()
        {
            try
            {
                ProductoLogica logica = new ProductoLogica();
                List<Producto> lista = new List<Producto>();
                lista = logica.obtenerProductos();
                dataGridProductos.ItemsSource = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
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

        private void txtPrecioCompra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtPrecioVenta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }
    }
}
