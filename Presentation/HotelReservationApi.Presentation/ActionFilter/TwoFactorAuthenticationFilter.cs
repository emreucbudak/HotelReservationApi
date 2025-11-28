using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Stripe.Forwarding;

namespace HotelReservationApi.Presentation.ActionFilter
{
    public class TwoFactorAuthenticationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? tempToken = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(tempToken) || !tempToken.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            tempToken = tempToken.Substring("Bearer ".Length).Trim();
            foreach (var args in context.ActionArguments.Values)
            {
                var property = args.GetType().GetProperty("TempToken");
                if (property != null && property.PropertyType == typeof(string))
                {
                    property.SetValue(args, tempToken);
                }
            }
            await next();
        }
    }
}
