using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GTP.ados;
using GTP.classes;
namespace GTP
{
    /// <summary>
    /// Logique d'interaction pour ListePromos.xaml
    /// </summary>
    public partial class ListePromos : Page
    {
        public ObservableCollection<classes.Promotion> promos;
        public classes.Promotion promotionMod;
        public ListePromos()
        {
            InitializeComponent();
            Charger();
        }
        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            classes.Promotion p = lvPromotions.SelectedItem as classes.Promotion;
            string sMessageBoxText = "Voulez-vous vraiment supprimer la promotion sélectionnée ?";
            string sCaption = "Gestion des TPs";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    PromotionAdo.SupprimerPromotion(p);
                    break;
                case MessageBoxResult.No:

                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
            Charger();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            promotionMod.Nom = txtNom.Text;
            promotionMod.AnneeDebut = Convert.ToInt32(txtAD.Text);
            promotionMod.AnneeFin = Convert.ToInt32(txtAF.Text);

            PromotionAdo.ModifierPromotion(promotionMod);

            txtNom.Text = "";
            txtAD.Text = "";
            txtAF.Text = "";
            btnModifier.IsEnabled = false;
            Charger();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            classes.Promotion p = new classes.Promotion();
            p.Nom = txtNom.Text;
            p.AnneeDebut = Convert.ToInt32(txtAD.Text);
            p.AnneeFin = Convert.ToInt32(txtAF.Text);

            PromotionAdo.AjouterPromotion(p);

            txtNom.Text = "";
            txtAD.Text = "";
            txtAF.Text = "";

            Charger();
        }

        private void lvPromotions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            promotionMod = lvPromotions.SelectedItem as classes.Promotion;
            txtNom.Text = promotionMod.Nom;
            txtAD.Text = promotionMod.AnneeDebut.ToString();
            txtAF.Text = promotionMod.AnneeFin.ToString();

            btnModifier.IsEnabled = true;
        }
        public void Charger()
        {
            promos = new ObservableCollection<classes.Promotion>(PromotionAdo.VoirPromotions());
            lvPromotions.ItemsSource = promos;
        }
    }
}
