using FundManager.Impl.Data;
using FundManager.Impl.Rules;
using FundManager.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace FundManager.UnitTests.Data
{
    [TestFixture]
    public class StockDataTests
    {
        private List<IStockObserver> _observers;
        private Stock _testingStock;

        [SetUp]
        public void SetUp()
        {
            _observers = new List<IStockObserver>();
            _testingStock = new Stock(_observers);
        }

        [Test]
        public void StockNameCalculationTests()
        {
            _observers.Add(new StockNameObserver(new[] { "StockTypeQuantity" }));
            _testingStock.StockType = StockType.BondTypeName;
            _testingStock.Notify("StockTypeQuantity", 2);

            Assert.AreEqual(StockType.BondTypeName + 2, _testingStock.Name);
        }
    }
}