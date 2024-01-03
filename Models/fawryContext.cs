using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CashHub.Models
{
    public partial class fawryContext : DbContext
    {
        public fawryContext()
        {
        }

        public fawryContext(DbContextOptions<fawryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Hub> Hubs { get; set; } = null!;
        public virtual DbSet<Line> Lines { get; set; } = null!;
        public virtual DbSet<Sub> Subs { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = .; Database = fawry ; Trusted_Connection=true ; MultipleActiveResultSets=true;Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchId).HasColumnName("branchId");

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(80)
                    .HasColumnName("branchAddress");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .HasColumnName("branchName");

                entity.Property(e => e.BranchPhone)
                    .HasMaxLength(15)
                    .HasColumnName("branchPhone")
                    .IsFixedLength();

                entity.Property(e => e.BranchRefNum)
                    .HasMaxLength(50)
                    .HasColumnName("branchRefNum");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HubId).HasColumnName("hub_id");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Hub)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.HubId)
                    .HasConstraintName("FK_Branch_Hub");
            });

            modelBuilder.Entity<Hub>(entity =>
            {
                entity.ToTable("Hub");

                entity.Property(e => e.HubId).HasColumnName("hubId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HubAddress)
                    .HasMaxLength(50)
                    .HasColumnName("hubAddress");

                entity.Property(e => e.HubName)
                    .HasMaxLength(50)
                    .HasColumnName("hubName");

                entity.Property(e => e.HunRefNum)
                    .HasMaxLength(50)
                    .HasColumnName("hunRefNum");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .HasColumnName("phone")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Line>(entity =>
            {
                entity.ToTable("Line");

                entity.Property(e => e.LineId).HasColumnName("lineId");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LineBalance)
                    .HasColumnType("money")
                    .HasColumnName("lineBalance");

                entity.Property(e => e.LineId1).HasColumnName("line_id");

                entity.Property(e => e.LineMaxAmount)
                    .HasColumnType("money")
                    .HasColumnName("lineMaxAmount");

                entity.Property(e => e.LineNumber)
                    .HasMaxLength(15)
                    .HasColumnName("lineNumber");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.LineId1Navigation)
                    .WithMany(p => p.Lines)
                    .HasForeignKey(d => d.LineId1)
                    .HasConstraintName("FK_Line_Sub");
            });

            modelBuilder.Entity<Sub>(entity =>
            {
                entity.ToTable("Sub");

                entity.Property(e => e.SubId).HasColumnName("subId");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SubAddress)
                    .HasMaxLength(80)
                    .HasColumnName("subAddress");

                entity.Property(e => e.SubName)
                    .HasMaxLength(50)
                    .HasColumnName("subName");

                entity.Property(e => e.SubPhone)
                    .HasMaxLength(15)
                    .HasColumnName("subPhone");

                entity.Property(e => e.SubRefNum)
                    .HasMaxLength(50)
                    .HasColumnName("subRefNum");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Subs)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Sub_Branch");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransId);

                entity.ToTable("Transaction");

                entity.Property(e => e.TransId).HasColumnName("transId");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SubId).HasColumnName("sub_id");

                entity.Property(e => e.TransType).HasColumnName("transType");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.SubId)
                    .HasConstraintName("FK_Transaction_Line");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
