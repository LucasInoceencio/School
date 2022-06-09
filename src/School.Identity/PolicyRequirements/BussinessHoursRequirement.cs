using Microsoft.AspNetCore.Authorization;

namespace School.Identity.PolicyRequirements;

public class BussinessHoursRequirement : IAuthorizationRequirement
{
    public BussinessHoursRequirement() { }
}