using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZivotinjskaFarma;
using System;
using System.Collections.Generic;

namespace Zadatak1
{
    //Testovi Amer Hrnjic
    [TestClass]
    public class FarmaTest
    {
        //static List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
        // static List<string> lista2 = new List<string>() { "Farma2", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
        //static Lokacija lokacija1 = new Lokacija(lista1, 50);
        //static Lokacija lokacija2 = new Lokacija(lista2,50);

        [TestMethod]
        public void PrazneLokacije()
        {

            //Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7),75,40,lokacija1);
            //Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddYears(-10), 100, 50, lokacija1);
            //Zivotinja z3 = new Zivotinja(ZivotinjskaVrsta.Patka, DateTime.Now.AddYears(-4), 10, 10, lokacija1);

            //Proizvod p1 = new Proizvod("Proizvod1","Neki Opis","Sir",z2,DateTime.Now.AddMonths(-1),DateTime.Now.AddMonths(2),3);
            //Proizvod p2 = new Proizvod("Proizvod2","Neki Opis2","Mlijeko",z1,DateTime.Now.AddDays(-15),DateTime.Now.AddMonths(2),5);

            //Kupovina k1 = new Kupovina( "Kupac123", DateTime.Now,DateTime.Now.AddDays(4), p1, 2, false );
            //Kupovina k2 = new Kupovina( "Kupac123", DateTime.Now,DateTime.Now.AddDays(3),p2,1,false);


            Farma f1 = new Farma();

            Assert.AreEqual(0, f1.ObračunajPorez());


        }

        [TestMethod]
        public void PorezZaLokacijuVecuOd10000()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija( lista1, 15000);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.2,Math.Round(f1.ObračunajPorez(),2));

        }
        [TestMethod]
        public void PorezZaLokacijuIzmedju1000i10000UBiH()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 6543);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.15, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaLokacijuIzmedju1000i10000VanBiH()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Hrvatska" };
            Lokacija lokacija1 = new Lokacija(lista1, 6543);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.5, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaLokacijuManjeod1000uSarajevu()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.1, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaLokacijuManjeod1000uTuzli()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Tuzla", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.1, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaLokacijuManjeod1000uZenici()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Zenica", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.1, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaLokacijuManjeod1000uMostaru()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Mostar", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.1, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaLokacijuManjeod1000ostaliGradovi()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Banja Luka", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(0.3, Math.Round(f1.ObračunajPorez(), 2));

        }
        [TestMethod]
        public void PorezZaViseLokacija()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Banja Luka", "71000", "Bosna i Hercegovina" };
            List<string> lista2 = new List<string>() { "Farma1", "Adresa1", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            List<string> lista3 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            List<string> lista4 = new List<string>() { "Farma1", "Adresa", "31", "Zagreb", "71000", "Hrvatska" };
            List<string> lista5 = new List<string>() { "Farma1", "Adresa2", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Lokacija lokacija2 = new Lokacija(lista2, 333);
            Lokacija lokacija3 = new Lokacija(lista3, 1337);
            Lokacija lokacija5 = new Lokacija(lista4, 1444);
            Lokacija lokacija4 = new Lokacija(lista5, 12345);

            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            f1.DodavanjeNoveLokacije(lokacija2);
            f1.DodavanjeNoveLokacije(lokacija3);
            f1.DodavanjeNoveLokacije(lokacija4);
            f1.DodavanjeNoveLokacije(lokacija5);
            Assert.AreEqual(1.25, Math.Round(f1.ObračunajPorez(), 2));

        }






    }
}