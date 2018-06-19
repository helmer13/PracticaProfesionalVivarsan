﻿using System.Windows;
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
using PracticaProfesionalVivarsan.Paginas;
using System;

namespace PracticaProfesionalVivarsan
{
    /// <summary>
    /// Interaction logic for frmMenu.xaml
    /// </summary>
    public partial class frmMenu : Window
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void mantProducto_Click(object sender, RoutedEventArgs e)
        {
            // PProducto pProducto = new PProducto();
            frame.Content = new PProducto();
        }

        private void mantProveedor_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PProveedor();
        }

        private void mantCliente_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PCliente();
        }

        private void mantUsuario_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PUsuario();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            lnlNombreUsuario.Content = "Usuario: " + usuarioGlobal.Nombre;
            frame.Content = new PMenuLogo();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mantMarca_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PMarca();
        }

        private void mantModelo_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PModelo();
        }

        private void ProcesoCompras_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PCompraFacturacion();
        }
    }
}
