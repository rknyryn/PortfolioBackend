using Core.Application.Rules.Abstractions;

namespace Core.Application.Rules.Factories;

public class BusinessRuleFactory : IBusinessRuleFactory
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;

    #endregion Fields

    #region Constructors

    public BusinessRuleFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    #endregion Constructors

    #region Methods

    public IBusinessRule GetBusinessRule(Type businessRuleType)
    {
        if (businessRuleType == null)
        {
            throw new ArgumentNullException(nameof(businessRuleType));
        }

        if (!typeof(IBusinessRule).IsAssignableFrom(businessRuleType))
        {
            throw new ArgumentException($"{businessRuleType} does not implement {typeof(IBusinessRule)}.");
        }

        return (IBusinessRule)_serviceProvider.GetService(businessRuleType);
    }

    #endregion Methods
}
    
