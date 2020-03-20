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
using Volley.Lib.Entities;

namespace VolleyCoach.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Speler huidigeSpeler;
        List<Speler> huidigePloeg;
        //WedstrijdBeheer huidigeWedstrijd;

        public MainWindow()
        {
            InitializeComponent();
            MaakPloeg();
        }

        void MaakPloeg()
        {
            Speler jan = new Speler();
            jan.Id = Guid.NewGuid();
            jan.Naam = "Jan";
            jan.Plaats = Plaatsen.Passeur;

            //Met object initializer
            Speler piet = new Speler
            {
                Id = Guid.NewGuid(),
                Naam = "Piet",
                Nummer = 2,
                Plaats = Plaatsen.Aanvaller
            };

            Speler frank = new Speler("Frank", 3, Plaatsen.Libero);

            Speler koen = new Speler("Koen", 4, guid: Guid.NewGuid());

            huidigePloeg = new List<Speler> { jan, piet, frank, koen };
        }

        void KoppelPloeg()
        {
            lstPloeg.ItemsSource = huidigePloeg;
            lstPloeg.Items.Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KoppelPloeg();
            cmbPlaats.ItemsSource = Enum.GetValues(typeof(Plaatsen));
            grdDetails.IsEnabled = false;

            //cmbPloeg.ItemsSource = huidigeWedstrijd.DeelnemendePloegen;
            //cmbPloeg.SelectedIndex = 0;
            tbkFeedback.Visibility = Visibility.Hidden;
        }

        private void lstPloeg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPloeg.SelectedItem != null)
            {
                huidigeSpeler = (Speler)lstPloeg.SelectedItem;
                lblGuid.Content = huidigeSpeler.Id;
                txtNaam.Text = huidigeSpeler.Naam;
                txtNummer.Text = huidigeSpeler.Nummer.ToString();
                cmbPlaats.SelectedItem = huidigeSpeler.Plaats;
                dtpInschrijvingsDatum.SelectedDate = huidigeSpeler.InschrijvingsDatum;

                grdDetails.IsEnabled = true;
                tbkFeedback.Visibility = Visibility.Hidden;
            }
        }
    }
}
