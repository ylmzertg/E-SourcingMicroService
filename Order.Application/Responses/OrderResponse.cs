﻿using System;

namespace Ordering.Application.Responses
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public string AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
