using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FundManager.Impl.ViewModel;
using FundManager.Interfaces.Data;
using FundManager.Interfaces.Validation;

namespace FundManager.UI.ViewModels
{
    public class StockViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IValidator<IStock> _validator;
        private string _error;
        private bool _hasError;
        private IEnumerable<IValidationResult> _validation;

        public StockViewModel(IStock model, IValidator<IStock> stockValidator)
        {
            Model = model;
            _validator = stockValidator;
        }

        public IStock Model { get; }

        public string Name => Model.Name;

        public uint Quantity
        {
            get { return Model.Quantity; }
            set
            {
                if (Model.Quantity == value) return;
                Model.Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public decimal? Price
        {
            get { return Model.Price; }
            set
            {
                if (Model.Price == value) return;
                Model.Price = value;
                OnPropertyChanged("Price");
            }
        }

        public decimal? MarketValue
        {
            get { return Model.MarketValue; }
            set
            {
                if (Model.MarketValue == value) return;
                Model.MarketValue = value;
                OnPropertyChanged("MarketValue");
            }
        }

        public decimal? TransactionCosts
        {
            get { return Model.TransactionCosts; }
            set
            {
                if (Model.TransactionCosts == value) return;
                Model.TransactionCosts = value;
                OnPropertyChanged("TransactionCosts");
            }
        }

        public decimal? StockWeight
        {
            get { return Model.StockWeight; }
            set
            {
                if (Model.StockWeight == value) return;
                Model.StockWeight = value;
                OnPropertyChanged("StockWeight");
            }
        }

        public string StockType
        {
            get { return Model.StockType; }
            set
            {
                if (Model.StockType == value) return;
                Model.StockType = value;
                OnPropertyChanged("StockType");
            }
        }

        public IEnumerable<IValidationResult> Validation
        {
            get { return _validation; }
            private set
            {
                if (_validation == value) return;
                _validation = value;
                OnPropertyChanged("Validation");
            }
        }

        public bool HasError
        {
            get { return _hasError; }
            private set
            {
                if (_hasError == value) return;
                _hasError = value;
                OnPropertyChanged("HasError");
            }
        }

        public string this[string columnName]
        {
            get
            {
                Validation = _validator.Validate(StockType, columnName, Model);

                if (Validation != null && Validation.Any())
                {
                    var error = new StringBuilder();
                    Validation.Where(p => p.ValidationType == ValidationType.Error)
                        .ToList()
                        .ForEach(p => error.AppendLine(p.ErrorMessage));
                    Error = error.ToString();
                    return Error;
                }

                return null;
            }
        }

        public string Error
        {
            get { return _error; }
            private set
            {
                if (_error == value) return;
                _error = value;
                HasError = string.IsNullOrEmpty(value) == false;
                OnPropertyChanged("Error");
            }
        }

        public void ValidateAll()
        {
            Validation = _validator.ValidateAll(StockType, Model);
            if (Validation != null && Validation.Any())
            {
                var error = new StringBuilder();
                Validation
                    .ToList()
                    .ForEach(p => error.AppendLine(p.ErrorMessage));
                Error = error.ToString();
            }
            else
            {
                Error = null;
            }
        }
    }
}