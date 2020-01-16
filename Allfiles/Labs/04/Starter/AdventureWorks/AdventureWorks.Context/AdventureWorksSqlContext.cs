using AdventureWorks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Context
{
    public class AdventureWorksSqlContext : DbContext, IAdventureWorksProductContext
    {
        public AdventureWorksSqlContext(string connectionString)
            : base(
                new DbContextOptionsBuilder<AdventureWorksSqlContext>().UseSqlServer(
                    connectionString
                ).Options
            )
        { }

        public DbSet<Model> Models { get; set; }

        public DbSet<Product> Products { get; set; }

        public async Task<Model> FindModelAsync(Guid id)
        {
            return await this.Models.Include(m => m.Products).SingleOrDefaultAsync(m => m.id == id);
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            return await this.Models.OrderByDescending(m => m.Products.Average(p => p.ListPrice)).ToListAsync<Model>();
        }

        public async Task<Product> FindProductAsync(Guid id)
        {
            return await this.Products.SingleOrDefaultAsync(p => p.id == id);
        }
    }
}