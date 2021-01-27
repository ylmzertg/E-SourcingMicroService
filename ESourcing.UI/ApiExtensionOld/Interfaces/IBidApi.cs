using ESourcing.UI.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension.Interfaces
{
    public interface IBidApi
    {
        Task SendBid(BidViewModel bidModel);
        Task<IEnumerable<BidViewModel>> GetBidsByAuctionId(string id);
        Task<IEnumerable<BidViewModel>> GetWinnerBid(string id);
    }
}
