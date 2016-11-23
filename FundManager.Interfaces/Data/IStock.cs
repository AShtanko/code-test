namespace FundManager.Interfaces.Data
{
    public interface IStock
    {
        string Name { get; set; }
        uint Quantity { get; set; }
        decimal? Price { get; set; }
        decimal? MarketValue { get; set; }
        decimal? TransactionCosts { get; set; }
        decimal? StockWeight { get; set; }
        string StockType { get; set; }
        void Notify(string propertyName, object propertyValue);
    }
}