﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using TrelloClone.Infrastructure;
using TrelloClone.Models;

namespace TrelloClone.Data
{
    public class TrelloCloneDbContext : 
        IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
      private readonly IConfiguration _config;
      public TrelloCloneDbContext(DbContextOptions<TrelloCloneDbContext> options, 
        IConfiguration config) : base(options)
      {
          _config = config;
          Database.EnsureCreated();
      }
        //tablolar için modellerden referans alınması
      public DbSet<Board> Boards { get; set; }
      public DbSet<Card> Cards { get; set; }
      public DbSet<Column> Columns { get; set; }

      protected override void OnModelCreating(ModelBuilder builder)
        {
            // modellerin oluşturulması
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ///<summary>
            /// Converts decimal to double since it is not supported in SqLite 
            /// </summary>
            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // db modelimizizn alınması döngüsü
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
