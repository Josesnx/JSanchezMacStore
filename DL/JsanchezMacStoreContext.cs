using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JsanchezMacStoreContext : DbContext
{
    public JsanchezMacStoreContext()
    {
    }

    public JsanchezMacStoreContext(DbContextOptions<JsanchezMacStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.; Database= JSanchezMacStore; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__USUARIO__98242AA9D610824F");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");
            entity.Property(e => e.Apellidomaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDOMATERNO");
            entity.Property(e => e.Apellidopaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDOPATERNO");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CONTRASEÑA");
            entity.Property(e => e.Edad).HasColumnName("EDAD");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
