using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using ShikimoriMe;
using System.Threading.Tasks;

//Активность, в которую мы получаем callback при OAuth2, тут же и вызываем Authorize у Viewer
[Activity(Label = "MyOAuthCallbackActivity", Exported = true, NoHistory = true, LaunchMode = LaunchMode.SingleTask)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "shikimorime", DataHost = "oauth2shikimorimecallback")]
public class MyOAuthCallbackActivity : Activity
{
    protected override async void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        await AuthorizeUser();
    }

    private async Task AuthorizeUser()
    {
        Intent intent = Intent;
        if (intent != null && intent.Data != null)
        {
            Uri uri = intent.Data;
            await Viewer.Authorize(uri.Query.ToString());
        }

        intent = new Intent(this, typeof(MainScreenActivity));
        intent.AddFlags(ActivityFlags.ReorderToFront);
        StartActivity(intent);
        Finish();
    }
}