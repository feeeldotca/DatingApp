using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // A DbContext instance represents a session with the database and can be used to query 
    // and save instances of your entities. DbContext is a combination of the Unit Of Work 
    // and Repository patterns.
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        // A DbSet<TEntity> can be used to query and save instances of <AppUser> 
        // LINQ queries against a DbSet<TEntity> will be translated into queries against the database.
        public DbSet<AppUser> Users { get; set; }
    }
}