using System.Collections.Generic;
using FundManager.Interfaces.Data;

namespace FundManager.Impl.Rules
{
    public class StockMarketValueObserver : StockObserver
    {
        public StockMarketValueObserver(IEnumerable<string> propertyWatchList) : base(propertyWatchList)
        {
        }

        protected override void Execute(object newValue, IStock stock)
        {
            if (stock == null) return;
            stock.MarketValue = stock.Price*stock.Quantity;
        }
    }
}