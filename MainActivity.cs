using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Java.Net;
using ShikimoriMe.UiHelper;
using System;

//Login activity, здесь проверка на наличие refresh-token'а и запуск аутентификации
namespace ShikimoriMe
{
    [Activity(Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_login);
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 0, 0, 0));
            InitLoginButton();
            
        }
        private void InitLoginButton()
        {
            this.FindViewById<Button>(Resource.Id.loginButton).Click += async (object sender, EventArgs e) => {
                try
                {
                    if (!Viewer.HasRefreshToken())
                    {
                        Intent intent = new Intent(this, typeof(AuthCustomTabActivity));
                        var yourActivityInstance = new AuthCustomTabActivity();
                        yourActivityInstance.OpenCustomTabForAuthorization();
                    }
                    else
                    {
                        await Viewer.Login();
                        StartActivity(new Intent(Application.Context, typeof(MainScreenActivity)));
                    }
                }
                catch(UnknownHostException ex)
                {
                    DialogConstructor dialog = new DialogConstructor("Internet issue. Try again later.", this);
                    dialog.ShowDialog();
                }
                catch (Exception ex)
                {
                    DialogConstructor dialog = new DialogConstructor("Unknown exception. Please, report this.", this);
                }
            };
        }

    }
}