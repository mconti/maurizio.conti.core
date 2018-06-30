
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace coreSQLite
{   
    public class FPContext : DbContext
    {
        // public FPContext (DbContextOptions<FPContext> options)
        //     : base(options)
        // {
        // }

        public DbSet<Dati> Datis { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bin/Debug/netcoreapp2.0/FamilyPurchase.db");
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
        }       
    }


    public class Dati
    {
        public int DatiId{ get; set; }
        public string nome { get; set; }
        public string categoria { get; set; }
        public string datati { get; set; }
        public string importo { get; set; }
        public string descrizione { get; set; }
    }

    public class User
    {
        public int UserId{ get; set; }
        public string utente { get; set; }
        public string password { get; set; }        
    }

    // public class UserSon
    // {
    //     public string dad { get; set; }
    //     public string utenteSon { get; set; }
    //     public string passwordSon { get; set; }
    // }

    public class UserLog
    {
        public int UserLogId{ get; set; }
        public string utentelog { get; set; }
    }
    
}
