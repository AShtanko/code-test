using FundManager.Interfaces.Data;

namespace FundManager.Interfaces
{
    public interface IStockObserver
    {
        void OnNotify(string propertyName, object newValue, IStock stock);
    }
}