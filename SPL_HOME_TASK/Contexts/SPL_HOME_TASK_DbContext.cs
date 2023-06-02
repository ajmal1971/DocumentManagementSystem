using SPL_HOME_TASK.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SPL_HOME_TASK.Contexts
{
    public class SPL_HOME_TASK_DbContext : DbContext
    {
        public DbSet<DocumentCategoryInfo> DocumentCategoryInfo { get; set; }
        public DbSet<DocumentInformation> DocumentInformation { get; set; }
        public DbSet<MetaDataInformation> MetaDataInformation { get; set; }
        public DbSet<FileInformation> FileInformation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentCategoryInfo>()
                .HasKey(e => e.CategoryId)
                .Property(e => e.CategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<DocumentInformation>()
                .HasKey(e => e.DocumentIdentity)
                .Property(e => e.DocumentIdentity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<MetaDataInformation>()
                .HasKey(e => e.MetaIdentity)
                .Property(e => e.MetaIdentity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<FileInformation>()
                .HasKey(e => e.FileIdentity)
                .Property(e => e.FileIdentity)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            modelBuilder.Entity<DocumentInformation>()
                .HasIndex(e => new { e.CategoryId, e.DocumentName })
                .IsUnique()
                .HasName("IX_UniqueKey");

            modelBuilder.Entity<MetaDataInformation>()
                .HasIndex(e => new { e.DocumentIdentity, e.MetaName })
                .IsUnique()
                .HasName("IX_UniqueKey");

            modelBuilder.Entity<FileInformation>()
                .HasIndex(e => new { e.DocumentIdentity, e.FileName })
                .IsUnique()
                .HasName("IX_UniqueKey");


            base.OnModelCreating(modelBuilder);
        }
    }
}