using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DAL.Model;

namespace TodoList.DAL.Data
{
    public class ObjectiveDbContext : DbContext
    {
        public ObjectiveDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Objective> Objectives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objective>()
                .HasKey(a => a.Id);



            modelBuilder.Entity<Objective>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);



            modelBuilder.Entity<Objective>()
                .Property(p => p.Description)
                .HasMaxLength(500);



            modelBuilder.Entity<Objective>()
                .Property(p => p.CompleteByDate)
                .IsRequired();



            modelBuilder.Entity<Objective>()
                .Property(p => p.CreatedDate)
                .IsRequired();
        }
    }
}
