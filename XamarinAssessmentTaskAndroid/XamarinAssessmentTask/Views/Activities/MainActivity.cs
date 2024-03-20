using Android.App;
using Android.OS;
using Android.Widget;
using XamarinAssessmentTask.Core;
using XamarinAssessmentTask.Enums;
using XamarinAssessmentTask.ViewModels;
using XamarinAssessmentTask.Views;

namespace XamarinAssessmentTask
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : BaseActivity
    {
        private EditText etUsername, etPassword;
        private Button btnLogin;
        private LoginViewModel viewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            SetupViews();
        }

        protected override void OnStart()
        {
            base.OnStart();
            SubscribeEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnSubscribeEvents();
        }

        private void SetupViews() {
            etUsername = FindViewById<EditText>(Resource.Id.et_username);
            etPassword = FindViewById<EditText>(Resource.Id.et_password);
            btnLogin = FindViewById<Button>(Resource.Id.btn_login);
            viewModel = new LoginViewModel();
        }

        private void SubscribeEvents() {
            if (btnLogin != null) {
                btnLogin.Click += BtnLogin_Click;
            }
        }

        private void UnSubscribeEvents() {
            if (btnLogin != null)
            {
                btnLogin.Click -= BtnLogin_Click;
            }
        }

        private void BtnLogin_Click(object sender, System.EventArgs e)
        {
            var loginResult = viewModel.DoLogin(etUsername.Text, etPassword.Text);
            switch (loginResult)
            {
                case LoginValidator.Success:
                    this.NavigateToActivity<RechargeActivity>();
                    break;
                case LoginValidator.EnterAllFields:
                    ShowAlert("Missing Information", "Please enter both username and password.");
                    break;
                case LoginValidator.InvalidUsernameOrPassword:
                    ShowAlert("Login Failed", "Invalid username or password.");
                    break;
                default:
                    ShowAlert("Error", "An unexpected error occurred.");
                    break;
            }
        }
    }
}