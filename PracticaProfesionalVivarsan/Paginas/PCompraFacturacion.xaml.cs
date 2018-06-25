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
            CargarCboProveedores();
            // Style dpStyle = new Style(typeof(System.Windows.Controls.DatePicker));
            // dpStyle.Setters.Add(new Setter(System.Windows.Controls.DatePicker.LanguageProperty, System.Windows.Markup.XmlLanguage.GetLanguage("es-US")));
            // this.Resources.Add(typeof(System.Windows.Controls.DatePicker), dpStyle);


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
                    listaAuxiliar = lista.Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
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

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];

            lineaDetalle.Id = Guid.NewGuid().ToString();
            lineaDetalle.Cantidad = Convert.ToInt32(txtCantidad.Text);
            producto.IdLineaDetalle = lineaDetalle.Id;
            producto.IdBodega= (int)cboBodegas.SelectedValue;
            lineaDetalle.Producto = producto;
            lineaDetalle.SubTotal = lineaDetalle.Cantidad * Convert.ToInt32(txtPrecioCosto.Text);

            listaDetalle.Add(lineaDetalle);
            dataGridLineaDetalle.ItemsSource = listaDetalle;
            dataGridLineaDetalle.Items.Refresh();
            //inventario
            inventario.Cantidad = Convert.ToInt32(txtCantidad.Text); ;
            inventario.IdBodega = (int)cboBodegas.SelectedValue;
            inventario.IdEmpresa = usuario.Empresa.IdEmpresa;
            inventario.Producto = producto;
            listaInventario.Add(inventario);

            //para el label del total
            double total = 0;
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

        private void CargarCboProveedores()
        {
            try
            {
                ProveedorLogica logica = new ProveedorLogica();
                List<Proveedor> lista = new List<Proveedor>();
                lista = logica.obtenerProveedores();
                cboProveedores.ItemsSource = lista;

                cboProveedores.DisplayMemberPath = "NombreProveedor";
                cboProveedores.SelectedValuePath = "Id";
                cboProveedores.SelectedValue = 1;
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
            FacturaCompras factura = new FacturaCompras();
            FacturaComprasLogica logica = new FacturaComprasLogica();

            var count = logica.ObtenerContadorFacturas();

            Usuario usuarioGlobal = new Usuario();

            Proveedor proveedor = (Proveedor)cboProveedores.SelectedItem;

            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];

            factura.Id = count.Id + 1;
            factura.Usuario = usuarioGlobal;
            factura.IdProveedor = proveedor.Id;
            factura.Fecha = fecha.SelectedDate.Value;
            factura.Total = Convert.ToDouble( txtSubTotal.Text);
            factura.TipoPago = cboTipoPago.Text;

            foreach (var item in listaDetalle)
            {
                item.IdFactua = factura.Id;
            }
            factura.LineasDetalleCompras = listaDetalle;

            logica.GuardarFactura(factura);

            MessageBox.Show("funciona");

        }
    }
}
