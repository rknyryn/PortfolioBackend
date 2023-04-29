using Core.Application.Rules.Abstractions;

namespace Core.Application.Rules.Factories;

public interface IBusinessRuleFactory
{
    #region Methods

    IBusinessRule GetBusinessRule(Type businessRule);

    #endregion Methods
}
