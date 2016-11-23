using System.Collections.Generic;
using FundManager.Interfaces.Data;

namespace FundManager.Impl.Rules
{
    public class StockNameObserver : StockObserver
    {
        public StockNameObserver(IEnumerable<string> propertyWatchList) : base(propertyWatchList)
        {
        }


        protected override void Execute(object newValue, IStock stock)
        {
            if (stock == null) return;

            stock.Name = stock.StockType + newValue;
        }
    }
}