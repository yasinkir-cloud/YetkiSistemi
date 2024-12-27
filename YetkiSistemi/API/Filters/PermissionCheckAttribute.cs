using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class PermissionCheckAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _requiredPermission;
    public PermissionCheckAttribute(string requiredPermission)
    {
        _requiredPermission = requiredPermission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var isAdminClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
        if (isAdminClaim != null && bool.Parse(isAdminClaim))
        {
            // Süper admin yetki kontrolü yapmaz
            return;
        }

        // Ek yetki kontrolü yapabilirsiniz
        // _requiredPermission ile ilgili izin kontrolü yapılabilir.
    }
}
