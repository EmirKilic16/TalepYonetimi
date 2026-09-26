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
    internal sealed class TalepConfiguration : IEntityTypeConfiguration<Talep>
    {
        public void Configure(EntityTypeBuilder<Talep> builder)
        {
            builder.ToTable("Talepler");

            builder.HasKey(talep => talep.Id);

            builder.Property(talep => talep.Baslik)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(talep => talep.Aciklama)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasOne(talep => talep.Kullanici)
                .WithMany(kullanici=>kullanici.Talepler)
                .HasForeignKey(talep=>talep.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(talep => talep.KullaniciId)
                .HasDatabaseName("IX_Talepler_KullaniciId");

            builder.HasIndex(talep => talep.OlusturmaTarihi)
                .HasDatabaseName("IX_Talepler_OlusturulmaTarihi");
            
        }

       
    }
}
