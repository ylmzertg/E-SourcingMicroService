﻿using ESourcing.Sourcing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Repositories.Interfaces
{
    public interface ISourcingRepository
    {
        Task<IEnumerable<Auction>> GetAuctions();
        Task<Auction> GetAuction(string id);
        Task<IEnumerable<Auction>> GetAuctionByName(string name);

        Task Create(Auction product);
        Task<bool> Update(Auction product);
        Task<bool> Delete(string id);
    }
}
