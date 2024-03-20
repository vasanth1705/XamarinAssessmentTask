using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using AndroidX.AppCompat.App;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace XamarinAssessmentTask.Core
{
	[Activity (Label = "BaseActivity")]			
	public class BaseActivity : AppCompatActivity
	{
        protected void NavigateToActivity<T>() where T : AppCompatActivity
        {
            Intent intent = new Intent(this, typeof(T));
            StartActivity(intent);
        }

        protected void ShowAlert(string title = "", string message = "", Action okAction = null)
        {
            AlertDialog.Builder alertBuilder = new AlertDialog.Builder(this);
            alertBuilder.SetTitle(title);
            alertBuilder.SetMessage(message);
            alertBuilder.SetPositiveButton("OK", (senderAlert, args) =>
            {
                okAction?.Invoke();
            });

            AlertDialog alertDialog = alertBuilder.Create();
            alertDialog.Show();
        }

        protected void ExecuteWithDelay(Action action, int delayMilliseconds = 2000)
        {
            Task.Delay(delayMilliseconds).ContinueWith(_ =>
            {
                RunOnUiThread(() =>
                {
                    action?.Invoke();
                });
            });
        }
    }
}