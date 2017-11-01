using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using WebApplicationOwin.Provider;

namespace WebApplicationOwin
{
    public partial class Startup
	{
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                //sets the URL by which the client will receive the token
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAppProvider(),
                
				//It indicates the route by which the user will be redirected for authorization.
                AuthorizeEndpointPath = new PathString("/api/User/LogIn"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                AllowInsecureHttp = true
            };
        }

        /// <summary>
        /// The application includes the functionality of tokens
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}