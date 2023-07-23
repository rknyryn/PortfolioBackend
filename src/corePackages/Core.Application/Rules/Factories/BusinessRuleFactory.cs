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

    public TRule GetBusinessRule<TRule>() where TRule : class, IBusinessRule
        => (TRule)_serviceProvider.GetService(typeof(TRule));

    #endregion Methods
}

