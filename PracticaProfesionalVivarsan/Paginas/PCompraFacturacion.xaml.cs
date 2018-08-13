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
        private string error = "";
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
                if (rbCodigo.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.IdProducto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }
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
            BodegaLogica bLogica = new BodegaLogica();

            usuario = (Usuario)App.Current.Properties["usuarioSesion"];

            if (ValidacionesAgregar() == true)
            {
                txtTextBlockDialogo.Text = error;
                dialogoMENS.IsOpen = true;
                return;
            }
            else
            {
                if (dataGridLineaDetalle.ItemsSource != null)
                {
                    foreach (var item in dataGridLineaDetalle.ItemsSource as List<LineaDetalleCompras>)
                    {
                        if (producto.IdProducto == item.Producto.IdProducto)
                        {
                            txtTextBlockDialogo.Text = "No puedes ingresar el mismo producto más de una vez";
                            dialogoMENS.IsOpen = true;
                            return;
                        }
                    }
                }
                lineaDetalle.Id = Guid.NewGuid().ToString();
                lineaDetalle.Cantidad = Convert.ToInt32(txtCantidad.Text);
                producto.IdLineaDetalle = lineaDetalle.Id;
                producto.IdBodega = (int)cboBodegas.SelectedValue;
                lineaDetalle.Producto = producto;
                lineaDetalle.SubTotal = lineaDetalle.Cantidad * Convert.ToInt32(txtPrecioCosto.Text);

                listaDetalle.Add(lineaDetalle);
                dataGridLineaDetalle.ItemsSource = listaDetalle;
                dataGridLineaDetalle.Items.Refresh();
                //inventario
                inventario.Cantidad = Convert.ToInt32(txtCantidad.Text);
                inventario.Bodega = bLogica.obtenerBodega((int)cboBodegas.SelectedValue);
                inventario.Empresa = usuario.Empresa;
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
            txtidProducto.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecioCosto.Text = string.Empty;
            producto = new Producto();


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
                cboProveedores.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            var lista = dataGridLineaDetalle.ItemsSource as List<LineaDetalleCompras>; ;
            var index = dataGridLineaDetalle.SelectedIndex;
            dataGridLineaDetalle.ItemsSource = null;


            lista.RemoveAt(index);
            dataGridLineaDetalle.ItemsSource = lista;
            txtSubTotal.Text= this.ActializarTotal();
        }



        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            FacturaCompras factura = new FacturaCompras();
            FacturaComprasLogica logica = new FacturaComprasLogica();

          

            Usuario usuarioGlobal = new Usuario();

            Proveedor proveedor = (Proveedor)cboProveedores.SelectedItem;

            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];

            if (ValidacionesFacturar() == true)
            {
                txtTextBlockDialogo.Text = "Debe completar todos los campos solicitados";
                dialogoMENS.IsOpen = true;
                return;
            }
            else
            {
                var error = logica.ObtenerVerificacionNumeroFactura(Convert.ToInt32(txtNumeroFactura.Text),proveedor.Id);

                if (error == "ERROR")
                {
                    txtTextBlockDialogo.Text = "No puedes ingresar el mismo numero de factura";
                    dialogoMENS.IsOpen = true;
                    return;
                }


                factura.Id = Convert.ToInt32(txtNumeroFactura.Text);
                factura.Usuario = usuarioGlobal;
                factura.IdProveedor = proveedor.Id;
                factura.Fecha = fecha.SelectedDate.Value;
                factura.Total = Convert.ToDouble(txtSubTotal.Text);
                factura.TipoPago = cboTipoPago.Text;

                foreach (var item in dataGridLineaDetalle.ItemsSource as List<LineaDetalleCompras>)
                {
                    item.IdFactua = factura.Id;
                }
                factura.LineasDetalleCompras = dataGridLineaDetalle.ItemsSource as List<LineaDetalleCompras>;

                logica.GuardarFactura(factura);


                txtTextBlockDialogo.Text = "Registro Procesado";
                dialogoMENS.IsOpen = true;

                factura = new FacturaCompras();
                proveedor = new Proveedor();
                listaInventario = new List<Inventario>();
                listaDetalle = new List<LineaDetalleCompras>();

                Limpiar();
            }
        }

        private Boolean ValidacionesFacturar()
        {
            if (string.IsNullOrEmpty(txtNumeroFactura.Text) || string.IsNullOrEmpty(fecha.Text) || listaDetalle.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean ValidacionesAgregar()
        {
            Boolean bandera = false;
            if (string.IsNullOrEmpty(txtidProducto.Text))
            {
                error = "Debe seleccionar un producto.";
                bandera = true;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                error = "Debe digitar la cantidad.";
                bandera = true;
            }
            if (string.IsNullOrEmpty(txtPrecioCosto.Text))
            {
                error = "Debe digitar el precio de compra del producto.";
                bandera = true;
            }
            if (string.IsNullOrEmpty(txtProducto.Text))
            {
                error = "Debe seleccionar un producto.";
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

        private void txtPrecioCosto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtNumeroFactura_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Consultar si solo son numeros o tambien letras
            //SoloNumeros(e);
        }

        public void Limpiar()
        {
            txtNumeroFactura.Text = string.Empty;
            fecha.SelectedDate = null;
            txtidProducto.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecioCosto.Text = string.Empty;
            dataGridLineaDetalle.ItemsSource = null;
            txtSubTotal.Text = string.Empty;
            Producto producto = new Producto();
            List<LineaDetalleCompras> listaDetalle = new List<LineaDetalleCompras>();
            List<Inventario> listaInventario = new List<Inventario>();
        }

      private string ActializarTotal()
        {
            txtSubTotal.Text = string.Empty;
            var data = dataGridLineaDetalle.ItemsSource as List<LineaDetalleCompras>;
            double total = 0;
            for (int i = 0; i < listaDetalle.Count; i++)
            {
                total += listaDetalle[i].SubTotal;

            }
            
            return total.ToString();
        }
    }
}
