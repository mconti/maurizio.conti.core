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

/*      
        // GET api/values
        [HttpGet]
        public Dati Get()
        {
            // Accende il db SQLite
            var db = new FPContext();

            // Se dentro non trova nulla, crea un record
            // giusto per  non avere il db vuoto
             if( db.Datis.Count() == 0 )
            {
                db.Datis.Add( new Dati{ 
                    nome = "Padre", 
                    categoria = "Rifornimento", 
                    datati = DateTime.Now.ToShortDateString(),
                    importo = "-5",
                    descrizione = "Benzina"
                } );
                
                db.SaveChanges();
            }
 
            return db.Datis.Find(1);
        }
*/
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost("postdatitabella")]
        public void Post([FromBody]Dati dati) 
        {
            FPContext db = new FPContext();
            db.Datis.Add( dati );
            db.SaveChanges();
        }

        // GET api/values
        [HttpGet("tab")]
        public IEnumerable<Dati> Get()  
        {
            FPContext db = new FPContext();
            return db.Datis.AsEnumerable();
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
