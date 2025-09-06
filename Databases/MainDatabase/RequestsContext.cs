using Microsoft.EntityFrameworkCore;

namespace RequestMaster.Databases.MainDatabase;

public partial class RequestsContext : DbContext
{
    public RequestsContext(){}

    public RequestsContext(DbContextOptions<RequestsContext> options)
        : base(options){}

    public virtual DbSet<Request> Requests { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=requests.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasIndex(e => e.RequestID, "IX_Requests_RequestID").IsUnique();

            entity.Property(e => e.RequestID).HasColumnName("RequestID");
            entity.Property(e => e.Description).HasColumnName("Description");
            entity.Property(e => e.TelephoneNumber).HasColumnName("TelephoneNumber");
            entity.Property(e => e.Status).HasColumnName("Status");
            entity.Property(e => e.WhoCreatedID).HasColumnName("WhoCreatedID");
            entity.Property(e => e.WhoClosedID).HasColumnName("WhoClosedID");

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserID, "IX_Users_UserID").IsUnique();

            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.Email).HasColumnName("Email");
            entity.Property(e => e.Login).HasColumnName("Login");
            entity.Property(e => e.Password).HasColumnName("Password");
            entity.Property(e => e.Theme).HasColumnName("Theme");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.CommentID, "IX_Comments_CommentID").IsUnique();

            entity.HasOne(r => r.Request).WithMany(c => c.Comments).HasForeignKey(k => k.RequestID);

            entity.Property(e => e.CommentID).HasColumnName("CommentID");
            entity.Property(e => e.Content).HasColumnName("Content");
            entity.Property(e => e.WhoCreated).HasColumnName("WhoCreated");
            entity.Property(e => e.DateTimeOfCreating).HasColumnName("DateTimeOfCreating");
            entity.Property(e => e.RequestID).HasColumnName("RequestID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
