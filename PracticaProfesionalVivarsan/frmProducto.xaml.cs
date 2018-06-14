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
using System.Windows.Shapes;

namespace PracticaProfesionalVivarsan
{
    /// <summary>
    /// Interaction logic for frmProducto.xaml
    /// </summary>
    public partial class frmProducto : Window
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto c = (Producto)dataGridProductos.SelectedCells[0].Item;
            txtCodigo.Text = c.IdProducto;
            //txtid.Text = c.Id;
            txtMarca.Text = c.Marca.Descripcion;
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
            //prod.Marca = txtMarca.Text;
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
    }
}
