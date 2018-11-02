using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace WebApplication.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpClient _cathttpClient = new HttpClient();

        public HttpService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpClient> GetClient(string adresse)
        {
            // get the current HttpContext to access the tokens
            var currentContext = _httpContextAccessor.HttpContext;

            // get access token
            var accessToken = await currentContext.GetTokenAsync(
                OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                // set as Bearer token
                _cathttpClient.SetBearerToken(accessToken);
            }

            _cathttpClient.BaseAddress = new Uri(adresse);
            _cathttpClient.DefaultRequestHeaders.Accept.Clear();
            _cathttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _cathttpClient;
        }
    }
}