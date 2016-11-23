using System.Collections.Generic;
using System.Linq;
using FundManager.Interfaces;
using FundManager.Interfaces.Data;

namespace FundManager.Impl.Rules
{
    public abstract class StockObserver : IStockObserver
    {
        private readonly IEnumerable<string> _propertyWatchList;

        protected StockObserver(IEnumerable<string> propertyWatchList)
        {
            _propertyWatchList = propertyWatchList;
        }

        public void OnNotify(string propertyName, object newValue, IStock stock)
        {
            if (_propertyWatchList == null || _propertyWatchList.Contains(propertyName) == false)
                return;

            Execute(newValue, stock);
        }

        protected abstract void Execute(object newValue, IStock stock);
    }
}