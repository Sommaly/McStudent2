using GTP.ados;
using GTP.classes;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rbtn_tp_Checked(object sender, RoutedEventArgs e)
        {
            this.main_frame.Content = new ListeTPs();
        }

        private void rbtn_promo_Checked(object sender, RoutedEventArgs e)
        {
            this.main_frame.Content = new ListePromos();
        }

        private void rbtn_eleve_Checked(object sender, RoutedEventArgs e)
        {
            this.main_frame.Content = new ListeEleves();
        }
    }
}