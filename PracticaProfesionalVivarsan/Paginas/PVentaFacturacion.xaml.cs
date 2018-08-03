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
        private string error = "";
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
            if (ValidacionesAgregar() == true)
            {
                txtTextBlockDialogo.Text = error;
                dialogoMENS.IsOpen = true;
                return;
            }
            else
            {
                if (dataGridLineaDetalle.ItemsSource!=null)
                {
                    foreach (var item in dataGridLineaDetalle.ItemsSource as List<LineaDetalleVentas>)
                    {
                        if (item.Producto.IdProducto == txtidProducto.Text)
                        {
                            //mensaje de error si el producto ya se encuentra en la linea detalle
                            txtTextBlockDialogo.Text = "No puedes ingresar el mismo producto más de una vez";
                            dialogoMENS.IsOpen = true;
                            return;
                        }
                    }
                }


                lineaDetalle.Id = Guid.NewGuid().ToString();
                if (Convert.ToInt32(txtCantDisp.Text) < Convert.ToInt32(txtCantidad.Text))
                {
                    //mensaje de error para la cantidad
                    txtTextBlockDialogo.Text = "La cantidad de producto ingresada, es mayor a la cantidad disponible.";
                    dialogoMENS.IsOpen = true;
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
            txtidProducto.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtidBodega.Text = string.Empty;
            txtidEmpresa.Text = string.Empty;
            txtCantDisp.Text = string.Empty;

            txtCantidad.Text= string.Empty;
           
        }

      

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            FacturaVentas factura = new FacturaVentas();
            FacturaVentasLogica logica = new FacturaVentasLogica();
            ClienteLogica cLogica = new ClienteLogica();


            var count = logica.ObtenerContadorFacturas();

            Usuario usuarioGlobal = new Usuario();

            Cliente cliente = cLogica.obtenerCliente(cboClientes.SelectedValue.ToString());

            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];

            if (ValidacionesFacturar() == true)
            {
                txtTextBlockDialogo.Text = "Debe completar todos los campos solicitados";
                dialogoMENS.IsOpen = true;
                return;
            }
            else
            {
                factura.Id = count.Id + 1;
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

                //MessageBox.Show("funciona");
                txtTextBlockDialogo.Text = "Registro Procesado";
                dialogoMENS.IsOpen = true;


                factura = new FacturaVentas();
                cliente = new Cliente();
                listaInventario = new List<Inventario>();
                listaDetalle = new List<LineaDetalleVentas>();
                }

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
            var lista = dataGridLineaDetalle.ItemsSource as List<LineaDetalleVentas>; ;
            var index = dataGridLineaDetalle.SelectedIndex;
            dataGridLineaDetalle.ItemsSource = null;


            lista.RemoveAt(index);
            dataGridLineaDetalle.ItemsSource = lista;
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
                Cliente cliente = new Cliente();
                cliente.Id = "GENERICO";
                cliente.Direccion = "nada";
                cliente.Correo = "nada";
                cliente.Indentificacion = "nada";
                cliente.Nombre = "Cliente no registrado";
                cliente.Telefono = 0;

                lista = logica.obtenerClientes();
                lista.Insert(0, cliente);
                cboClientes.ItemsSource = lista;

                cboClientes.DisplayMemberPath = "Nombre";
                cboClientes.SelectedValuePath = "Id";
                cboClientes.SelectedIndex = 0;
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
            fecha.SelectedDate = DateTime.Now;
            FacturaVentasLogica logica = new FacturaVentasLogica();
            var contador = logica.ObtenerContadorFacturas().Id + 1;
            txtNumeroFactura.Text = contador.ToString();
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            Refrescar();
            dialogo.IsOpen = true;
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

        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
        }

        private void txtPrecioCosto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ",")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }
    }
}
