using ESourcing.Sourcing.Data.Interfaces;
using MongoDB.Driver;
using System;
using ESourcing.Sourcing.Settings;
using ESourcing.Sourcing.Entities;

namespace ESourcing.Sourcing.Data
{
    public class SourcingContext : ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Auctions = database.GetCollection<Auction>(settings.CollectionName);

            SourcingContextSeed.SeedData(Auctions);
        }

        public IMongoCollection<Auction> Auctions { get; }
    }
}
