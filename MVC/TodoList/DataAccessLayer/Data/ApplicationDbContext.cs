using DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<Objective> Objective { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objective>().HasKey(Id => Id.TaskId);
            modelBuilder.Entity<Objective>().Property(a=>a.Title).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Objective>().Property(a => a.Description).HasMaxLength(200);
            modelBuilder.Entity<Objective>().Property(a => a.CompleteByDate).IsRequired();
            modelBuilder.Entity<Objective>().Property(a => a.CreatedDate).IsRequired();
            modelBuilder.Entity<Objective>().Property(a => a.UpdatedDate).IsRequired();
        }
    }
}
