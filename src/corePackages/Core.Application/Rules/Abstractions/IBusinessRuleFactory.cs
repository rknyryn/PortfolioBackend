namespace Core.Application.Rules.Abstractions;

public interface IBusinessRuleFactory
{
    #region Methods

    TRule GetBusinessRule<TRule>() where TRule : class, IBusinessRule;

    #endregion Methods
}
