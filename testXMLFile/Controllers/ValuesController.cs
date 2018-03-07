using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace testXMLFile.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IHostingEnvironment _env;
        private string pathToDataFile = "/Data/dati.xml";

        public ValuesController( IHostingEnvironment env )
        {
            this._env = env;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<XElement> Get()
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");

            // prelevo il primo elemento e accedo al suo attributo "Nome"
            string str = Dati.Element("root").Element("persona").Attribute("nome").Value;

            // accedo a tutti gli elementi "persona"
            // Notare che il motore di conversione verso json, converte tutto l'XML in json!!!
            return Dati.Element("root").Elements("persona");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");
            var studente =  from s in Dati.Element("root").Elements("persona")
                            where s.Attribute("id").Value == id.ToString()
                            select s;
            
            if(studente.Count() > 0)
                return studente.First().Attribute("nome").Value;
            else
                return "non trovato elemento " + id.ToString();
        }

        // POST api/values
        [HttpPost("{id}")]
        public void Post(int id, [FromBody]Studente value)
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");
            var studente =  from s in Dati.Element("root").Elements("persona")
                            where s.Attribute("id").Value == id.ToString()
                            select s;
            
            if(studente.Count() == 0)
            {
                // Aggiungo un elemento "persona"...
                Dati.Element("root").Add
                (
                    new XElement("persona",
                        new XAttribute("id", id ),
                        new XAttribute("nome", value.Nome ),
                        new XAttribute("cognome", value.Cognome ))
                );

                // lo salvo...
                Dati.Save($"{_env.ContentRootPath}/{pathToDataFile}");
            }
       }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Studente value)
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");
            var studente =  from s in Dati.Element("root").Elements("persona")
                            where s.Attribute("id").Value == id.ToString()
                            select s;
            
            if(studente.Count() > 0)
            {
                // Modifico un elemento "persona"...
                studente.First().Attribute("nome").Value = value.Nome;
                studente.First().Attribute("cognome").Value = value.Cognome;

                // lo salvo...
                Dati.Save($"{_env.ContentRootPath}/{pathToDataFile}");
            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Carico il file XML
            XDocument Dati = XDocument.Load($"{_env.ContentRootPath}/{pathToDataFile}");
            var studente = from s in Dati.Element("root").Elements("persona")
                           where s.Attribute("id").Value == id.ToString()
                           select s;

            if (studente.Count() > 0)
                studente.First().Remove();

            // lo salvo...
            Dati.Save($"{_env.ContentRootPath}/{pathToDataFile}");
        }
    }

    public class Studente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }
}
