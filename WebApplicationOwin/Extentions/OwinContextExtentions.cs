using Microsoft.Owin;
using System.Linq;

namespace WebApplicationOwin.Extentions
{
    public static class OwinContextExtentions
    {
        public static string GetUserId(this IOwinContext ctx)
        {
            var result = "-1";
            var claim = ctx.Authentication.User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (claim != null)
            {
                result = claim.Value;
            }
            return result;
        }
    }
}