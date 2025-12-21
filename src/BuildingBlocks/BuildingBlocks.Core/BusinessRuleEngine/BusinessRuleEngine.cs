using BuildingBlocks.Core.Results;

namespace BuildingBlocks.Core.BusinessRuleEngine;

public class BusinessRuleEngine
{
    public static ServiceResult Run(params ServiceResult[] rules)
    {
        foreach (var rule in rules)
        {
            if (!rule.IsSuccess)
            {
                return rule;
            }
        }
        return ServiceResult.SuccessAsNoContent();
    }

    public static async Task<ServiceResult> RunAsync(params Func<Task<ServiceResult>>[] rules)
    {
        foreach (var rule in rules)
        {
            var result = await rule();
            if (!result.IsSuccess)
            {
                return result;
            }
        }
        return ServiceResult.SuccessAsNoContent();
    }
}
