using Microsoft.EntityFrameworkCore;

namespace RequestMaster.Databases.MainDatabase;

public partial class RequestsContext : DbContext
{
    public RequestsContext(){}

    public RequestsContext(DbContextOptions<RequestsContext> options)
        : base(options){}

    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=requests.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_Requests_RequestID").IsUnique();

            entity.Property(e => e.ID).HasColumnName("RequestID");
            entity.Property(e => e.Description).HasColumnName("Description");
            entity.Property(e => e.TelephoneNumber).HasColumnName("TelephoneNumber");
            entity.Property(e => e.Status).HasColumnName("Status");
            entity.Property(e => e.whoCreatedID).HasColumnName("WhoCreatedID");
            entity.Property(e => e.whoClosedID).HasColumnName("WhoClosedID");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserID, "IX_Users_UserID").IsUnique();

            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.Login).HasColumnName("Login");
            entity.Property(e => e.Password).HasColumnName("Password");
            entity.Property(e => e.howManyRequestsCreated).HasColumnName("howManyRequestsCreated");
            entity.Property(e => e.howManyRequestsClosed).HasColumnName("howManyRequestsClosed");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
