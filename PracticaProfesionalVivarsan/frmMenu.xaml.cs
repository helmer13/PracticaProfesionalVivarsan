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
using PracticaProfesionalVivarsan.Reportes;
using System.IO;
using Microsoft.Win32;

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
            Uri iconUri = new Uri("pack://application:,,,/PracticaProfesionalVivarsan;component/imagenes/Background White.png");
            this.Icon = BitmapFrame.Create(iconUri);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
            frame.Content = new PMenuLogo();
        }

        private void mantProducto_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frame.Content = new PProducto();
            }
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
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frame.Content = new PUsuario();
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            //lnlNombreUsuario.Content = "Usuario: " + usuarioGlobal.Nombre;

            btn4.Content = "Usuario: " + usuarioGlobal.Nombre;

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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow frmlogin = new MainWindow();
            this.Hide();
            frmlogin.Show();


        }

        private void ProcesoVentas_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            CajaLogica logica = new CajaLogica();
            Caja caja = new Caja();
            caja = logica.ObtenerCajaAbierta(usuario.Id);

            if (caja.Id == null)
            {
                dialogo.IsOpen = true;
                txtTextBlockDialogo.Text = "La caja no se encuentra abierta";
                return;

            }
            else
            {
                if (caja.FechaApertura.Date < caja.FechaCierre.Date)
                {
                    dialogo.IsOpen = true;
                    txtTextBlockDialogo.Text = "No puedes facturar ventas un día después de haber abierto la caja";
                    return;
                }


                frame.Content = new PVentaFacturacion();
            }
        }

        private void ProcesoGastos_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            CajaLogica logica = new CajaLogica();
            Caja caja = new Caja();
            caja = logica.ObtenerCajaAbierta(usuario.Id);

            if (caja.Id == null)
            {
                dialogo.IsOpen = true;
                txtTextBlockDialogo.Text = "La caja no se encuentra abierta";
                return;

            }
            else
            {
                if (caja.FechaApertura.Date < caja.FechaCierre.Date)
                {
                    dialogo.IsOpen = true;
                    txtTextBlockDialogo.Text = "No puedes facturar gastos un día después de haber abierto la caja";
                    return;
                }

                frame.Content = new PGasto();
            }

        }

        private void ProcesoDevoluciones_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PDevolucion();
        }

        private void AbrirCaja_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new PAbrirCaja();
        }

        private void cerrarCaja_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)App.Current.Properties["usuarioSesion"];
            CajaLogica logica = new CajaLogica();
            Caja caja = new Caja();
            caja = logica.ObtenerCajaAbierta(usuario.Id);

            if (caja.Id == null)
            {
                dialogo.IsOpen = true;
                txtTextBlockDialogo.Text = "La caja no se encuentra abierta";
                return;

            }
            else
            {
                frame.Content = new PCerrarCaja();
            }

        }

        private void UsuarioMantenimientoPersonal_Click(object sender, RoutedEventArgs e)
        {
            Usuario c = new Usuario();
            c = (Usuario)App.Current.Properties["usuarioSesion"];
            PUsuario pusuario = new PUsuario();

            pusuario.txtIndentificacion.Text = c.Id;
            pusuario.txtNombre.Text = c.Nombre;
            pusuario.txtContrasena.Password = c.Contrasena;
            pusuario.txtUsuarioGeneral.Text = c.UsuarioGeneral;
            pusuario.cboTipo.Text = c.Tipo;

            pusuario.txtIndentificacion.IsReadOnly = true;
            pusuario.btnVolver.Visibility = Visibility.Collapsed;
            pusuario.cboTipo.IsEnabled = false;
            pusuario.gridForm.Visibility = Visibility.Visible;
            pusuario.gridTabla.Visibility = Visibility.Collapsed;
            frame.Content = pusuario;
        }

        private void ReporteProductos_Click(object sender, RoutedEventArgs e)
        {
            frmReporteProd r = new frmReporteProd();
            r.Show();
        }

        private void ReporteInventario_Click(object sender, RoutedEventArgs e)
        {
            frmReporteInventario r = new frmReporteInventario();
            r.Show();
        }

        private void ReporteClientes_Click(object sender, RoutedEventArgs e)
        {
            frmReporteClientes r = new frmReporteClientes();
            r.Show();
        }

        private void ReporteProveedores_Click(object sender, RoutedEventArgs e)
        {
            frmReporteProveedores r = new frmReporteProveedores();
            r.Show();
        }

        private void ReporteGastos_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteGastos r = new frmReporteGastos();
                r.Show();
            }
        }

        private void ReporteTotalCompras_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteTotalCompras r = new frmReporteTotalCompras();
                r.Show();
            }
        }

        private void ReporteTotalVentas_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteTotalVentas r = new frmReporteTotalVentas();
                r.Show();
            }
        }

        private void ReporteComprasProveedor_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteComprasProveedor r = new frmReporteComprasProveedor();
                r.Show();
            }
        }

        private void ReporteVentasCliente_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteVentasCliente r = new frmReporteVentasCliente();
                r.Show();
            }
        }

        private void ReporteDevoluciones_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteDevoluciones r = new frmReporteDevoluciones();
                r.Show();
            }
        }

        private void ReporteCajas_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteCajas r = new frmReporteCajas();
                r.Show();
            }
        }

        private void backup_Click(object sender, RoutedEventArgs e)
        {
            FacturaComprasLogica logica = new FacturaComprasLogica();

            File.Delete("C:\\backupCCV\\backUp_backupCCV.bak");
            logica.GuardarBackUp();
            dialogo.IsOpen = true;
            txtTextBlockDialogo.Text = "Backup realizado con éxito";

        }

        private void restore_Click(object sender, RoutedEventArgs e)
        {
            try
            {



                OpenFileDialog open = new OpenFileDialog();
                //  open.InitialDirectory = @"C:/";
                open.Title = "Buscar respaldo";

                open.CheckFileExists = true;
                open.CheckPathExists = true;

                open.DefaultExt = "BAK";
                open.Filter = "Text files (*.bak)|*.bak";
                open.FilterIndex = 2;
                open.RestoreDirectory = true;

                open.ReadOnlyChecked = true;
                open.ShowReadOnly = true;

                Nullable<bool> result1 = open.ShowDialog();

                if (result1 == true)
                {
                    FacturaComprasLogica logica = new FacturaComprasLogica();
                    logica.RestoreDB(open.FileName);
                    dialogo.IsOpen = true;
                    txtTextBlockDialogo.Text = "Restore realizado con éxito";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void ReporteBitacora_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuarioGlobal = new Usuario();
            usuarioGlobal = (Usuario)App.Current.Properties["usuarioSesion"];
            if (usuarioGlobal.Tipo == "Administrador")
            {
                frmReporteBitacora r = new frmReporteBitacora();
                r.Show();
            }
        }

        private void AcercaDe_Click(object sender, RoutedEventArgs e)
        {
            txtTextBlockDialogo.Text = string.Format(
                "                      Acerca de " +
                "\n       Control de Compras Vivarsan " +
                "\n                   Repuestos Vini \n " +
                "\n            Helmer Zúñiga Salas " +
                "\n            Kevin Chaves Parajeles \n "+
                  "\n                     versión: 1.0 ");
            dialogo.IsOpen = true;


        }
    }
}
