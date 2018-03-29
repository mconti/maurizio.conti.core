using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace jquery.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IHostingEnvironment _env;
        private string pathToDataFile = "/App_Data/dati.xml";

        public ValuesController( IHostingEnvironment env )
        {
            this._env = env;
        }

        // GET api/values
        [HttpGet()]
        public IEnumerable<Temperatura> GetAll()
        {
            try {
                // Carico il file XML
                XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

                // prelevo quello di indice idx
                var temperature = from t in Dati.Element("root").Elements("Temperatura")
                select new Temperatura{ Valore = Convert.ToDouble(t.Attribute("Valore").Value)};
                
                return temperature;
            }
            catch
            {
                return new List<Temperatura>();
            }
        }

        // GET api/values/2
        [HttpGet("{idx}")]
        public Temperatura GetVal(int idx)
        {
            try {
                // Carico il file XML
                XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

                // prelevo quello di indice idx
                var elementi = Dati.Element("root").Elements("Temperatura");
                var elemento = elementi.ElementAtOrDefault(idx);
                return new Temperatura{ Valore = Convert.ToDouble(elemento.Attribute("Valore").Value) };
            }
            catch
            {
                return new Temperatura{ Valore = 0 };
            }
        }

        // GET api/values/count
        [HttpGet("count")]
        public int GetCount()
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

            // prelevo il piu grande
            var elementi = Dati.Element("root").Elements("Temperatura");
            return elementi.Count();
        }

        // GET api/values/max
        [HttpGet("max")]
        public Temperatura GetMax()
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

            // prelevo il piu grande
            var elementi = Dati.Element("root").Elements("Temperatura");
            var tVal =  elementi.Max( t => Convert.ToDouble( t.Attribute("Valore").Value ) );
            return new Temperatura { Valore=tVal };
        }

        // GET api/values/min
        [HttpGet("min")]
        public Temperatura GetMin()
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

            // prelevo il più piccolo
            var elementi = Dati.Element("root").Elements("Temperatura");
            var tVal =  elementi.Min( t => Convert.ToDouble( t.Attribute("Valore").Value ) );
            return new Temperatura { Valore = tVal };
        }

        // POST api/values
        [HttpPost()]
        public void Post([FromBody]Temperatura t )
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

            XElement nuovoElemento = new XElement(
                    "Temperatura",
                        new XAttribute("Valore", t.Valore));
            
            Dati.Elements("root").First().Add(nuovoElemento);
            Dati.Save($"{_env.ContentRootPath}/{pathToDataFile}");
        }

        // PUT api/values/5
        [HttpPut("{idx}")]
        public void Put(int idx, [FromBody]Temperatura t)
        {
            try {
                // Carico il file XML
                XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

                // prelevo quello di indice idx
                var elementi = Dati.Element("root").Elements("Temperatura");
                var elemento = elementi.ElementAtOrDefault(idx);
                elemento.Attribute("Valore").Value = t.Valore.ToString();
                Dati.Save($"{_env.ContentRootPath}/{pathToDataFile}");
            }
            catch{}
        }

        // DELETE api/values/5
        [HttpDelete("{idx}")]
        public void Delete(int idx)
        {
            try {
                // Carico il file XML
                XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

                // prelevo quello di indice idx
                var elementi = Dati.Element("root").Elements("Temperatura");
                var elemento = elementi.ElementAtOrDefault(idx);
                elemento.Remove();
                Dati.Save($"{_env.ContentRootPath}/{pathToDataFile}");
            }
            catch{}
        }
    }

    public class Temperatura{
        public double Valore {get;set;}
    }
}
