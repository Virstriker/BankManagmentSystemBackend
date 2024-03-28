using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BankManagmentSystem.Models
{
    public partial class BankingSystemContext : DbContext
    {
        public BankingSystemContext()
        {
        }

        public BankingSystemContext(DbContextOptions<BankingSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<BankEmployee> BankEmployees { get; set; } = null!;
        public virtual DbSet<BankUser> BankUsers { get; set; } = null!;
        public virtual DbSet<MoneyTransaction> MoneyTransactions { get; set; } = null!;
        public virtual DbSet<NetBanking> NetBankings { get; set; } = null!;
        public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
        public virtual DbSet<JoinModel> JoinModels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BankingSystem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JoinModel>(entity =>
            {
                entity.HasKey(e => e.UserId)
                           .HasName("PK__Account___47F96591F4728782");
                entity.ToTable("Join_Table");
                entity.Property(e => e.UserId)
                      .HasMaxLength(30)
                      .IsUnicode(false);
                //.HasColumnName("JoinTable");
            });
            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasKey(e => e.AccountType1)
                    .HasName("PK__Account___47F96591F4728781");

                entity.ToTable("Account_Type");

                entity.Property(e => e.AccountType1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("AccountType");
            });

            modelBuilder.Entity<BankEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__BankEmpl__7AD04F111DE6C2E0");

                entity.ToTable("BankEmployee");

                entity.Property(e => e.EmployeLoginId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmployeeEmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeIsAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmployeeLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeLoginPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Bank_Use__1788CC4CD5F1E1B4");

                entity.ToTable("Bank_User");

                entity.HasIndex(e => e.FirstName, "NCI_User_FirstName");

                entity.HasIndex(e => e.AadharNo, "UC_Aadhar")
                    .IsUnique();

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MoneyTransaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__MoneyTra__55433A6B3BF6EFBD");

                entity.ToTable("MoneyTransaction");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.TransactionAccountNavigation)
                    .WithMany(p => p.MoneyTransactionTransactionAccountNavigations)
                    .HasForeignKey(d => d.TransactionAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account");

                entity.HasOne(d => d.TransactionToAccountNavigation)
                    .WithMany(p => p.MoneyTransactionTransactionToAccountNavigations)
                    .HasForeignKey(d => d.TransactionToAccount)
                    .HasConstraintName("FK_ToAccount");
            });

            modelBuilder.Entity<NetBanking>(entity =>
            {
                entity.HasKey(e => e.AccountUserId)
                    .HasName("PK__NetBanki__2E3A0B91E8666602");

                entity.ToTable("NetBanking");

                entity.Property(e => e.AccountUserId)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AccountUserPassword)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.AadharNumberNavigation)
                    .WithMany(p => p.NetBankings)
                    .HasPrincipalKey(p => p.AadharNo)
                    .HasForeignKey(d => d.AadharNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Aadhar");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__User_Acc__BE2ACD6EE5C14B24");

                entity.ToTable("User_Account");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AccountTypeNavigation)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.AccountType)
                    .HasConstraintName("FK_Type");

                entity.HasOne(d => d.AccountUserNavigation)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.AccountUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        internal BankEmployee Find(int employeeId)
        {
            throw new NotImplementedException();
        }

        internal Task<object> FindAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
