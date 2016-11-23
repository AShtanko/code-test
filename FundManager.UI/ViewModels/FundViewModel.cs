using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FundManager.Impl.Command;
using FundManager.Impl.Data;
using FundManager.Impl.ViewModel;
using FundManager.Interfaces;
using FundManager.Interfaces.Data;
using FundManager.Interfaces.Validation;

namespace FundManager.UI.ViewModels
{
    public class FundViewModel : ViewModelBase
    {
        private readonly IEnumerable<IStockObserver> _stockObservers;
        private readonly IValidator<IStock> _stockValidator;
        private int _bondQuantity;
        private int _equityQuantity;
        private StockViewModel _newStock;
        private ObservableCollection<StockViewModel> _stocks;
        private decimal _totalBondWeight;
        private decimal _totalEquityWeight;
        private decimal _totalBondMarketValue;
        private decimal _totalEquityMarketValue;
        private decimal _totalMarketValue;
        private int _totalQuantity;
        private decimal _totalWeight;

        public FundViewModel(IValidator<IStock> stockValidator, IEnumerable<IStockObserver> stockObservers)
        {
            _stockValidator = stockValidator;
            _stockObservers = stockObservers;
            NewStock = new StockViewModel(new Stock(_stockObservers), _stockValidator);
            AddNewStockCommand = new RelayCommand((o => AddNewStock()));
            ClearStockDataCommand = new RelayCommand((o => ClearStockData()));
        }

        public ICommand ClearStockDataCommand { get; }

        public ICommand AddNewStockCommand { get; }

        #region helpInfo
        public int BondQuantity
        {
            get { return _bondQuantity; }
            set
            {
                if (_bondQuantity == value) return;
                _bondQuantity = value;
                OnPropertyChanged("BondQuantity");
            }
        }

        public int EquityQuantity
        {
            get { return _equityQuantity; }
            set
            {
                if (_equityQuantity == value) return;
                _equityQuantity = value;
                OnPropertyChanged("EquityQuantity");
            }
        }

        public decimal TotalBondWeight
        {
            get { return _totalBondWeight; }
            set
            {
                if (_totalBondWeight == value) return;
                _totalBondWeight = value;
                OnPropertyChanged("TotalBondWeight");
            }
        }

        public decimal TotalEquityWeight
        {
            get { return _totalEquityWeight; }
            set
            {
                if (_totalEquityWeight == value) return;
                _totalEquityWeight = value;
                OnPropertyChanged("TotalEquityWeight");
            }
        }

        public decimal TotalBondMarketValue
        {
            get { return _totalBondMarketValue; }
            set
            {
                if (_totalBondMarketValue == value) return;
                _totalBondMarketValue = value;
                OnPropertyChanged("TotalBondMarketValue");
            }
        }

        public decimal TotalEquityMarketValue
        {
            get { return _totalEquityMarketValue; }
            set
            {
                if (_totalEquityMarketValue == value) return;
                _totalEquityMarketValue = value;
                OnPropertyChanged("TotalEquityMarketValue");
            }
        }

        public int TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                if (_totalQuantity == value) return;
                _totalQuantity = value;
                OnPropertyChanged("TotalQuantity");
            }
        }

        public decimal TotalWeight
        {
            get { return _totalWeight; }
            set
            {
                if (_totalWeight == value) return;
                _totalWeight = value;
                OnPropertyChanged("TotalWeight");
            }
        }

        public decimal TotalMarketValue
        {
            get { return _totalMarketValue; }
            set
            {
                if (_totalMarketValue == value) return;
                _totalMarketValue = value;
                OnPropertyChanged("TotalMarketValue");
                foreach (var stock in Stocks)
                {
                    stock.Model.Notify("TotalMarketValue", value);
                }
            }
        }

        #endregion
        public ObservableCollection<StockViewModel> Stocks
        {
            get { return _stocks ?? (_stocks = new ObservableCollection<StockViewModel>()); }
            set
            {
                if (_stocks == value) return;
                _stocks = value;
                OnPropertyChanged("Stocks");
            }
        }

        public StockViewModel NewStock
        {
            get { return _newStock; }
            set
            {
                if (_newStock == value) return;
                _newStock = value;
                OnPropertyChanged("NewStock");
            }
        }

        private void ClearStockData()
        {
            NewStock = new StockViewModel(new Stock(_stockObservers), _stockValidator);
        }

        private void AddNewStock()
        {
            Stocks.Add(NewStock);
            NewStock.ValidateAll();
            BondQuantity = Stocks.Count(p => p.StockType == StockType.BondTypeName);
            EquityQuantity = Stocks.Count(p => p.StockType == StockType.EquityTypeName);
            TotalBondMarketValue = Stocks.Where(p => p.StockType == StockType.BondTypeName).Sum(s => s.MarketValue ?? 0);
            TotalEquityMarketValue = Stocks.Where(p => p.StockType == StockType.EquityTypeName).Sum(s => s.MarketValue ?? 0);
            TotalBondWeight = Stocks.Where(p => p.StockType == StockType.BondTypeName).Sum(s => s.StockWeight ?? 0);
            TotalEquityWeight = Stocks.Where(p => p.StockType == StockType.EquityTypeName).Sum(s => s.StockWeight ?? 0);
            TotalQuantity = Stocks.Count;
            TotalMarketValue = Stocks.Sum(s => s.MarketValue ?? 0);
            TotalWeight = Stocks.Sum(s => s.StockWeight ?? 0);
            NewStock.Model.Notify("StockTypeQuantity", NewStock.Model.StockType == StockType.BondTypeName ? BondQuantity : EquityQuantity);
            ClearStockData();
        }
    }
}