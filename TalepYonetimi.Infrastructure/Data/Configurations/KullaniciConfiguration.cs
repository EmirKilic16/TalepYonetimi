using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalepYonetimi.Domain.Entities;

namespace TalepYonetimi.Infrastructure.Data.Configurations
{
    internal sealed class KullaniciConfiguration : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.ToTable("Kullanicilar");

            builder.HasKey(kullanici => kullanici.Id);

            builder.Property(kullanici => kullanici.Ad)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(kullanici => kullanici.Soyad)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(kullanici => kullanici.Eposta)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(kullanici => kullanici.SifreHash)
                .HasMaxLength(255)
                .IsRequired();


            builder.HasIndex(kullanici => kullanici.Eposta)
                .IsUnique()
                .HasDatabaseName("UX_Kullanicilar_Eposta");


        }
    }
}
