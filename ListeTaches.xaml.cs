using GTP.ados;
using GTP.classes;
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

namespace GTP
{
    /// <summary>
    /// Logique d'interaction pour ListeTaches.xaml
    /// </summary>
    public partial class ListeTaches : Page
    {
        int idTP;
        public Tache TacheMod;
        public ObservableCollection<Tache> taches;
        public ListeTaches(int idTP)
        {
            InitializeComponent();
            Charger(idTP);
        }
        private void Charger(int idTP)
        {
            taches = new ObservableCollection<Tache>(TacheAdo.VoirTaches(idTP));
            lvTaches.ItemsSource = taches;
        }

        private void btnAjouter_Click_1(object sender, RoutedEventArgs e)
        {
            Tache tache = new Tache();
            tache.Titre = txtTitre.Text;
            tache.Description = txtDescription.Text;
            List<TP> lesTP = TPAdo.VoirTp();
            TP leTP = lesTP[idTP];
            tache.TP = leTP;
            leTP.Taches.Add(tache);

            TacheAdo.AjouterTache(tache);

            txtTitre.Text = "";
            txtDescription.Text = "";

            int lID = leTP.Id;

            Charger(lID);
        }

        private void btnSupprimer_Click_1(object sender, RoutedEventArgs e)
        {
            Tache t = lvTaches.SelectedItem as Tache;
            string sMessageBoxText = "Voulez-vous vraiment supprimer la tache sélectionnée ?";
            string sCaption = "Gestion des taches";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    TacheAdo.SupprimerTache(t);
                    break;
                case MessageBoxResult.No:

                    break;
                case MessageBoxResult.Cancel:

                    break;
            }
            Charger(t.TP.Id);
        }
    }
}
