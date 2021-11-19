﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZivotinjskaFarma
{
    public class Farma
    {
        #region Atributi

        List<Zivotinja> zivotinje;
        List<Lokacija> lokacije;
        List<Proizvod> proizvodi;
        List<Kupovina> kupovine;

        #endregion

        #region Properties

        public List<Zivotinja> Zivotinje { get => zivotinje; }
        public List<Lokacija> Lokacije { get => lokacije; }
        public List<Proizvod> Proizvodi { get => proizvodi; set => proizvodi = value; }
        public List<Kupovina> Kupovine { get => kupovine; }

        #endregion

        #region Konstruktor

        public Farma()
        {
            zivotinje = new List<Zivotinja>();
            lokacije = new List<Lokacija>();
            proizvodi = new List<Proizvod>();
            kupovine = new List<Kupovina>();
        }

        #endregion

        #region Metode

        public void RadSaZivotinjama(string opcija, Zivotinja zivotinja)
        {
            Zivotinja postojeca = zivotinje.Find(z => z.ID1 == zivotinja.ID1);

            if (opcija == "Dodavanje" && postojeca == null)
                zivotinje.Add(zivotinja);
            else if (opcija == "Izmjena" && postojeca != null)
            {
                zivotinje.Remove(postojeca);
                zivotinje.Add(zivotinja);
            }
            else if (opcija == "Brisanje" && postojeca != null)
                zivotinje.Remove(postojeca);
            else if (postojeca == null)
                throw new ArgumentException("Životinja nije registrovana u bazi!");
            else
                throw new ArgumentException("Životinja je već registrovana u bazi!");
        }

        public void DodavanjeNoveLokacije(Lokacija lokacija)
        {
            if (lokacije.Any(l => l.Grad == lokacija.Grad && l.Adresa == lokacija.Adresa
                        && l.BrojUlice == lokacija.BrojUlice))
                throw new InvalidOperationException("Ista lokacija je već zabilježena!");
            lokacije.Add(lokacija);
        }

        public bool BrisanjeLokacije(Lokacija lokacija)
        {
            return lokacije.Remove(lokacija);
        }

        public void SpecijalizacijaFarme(ZivotinjskaVrsta vrsta, int brojGrla)
        {
            zivotinje.Clear();
            lokacije.Clear();
            for (int i = 0; i < brojGrla; i++)
            {
                if (i % 25 == 0)
                {

                }
                if (i % 4 == 0)
                {
                    zivotinje.Add(new Zivotinja(vrsta, new DateTime(2010, 1, 1), 25, 10,
                        new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Split", "21000", "Bosna i Hercegovina" }, 25.22)));
                    lokacije.Add(new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Split", "21000", "Bosna i Hercegovina" },25));
                }
                else
                {
                    zivotinje.Add(new Zivotinja(vrsta, new DateTime(2010, 1, 1), 25, 10,
                        new Lokacija(new List<string>() { "Velika štala", "Seoski put", "12", "Split", "21000", "Hrvatska" }, 25.22)));
                }

            }
        }

        public bool KupovinaProizvoda(Proizvod p, DateTime rok, int količina)
        {
            bool popust = Praznik(DateTime.Now);
            int id = Kupovina.DajSljedeciBroj();
            Kupovina kupovina = new Kupovina(id.ToString(), DateTime.Now, rok, p, količina, popust);
            if (!kupovina.VerificirajKupovinu())
                return false;
            else
            {
                kupovine.Add(kupovina);
                return true;
            }
        }

        public void BrisanjeKupovine(Kupovina kupovina)
        {
            if (kupovine.Contains(kupovina))
                kupovine.Remove(kupovina);
        }

        public void ObaviSistematskiPregled(List<List<string>> informacije)
        {
            int i = 0;
            foreach (var zivotinja in zivotinje)
            {
                zivotinja.PregledajZivotinju(informacije[i].ElementAt(0), informacije[i].ElementAt(1), informacije[i].ElementAt(2));
                zivotinja.ProvjeriStanjeZivotinje();
                i++;
            }
        }

        public void ObaviVeterinarskiPregled(IVeterinar v)
        {
            List<Zivotinja> sveZivotinjeZaVeterinara = zivotinje.FindAll(z => z.Proizvođač == false);

            foreach (Zivotinja zivotinja in sveZivotinjeZaVeterinara)
            {
                if (v.ocjenaZdravstvenogStanjaZivotinje(zivotinja) > 4)
                {
                    zivotinja.Proizvođač = true;
                }
                else if (v.ocjenaZdravstvenogStanjaZivotinje(zivotinja) > 3)
                {
                    zivotinja.PregledajZivotinju("Veterinarski pregled", "Životinja nije bila proizvođač", v.ocjenaZdravstvenogStanjaZivotinje(zivotinja).ToString());
                }
            }
        }

        public static bool Praznik(DateTime datum)
        {
            List<List<int>> praznici = new List<List<int>>()
            {
                new List<int>() { 01, 01 },
                new List<int>() { 01, 03 },
                new List<int>() { 01, 05 },
                new List<int>() { 25, 11 },
                new List<int>() { 31, 12 }
            };

            List<int> dan = new List<int>()
            { datum.Day, datum.Month };

            return praznici.Find(datum => datum[0] == dan[0] && datum[1] == dan[1]) != null;
        }

        /// <summary>
        /// Metoda koja vrši obračun ukupnog poreza na imovinu.
        /// Osnovica za porez iznosi 10 KM.
        /// Na sve lokacije sa površinom većom od 10,000 m2 plaća se porez od 2%.
        /// Na lokacije sa površinom između 1,000 m2 i 10,000 m2 plaća se porez od 1.5%
        /// ukoliko se nalaze u Bosni i Hercegovini, a u suprotnom se plaća porez od 5%.
        /// Na sve lokacije s površinom manjom od 1,000 m2 plaća se porez od 1%
        /// ukoliko se nalaze u Sarajevu, Banja Luci, Tuzli ili Mostaru, a u suprotnom
        /// se plaća porez od 3%.
        /// Potrebno je sabrati iznose poreza za sve lokacije (osnovica x koeficijent postotka)
        /// i vratiti konačni rezultat.
        /// Ukoliko nije definisana nijedna lokacija, potrebno je vratiti iznos od 0 KM.
        /// </summary>
        /// <returns></returns>
        /// metodu implementirao Kerim Nurikic
        public double ObračunajPorez()
        {
            int osnovica = 10;
            if (lokacije.Count == 0)
            {
                return 0;
            }
            double porez = 0;
            float koeficijentPoreza = 0;
            foreach (Lokacija lokacija in lokacije)
            {
                koeficijentPoreza = 0;
                if (lokacija.Površina > 10000)
                {
                    koeficijentPoreza += 0.2f;
                }
                if (lokacija.Površina >= 1000 && lokacija.Površina <= 10000)
                {
                    if (lokacija.Država == "Bosna i Hercegovina")
                    {
                        koeficijentPoreza = koeficijentPoreza + 0.15f;
                    }
                    else
                    {
                        koeficijentPoreza += 0.5f;
                    }

                }
                if (lokacija.Površina < 1000)
                {
                    if (new string[] { "Sarajevo", "Tuzla", "Zenica", "Mostar" }.Contains(lokacija.Grad))
                    {
                        koeficijentPoreza += 0.1f;
                    }
                    else
                    {
                        koeficijentPoreza += 0.3f;
                    }
                }
                porez += osnovica * koeficijentPoreza;
            }
            return porez;
        }

        #endregion
    }
}
