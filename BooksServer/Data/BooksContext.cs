using BooksServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksServer.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
           
        }

        public DbSet<Title> Titles { get; set; }
        
        //public DbSet<AuthorsISBN> AuthorsISBN { get; set; }
        //public DbSet<Author> Authors { get; set; }
    }
}
