using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Linq;
using hackaton1.Models;

namespace hackaton1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IHostingEnvironment ambiente{get;set;}
        public XDocument Dati { get; set; }
        private string pathToDataFile = "/App_Data/dati.xml";
        public List<Persona> Persone{get;set;}

        public ValuesController( IHostingEnvironment env )
        {
            ambiente = env;
            Dati = XDocument.Load($"{ambiente.ContentRootPath}/{pathToDataFile}");

            Persone = (
                from p in Dati.Element("root").Element("Persone").Elements("Persona")
                select new Persona{ Nome = p.Attribute("Nome").Value } 
            ).ToList<Persona>();

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Blog> Get()
        {
            // Gli array, le List ... sono derivate di IEnumerable...    
            // return Persone;
            //return new string[] { "value1", "value2" };
            var db = new BloggingContext();
            if( db.Blogs.Count() < 10 ) {
                db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                var count = db.SaveChanges();
            }
            else{
                foreach( var r in db.Blogs )
                    db.Blogs.Remove(r);

                db.SaveChanges();
            }
            return db.Blogs.ToList();
        }

        // GET api/values/qualcosa
        [HttpGet("{qualcosa}")]
        public Persona Get(string qualcosa)
        {
            var persone =   from p in Persone
                            where p.Nome == qualcosa
                            select p;
            
            if( persone.Count() > 0 )
                return persone.First();

            return new Persona{ Nome="??" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Persona oggettoDalWeb)
        {
            XElement nuovoElemento = new XElement(
                    "Persona",
                        new XAttribute("Nome", oggettoDalWeb.Nome));
            
            Dati.Element("root").Element("Persone").Add(nuovoElemento);
            Dati.Save($"{ambiente.ContentRootPath}/{pathToDataFile}");
        }

        // PUT1 api/values/id   (senza graffe si aspetta la stringa id...)
        [HttpPut("id")]
        public void Put1([FromBody]Persona value)
        {
            // ... la parola id è un selettore... non una variabile
            // la roba arriva in formato json dal body e viene trasformata 
            // nell' oggetto value di tipo Persona
            
        }

        // PUT2 api/values/5  (con le graffe si aspetta una variabile...)
        [HttpPut("{id}")]
        public void Put2(int id, [FromBody]Persona value)
        {
            // ... id è una variabile intera che arriva dall'url del web...
            // value è "content" che viene spedito nel Body della chiamata
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
