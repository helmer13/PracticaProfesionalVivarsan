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
                //txtMarca.Text = c.Marca.Descripcion;
                cboMarcas.SelectedValue = (Convert.ToString(c.Modelo.Marca.Id));
                CargarCboModelos();
                cboModelos.SelectedValue = (Convert.ToString(c.Modelo.Id));
                txtNombre.Text = c.Nombre;
                //  txtPrecioCompra.Text = c.PrecioCompra.ToString();
                txtPrecioCompra.Text = string.Format("{0:N2}", Convert.ToDecimal(c.PrecioCompra));
                // txtPrecioVenta.Text = c.PrecioVenta.ToString();
                txtPrecioVenta.Text = string.Format("{0:N2}", Convert.ToDecimal(c.PrecioVenta));

                txtCodigo.IsReadOnly = true;

                gridTabla.Visibility = Visibility.Collapsed;
                gridForm.Visibility = Visibility.Visible;

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
                ModeloLogica logModelo = new ModeloLogica();

                if (validaciones() == true)
                {
                    txtTextBlockDialogo.Text = "Debe completar todos los campos solicitados";
                    dialogo.IsOpen = true;
                    return;
                }
                else
                {
                    prod.IdProducto = txtCodigo.Text;
                    prod.Nombre = txtNombre.Text;
                    prod.Modelo = logModelo.seleccionarModelo(Convert.ToInt32(cboModelos.SelectedValue));
                    prod.PrecioCompra = 0; // Convert.ToDecimal(txtPrecioCompra.Text);
                    prod.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);

                    logica.InsertarActialiarProducto(prod);
                    Refrescar();
                    txtTextBlockDialogo.Text = "Registro procesado";
                    dialogo.IsOpen = true;
                    btnVolver_Click(sender, e);
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public Boolean validaciones()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecioVenta.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtid.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtCodigo.Text = string.Empty;
            //cboMarcas.SelectedValue = 1;
            txtCodigo.IsReadOnly = false;

            gridTabla.Visibility = Visibility.Collapsed;
            gridForm.Visibility = Visibility.Visible;

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
            rbCodigo.IsChecked = true;
            Refrescar();
            CargarCboMarcas();
            CargarCboModelos();
        }

        private void CargarCboMarcas()
        {
            try
            {
                MarcaLogica logica = new MarcaLogica();
                List<Marca> lista = new List<Marca>();
                lista = logica.obtenerMarcas();
                cboMarcas.ItemsSource = lista;

                cboMarcas.DisplayMemberPath = "Descripcion";
                cboMarcas.SelectedValuePath = "Id";
                cboMarcas.SelectedValue = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void CargarCboModelos()
        {
            try
            {
                ModeloLogica logica = new ModeloLogica();
                List<Modelo> lista = new List<Modelo>();
                lista = logica.seleccionarModeloPorMarca(Convert.ToInt32(cboMarcas.SelectedValue));
                cboModelos.ItemsSource = lista;

                cboModelos.DisplayMemberPath = "NombreCompleto";
                cboModelos.SelectedValuePath = "Id";
                cboModelos.SelectedValue = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void cboMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CargarCboModelos();
        }

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockAyuda.Text = string.Format(" En el campo Código, se debe digitar la código del producto a registrar.  \n En el campo Nombre, se debe digitar el nombre del producto." +
                "\n En el combo box Marca, debe elegir la marca de los automóviles en la que funciona ese producto. \n En el combo box Modelo, debe elegir el modelo del automóvil en la que funciona ese producto. "+
                "\n En el campo Precio Venta, se debe digitar el precio en el que desea vender ese producto. \n ");
            ayuda.IsOpen = true;
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;

            List<Producto> lista = new List<Producto>();
            List<Producto> listaAxiliar = new List<Producto>();
            lista = (List<Producto>)dataGridProductos.ItemsSource;

            if (filter == "")
            {
                Refrescar();
            }
            else
            {
                if (rbCodigo.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.IdProducto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAxiliar;
                }
                if (rbNombre.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAxiliar;
                }
                if (rbMarca.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Modelo.Marca.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAxiliar;
                }
                if (rbModelo.IsChecked.Value)
                {
                    listaAxiliar = lista.Where(p => p.Modelo.NombreCompleto.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                    dataGridProductos.ItemsSource = listaAxiliar;
                }
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtid.Text = string.Empty;
            txtPrecioCompra.Text = string.Empty;
            txtPrecioVenta.Text = string.Empty;
            txtCodigo.Text = string.Empty;


            gridTabla.Visibility = Visibility.Visible;
            gridForm.Visibility = Visibility.Collapsed;
        }

        private void txtPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                if (!string.IsNullOrEmpty(txtPrecioVenta.Text))
                {
                    if (txtPrecioVenta.Text.Substring(0, 1) == ".")
                    {
                        var v = txtPrecioVenta.Text.Remove(0, 1);
                        txtPrecioVenta.Text = v;
                    }

                    txtPrecioVenta.Text = string.Format("{0:N2}", Convert.ToDecimal(txtPrecioVenta.Text));
                }
            }
        }

        private void txtPrecioVenta_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrecioVenta.Text))
            {
                if (txtPrecioVenta.Text.Substring(0, 1) == ".")
                {
                    var v = txtPrecioVenta.Text.Remove(0, 1);
                    txtPrecioVenta.Text = v;
                }

                txtPrecioVenta.Text = string.Format("{0:N2}", Convert.ToDecimal(txtPrecioVenta.Text));
            }
        }
    }
}
