using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{



    public class Ujdiak:IFelvetelizo
    {
        string Om;
        string nev;
        string email;
        DateTime szuletes;
        string cim;
        int magyar;
        int matek;

        public Ujdiak(string oM, string nev, string email, DateTime szuletes, string cim, int magyar, int matek)
        {
            Om = oM;
            this.nev = nev;
            this.email = email;
            this.szuletes = szuletes;
            this.cim = cim;
            this.magyar = magyar;
            this.matek = matek;
        }

        public Ujdiak(string[] adatok)
        {
            this.Om = adatok[0];
            this.nev = adatok[1];
            this.email = adatok[2];
            this.szuletes = Convert.ToDateTime(adatok[3]);
            this.cim = adatok[4];
            this.matek = adatok[5] == "NULL" ? -1 : Convert.ToInt32(adatok[5]);
            this.magyar = adatok[6] == "NULL" ? -1 : Convert.ToInt32(adatok[6]);
        }

        public Ujdiak()
        {
            Om = "";
            this.nev = "";
            this.szuletes = DateTime.Now;
            this.cim = "";
            this.magyar = 0;
            this.matek = 0;
        }

        public string OM_Azonosito { get => Om; set => Om = value; }
        public string Neve { get => nev; set => nev = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szuletes; set => szuletes = value; }
        public string ErtesitesiCime { get => cim; set => cim = value; }
        public int Magyar { get => magyar; set => magyar = value; }
        public int Matematika { get => matek; set => matek = value; }

        public string CSVSortAdVissza()
        {
            return $"{OM_Azonosito};{Neve};{Email};{SzuletesiDatum};{ErtesitesiCime};{Magyar};{Matematika}";
        }

        public void ModositCSVSorral(string csvString)
        {
            string[] mezok = csvString.Split(';');
            this.Om = mezok[0];
            this.nev = mezok[1];
            this.email = mezok[2];
            this.szuletes = DateTime.Parse(mezok[3]);
            this.cim = mezok[4];
            this.magyar = Convert.ToInt32(mezok[5]);
            this.matek = Convert.ToInt32(mezok[6]);
        }
    }
}
