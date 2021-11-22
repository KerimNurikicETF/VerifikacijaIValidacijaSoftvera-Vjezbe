using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
using ZivotinjskaFarma;

namespace Zadatak3
{
    //Emina Basic

    [TestClass]
    public class LokacijaTestovi
    {
        static IEnumerable<object[]> LokacijaNXML
        {
            get
            {
                return UcitajNeispravnePodatkeXML();
            }
        }
        static IEnumerable<object[]> LokacijaIXML
        {
            get
            {
                return UcitajispravnePodatkeXML();
            }
        }

        [TestMethod]
        [DynamicData("LokacijaNXML")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNeispravniPodaci(List<string> parametri, double povrsina)
        {
            Lokacija l = new Lokacija(parametri, povrsina);
        }

        [TestMethod]
        [DynamicData("LokacijaIXML")]
        public void TestIspravniPodaci(List<string> parametri, double povrsina)
        {
            Lokacija l = new Lokacija(parametri, povrsina);
            Assert.AreEqual(l.Grad, "Sarajevo");
            Assert.AreEqual(l.Naziv, "Farmica");
            Assert.IsTrue(l.PoštanskiBroj == 71000);
            Assert.IsTrue(l.Površina > 0);
            Assert.AreEqual(l.Država, "Bosna i Hercegovina");
            Assert.IsTrue(l.Adresa == "Omladinsko šetalište");
            Assert.IsTrue(l.BrojUlice == 5);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test1Setteri()
        {
            Lokacija l = new Lokacija(new List<string>{ "Farmica", "Omladinsko šetalište", "5", "Sarajevo", "71000", "Bosna i Hercegovina"}, 50);
            l.Naziv = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test2Setteri()
        {
            Lokacija l = new Lokacija(new List<string> { "Farmica", "Omladinsko šetalište", "5", "Sarajevo", "71000", "Bosna i Hercegovina" }, 50);
            l.Adresa = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test3Setteri()
        {
            Lokacija l = new Lokacija(new List<string> { "Farmica", "Omladinsko šetalište", "5", "Sarajevo", "71000", "Bosna i Hercegovina" }, 50);
            l.BrojUlice = -40;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test4Setteri()
        {
            Lokacija l = new Lokacija(new List<string> { "Farmica", "Omladinsko šetalište", "5", "Sarajevo", "71000", "Bosna i Hercegovina" }, 50);
            l.Površina = -2000;
        }

        public static IEnumerable<object[]> UcitajNeispravnePodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("LokacijaNeispravniPodaci.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                List<string> parametri = new List<string>();
                for(int i = 0; i < elements.Count - 1; i++)
                {
                    parametri.Add(elements[i]);
                }
                yield return new object[] { parametri, Convert.ToDouble(elements[elements.Count-1]) };
            }
        }

        public static IEnumerable<object[]> UcitajispravnePodatkeXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("LokacijaIspravniPodaci.xml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                List<string> parametri = new List<string>();
                for (int i = 0; i < elements.Count - 1; i++)
                {
                    parametri.Add(elements[i]);
                }
                yield return new object[] { parametri, Convert.ToDouble(elements[elements.Count - 1]) };
            }
        }
    }
}
