using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;

namespace OprawaObrazowWebApi;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext() { }
    
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options){}
    
    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Frame> Frames { get; set; }

    public virtual DbSet<FramePiece> FramePieces { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:OprawaDB");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pk");

            entity.ToTable("clients", "oprawa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .HasColumnName("email_address");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("deliveries_pk");

            entity.ToTable("deliveries", "oprawa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateDue).HasColumnName("date_due");
            entity.Property(e => e.IsDelivered).HasColumnName("is_delivered");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deliveries_suppliers_fk");
        });

        modelBuilder.Entity<Frame>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("frame_pk");

            entity.ToTable("frames", "oprawa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.HasDecoration).HasColumnName("has_decoration");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Frames)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("frame_suppliers_id_fk");
        });

        modelBuilder.Entity<FramePiece>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("frame_pieces_pk");

            entity.ToTable("frame_pieces", "oprawa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.FrameId).HasColumnName("frame_id");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(10)
                .HasColumnName("storage_location");
            entity.Property(e => e.IsDamaged).HasColumnName("is_damaged");
            entity.Property(e => e.IsInStock).HasColumnName("is_in_stock");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Delivery).WithMany(p => p.FramePieces)
                .HasForeignKey(d => d.DeliveryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("frame_pieces_deliveries_fk");

            entity.HasOne(d => d.Frame).WithMany(p => p.FramePieces)
                .HasForeignKey(d => d.FrameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("frame_pieces_frame_id_fk");

            entity.HasOne(d => d.Order).WithMany(p => p.FramePieces)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("frame_pieces_orders_id_fk");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pk");

            entity.ToTable("orders", "oprawa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DateDue).HasColumnName("date_due");
            entity.Property(e => e.IsClosed).HasColumnName("is_closed");
            entity.Property(e => e.IsFinished).HasColumnName("is_finished");
            entity.Property(e => e.PictureHeight).HasColumnName("picture_height");
            entity.Property(e => e.PictureWidth).HasColumnName("picture_width");
            entity.Property(e => e.PlannedDate).HasColumnName("planned_date");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_clients_id_fk");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suppliers_pk");

            entity.ToTable("suppliers", "oprawa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .HasColumnName("email_address");
            entity.Property(e => e.LastSupply).HasColumnName("last_supply");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users", "oprawa");
            
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}