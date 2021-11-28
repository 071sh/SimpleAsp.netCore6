using Domain.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class AppSettingTypeConfiguration : IEntityTypeConfiguration<AppSetting>
    {
        public void Configure(EntityTypeBuilder<AppSetting> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Key).HasColumnType("VARCHAR").HasMaxLength(256);
            builder.Property(p => p.Value).HasMaxLength(2048);
            builder.Property(p => p.Description).HasMaxLength(256).IsRequired(false);
        }
    }
}
