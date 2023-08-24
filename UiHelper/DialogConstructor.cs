using Android.Content;

namespace ShikimoriMe.UiHelper
{
    class DialogConstructor
    {
        Android.App.AlertDialog.Builder builder;
        Android.App.AlertDialog dialog;
        public DialogConstructor(string message, Context context)
        {
            builder = new Android.App.AlertDialog.Builder(context);
            builder.SetMessage(message);
            builder.SetPositiveButton("OK", (sender, args) =>
            {
                (sender as Android.App.AlertDialog).Cancel();
            });

            dialog = builder.Create();
        }

        public void ShowDialog() => dialog.Show();

    }
}