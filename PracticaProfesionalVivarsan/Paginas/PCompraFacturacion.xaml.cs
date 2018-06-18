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
    /// Interaction logic for PCompraFacturacion.xaml
    /// </summary>
    public partial class PCompraFacturacion : Page
    {

        public Producto producto = new Producto();
        List<LineaDetalleCompras> listaDetalle = new List<LineaDetalleCompras>();
        List<Inventario> listaInventario = new List<Inventario>();

        public PCompraFacturacion()
        {
            InitializeComponent();
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            Refrescar();
            dialogo.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CargarCboBodegas();

            txtSubTotal.Text = "0";
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                producto = (Producto)dataGridProductos.SelectedCells[0].Item;
                txtidProducto.Text = producto.IdProducto;
                txtProducto.Text = producto.Nombre;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnEncontrarProducto_Click(object sender, RoutedEventArgs e)
        {

            Refrescar();
        }

        private void Refrescar()
        {
            try
            {
                ProductoLogica logica = new ProductoLogica();
                List<Producto> lista = new List<Producto>();
                List<Producto> listaAuxiliar = new List<Producto>();
                lista = logica.obtenerProductos();
                dataGridProductos.ItemsSource = lista;
                if (rbNombre.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p=> p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar; 
                }
                if (rbMarca.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.Modelo.Marca.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }
                if (rbModelo.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.Modelo.NombreCompleto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

            Usuario usuario = new Usuario();
            LineaDetalleCompras lineaDetalle = new LineaDetalleCompras();
            Inventario inventario = new Inventario();

            usuario= (Usuario)App.Current.Properties["usuarioSesion"];

            lineaDetalle.Id = Guid.NewGuid().ToString();
            lineaDetalle.Cantidad = Convert.ToInt32( txtCantidad.Text);
            lineaDetalle.Producto = producto;
            lineaDetalle.SubTotal = lineaDetalle.Cantidad * Convert.ToInt32( txtPrecioCosto.Text);
            listaDetalle.Add(lineaDetalle);
            dataGridLineaDetalle.ItemsSource = listaDetalle;
            dataGridLineaDetalle.Items.Refresh();
            //inventario
            inventario.Cantidad = Convert.ToInt32(txtCantidad.Text); ;
            inventario.IdBodega =  (int)cboBodegas.SelectedValue;
            inventario.IdEmpresa = usuario.Empresa.IdEmpresa;
            inventario.Producto = producto;
            listaInventario.Add(inventario);

            //para el label del total
            double total=0;
            for (int i = 0; i < listaDetalle.Count; i++)
            {
                 total += listaDetalle[i].SubTotal;
              
            }
            txtSubTotal.Text = total.ToString();
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGridLineaDetalle_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void dataGridLineaDetalle_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void dataGridLineaDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
