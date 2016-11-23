using System.Collections.Generic;
using System.ComponentModel;
using FundManager.Impl.Annotations;
using FundManager.Interfaces;
using FundManager.Interfaces.Data;

namespace FundManager.Impl.Data
{
    public static class StockType
    {
        public const string EquityTypeName = "Equity";
        public const string BondTypeName = "Bond";
    }

    public class Stock : IStock, INotifyPropertyChanged
    {
        private readonly IEnumerable<IStockObserver> _observers;
        private decimal? _marketValue;
        private string _name;
        private decimal? _price;
        private uint _quantity;
        private string _stockType;
        private decimal? _stockWeight;
        private decimal? _transactionCosts;

        public Stock(IEnumerable<IStockObserver> observers)
        {
            _observers = observers;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Notify("Name", value);
                OnPropertyChanged(nameof(Name));
            }
        }

        public uint Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                Notify("Quantity", value);
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                Notify("Price", value);
                OnPropertyChanged(nameof(Price));
            }
        }

        public decimal? MarketValue
        {
            get { return _marketValue; }
            set
            {
                _marketValue = value;
                Notify("MarketValue", value);
                OnPropertyChanged(nameof(MarketValue));
            }
        }

        public decimal? TransactionCosts
        {
            get { return _transactionCosts; }
            set
            {
                _transactionCosts = value;
                Notify("TransactionCosts", value);
                OnPropertyChanged(nameof(TransactionCosts));
            }
        }

        public decimal? StockWeight
        {
            get { return _stockWeight; }
            set
            {
                _stockWeight = value;
                Notify("StockWeight", value);
                OnPropertyChanged(nameof(StockWeight));
            }
        }

        public string StockType
        {
            get { return _stockType; }
            set
            {
                _stockType = value;
                Notify("StockType", value);
                OnPropertyChanged(nameof(StockType));
            }
        }

        public void Notify(string propertyName, object propertyValue)
        {
            foreach (var stockObserver in _observers)
            {
                stockObserver.OnNotify(propertyName, propertyValue, this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}