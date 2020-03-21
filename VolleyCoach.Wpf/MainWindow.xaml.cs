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
        Ploeg huidigePloeg;
        //WedstrijdBeheer huidigeWedstrijd;

        public MainWindow()
        {
            InitializeComponent();
            huidigePloeg = new Ploeg("Roeselare");
            for (int i = 0; i < Speler.AantalPerPlaats.Length; i++)
            {
                Console.WriteLine($"{(Plaatsen)i}: {Speler.AantalPerPlaats[i]}");
            }
        }

        void KoppelPloeg()
        {
            lstPloeg.ItemsSource = huidigePloeg.Spelers;
            lstPloeg.Items.Refresh();

            cmbPloeg.Items.Clear();
            cmbPloeg.Items.Add(huidigePloeg);
            //cmbPloeg.ItemsSource = huidigeWedstrijd.DeelnemendePloegen;
            cmbPloeg.SelectedIndex = 0;
        }

        void VerwijderInput()
        {
            ClearPanel(grdDetails);
            huidigeSpeler = null;
            dtpInschrijvingsDatum.SelectedDate = DateTime.Today;
            cmbPlaats.SelectedIndex = 0;
            grdDetails.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KoppelPloeg();
            cmbPlaats.ItemsSource = Enum.GetValues(typeof(Plaatsen));
            grdDetails.IsEnabled = false;
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
            else
            {
                VerwijderInput();
            }
        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                huidigePloeg.Verwijder(huidigeSpeler);
                ToonMelding($"{huidigeSpeler.Naam} is verwijderd", true);
                lstPloeg.SelectedItem = null;
                KoppelPloeg();
            }
            catch (Exception ex)
            {
                ToonMelding(ex.Message);
            }
        }

        private void btnSlaOp_Click(object sender, RoutedEventArgs e)
        {
            Guid id = (huidigeSpeler == null) ? Guid.NewGuid() : huidigeSpeler.Id;
            string naam = txtNaam.Text;
            int nummer = 0;
            Plaatsen plaats = (Plaatsen)cmbPlaats.SelectedItem;
            DateTime? inschrijvingsDatum = dtpInschrijvingsDatum.SelectedDate;
            try
            {
                nummer = int.Parse(txtNummer.Text);
                tbkFeedback.Visibility = Visibility.Hidden;
                try
                {
                    Speler speler = new Speler(naam, nummer, plaats, id, inschrijvingsDatum);
                    huidigePloeg.SlaOp(speler);
                    KoppelPloeg();
                    lstPloeg.SelectedItem = null;
                    VerwijderInput();
                    ToonMelding($"{speler.Naam} van {huidigePloeg.Naam} is opgeslagen", true);
                }
                catch (Exception ex)
                {
                    ToonMelding(ex.Message);
                }
            }
            catch (Exception)
            {
                ToonMelding("Het nummer is geen geldig getal");
                txtNummer.Focus();
                txtNummer.SelectAll();
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            lstPloeg.SelectedItem = null;
            grdDetails.IsEnabled = true;
            txtNaam.Focus();
        }
    }
}
