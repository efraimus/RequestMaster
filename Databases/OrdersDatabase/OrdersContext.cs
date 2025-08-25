using Microsoft.EntityFrameworkCore;

namespace OrdersApp.Databases.OrdersDatabase;

public partial class OrdersContext : DbContext
{
    public OrdersContext()
    {
    }

    public OrdersContext(DbContextOptions<OrdersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=Orders.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_Orders_ID").IsUnique();

            entity.HasOne(u => u.Employer).WithOne(o => o.Order).HasForeignKey<User>(u => u.OrderID);

            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.Payment).HasColumnName("Payment");
            entity.Property(e => e.Description).HasColumnName("Description");
            entity.Property(e => e.TelephoneNumber).HasColumnName("TelephoneNumber");
            entity.Property(e => e.CountOfPeopleNeeded).HasColumnName("CountOfPeopleNeeded");
            entity.Property(e => e.Status).HasColumnName("Status");
            entity.Property(e => e.EmployerID).HasColumnName("EmployerID");
            entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
            entity.Property(e => e.WhoConfirmedCompletion).HasColumnName("WhoConfirmedCompletion");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Users_userID").IsUnique();

            entity.HasOne(o => o.Order).WithOne(e => e.Employer).HasForeignKey<Order>(o => o.EmployerID);

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Balance).HasColumnName("Balance");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.Login).HasColumnName("Login");
            entity.Property(e => e.Password).HasColumnName("Password");
            entity.Property(e => e.OrderID).HasColumnName("OrderID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
