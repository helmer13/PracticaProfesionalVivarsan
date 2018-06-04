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
    /// Interaction logic for frmMenu.xaml
    /// </summary>
    public partial class frmMenu : Window
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            frmProducto frmProducto = new frmProducto();
            frmProducto.Show();
            this.Close();
        }
    }
}
