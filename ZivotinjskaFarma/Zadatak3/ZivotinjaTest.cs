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
    public class ZivotinjaTest //Kerim Nurikic
    {
        static IEnumerable<object[]> ZivotinjaXML
        {
            get
            {
                return UcitajPodatkeXML("ZivotinjaPodaci.xml");
            }
        }

        static IEnumerable<object[]> ZivotinjaNeispravniXML
        {
            get
            {
                return UcitajPodatkeXML("NeispravneZivotinje.xml");
            }
        }

        [TestMethod]
        [DynamicData("ZivotinjaXML")]
        public void IspravniPodaci(ZivotinjskaVrsta vrsta, DateTime starost, double masa, double visina, Lokacija lokacija)
        {
            Zivotinja z1 = new Zivotinja(vrsta, starost, masa, visina, lokacija);
            Assert.AreEqual(z1.Starost, starost);
            Assert.AreEqual(z1.TjelesnaMasa, masa);
            Assert.AreEqual(z1.Visina, visina);
            Assert.AreEqual(z1.Pregledi.Count, 0);
        }

        [TestMethod]
        [DynamicData("ZivotinjaNeispravniXML")]
        [ExpectedException(typeof(FormatException))]
        public void NeispravniPodaci(ZivotinjskaVrsta vrsta, DateTime starost, double masa, double visina, Lokacija lokacija)
        {
            Zivotinja zivotinja = new Zivotinja(vrsta, starost, masa, visina, lokacija);
        }


        public static IEnumerable<object[]> UcitajPodatkeXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xml);

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

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                ZivotinjskaVrsta vrsta;
                Enum.TryParse(elements[0], out vrsta);

                yield return new object[] {vrsta, DateTime.Parse(elements[1]),Convert.ToDouble(elements[2]),Convert.ToDouble(elements[3]),
                    new Lokacija(parametri,Convert.ToDouble(elementsLokacija[elementsLokacija.Count - 1]))};
            }
        }

    }
}
