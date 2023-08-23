﻿


using Microsoft.EntityFrameworkCore;

namespace EntitiesClasses.DataContext;
   public  class DataContexts : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserTypes> UserTypes { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; } 
    public DbSet<FarqaCategory> FarqaCategories { get; set; }
    public DbSet<Scholar> Scholars { get; set; }    
    public DataContexts(DbContextOptions<DataContexts> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
           
        modelBuilder.Entity<User>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<BookCategory>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<FarqaCategory>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
        modelBuilder.Entity<Scholar>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));


        base.OnModelCreating(modelBuilder);
    }




}
 
