using Android.App;
using Android.Content;
using Android.Net;
using AndroidX.Browser.CustomTabs;

//CustomTab для логина в первый раз через браузер
namespace ShikimoriMe
{
    [Activity(Label = "AuthCustomTabActivity")]
    public class AuthCustomTabActivity : Activity
    {
        // ...

        public void OpenCustomTabForAuthorization()
        {
            string authorizationUrl = ClientConstants.AuthCodeUri;

            CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
            CustomTabsIntent customTabsIntent = builder.Build();

            customTabsIntent.Intent.SetFlags(ActivityFlags.NewTask);
            customTabsIntent.LaunchUrl(Application.Context, Uri.Parse(authorizationUrl));
        }

        // ...
    }
}