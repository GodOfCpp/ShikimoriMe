using Android.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

//Все api запросы выполняются здесь
namespace ShikimoriMe
{
    class ShikimoriApiRequests
    {
        private string AccessToken { get; set; }
        private string RefreshToken { get; set; }

        public ShikimoriApiRequests(string accessToken, string refreshToken = null)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        //Пока поддерживаются только POST и GET запросы
        private async Task<string> _SendRequest(string requestType, string apiUrl, List<(string, string)> requestHeaders = null , MultipartFormDataContent content = null)
        {
            using (var httpClient = new HttpClient())
            {
                foreach (var requestHeader in requestHeaders)
                    httpClient.DefaultRequestHeaders.Add(requestHeader.Item1, requestHeader.Item2);

                HttpResponseMessage response;
                string responseBody;
                try
                {
                    switch (requestType)
                    {
                        case "GET":
                            response = await httpClient.GetAsync(apiUrl);
                            responseBody = await response.Content.ReadAsStringAsync();
                            return responseBody;

                        case "POST":
                            response = await httpClient.PostAsync(apiUrl, content);
                            responseBody = await response.Content.ReadAsStringAsync();
                            return responseBody;
                        default:
                            throw new ArgumentException("Wrong request type was given.");

                    }
                }
                catch(ArgumentException ex)
                {
                    Log.Debug("HttpResponseError: ", ex.Message);
                    throw ex;
                }
                catch (Exception ex)
                {
                    Log.Debug("HttpResponseError: ", ex.Message);
                    throw ex;
                }
            }
        }
        public async Task<string> RefreshAccessToken()
        {
            MultipartFormDataContent content = new MultipartFormDataContent
            {
                { new StringContent("refresh_token"), "grant_type" },
                { new StringContent(ClientConstants.ClientID), "client_id" },
                { new StringContent(ClientConstants.ClientSecret), "client_secret" },
                { new StringContent(RefreshToken), "refresh_token" }
            };
            var _tokens =  await _SendRequest("POST", ClientConstants.ApiRefreshToken, ClientConstants.HeaderBase, content);
            JObject responseObject = JObject.Parse(_tokens);
            var temp = new TokenInfo((string)responseObject["access_token"], (string)responseObject["refresh_token"]);
            AccessToken = temp.AccessToken;
            RefreshToken = temp.RefreshToken;
            return RefreshToken;
        }

        //Ниже идут конкретные api запросы, которые используют _SendRequest как базу для выполнения своего запроса
        public async Task<UserInfo> GetMyInfo()
        {
            string jsonResponse = await _SendRequest("GET", "https://shikimori.me/api/users/whoami", new List<(string, string)> { ("User-Agent", "ShikimoriMe"), 
                ("Authorization", $"Bearer {AccessToken}") });
            return JsonConvert.DeserializeObject<UserInfo>(jsonResponse);
        }

        public async Task<Anime[]> GetAnime(AnimeRequestSettings settings)
        {
            string jsonResponse =  await _SendRequest("GET", $"{ClientConstants.ApiRequestBase}animes?{ApiParser<AnimeRequestSettings>.ParseApi(settings)}", 
                ClientConstants.HeaderBase);
            return JsonConvert.DeserializeObject<Anime[]>(jsonResponse);
        }

        public async Task<AnimeDetailedInfo> GetAnimeDetailedInfo(int id)
        {
            string jsonResponse = await _SendRequest("GET", $"{ClientConstants.ApiRequestBase}animes/{id}",
                ClientConstants.HeaderBase);
            return JsonConvert.DeserializeObject<AnimeDetailedInfo>(jsonResponse);
        }
        public async Task<Anime[]> GetAnimesByName(string name)
        {
            var settings = new AnimeRequestSettings { limit = 50, search = name };
            string jsonResponse = await _SendRequest("GET", $"{ClientConstants.ApiRequestBase}animes?{ApiParser<AnimeRequestSettings>.ParseApi(settings)}", 
                ClientConstants.HeaderBase);
            return JsonConvert.DeserializeObject<Anime[]>(jsonResponse);
        }

    }
}