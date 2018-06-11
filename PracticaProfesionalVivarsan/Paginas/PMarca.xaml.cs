﻿using Entidad;
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
    /// Interaction logic for PMarca.xaml
    /// </summary>
    public partial class PMarca : Page
    {
        public PMarca()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Marca marca = new Marca();
                MarcaLogica logica = new MarcaLogica();

                if (txtid.Text == "")
                {
                    marca.Id = logica.obtenerMarcas().Count +1;
                }
                else
                {
                    marca.Id = Convert.ToInt32(txtid.Text);
                }

                marca.Descripcion = txtDescripcion.Text;

                logica.InsertarActualizarMarca(marca);
                Refrescar();
                //txtTextBlockDialogo.Text = "Registro procesado";
                //dialogo.IsOpen = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "";
            txtid.Text = "";
        }

        private void dgMarcas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Marca m = (Marca)dgMarcas.SelectedCells[0].Item;
                txtDescripcion.Text = m.Descripcion;
                txtid.Text = Convert.ToString(m.Id);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refrescar();
        }

        private void Refrescar()
        {
            try
            {
                MarcaLogica logica = new MarcaLogica();
                List<Marca> lista = new List<Marca>();
                lista = logica.obtenerMarcas();
                dgMarcas.ItemsSource = lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}