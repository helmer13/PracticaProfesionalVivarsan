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
    /// Interaction logic for PVentaFacturacion.xaml
    /// </summary>
    public partial class PVentaFacturacion : Page
    {
        public Inventario inventario = new Inventario();
        List<LineaDetalleVentas> listaDetalle = new List<LineaDetalleVentas>();
        List<Inventario> listaInventario = new List<Inventario>();
        public PVentaFacturacion()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            LineaDetalleVentas lineaDetalle = new LineaDetalleVentas();
            Inventario inventario = new Inventario();
            ProductoLogica pLogica = new ProductoLogica();
            BodegaLogica bLogica = new BodegaLogica();

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];

            lineaDetalle.Id = Guid.NewGuid().ToString();
            if(Convert.ToInt32(txtCantDisp.Text) < Convert.ToInt32(txtCantidad.Text))
            {
                //mensaje de error para la cantidad
                return;
            }
            else
            {
                lineaDetalle.Cantidad = Convert.ToInt32(txtCantidad.Text);
            }

            lineaDetalle.Producto = pLogica.obtenerProducto(txtidProducto.Text);
            lineaDetalle.Producto.IdBodega = Convert.ToInt32(txtidBodega.Text);
            lineaDetalle.Producto.IdLineaDetalle = lineaDetalle.Id;
            lineaDetalle.SubTotal = Convert.ToDouble(lineaDetalle.Cantidad * lineaDetalle.Producto.PrecioVenta);

            listaDetalle.Add(lineaDetalle);
            dataGridLineaDetalle.ItemsSource = listaDetalle;
            dataGridLineaDetalle.Items.Refresh();

            //inventario
            inventario.Producto = pLogica.obtenerProducto(txtidProducto.Text);
            inventario.Bodega = bLogica.obtenerBodega(Convert.ToInt32(txtidBodega.Text));
            inventario.Empresa = usuario.Empresa;
            
            listaInventario.Add(inventario);

            //para el label del total
            double total = 0;
            for (int i = 0; i < listaDetalle.Count; i++)
            {
                total += listaDetalle[i].SubTotal;

            }
            txtSubTotal.Text = total.ToString();
        }

        private void dataGridLineaDetalle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGridLineaDetalle_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void dataGridLineaDetalle_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            FacturaVentas factura = new FacturaVentas();
            FacturaVentasLogica logica = new FacturaVentasLogica();
            ClienteLogica cLogica = new ClienteLogica();


            //var count = logica.ObtenerContadorFacturas();

            Usuario usuarioGlobal = new Usuario();

            Cliente cliente = cLogica.obtenerCliente(cboClientes.SelectedValue.ToString());

            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];

            //se puede cambiar a como esta en compras
            factura.Id = Convert.ToInt32(txtNumeroFactura.Text);
            factura.Usuario = usuarioGlobal;
            factura.Cliente = cliente;
            factura.Fecha = fecha.SelectedDate.Value;
            factura.Total = Convert.ToDouble(txtSubTotal.Text);
            factura.TipoPago = cboTipoPago.Text;

            foreach (var item in listaDetalle)
            {
                item.IdFactura = factura.Id;
            }
            factura.LineaDetalleVentas = listaDetalle;

            logica.GuardarFactura(factura);

            MessageBox.Show("funciona");



            factura = new FacturaVentas();
            cliente = new Cliente();
            listaInventario = new List<Inventario>();
            listaDetalle = new List<LineaDetalleVentas>();
        }

        private void btnEncontrarProducto_Click(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                inventario = (Inventario)dataGridProductos.SelectedCells[0].Item;
                txtidProducto.Text = inventario.Producto.IdProducto;
                txtProducto.Text = inventario.Producto.Nombre;
                txtidBodega.Text = inventario.Bodega.Id.ToString();
                txtidEmpresa.Text = inventario.Empresa.IdEmpresa.ToString();
                txtCantDisp.Text = inventario.Cantidad.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Refrescar()
        {
            try
            {
                InventarioLogica logica = new InventarioLogica();
                List<Inventario> lista = new List<Inventario>();
                List<Inventario> listaAuxiliar = new List<Inventario>();
                Usuario usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
                lista = logica.obtenerInventario(usuarioGlobal.Empresa.IdEmpresa);
                dataGridProductos.ItemsSource = lista;
                //if (rbNombre.IsChecked.Value)
                //{
                //    listaAuxiliar = lista.Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                //    dataGridProductos.ItemsSource = listaAuxiliar;
                //}
                //if (rbMarca.IsChecked.Value)
                //{
                //    listaAuxiliar = lista.Where(p => p.Modelo.Marca.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                //    dataGridProductos.ItemsSource = listaAuxiliar;
                //}
                //if (rbModelo.IsChecked.Value)
                //{
                //    listaAuxiliar = lista.Where(p => p.Modelo.NombreCompleto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                //    dataGridProductos.ItemsSource = listaAuxiliar;
                //}


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
       

        private void CargarCboClientes()
        {
            try
            {
                ClienteLogica logica = new ClienteLogica();
                List<Cliente> lista = new List<Cliente>();
                lista = logica.obtenerClientes();
                cboClientes.ItemsSource = lista;

                cboClientes.DisplayMemberPath = "Nombre";
                cboClientes.SelectedValuePath = "Id";
                cboClientes.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CargarCboClientes();
            txtSubTotal.Text = "0";
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            Refrescar();
            dialogo.IsOpen = true;
        }
    }
}
