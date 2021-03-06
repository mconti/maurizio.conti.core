using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace hackaton1.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
            //optionsBuilder.UseSqlite("Data Source=bin/Debug/netcoreapp2.0/blogging.db");
            optionsBuilder.UseSqlite("Data Source=App_Data/blogging.db");
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
        public Blog()
        {
            BlogId = 0;
            Url = "non trovato!";
            Posts = new List<Post>();
        }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}