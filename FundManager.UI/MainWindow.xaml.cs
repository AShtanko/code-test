using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Windows;
using FundManager.Impl.Rules;
using FundManager.Impl.Validation;
using FundManager.Interfaces;
using FundManager.Interfaces.Data;
using FundManager.Interfaces.Validation;
using FundManager.UI.ViewModels;
using Newtonsoft.Json;

namespace FundManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // TODO: move preferences load to repository via bootstrapper

            var preferences = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText("Artifacts/validationRules.json"));
            var validator = new StockValidator();
            IEnumerable<dynamic> validations = preferences.validations;
            foreach (var validation in validations)
            {
                StockSerializer.DeserializeValidator(validator, validation);
            }

            var observers = new List<IStockObserver>()
            {
                new StockNameObserver(new[] {"StockTypeQuantity"}),
                new StockMarketValueObserver(new []{"Price", "Quantity"}),
                new StockTransactionCostsObserver(new[] {"MarketValue", "StockType"}, new Dictionary<string, decimal>() { {"Bond",0.5m}, {"Equity",0.3m}}),
                new StockWeightObserver(new[] {"TotalMarketValue"})
            };

            DataContext = new FundViewModel(validator, observers);
        }
    }

    public static class StockSerializer
    {
        public static void DeserializeValidator(StockValidator validator, dynamic validation)
        {
            string stockType = validation.stock_type;
            IEnumerable<dynamic> validations = validation.validations;
            foreach (var propertyValidation in validations)
            {
                string propertyName = propertyValidation.property_name;
                validator.AddNewValidationRule(stockType, GetExpression(propertyName),
                    GetValidation(propertyValidation));
            }
        }

        private static Expression<Func<IStock, object>> GetExpression(string propertyName)
        {
            switch (propertyName)
            {
                case "Quantity":
                    return stock => stock.Quantity;
                case "Price":
                    return stock => stock.Price;
                case "TransactionCosts":
                    return stock => stock.TransactionCosts;
            }

            return null;
        }

        private static IValidation GetValidation(dynamic validation)
        {
            string type = validation.type;
            switch (type)
            {
                case "boundary_validation":
                    {
                        decimal? lowLimit = validation.lower_limit;
                        decimal? upLimit = validation.upper_limit;
                        int? errorId = validation.error_id;
                        string propertyValidationType = validation.validation_type;
                        ValidationType validationType = SelectValidationType(propertyValidationType);
                        return new BoundaryValidation(lowLimit, upLimit, errorId, validationType);
                    }

                case "is_null_or_empty_validation":
                    {
                        int? errorId = validation.error_id;
                        string propertyValidationType = validation.validation_type;
                        ValidationType validationType = SelectValidationType(propertyValidationType);
                        return new IsNullOrDefaultValidation(errorId, validationType);
                    }
            }
            return null;
        }

        private static ValidationType SelectValidationType(string validationType)
        {
            switch (validationType)
            {
                case "adorner":
                    return ValidationType.Adorner;
                case "error":
                    return ValidationType.Error;
                case "warning":
                    return ValidationType.Warning;
            }

            return ValidationType.Error;
        }
    }
}