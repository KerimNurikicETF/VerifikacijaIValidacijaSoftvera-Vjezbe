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
    public class ProizvodTest //Kerim Nurikic
    {
        static Lokacija lokacija = new Lokacija(new List<string> { "Farmica", "Omladinsko šetalište", "5", "Sarajevo", "71000", "Bosna i Hercegovina" }, 50);
        static Zivotinja zivotinja = new Zivotinja(ZivotinjskaVrsta.Krava,DateTime.Parse("01/01/2000"),15,100,lokacija);
        static IEnumerable<object[]> IspravanProizvod
        {
            get
            {
                return UcitajPodatkeXML("IspravniProizvodi.xml");
            }
        }

        static IEnumerable<object[]> NeispravniCSV
        {
            get
            {
                return UcitajPodatkeCSV();
            }
        }
        [TestMethod]
        [DynamicData("IspravanProizvod")]
        public void IspravniProizvodi(string ime,string opis, string vrsta, DateTime proizvodnja, DateTime rok, int kol)
        {
            Proizvod p = new Proizvod(ime,opis,vrsta,zivotinja,proizvodnja,rok,kol);
            Assert.AreEqual(p.Vrsta,vrsta);
            Assert.AreEqual(p.Proizvođač, zivotinja);
            Assert.AreEqual(p.DatumProizvodnje, proizvodnja);
            Assert.AreEqual(p.RokTrajanja, rok);
            Assert.AreEqual(p.KoličinaNaStanju, kol);

        }

        [TestMethod]
        [DynamicData("NeispravniCSV")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NeispravniProizvodi(string ime, string opis, string vrsta, DateTime proizvodnja, DateTime rok, int kol)
        {
            Proizvod p = new Proizvod(ime, opis, vrsta, zivotinja, proizvodnja, rok, kol);
        }



        public static IEnumerable<object[]> UcitajPodatkeXML(string xml)
        {
            XmlDocument proizvodiDoc = new XmlDocument();
            proizvodiDoc.Load(xml);
            foreach (XmlNode node in proizvodiDoc.DocumentElement.ChildNodes)
            {
                List<string> proizvodi = new List<string>();
                foreach (XmlNode innerNode in node)
                {
                    proizvodi.Add(innerNode.InnerText);
                }
                yield return new object[] {proizvodi[0],proizvodi[1],proizvodi[2], DateTime.Parse(proizvodi[3]),DateTime.Parse(proizvodi[4]),Convert.ToInt32(proizvodi[5])};
            }
        }

        public static IEnumerable<object[]> UcitajPodatkeCSV()
        {
            using (var reader = new StreamReader("NeispravniProizvodi.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var rows = csv.GetRecords<dynamic>();
                foreach (var row in rows)
                {
                    var values = ((IDictionary<String, Object>)row).Values;
                    var proizvodi = values.Select(elem => elem.ToString()).ToList();
                    yield return new object[] { proizvodi[0], proizvodi[1], proizvodi[2], DateTime.Parse(proizvodi[3]), DateTime.Parse(proizvodi[4]), Convert.ToInt32(proizvodi[5]) }; ;
                }
            }
        }

    }
}
