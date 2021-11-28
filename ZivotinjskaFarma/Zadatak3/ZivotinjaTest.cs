using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        static Lokacija lokacija = new Lokacija(new List<string>{"Farmica", "Omladinsko šetalište", "5", "Sarajevo", "71000", "Bosna i Hercegovina"},50);
        static IEnumerable<object[]> ZivotinjaXML
        {
            get
            {
                return UcitajPodatkeXML("ZivotinjaPodaci.xml");
            }
        }

        static IEnumerable<object[]> ZivotinjaNeispravniCSV
        {
            get
            {
                return UcitajPodatkeCSV();
            }
        }

        [TestMethod]
        [DynamicData("ZivotinjaXML")]
        public void IspravniPodaci(ZivotinjskaVrsta vrsta, DateTime starost, double masa, double visina)
        {
            Zivotinja z1 = new Zivotinja(vrsta, starost, masa, visina, lokacija);
            Assert.AreEqual(z1.Starost, starost);
            Assert.AreEqual(z1.TjelesnaMasa, masa);
            Assert.AreEqual(z1.Visina, visina);
            Assert.AreEqual(z1.Pregledi.Count, 0);
        }

        [TestMethod]
        [DynamicData("ZivotinjaNeispravniCSV")]
        [ExpectedException(typeof(FormatException))]
        public void NeispravniPodaci(ZivotinjskaVrsta vrsta, DateTime starost, double masa, double visina)
        {
            Zivotinja zivotinja = new Zivotinja(vrsta, starost, masa, visina, lokacija);
        }


        public static IEnumerable<object[]> UcitajPodatkeXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xml);

            XmlDocument docLokacija = new XmlDocument(); 

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                List<string> elements = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    elements.Add(innerNode.InnerText);
                }
                ZivotinjskaVrsta vrsta;
                Enum.TryParse(elements[0], out vrsta);

                yield return new object[] {vrsta, DateTime.Parse(elements[1]),Convert.ToDouble(elements[2]),Convert.ToDouble(elements[3])};
            }
        }

        public static IEnumerable<object[]> UcitajPodatkeCSV()
        {
            using (var reader = new StreamReader("NeispravneZivotinje.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var elements = values.Select(elem => elem.ToString()).ToList();
                    ZivotinjskaVrsta vrsta;
                    Enum.TryParse(elements[0], out vrsta);
                    yield return new object[] { vrsta, DateTime.Parse(elements[1]),
                    Convert.ToDouble(elements[2]), Convert.ToDouble(elements[3])};
                }
            }
        }

    }
}
