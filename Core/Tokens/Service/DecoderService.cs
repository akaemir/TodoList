using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Core.Tokens.Service;

public class DecoderService(IHttpContextAccessor httpContextAccessor)
{
    public string GetUserId()
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }

    public string GetEmail()
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
}