using ESourcing.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension.Interfaces
{
    public interface IAuctionApi
    {
        Task<IEnumerable<AuctionViewModel>> GetAuctions();
        Task<AuctionViewModel> GetAuction(string id);
        Task<AuctionViewModel> CreateAuction(AuctionViewModel product);
        Task DeleteAuctionById(string id);
        Task<AuctionViewModel> CompleteAuction(string id);
        Task UpdateAuction(AuctionViewModel value);
    }
}
