using Microsoft.Win32;
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
using System.IO;
using System.Collections.ObjectModel;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {




        ObservableCollection<Ujdiak> diakok = new ObservableCollection<Ujdiak>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Importalas_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt vagy csv fajlok (*.txt;*.csv)|*.txt;*.csv";
            if (diakok.Count > 0)
            {
                MessageBoxResult valasz = MessageBox.Show("A meglévő adatokat szeretné törölni?", "Értesítés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (valasz == MessageBoxResult.Yes || valasz == MessageBoxResult.No)
                {
                    if (valasz == MessageBoxResult.Yes)
                    {
                        diakok.Clear();
                    }
                    if (ofd.ShowDialog() == true)
                    {
                        foreach (string fajl in File.ReadAllLines(ofd.FileName).Skip(1))
                        {
                            diakok.Add(new Ujdiak(fajl.Split(";")));
                        }
                    }
                    dgLista.ItemsSource = diakok;
                    MessageBox.Show("Sikeres importálás!");
                }
            }
            else
            {

                if (ofd.ShowDialog() == true)
                {
                    foreach (string fajl in File.ReadAllLines(ofd.FileName).Skip(1))
                    {
                        diakok.Add(new Ujdiak(fajl.Split(";")));
                    }
                }
                dgLista.ItemsSource = diakok;
                MessageBox.Show("Sikeres importálás!");

            }

        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            List<Ujdiak> temp = new List<Ujdiak>();
            foreach (Ujdiak item in dgLista.SelectedItems)
            {
                temp.Add(item);
            }

            foreach (Ujdiak item in temp)
            {
                diakok.Remove(item);
            }

            dgLista.ItemsSource = diakok;
            MessageBox.Show("Sikeres törlés!");
        }

        private void Felvetel_Click(object sender, RoutedEventArgs e)
        {
            Ujdiak asd = new Ujdiak();
            Bekeres ujablak = new Bekeres(asd);
            ujablak.ShowDialog();
            diakok.Add(asd);
            dgLista.ItemsSource = diakok;
        }

        private void Exportalas_Click(object sender, RoutedEventArgs e)
        {
            if (diakok.Count > 0)
            {
                List<string> lementes = new List<string>();
                lementes.Add("OM_Azonosito;Nev;Email;Szuletesi_Datum;ErtesitesiCim;Magyar;Matematika");
                diakok.ToList().ForEach(asd => lementes.Add($"{asd.OM_Azonosito};{asd.Neve};{asd.Email};{asd.SzuletesiDatum.ToShortDateString().Replace(" ", "")};{asd.ErtesitesiCime};{asd.Magyar};{asd.Matematika}"));
                File.WriteAllLines("export.csv", lementes);
                MessageBox.Show("Sikeres mentés!");
            }
            else
            {
                MessageBox.Show("Nincsen menthető adat!");
            }
        }
    }
}
