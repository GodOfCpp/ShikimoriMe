using Microsoft.Extensions.Logging;
using ShikimoriSharp;
using ShikimoriSharp.Bases;
using System.Threading.Tasks;

namespace ShikimoriMe
{
    //Абстракция клиента для выполнения api запросов
    class Client
    {
        private static ShikimoriClient _client;
        public static ShikimoriApiRequests _apiRequests { get; private set; }
        private static ILogger<Client> _logger;
        private static TokenInfo _tokens;
        private static string _authCode;
        public Client(string authCode)
        {
            _client = new ShikimoriClient(_logger, new ClientSettings("ShikimoriMe", ClientConstants.ClientID, ClientConstants.ClientSecret));
            _authCode = authCode;
        }


        public async Task<string> SetAccessToken()
        {
            _tokens = await ClientAuth.GetAccessAndRefreshToken(_authCode.Substring(5));
            _apiRequests = new ShikimoriApiRequests(_tokens.AccessToken, _tokens.RefreshToken);
            return _tokens.RefreshToken;;
        }

        public async Task<string> RefreshAccessToken()
        {
            _apiRequests = new ShikimoriApiRequests(null, _authCode);
            return await _apiRequests.RefreshAccessToken();
        }

    }
}