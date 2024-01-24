using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for Bekeres.xaml
    /// </summary>
    public partial class Bekeres : Window
    {
        Ujdiak hhh;
        bool hiba;
        string matekMentes;
        string magyarMentes;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool joSzoveg(string szoveg)
        {
            return !_regex.IsMatch(szoveg);
        }
        public Bekeres()
        {
            InitializeComponent();
        }

        public Bekeres(Ujdiak aa):this()
        {
            this.hhh = aa;
            txtOm.Text = hhh.OM_Azonosito;
            txtNev.Text = hhh.Neve.Trim();
            txtCim.Text = hhh.ErtesitesiCime;
            txtDatum.Text = hhh.SzuletesiDatum.ToString();
            txtEmail.Text = hhh.Email;
            txtMagyar.Text = hhh.Magyar.ToString();
            txtMatek.Text = hhh.Matematika.ToString();
        }

        private void txtMagyar_LostFocus(object sender, RoutedEventArgs e)
        {
            hiba = false;
            hhh.OM_Azonosito = txtOm.Text;
            hhh.Neve = txtNev.Text;
            hhh.Email = txtEmail.Text;
            try
            {
                hhh.SzuletesiDatum = DateTime.Parse(txtDatum.Text);
            }
            catch (Exception)
            {
                hiba=true;
                MessageBox.Show("A dátum formátuma nem megfelelő");
                txtDatum.Focus();
            }
            
            hhh.ErtesitesiCime = txtCim.Text;

            if (!hiba)
            {
                try
                {
                    if (!txtMagyar.IsEnabled)
                    {
                        hhh.Magyar = -1;
                    }
                    else
                    {
                        hhh.Magyar = Convert.ToInt32(txtMagyar.Text);
                    }
                    
                }
                catch (Exception)
                {
                    hiba = true;
                    MessageBox.Show("A magyar pontszám csak számjegyeket tartalmazhat");
                    txtMagyar.Focus();
                }
            }


            if (!hiba)
            {
                try
                {
                    if (!txtMatek.IsEnabled)
                    {
                        hhh.Matematika = -1;
                    }
                    else
                    {
                        hhh.Matematika = Convert.ToInt32(txtMatek.Text);
                    }
                    
                }
                catch (Exception)
                {
                    hiba = true;
                    MessageBox.Show("A matematika pontszám csak számjegyeket tartalmazhat");
                    txtMatek.Focus();
                }
            }


            

            if (!hiba)
            {
                if (hhh.OM_Azonosito.Length != 11)
                {
                    MessageBox.Show("Az OM azonosító nem 11 karakterből áll");
                    txtOm.Focus();
                }
                else if (!joSzoveg(hhh.OM_Azonosito))
                {
                    MessageBox.Show("Az OM azonosító nem csak számjegyeket tartalmaz");
                    txtOm.Focus();
                }
                else if (long.Parse(hhh.OM_Azonosito) < 0)
                {
                    MessageBox.Show("Az OM azonosító csak pozitív szám lehet");
                    txtOm.Focus();
                }
                else if (hhh.Neve.Trim().Split(' ').Length < 2)
                {
                    MessageBox.Show("A név nem áll legalább 2 tagból");
                    txtNev.Focus();
                }
                else if(hhh.Neve.Split(' ').Where(n => Char.IsLower(n[0])).ToList().Count()!=0)
                {
                    MessageBox.Show("Minden név nagy kezdűbetűvel kell, hogy szerepeljen");
                    txtNev.Focus();
                }
                else if (hhh.Email.Count(n => n == '@') != 1 || !hhh.Email.Contains('.') || hhh.Email.Contains(' '))
                {
                    MessageBox.Show("Valós email címet adjon meg!");
                    txtEmail.Focus();
                }
                else if (hhh.ErtesitesiCime == "")
                {
                    MessageBox.Show("A cím mezőt kötelező kitölteni!");
                    txtCim.Focus();
                }
                else if ((txtMagyar.IsEnabled) && (hhh.Magyar < 0 || hhh.Magyar > 50))
                {
                    MessageBox.Show("A magyar pontszámnak 0 és 50 közé kell esnie");
                    txtMagyar.Focus();
                }
                else if ((txtMatek.IsEnabled) && (hhh.Matematika < 0 || hhh.Matematika > 50))
                {
                    MessageBox.Show("A matematika pontszámnak 0 és 50 közé kell esnie");
                    txtMatek.Focus();
                }
                else if (!hiba)
                {
                    Close();
                }
            }
                    

        }

        private void felvetelizett_Checked(object sender, RoutedEventArgs e)
        {
            txtMagyar.IsEnabled = true;
            txtMatek.IsEnabled = true;
            txtMatek.Text = matekMentes;
            txtMagyar.Text = magyarMentes;
        }

        private void felvetelizett_UnChecked(object sender, RoutedEventArgs e)
        {
            magyarMentes = txtMagyar.Text;
            matekMentes = txtMatek.Text;
            txtMatek.Text = "";
            txtMagyar.Text = "";
            txtMagyar.IsEnabled = false;
            txtMatek.IsEnabled= false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //txtMagyar_LostFocus(sender, e);
        }
    }
}
