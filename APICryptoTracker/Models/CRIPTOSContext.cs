using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCryptoTracker.Models
{
    public partial class CRIPTOSContext : DbContext
    {
        public CRIPTOSContext()
        {
        }

        public CRIPTOSContext(DbContextOptions<CRIPTOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BalanceHistory> BalanceHistories { get; set; } = null!;
        public virtual DbSet<Chain> Chains { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<WalletXToken> WalletXTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-FQBUJMB\\SQLEXPRESS; Database=CRIPTOS; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BalanceHistory>(entity =>
            {
                entity.HasKey(e => e.IdBalanceHistory);

                entity.ToTable("BALANCE_HISTORY");

                entity.Property(e => e.IdBalanceHistory).HasColumnName("ID_BALANCE_HISTORY");

                entity.Property(e => e.Balance).HasColumnName("BALANCE");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.FkToken).HasColumnName("FK_TOKEN");

                entity.Property(e => e.FkWallet).HasColumnName("FK_WALLET");

                entity.Property(e => e.OldPrice).HasColumnName("OLD_PRICE");

                entity.HasOne(d => d.FkTokenNavigation)
                    .WithMany(p => p.BalanceHistories)
                    .HasForeignKey(d => d.FkToken)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BALANCE_HISTORY_TOKEN");

                entity.HasOne(d => d.FkWalletNavigation)
                    .WithMany(p => p.BalanceHistories)
                    .HasForeignKey(d => d.FkWallet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BALANCE_HISTORY_WALLET");
            });

            modelBuilder.Entity<Chain>(entity =>
            {
                entity.HasKey(e => e.IdChain);

                entity.ToTable("CHAIN");

                entity.Property(e => e.IdChain).HasColumnName("ID_CHAIN");

                entity.Property(e => e.BlockExplorerUrl)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("BLOCK_EXPLORER_URL");

                entity.Property(e => e.ChainId).HasColumnName("CHAIN_ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.RpcUrl)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("RPC_URL");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.IdToken);

                entity.ToTable("TOKEN");

                entity.Property(e => e.IdToken).HasColumnName("ID_TOKEN");

                entity.Property(e => e.CgTicker)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CG_TICKER");

                entity.Property(e => e.ContractAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTRACT_ADDRESS");

                entity.Property(e => e.FkChain).HasColumnName("FK_CHAIN");

                entity.Property(e => e.IsNative).HasColumnName("IS_NATIVE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.HasOne(d => d.FkChainNavigation)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.FkChain)
                    .HasConstraintName("FK_TOKEN_CHAIN1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("USER");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.IdWallet);

                entity.ToTable("WALLET");

                entity.Property(e => e.IdWallet).HasColumnName("ID_WALLET");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.FkUser).HasColumnName("FK_USER");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.HasOne(d => d.FkUserNavigation)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.FkUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WALLET_USER");
            });

            modelBuilder.Entity<WalletXToken>(entity =>
            {
                entity.HasKey(e => e.IdWalletXToken);

                entity.ToTable("WALLET_X_TOKEN");

                entity.Property(e => e.IdWalletXToken).HasColumnName("ID_WALLET_X_TOKEN");

                entity.Property(e => e.FkToken).HasColumnName("FK_TOKEN");

                entity.Property(e => e.FkWallet).HasColumnName("FK_WALLET");

                entity.Property(e => e.TokenBalance).HasColumnName("TOKEN_BALANCE");

                entity.HasOne(d => d.FkTokenNavigation)
                    .WithMany(p => p.WalletXTokens)
                    .HasForeignKey(d => d.FkToken)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WALLET_X_TOKEN_TOKEN1");

                entity.HasOne(d => d.FkWalletNavigation)
                    .WithMany(p => p.WalletXTokens)
                    .HasForeignKey(d => d.FkWallet)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WALLET_X_TOKEN_WALLET1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
