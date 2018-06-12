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
        }

        private void dataGridProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Producto c = (Producto)dataGridProductos.SelectedCells[0].Item;
                txtidProducto.Text = c.IdProducto;
                txtProducto.Text = c.Nombre;
                
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


    }
}
