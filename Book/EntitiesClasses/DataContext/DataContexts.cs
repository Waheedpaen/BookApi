


using Microsoft.EntityFrameworkCore;

namespace EntitiesClasses.DataContext;
   public  class DataContexts : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserTypes> UserTypes { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; } 
    public DataContexts(DbContextOptions<DataContexts> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
           
        modelBuilder.Entity<User>().HasQueryFilter((d => EF.Property<bool>(d, "IsDeleted") == false));
 
   
        base.OnModelCreating(modelBuilder);
    }




}
 
