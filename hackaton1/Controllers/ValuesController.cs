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

        public BloggingContext db {get; set;}

        public ValuesController( IHostingEnvironment env )
        {
            // Setta le variabili di ambiente (per poter accedere al file system)
            ambiente = env;
            Dati = XDocument.Load($"{ambiente.ContentRootPath}/{pathToDataFile}");

            // Carica il file XML
            Persone = (
                from p in Dati.Element("root").Element("Persone").Elements("Persona")
                select new Persona{ Nome = p.Attribute("Nome").Value } 
            ).ToList<Persona>();

            // si connette al db
            db = new BloggingContext();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Blog> GetFromDb()
        {    
            return db.Blogs.ToList();
        }        

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Blog oggettoDalWeb)
        {
            db.Blogs.Add( oggettoDalWeb );
            db.SaveChanges();
        }

        // PUT api/values/5  (con le graffe si aspetta una variabile...)
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Blog oggettoDalWeb)
        {            
            var trovato = db.Blogs.Find( id );
            if( trovato != null ) {
                trovato.Posts = oggettoDalWeb.Posts;
                trovato.Url = oggettoDalWeb.Url;
                db.SaveChanges();
            }
        }

        // PATCH api/values/5  (con le graffe si aspetta una variabile...)
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody]Blog oggettoDalWeb)
        {            
            var trovato = db.Blogs.Find( id );
            if( trovato != null ) {
                trovato.Posts = oggettoDalWeb.Posts;
                trovato.Url = oggettoDalWeb.Url;
                db.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var trovato = db.Blogs.Find( id );
            if( trovato != null ) {
                db.Blogs.Remove( trovato );
                db.SaveChanges();
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Blog Get(int id)
        {
            return db.Blogs.Find( id );
        }

        // OPTIONS api/values
        public void Options()
        {        
            
        }


        // 
        // 
        // Da qui in poi si lavora sul file XML
        // 
        // 

        // GET api/values
        //[HttpGet]
        //public IEnumerable<Blog> GetXML()
        //{            
            // if( db.Blogs.Count() < 10 ) {
            //     db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            //     var count = db.SaveChanges();
            // }
            // else
            // {
            //     foreach( var r in db.Blogs )
            //         db.Blogs.Remove(r);

            //     db.SaveChanges();
            // }
        //    return db.Blogs.ToList();
        //
        //}

        // PUT1 api/values/id   (senza graffe si aspetta la stringa id...)
        [HttpPut("id")]
        public void Put1([FromBody]Persona value)
        {
            // ... la parola id è un selettore... non una variabile
            // la roba arriva in formato json dal body e viene trasformata 
            // nell' oggetto value di tipo Persona
            
        }

        // GET api/values/qualcosa
        [HttpGet("{qualcosa}")]
        public Persona GetWithParam(string qualcosa)
        {
            var persone =   from p in Persone
                            where p.Nome == qualcosa
                            select p;
            
            if( persone.Count() > 0 )
                return persone.First();

            return new Persona{ Nome="??" };
        }

    }
}
