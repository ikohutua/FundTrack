using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.WebUI.secutiry
{
    public class ClaimsTransformer : IClaimsTransformer
    {
        readonly Dictionary<string, string> _options;
        private UserInfoViewModel userInfo;
        public ClaimsTransformer(IOptions<Dictionary<string, string>> options)
        {
            _options = options.Value;

        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsTransformationContext context)
        {
            var principal = context.Principal;
            var identity = (ClaimsIdentity)principal.Identity;

            var auth = context.Context.Request?.Headers["Authorization"];
            var token = auth?.ToString();

            if (!string.IsNullOrEmpty(token))
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_options["owinAuthorization"]);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    AuthenticationHeaderValue authenticationHeader = new AuthenticationHeaderValue("Bearer", token);

                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization = authenticationHeader;

                    HttpResponseMessage response = await client.GetAsync(_options["claimsEndpoint"]);

                    var stream = await response.Content.ReadAsStreamAsync();
                    var reader = new StreamReader(stream, Encoding.UTF8);

                    userInfo = new JsonSerializer().Deserialize<UserInfoViewModel>(new JsonTextReader(reader));
                }

                if (userInfo != null)
                {
                    identity.AddClaim(new Claim("UserId", userInfo.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Role, userInfo.Role));
                    identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.Login));
                    identity.AddClaim(new Claim(ClaimTypes.Email, userInfo.Email));
                }
            }
            return await Task.FromResult(principal);
        }
    }
}
