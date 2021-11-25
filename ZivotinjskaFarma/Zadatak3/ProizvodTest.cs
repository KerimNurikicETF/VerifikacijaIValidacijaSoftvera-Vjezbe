using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ZivotinjskaFarma;

namespace Zadatak3 
{
    [TestClass]
    public class ProizvodTest //Kerim Nurikic
    {
        static IEnumerable<object[]> IspravanProizvod
        {
            get
            {
                return UcitajPodatkeXML("IspravniProizvodi.xml");
            }
        }

        static IEnumerable<object[]> NeispravniXML
        {
            get
            {
                return UcitajPodatkeXML("NeispravniProizvodi.xml");
            }
        }
        [TestMethod]
        [DynamicData("IspravanProizvod")]
        public void IspravniProizvodi(string ime,string opis, string vrsta, Zivotinja proizvodjac, DateTime proizvodnja, DateTime rok, int kol)
        {
            Proizvod p = new Proizvod(ime,opis,vrsta,proizvodjac,proizvodnja,rok,kol);
            Assert.AreEqual(p.Vrsta,vrsta);
            Assert.AreEqual(p.Proizvođač, proizvodjac);
            Assert.AreEqual(p.DatumProizvodnje, proizvodnja);
            Assert.AreEqual(p.RokTrajanja, rok);
            Assert.AreEqual(p.KoličinaNaStanju, kol);

        }

        [TestMethod]
        [DynamicData("NeispravniXML")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NeispravniProizvodi(string ime, string opis, string vrsta, Zivotinja proizvodjac, DateTime proizvodnja, DateTime rok, int kol)
        {
            Proizvod p = new Proizvod(ime, opis, vrsta, proizvodjac, proizvodnja, rok, kol);
        }



        public static IEnumerable<object[]> UcitajPodatkeXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ZivotinjaPodaci.xml");

            XmlDocument docLokacija = new XmlDocument();
            docLokacija.Load("LokacijaIspravniPodaci.xml");
            List<string> elementsLokacija = new List<string>();
            foreach (XmlNode innerNode in docLokacija.DocumentElement.ChildNodes[0])
            {
                elementsLokacija.Add(innerNode.InnerText);
            }

            List<string> parametri = new List<string>();
            for (int i = 0; i < elementsLokacija.Count - 1; i++)
            {
                parametri.Add(elementsLokacija[i]);
            }

            List<string> elements = new List<string>();
            foreach (XmlNode innerNode in doc.DocumentElement.ChildNodes[0])
            {
                elements.Add(innerNode.InnerText);
            }
            ZivotinjskaVrsta vrsta;
            Enum.TryParse(elements[0], out vrsta);

            XmlDocument proizvodiDoc = new XmlDocument();
            proizvodiDoc.Load(xml);
            foreach (XmlNode node in proizvodiDoc.DocumentElement.ChildNodes)
            {
                List<string> proizvodi = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    proizvodi.Add(innerNode.InnerText);
                }
                yield return new object[] {proizvodi[0],proizvodi[1],proizvodi[2],
                    new Zivotinja(ZivotinjskaVrsta.Krava, DateTime.Parse(elements[1]),Convert.ToDouble(elements[2]),Convert.ToDouble(elements[3]),
                    new Lokacija(parametri,Convert.ToDouble(elementsLokacija[elementsLokacija.Count - 1]))),
                    DateTime.Parse(proizvodi[3]),DateTime.Parse(proizvodi[4]),Convert.ToInt32(proizvodi[5])};
            }
        }
    }
}
