using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZivotinjskaFarma;
using System;
using System.Collections.Generic;

namespace Zadatak3
{
    //Testovi Amer Hrnjic
    [TestClass]
    public class FarmaTest
    {

        //Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7),75,40,lokacija1);
        //Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddYears(-10), 100, 50, lokacija1);
        //Zivotinja z3 = new Zivotinja(ZivotinjskaVrsta.Patka, DateTime.Now.AddYears(-4), 10, 10, lokacija1);

        //Proizvod p1 = new Proizvod("Proizvod1","Neki Opis","Sir",z2,DateTime.Now.AddMonths(-1),DateTime.Now.AddMonths(2),3);
        //Proizvod p2 = new Proizvod("Proizvod2","Neki Opis2","Mlijeko",z1,DateTime.Now.AddDays(-15),DateTime.Now.AddMonths(2),5);

        //Kupovina k1 = new Kupovina( "Kupac123", DateTime.Now,DateTime.Now.AddDays(4), p1, 2, false );
        //Kupovina k2 = new Kupovina( "Kupac123", DateTime.Now,DateTime.Now.AddDays(3),p2,1,false);


        [TestMethod]
        public void RadSaZivotinjamaDodavanje()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7),75,40,lokacija1);
            Farma f1 = new Farma();

            f1.RadSaZivotinjama("Dodavanje", z1);

            Assert.AreEqual(1, f1.Zivotinje.Count);
            Assert.AreEqual(z1, f1.Zivotinje[0]);
        }
        [TestMethod]
        public void RadSaZivotinjamaIzmjena1() {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();


          
            f1.RadSaZivotinjama("Dodavanje", z1);
            z1.Visina = 44;
            z1.TjelesnaMasa = 80;
            f1.RadSaZivotinjama("Izmjena", z1);


            Assert.AreEqual(80, f1.Zivotinje[0].TjelesnaMasa);
            Assert.AreEqual(44, f1.Zivotinje[0].Visina);


        }
        [TestMethod]
        public void RadSaZivotinjamaBrisanje()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Zivotinja z2 = new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Now.AddYears(-5), 100, 50, lokacija1);
            Farma f1 = new Farma();



            f1.RadSaZivotinjama("Dodavanje", z1);
            f1.RadSaZivotinjama("Dodavanje", z2);

            Assert.AreEqual(2, f1.Zivotinje.Count);

            f1.RadSaZivotinjama("Brisanje", z1);

            Assert.AreEqual(1, f1.Zivotinje.Count);
            Assert.IsFalse(f1.Zivotinje.Contains(z1));


        }
        [TestMethod]
        public void RadSaZivotinjamaNijeRegistrovana()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();


            Assert.ThrowsException<ArgumentException>(() =>f1.RadSaZivotinjama("Izmjena", z1));

            Assert.ThrowsException<ArgumentException>(() => f1.RadSaZivotinjama("Brisanje", z1));


        }
        [TestMethod]
        public void RadSaZivotinjamaVecRegistrovana()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();
            f1.RadSaZivotinjama("Dodavanje", z1);


            Assert.ThrowsException<ArgumentException>(() => f1.RadSaZivotinjama("Dodavanje", z1));


        }
        [TestMethod]
        public void RadSaZivotinjamaVecNepoznataOpcija()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();


            Assert.ThrowsException<ArgumentException>(() => f1.RadSaZivotinjama("Opcija", z1));


        }
        [TestMethod]
        public void DodavanjeNoveLokacije()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Farma f1 = new Farma();

            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.AreEqual(lokacija1, f1.Lokacije[0]);
        }
        [TestMethod]
        public void DodavanjeNoveLokacijePonovljenaLokacija()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Farma f1 = new Farma();

            f1.DodavanjeNoveLokacije(lokacija1);
            Assert.ThrowsException<InvalidOperationException>(() => f1.DodavanjeNoveLokacije(lokacija1));
        }
        [TestMethod]
        public void BrisanjeLokacije()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Farma f1 = new Farma();

            f1.DodavanjeNoveLokacije(lokacija1);

            Assert.AreEqual(1, f1.Lokacije.Count);
            f1.BrisanjeLokacije(lokacija1);

            Assert.AreEqual(0, f1.Lokacije.Count);
        }

        [TestMethod]
        public void SpecijalizacijaFarmeKozaException()
        {
            Farma f1 = new Farma();

            Assert.ThrowsException<InvalidOperationException>(() => f1.SpecijalizacijaFarme(ZivotinjskaVrsta.Koza, 20));

        }
        [TestMethod]
        public void SpecijalizacijaFarmePreviseGrlaException()
        {
            Farma f1 = new Farma();

            Assert.ThrowsException<ArgumentException>(() => f1.SpecijalizacijaFarme(ZivotinjskaVrsta.Magarac, 10000));

        }

        [TestMethod]
        public void SpecijalizacijaFarmeVrstaNijeMagarac1() // 25<=grla<=50
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

            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            f1.DodavanjeNoveLokacije(lokacija2);
            f1.DodavanjeNoveLokacije(lokacija3);
            f1.DodavanjeNoveLokacije(lokacija4);
            f1.DodavanjeNoveLokacije(lokacija5);
            f1.RadSaZivotinjama("Dodavanje", z1);

            f1.SpecijalizacijaFarme(ZivotinjskaVrsta.Krava, 26);



            Lokacija pomocna = new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Split", "21000", "Bosna i Hercegovina" }, 25);

            Assert.AreEqual(1, f1.Lokacije.Count);
            Assert.AreEqual(pomocna.Naziv, f1.Lokacije[0].Naziv);
            Assert.AreEqual(pomocna.Adresa, f1.Lokacije[0].Adresa);
            Assert.AreEqual(pomocna.Grad, f1.Lokacije[0].Grad);
            Assert.AreEqual(pomocna.BrojUlice, f1.Lokacije[0].BrojUlice);
            Assert.AreEqual(pomocna.PoštanskiBroj, f1.Lokacije[0].PoštanskiBroj);
            Assert.AreEqual(pomocna.Površina, f1.Lokacije[0].Površina);


        }
        [TestMethod]
        public void SpecijalizacijaFarmeVrstaNijeMagarac2()// grla>=50
        {


            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Banja Luka", "71000", "Bosna i Hercegovina" };
            List<string> lista2 = new List<string>() { "Farma1", "Adresa1", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            List<string> lista3 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };

            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Lokacija lokacija2 = new Lokacija(lista2, 333);
            Lokacija lokacija3 = new Lokacija(lista3, 1337);

            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            f1.DodavanjeNoveLokacije(lokacija2);
            f1.DodavanjeNoveLokacije(lokacija3);
            f1.RadSaZivotinjama("Dodavanje", z1);

            f1.SpecijalizacijaFarme(ZivotinjskaVrsta.Krava, 78);


            Lokacija pomocna = new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Split", "21000", "Bosna i Hercegovina" }, 25);

            Assert.AreEqual(3, f1.Lokacije.Count);


            for (int i = 0; i < f1.Lokacije.Count; i++)
            {
                Assert.AreEqual(pomocna.Naziv, f1.Lokacije[i].Naziv);
                Assert.AreEqual(pomocna.Adresa, f1.Lokacije[i].Adresa);
                Assert.AreEqual(pomocna.Grad, f1.Lokacije[i].Grad);
                Assert.AreEqual(pomocna.BrojUlice, f1.Lokacije[i].BrojUlice);
                Assert.AreEqual(pomocna.PoštanskiBroj, f1.Lokacije[i].PoštanskiBroj);
                Assert.AreEqual(pomocna.Površina, f1.Lokacije[i].Površina);
            }

        }

        [TestMethod]
        public void SpecijalizacijaFarmeVrstaJeMagarac()
        {


            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Banja Luka", "71000", "Bosna i Hercegovina" };
            List<string> lista2 = new List<string>() { "Farma1", "Adresa1", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            List<string> lista3 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };

            Lokacija lokacija1 = new Lokacija(lista1, 150);
            Lokacija lokacija2 = new Lokacija(lista2, 333);
            Lokacija lokacija3 = new Lokacija(lista3, 1337);

            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();
            f1.DodavanjeNoveLokacije(lokacija1);
            f1.DodavanjeNoveLokacije(lokacija2);
            f1.DodavanjeNoveLokacije(lokacija3);
            f1.RadSaZivotinjama("Dodavanje", z1);

            f1.SpecijalizacijaFarme(ZivotinjskaVrsta.Magarac, 26);


            Lokacija pomocna = new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Split", "21000", "Bosna i Hercegovina" }, 25);

            Assert.AreEqual(24, f1.Zivotinje.Count);
            Assert.AreEqual(3, f1.Lokacije.Count);

        }

        [TestMethod]
        public void KupovinaProizvoda() { 
            Farma f1 = new Farma();
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Proizvod p = new Proizvod("Mlijeko", "Najbolje mlijeko na svijetu", "Vuna", new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddYears(-7), 75, 40, lokacija1), DateTime.Now, DateTime.Now.AddYears(1), 25);
            Assert.IsTrue(f1.KupovinaProizvoda(p,DateTime.Now.AddDays(35),10));
        }
        [TestMethod]
        public void KupovinaProizvodaNetacna()
        {
            Farma f1 = new Farma();
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Proizvod p = new Proizvod("Mlijeko", "Najbolje mlijeko na svijetu", "Vuna", new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddYears(-7), 75, 40, lokacija1), DateTime.Now, DateTime.Now.AddYears(1), 25);
            Assert.IsFalse(f1.KupovinaProizvoda(p, DateTime.Now.AddDays(5), 10));
        }


        [TestMethod]
        public void BrisanjeKupovine()
        {
            Farma f1 = new Farma();
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Proizvod p = new Proizvod("Mlijeko", "Najbolje mlijeko na svijetu", "Vuna", new Zivotinja(ZivotinjskaVrsta.Ovca, DateTime.Now.AddYears(-7), 75, 40, lokacija1), DateTime.Now, DateTime.Now.AddYears(1), 25);
            f1.KupovinaProizvoda(p, DateTime.Now.AddDays(35), 10);
            Kupovina k1 = f1.Kupovine[0];
            f1.BrisanjeKupovine(k1);
            Assert.AreEqual(0, f1.Kupovine.Count);
           
        }


        [TestMethod]
        public void ObaviSistematskiPregled()
        {
            List<string> lista1 = new List<string>() { "Farma1", "Adresa", "31", "Sarajevo", "71000", "Bosna i Hercegovina" };
            Lokacija lokacija1 = new Lokacija(lista1, 50);
            Zivotinja z1 = new Zivotinja(ZivotinjskaVrsta.Koza, DateTime.Now.AddYears(-7), 75, 40, lokacija1);
            Farma f1 = new Farma();
            f1.RadSaZivotinjama("Dodavanje", z1);

            List<string> lista2 = new List<string> { "informacije", "napomena", "5" };
            List<List<string>> lista3 = new List<List<string>>() { lista2 };

            f1.ObaviSistematskiPregled(lista3);


            Assert.IsTrue(f1.Zivotinje[0].Proizvođač);


        }








    }
}