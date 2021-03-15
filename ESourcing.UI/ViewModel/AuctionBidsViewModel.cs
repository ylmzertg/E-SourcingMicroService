using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.UI.ViewModel
{
    public class AuctionBidsViewModel
    {
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public List<BidViewModel> Bids { get; set; }
    }
}
