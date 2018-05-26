﻿using System;
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

namespace PracticaProfesionalVivarsan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Uri iconUri = new Uri("C:/Users/PC/Desktop/key_102281.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioLogica usuarioLogica = new UsuarioLogica();
            usuarioLogica.obtenerUsuarios();


        }
    }
}
