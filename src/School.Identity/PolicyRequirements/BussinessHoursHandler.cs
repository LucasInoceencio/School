using Microsoft.AspNetCore.Authorization;

namespace School.Identity.PolicyRequirements;

public class BussinessHoursHandler : AuthorizationHandler<BussinessHoursRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BussinessHoursRequirement requirement)
    {
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);
        if (currentTime.Hour >= 8 && currentTime.Hour <= 18)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}