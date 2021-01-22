using ESourcing.Sourcing.Data.Interfaces;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Bid>> GetBidsByAuctionId(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(p => p.AuctionId, id);

            List<Bid> bids = await _context
                          .Bids
                          .Find(filter)
                          .ToListAsync();

            bids = bids.OrderByDescending(a => a.CreatedAt)
                                   .GroupBy(x => x.SellerUserName)
                                   .Select(a => new Bid
                                   {
                                       AuctionId = a.FirstOrDefault().AuctionId,
                                       Price = a.FirstOrDefault().Price,
                                       CreatedAt = a.FirstOrDefault().CreatedAt,
                                       SellerUserName = a.FirstOrDefault().SellerUserName,
                                       ProductId = a.FirstOrDefault().ProductId,
                                       Id = a.FirstOrDefault().Id
                                   })
                                   .ToList();

            return bids;
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            List<Bid> bids = await GetBidsByAuctionId(id);

            return bids.OrderByDescending(b => b.Price).FirstOrDefault();
        }

        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }
    }
}
