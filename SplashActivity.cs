using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using AndroidX.AppCompat.App;


//Замена стандартного splashscreen xamarin на кастомный
namespace ShikimoriMe
{
    [Activity(MainLauncher = true, Theme = "@style/SplashTheme", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}