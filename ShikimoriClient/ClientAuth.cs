using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//Аутентификация пользователя в первый раз
namespace ShikimoriMe
{
    class TokenInfo
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenInfo(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
    static class ClientAuth
    {
        public static async Task<TokenInfo> GetAccessAndRefreshToken(string authCode)
        {
            using (HttpClient client = new HttpClient())
            {
                var formContent = new MultipartFormDataContent
                {
                    { new StringContent("authorization_code"), "grant_type" },
                    { new StringContent(ClientConstants.ClientID), "client_id" },
                    { new StringContent(ClientConstants.ClientSecret), "client_secret" },
                    { new StringContent(authCode), "code" },
                    { new StringContent(ClientConstants.OAuthCallback), "redirect_uri" }
                };

                client.DefaultRequestHeaders.Add("User-Agent", "ShikimoriMe");

                HttpResponseMessage response = await client.PostAsync("https://shikimori.me/oauth/token", formContent);
                

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject responseObject = JObject.Parse(responseBody);

                    return new TokenInfo((string)responseObject["access_token"], (string)responseObject["refresh_token"]);
                }
                else
                {
                    throw new HttpRequestException();
                }
            }
        }
    }
}