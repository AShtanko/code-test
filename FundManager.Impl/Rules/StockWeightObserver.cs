using System.Collections.Generic;
using FundManager.Interfaces.Data;

namespace FundManager.Impl.Rules
{
    public class StockWeightObserver : StockObserver
    {
        public StockWeightObserver(IEnumerable<string> propertyWatchList) : base(propertyWatchList)
        {
        }

        protected override void Execute(object newValue, IStock stock)
        {
            if (stock == null || newValue is decimal == false) return;

            stock.StockWeight = (stock.MarketValue*100)/(decimal) newValue;
        }
    }
}