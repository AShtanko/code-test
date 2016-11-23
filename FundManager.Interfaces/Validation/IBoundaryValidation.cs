namespace FundManager.Interfaces.Validation
{
    public interface IBoundaryValidation: IValidation
    {
        decimal? LowerLimit { get; }

        decimal? UpperLimit { get; }
    }
}