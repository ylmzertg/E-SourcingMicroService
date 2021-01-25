namespace ESourcing.UI.Settings
{
    public interface IApiSettings
    {
        public string BaseAddress { get; set; }
        public string ProductPath { get; set; }
        public string OrderPath { get; set; }
        public string AuctionPath { get; set; }
        public string BidPath { get; set; }
    }
}
