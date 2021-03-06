﻿using Entidad;
using Logica;
using PracticaProfesionalVivarsan.Reportes;
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
        private Double iv = 0;
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
                //precio con impuesto
                Double impuesto = Convert.ToDouble(Convert.ToDouble(lineaDetalle.Producto.PrecioVenta) * 0.13) + Convert.ToDouble(lineaDetalle.Producto.PrecioVenta);
                Double precioarticulos = Convert.ToDouble(lineaDetalle.Cantidad * lineaDetalle.Producto.PrecioVenta);
                Double precioImpuestos = (Convert.ToDouble(lineaDetalle.Producto.PrecioVenta) * 0.13) * lineaDetalle.Cantidad;
                lineaDetalle.SubTotal = precioarticulos + precioImpuestos;

                iv += precioImpuestos;

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
              //  txtSubTotal.Text = total.ToString();
                txtSubTotal.Text = string.Format("{0:N2}", Convert.ToDecimal(total));
            }
            txtidProducto.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtidBodega.Text = string.Empty;
            txtidEmpresa.Text = string.Empty;
            txtCantDisp.Text = string.Empty;

            txtCantidad.Text= string.Empty;
            txtPrecioImpuesto.Text = "";
            txtPrecioNormal.Text = "";
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

                txtTextBlockDialogo.Text = "Registro Procesado";
                dialogoMENS.IsOpen = true;

                frmFacturaVenta fr = new frmFacturaVenta();              
                fr.GenerarReporte(iv,logica.EncabezadoFactura(factura.Id), logica.ReporteFactura2(factura.Id));
                fr.Show();

                factura = new FacturaVentas();
                cliente = new Cliente();
                listaInventario = new List<Inventario>();
                listaDetalle = new List<LineaDetalleVentas>();
                inventario = new Inventario();
                var contador = logica.ObtenerContadorFacturas().Id + 1;
                txtNumeroFactura.Text = contador.ToString();
                CargarCboClientes();
                txtidProducto.Text = string.Empty;
                txtProducto.Text = string.Empty;
                txtidBodega.Text = string.Empty;
                txtidEmpresa.Text = string.Empty;
                txtCantDisp.Text = string.Empty;
                txtCantidad.Text = string.Empty;
                txtSubTotal.Text = string.Empty;
                dataGridLineaDetalle.ItemsSource = null;
                iv = 0;
                txtPrecioImpuesto.Text = "";
                txtPrecioNormal.Text = "";

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
                dataGridProductos.UnselectAll();
                inventario = (Inventario)dataGridProductos.SelectedCells[0].Item;
                txtidProducto.Text = inventario.Producto.IdProducto;
                txtProducto.Text = inventario.Producto.Nombre;
                txtidBodega.Text = inventario.Bodega.Id.ToString();
                txtidEmpresa.Text = inventario.Empresa.IdEmpresa.ToString();
                txtCantDisp.Text = inventario.Cantidad.ToString();
                //campos de impuesto y precio normal
                Double impuesto = Convert.ToDouble(Convert.ToDouble(inventario.Producto.PrecioVenta) * 0.13) + Convert.ToDouble(inventario.Producto.PrecioVenta);
                //   txtPrecioImpuesto.Text = impuesto.ToString();
                txtPrecioImpuesto.Text = string.Format("{0:N2}", Convert.ToDecimal(impuesto));
                //  txtPrecioNormal.Text = inventario.Producto.PrecioVenta.ToString();
                txtPrecioNormal.Text = string.Format("{0:N2}", Convert.ToDecimal(inventario.Producto.PrecioVenta));
                //lineaDetalle.SubTotal = Convert.ToDouble(inventario.Cantidad * inventario.Producto.PrecioVenta) + (impuesto * inventario.Cantidad);

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
          //  txtSubTotal.Text = this.ActializarTotal();
            txtSubTotal.Text = string.Format("{0:N2}", Convert.ToDecimal(this.ActializarTotal()));
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

                if (rbCodigo.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.Producto.IdProducto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }
                if (rbNombre.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.Producto.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }
                if (rbMarca.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.Producto.Modelo.Marca.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }
                if (rbModelo.IsChecked.Value)
                {
                    listaAuxiliar = lista.Where(p => p.Producto.Modelo.NombreCompleto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAuxiliar;
                }


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
            txtBuscar.Text = string.Empty;
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
          private string ActializarTotal()
        {
            txtSubTotal.Text = string.Empty;
            var data = dataGridLineaDetalle.ItemsSource as List<LineaDetalleVentas>;
            double total = 0;
            for (int i = 0; i < listaDetalle.Count; i++)
            {
                total += listaDetalle[i].SubTotal;

            }
            
            return total.ToString();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {

                btnEncontrarProducto_Click(sender, e);
            }

        }
    }
}
