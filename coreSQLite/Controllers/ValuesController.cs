using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace coreSQLite.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            // Accende il db SQLite
            var db = new BloggingContext();

            // Se dentro non trova nulla, crea un Blog a casissimo
            if( db.Blogs.Count() == 0 )
            {
                db.Blogs.Add( new Blog{ Url="http://www.fablabromagna.org" } );
                db.SaveChanges();
            }

            // Prende il primo Blog (l'unico che c'è...)
            Blog b = db.Blogs.Find(1);
            if ( b!= null ){
                if( b.Posts == null)
                    b.Posts = new List<Post>();

                // Se non ci sono Post, ne crea uno a casissimo
                b.Posts.Add( 
                    new Post{
                        Title="School Maker Day 2018",
                        Content="Finalmente anche quest'anno è arrivata la fiera dei Makers della scuola."
                    });
                db.SaveChanges();

                Post p = b.Posts[0];
                return $"Titolo: {p.Title}\nTesto: {p.Content}";
            }

            return "Non trovato!";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
