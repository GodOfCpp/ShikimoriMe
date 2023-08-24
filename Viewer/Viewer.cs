using Android.App;
using Android.Content;
using Android.Util;
using ShikimoriMe;
using System.Threading.Tasks;

//Представление Viewer для связи ui и api части
static class Viewer
{
    private static Client client;

    public static bool HasRefreshToken()
    {
        var prefs = Application.Context.GetSharedPreferences("ShikimoriMe", FileCreationMode.Private);
        return (prefs.Contains("refresh_token")) ? true : false;    
    }
    //Авторизация выполняется только если еще не получен refresh-token, иначе используется Login()
    public static async Task Authorize(string authCode)
    {
        var prefs = Application.Context.GetSharedPreferences("ShikimoriMe", FileCreationMode.Private);
        var editor = prefs.Edit();
        string refToken;

        client = new Client(authCode);

        refToken = await client.SetAccessToken();

        editor.PutString("refresh_token", refToken);
        editor.Commit();
        Log.Debug("Auth info: ", "Authorization complete!");

    }
    //Авторизация через refresh-token
    public static async Task Login()
    {
        var prefs = Application.Context.GetSharedPreferences("ShikimoriMe", FileCreationMode.Private);
        var editor = prefs.Edit();
        string refToken;

        client = new Client(prefs.GetString("refresh_token", null));
        refToken = await client.RefreshAccessToken();

        editor.PutString("refresh_token", refToken);
        editor.Commit();
        Log.Debug("Auth info: ", "Login complete!");
    }

    public static async Task<Anime[]> GetOnGoingAnime()
    {
        return await Client._apiRequests.GetAnime(new AnimeRequestSettings { limit=50, order=Order.ranked });
    }

    public static async Task<AnimeDetailedInfo> GetAnimeInfoById(int id)
    {
        return await Client._apiRequests.GetAnimeDetailedInfo(id);
    }

    public static async Task<Anime[]> GetAnimeByName(string name)
    {
        return await Client._apiRequests.GetAnimesByName(name);
    }

    public static async Task<UserInfo> GetMyInfo()
    {
        return await Client._apiRequests.GetMyInfo();
    }
}