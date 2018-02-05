using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;

using System.IO;
using System.IO.Compression;

namespace streams
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleWebClient wcl = new SimpleWebClient(@"http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");


            bool status = true;
            string msg = null;

            string data = wcl.getData(out status, out msg);

            FileOP files = new FileOP(@"files");
            string fileName = files.saveFile("data", data, "xml", out status, out msg);

            if (!status)
            {
                Console.WriteLine("Pri ukladani suboru sa vyskytla chyba: {0}",msg);
                Console.ReadKey();
                return;
            }

            string zipFile = files.compressFile(fileName, out status, out msg);

            if (!status)
            {
                Console.WriteLine("Pri komprimovani suboru sa vyskytla chyba: {0}", msg);
                Console.ReadKey();
                return;
            }


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);

            XmlNamespaceManager nms = new XmlNamespaceManager(xmlDoc.NameTable);
            nms.AddNamespace("foo", "http://www.ecb.int/vocabulary/2002-08-01/eurofxref");

            XmlNodeList nodes = xmlDoc.SelectNodes("//foo:Cube[@currency]",nms);
            
            foreach (XmlNode nd in nodes)
            {
                Console.WriteLine("Currency: {0} Rate: {1}", nd.Attributes["currency"].Value.ToString(), nd.Attributes["rate"].Value.ToString());
            }



          /*  XmlSerializer XmlSer = new XmlSerializer(typeof(Gesmes));

            using (StreamReader sr = new StreamReader(data))
            {
                Gesmes Gesmes = (Gesmes)XmlSer.Deserialize(sr);
            }

            nasledne by sa stym dalo pracovat, ale pretoze c# generator z XML to spravi dost skaredo je to nutne 
            este poupratovat, principialne to viem spravit, pretoze vramci eZdravia som to robil, ale je to dost
            pracne, btw vtedy som to robil cez XSD utilitu, ale vysledok je ten isty, proste on vnorene elementy
            da camel casom napr .tu by to bolo class CubeCubeCube. 
            Som lenivy, ale pretoze som na tom zabil tyzden studovania viem to spravit ale je to pracne.. Takze principialne
            Serializacia a Deserializacia tried do XML sa tiez robi cez using :)

                
          */  
                          

            Console.WriteLine("Hura");
            Console.ReadKey();





        }
    }
}
