using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZivotinjskaFarma;

namespace Zadatak1
{
    [TestClass] //Testovi Kerim Nurikic
    public class ZivotinjaTest
    {
        static List<string> parametri;
        static Lokacija lokacija;
        static string info = "ok";
        static string napomena = "ok";

        [ClassInitialize]
        public static void PostavakLokacije(TestContext context)
        {
            parametri = new List<string> { "Ba", "Bazen", "9", "Sarajevo", "71000", "Bosna i Hercegovina" };
            lokacija = new Lokacija(parametri, 25);
        }

        [TestMethod]
        public void StarijaOd10() //Sve zivotinje starije od 10 godina
        {
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2005, 1, 1), 25, 25, lokacija);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2010, 2, 2), 25, 25, lokacija);
            Zivotinja z3 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2011, 11, 18), 25, 25, lokacija);
            
            z1.ProvjeriStanjeZivotinje();
            z2.ProvjeriStanjeZivotinje();
            z3.ProvjeriStanjeZivotinje();

            Assert.IsFalse(z1.Proizvođač);
            Assert.IsFalse(z2.Proizvođač);
            Assert.IsFalse(z3.Proizvođač);
        }

        [TestMethod]
        public void StarijaOd7()
        {
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2014, 1, 1), 25, 25, lokacija);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2013, 2, 2), 25, 25, lokacija);
            Zivotinja z3 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2013, 2, 2), 25, 25, lokacija);
            for (int i = 0; i < 5; i++)
            {
                z1.PregledajZivotinju(info, napomena, "3.5");
                z2.PregledajZivotinju(info, napomena, "2.9");
                z3.PregledajZivotinju(info, napomena, "5");
            }
            z3.PregledajZivotinju(info, napomena, "3");

            z3.ProvjeriStanjeZivotinje();
            z1.ProvjeriStanjeZivotinje();
            z2.ProvjeriStanjeZivotinje();

            Assert.IsFalse(z1.Proizvođač); //Starija od 7 godina, i posljednji pregled ima ocjenu <= 3.5
            Assert.IsFalse(z2.Proizvođač); //Starija od 7 godina, i posljednji pregled ima ocjenu <=3.5
            Assert.IsFalse(z3.Proizvođač); // Starija od 7 godina, posljednja ocjena <=3, prosjek > 4
        }

        [TestMethod]
        public void ProsjekIspod4()
        {
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2012, 2, 2), 25, 25, lokacija);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2020, 2, 2), 25, 25, lokacija);
            for (int i = 0; i < 4; i++)
            {
                z1.PregledajZivotinju(info, napomena, "3.9");
                z2.PregledajZivotinju(info, napomena, (i + 1).ToString());
            }

            z1.ProvjeriStanjeZivotinje();
            z2.ProvjeriStanjeZivotinje();

            Assert.IsFalse(z1.Proizvođač); //Starija od 7 godina, posljednji pregled >=, prosjek zadnja tri pregleda < 4
            Assert.IsFalse(z2.Proizvođač); //Mladja od 7 godina, prosjek zadnja tri pregleda < 4 
        }

        [TestMethod]
        public void NemaDovoljnoPregleda()  //Ako treba provjeriti posljednji ili posljednja tri pregleda, kojih nema, zivotinja ostaje proizvodjac
        {
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2012, 1, 1), 25, 25, lokacija);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2012, 2, 2), 25, 25, lokacija);
            z2.PregledajZivotinju(info, napomena, "4");

            z1.ProvjeriStanjeZivotinje();
            z2.ProvjeriStanjeZivotinje();

            Assert.IsTrue(z1.Proizvođač);
            Assert.IsTrue(z2.Proizvođač);
        }

        [TestMethod]
        public void ZivotinjeProizvodjaci() //Zivotinje koje trebaju ostati proizvodjaci
        {
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2020, 1, 1), 25, 25, lokacija);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Guska, new DateTime(2012, 2, 2), 25, 25, lokacija);
            for (int i = 0; i < 4; i++)
            {
                z1.PregledajZivotinju(info, napomena, "5");
                z2.PregledajZivotinju(info, napomena, "4");
            }

            Assert.IsTrue(z1.Proizvođač);
            Assert.IsTrue(z2.Proizvođač);
        }
    }
}
