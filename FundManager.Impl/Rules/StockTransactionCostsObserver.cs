using System.Collections.Generic;
using FundManager.Interfaces.Data;

namespace FundManager.Impl.Rules
{
    public class StockTransactionCostsObserver : StockObserver
    {
        private readonly IDictionary<string, decimal> _stockTypeMultiplier;

        public StockTransactionCostsObserver(IEnumerable<string> propertyWatchList,
            IDictionary<string, decimal> stockTypeMultiplier) : base(propertyWatchList)
        {
            _stockTypeMultiplier = stockTypeMultiplier;
        }

        protected override void Execute(object newValue, IStock stock)
        {
            if (stock?.StockType == null || _stockTypeMultiplier.ContainsKey(stock.StockType) == false) return;
            stock.TransactionCosts = stock.MarketValue*_stockTypeMultiplier[stock.StockType];
        }
    }
}