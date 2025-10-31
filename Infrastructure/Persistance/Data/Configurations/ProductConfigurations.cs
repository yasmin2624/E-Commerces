using Domain.Entities.ProductModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            #region Product 
            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p=>p.Price).HasColumnType("decimal(18,3)");


            #endregion

            #region ProductBrand
            builder.HasOne(P=>P.ProductBrand)
                .WithMany()
                .HasForeignKey(P=>P.BrandId);
            #endregion

            #region ProducType
            builder.HasOne(P => P.ProductType)
                .WithMany()
                .HasForeignKey(P => P.TypeId);
            #endregion
        }
    }
}
