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
    /// Interaction logic for PModelo.xaml
    /// </summary>
    public partial class PModelo : Page
    {
        public PModelo()
        {
            InitializeComponent();
        }

        private void dgModelos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Modelo m = (Modelo)dgModelos.SelectedCells[0].Item;
                txtDescripcion.Text = m.Descripcion;
                txtAnno.Text = Convert.ToString(m.Anno);
                txtid.Text = Convert.ToString(m.Id);
                cboMarcas.SelectedValue = Convert.ToString(m.Marca.Id);
                

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
                Modelo modelo = new Modelo();
                ModeloLogica logica = new ModeloLogica();
                MarcaLogica logMarca = new MarcaLogica();

                if (txtid.Text == "")
                {
                    modelo.Id = logica.obtenerModelos().Count + 1;
                }
                else
                {
                    modelo.Id = Convert.ToInt32(txtid.Text);
                }

                if (validaciones() == true )
                {
                    txtTextBlockDialogo.Text = "Debe completar todos los campos solicitados";
                    dialogo.IsOpen = true;
                    return;
                }
                else
                {
                    modelo.Descripcion = txtDescripcion.Text;
                    modelo.Anno = Convert.ToInt32(txtAnno.Text);
                    modelo.Marca = logMarca.seleccionarMarca(Convert.ToInt32(cboMarcas.SelectedValue));

                    logica.InsertarActualizarModelo(modelo);

                    Refrescar();
                    txtTextBlockDialogo.Text = "Registro procesado";
                    dialogo.IsOpen = true;
                }              
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Boolean validaciones()
        {           
            if (string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrEmpty(txtDescripcion.Text))
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
            txtDescripcion.Text = "";
            txtid.Text = "";
            txtAnno.Text = "";
            CargarCboMarcas();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
            CargarCboMarcas();
        }

        private void Refrescar()
        {
            try
            {
                ModeloLogica logica = new ModeloLogica();
                List<Modelo> lista = new List<Modelo>();
                lista = logica.obtenerModelos();
                dgModelos.ItemsSource = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

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
        private void txtAnno_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            SoloNumeros(e);
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

        private void btnAyuda_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockAyuda.Text = string.Format(" En el combo box Marca, debe de elegir la correspondiente al modelo que desea registrar. \n En el campo descipción, debe de ingresar el modelo.  \n En el campo Año, debe ingresar el año que corresponda al modelo que se esta registrando.");
            ayuda.IsOpen = true;
        }
    }
}
