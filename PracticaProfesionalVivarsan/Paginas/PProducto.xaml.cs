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
            Producto c = (Producto)dataGridProductos.SelectedCells[0].Item;
            txtCodigo.Text = c.IdProducto;
            //txtid.Text = c.Id;
            txtMarca.Text = c.Marca;
            txtNombre.Text = c.Nombre;
            txtPrecioCompra.Text = c.PrecioCompra.ToString();
            txtPrecioVenta.Text = c.PrecioVenta.ToString();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Producto prod = new Producto();
            ProductoLogica logica = new ProductoLogica();

            //cliente.Id = txtid.Text;
            //if (cliente.Id == "")
            //{
            //    cliente.Id = Guid.NewGuid().ToString();
            //}


            //  cliente.Id = Guid.NewGuid().ToString();
            prod.IdProducto = txtCodigo.Text;
            prod.Nombre = txtNombre.Text;
            prod.Marca = txtMarca.Text;
            prod.PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text);
            prod.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);

            logica.InsertarActialiarProducto(prod);
            MessageBox.Show("Correcto.", "Advertencia");
            Refrescar();
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
            ProductoLogica logica = new ProductoLogica();
            List<Producto> lista = new List<Producto>();
            lista = logica.obtenerProductos();
            dataGridProductos.ItemsSource = lista;
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
