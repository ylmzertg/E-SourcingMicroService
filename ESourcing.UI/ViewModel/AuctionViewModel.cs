using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESourcing.UI.ViewModel
{
    public class AuctionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Please fill Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description fill Name")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product required")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Quantity required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Start Date required")]
        public DateTime StartedAt { get; set; }

        [Required(ErrorMessage = "Finish Date required")]
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public int SellerId { get; set; }
        public List<string> IncludedSellers { get; set; }
    }
}
