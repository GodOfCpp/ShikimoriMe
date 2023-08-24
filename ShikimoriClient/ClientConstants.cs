using System.Collections.Generic;

//Константы
static class ClientConstants
{
    public static string AuthCodeUri { get; }
    public static string ClientID { get; }
    public static string ClientSecret { get; }
    public static string OAuthCallback { get; }
    public static string ApiRequestBase { get; }
    public static string ApiRefreshToken { get; }
    public static string BaseUrl { get; }
    public static string YTApiKey { get; }
    public static List<(string, string)> HeaderBase { get; }

    static ClientConstants()
    {
        YTApiKey = $"AIzaSyAA40h1dET4S9E80f-3OVRjPGPFQzoLl5s";
        HeaderBase = new List<(string, string)> { ("User-Agent", "ShikimoriMe") };
        BaseUrl = $"https://shikimori.me";
        ApiRefreshToken = $"https://shikimori.me/oauth/token/";
        ApiRequestBase = $"https://shikimori.me/api/";
        OAuthCallback = $"shikimorime://oauth2shikimorimecallback";
        AuthCodeUri = $"https://shikimori.me/oauth/authorize?client_id=aI1qMnsBmGHAxz9DfyPIDxJvDreX-CuwB--46eytVPA&redirect_uri=shikimorime://oauth2shikimorimecallback&response_type=code";
        ClientID = $"aI1qMnsBmGHAxz9DfyPIDxJvDreX-CuwB--46eytVPA";
        ClientSecret = $"Uy3COvXmZDwSzP6pkzfTRgYWHbUSpfkMM-x6T2LNmJg";
    }
}