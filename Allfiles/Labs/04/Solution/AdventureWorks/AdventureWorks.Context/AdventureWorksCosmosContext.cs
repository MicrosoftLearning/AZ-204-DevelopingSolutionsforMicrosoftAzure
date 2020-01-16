using AdventureWorks.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorks.Context
{
    public class AdventureWorksCosmosContext : IAdventureWorksProductContext
    {
        private readonly Container _container;

        public AdventureWorksCosmosContext(string connectionString, string database = "Retail", string container = "Online")
        {
            _container = new CosmosClient(connectionString)
                .GetDatabase(database)
                .GetContainer(container);
        }
        public async Task<Model> FindModelAsync(Guid id)
        {
            var iterator = _container.GetItemLinqQueryable<Model>()
                .Where(m => m.id == id)
                .ToFeedIterator<Model>();

            List<Model> matches = new List<Model>();
            while (iterator.HasMoreResults)
            {
                var next = await iterator.ReadNextAsync();
                matches.AddRange(next);
            }

            return matches.SingleOrDefault();
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            string query = $@"SELECT * FROM items";

            var iterator = _container.GetItemQueryIterator<Model>(query);

            List<Model> matches = new List<Model>();
            while (iterator.HasMoreResults)
            {
                var next = await iterator.ReadNextAsync();
                matches.AddRange(next);
            }

            return matches;
        }

        public async Task<Product> FindProductAsync(Guid id)
        {
            string query = $@"SELECT VALUE products
                                FROM models
                                JOIN products in models.Products
                                WHERE products.id = '{id}'";

            var iterator = _container.GetItemQueryIterator<Product>(query);

            List<Product> matches = new List<Product>();
            while (iterator.HasMoreResults)
            {
                var next = await iterator.ReadNextAsync();
                matches.AddRange(next);
            }

            return matches.SingleOrDefault();
        }
    }
}