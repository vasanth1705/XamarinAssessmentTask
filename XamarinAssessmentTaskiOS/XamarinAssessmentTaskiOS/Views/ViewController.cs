using System;
using UIKit;
using XamarinAssessmentTaskiOS.Core;
using XamarinAssessmentTaskiOS.Enums;
using XamarinAssessmentTaskiOS.Utilities;
using XamarinAssessmentTaskiOS.ViewModels;
using XamarinAssessmentTaskiOS.Views;

namespace XamarinAssessmentTaskiOS
{
    public partial class ViewController : BaseViewController
    {
        private UITapGestureRecognizer tapGesture;
        private LoginViewModel viewModel;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public ViewController() { }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            SetupViews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SubscribeEventsAndGestures();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            UnSubscribeEventsAndGestures();
        }

        private void SetupViews() {
            viewModel = new LoginViewModel();
            if (UsernameTextfield != null) {
                UsernameTextfield.Delegate = new CustomTextFieldDelegate();
            }
            if (PasswordTextField != null)
            {
                PasswordTextField.Delegate = new CustomTextFieldDelegate();
            }
            tapGesture = new UITapGestureRecognizer(() => View.EndEditing(true));
        }

        private void SubscribeEventsAndGestures() {
            if (LogInButton != null)
            {
                LogInButton.TouchUpInside += LogInButton_TouchUpInside;
            }
            View.AddGestureRecognizer(tapGesture);
        }

        private void UnSubscribeEventsAndGestures() {
            if (LogInButton != null)
            {
                LogInButton.TouchUpInside -= LogInButton_TouchUpInside;
            }
            View.RemoveGestureRecognizer(tapGesture);
        }

        private void LogInButton_TouchUpInside(object sender, EventArgs e)
        {
            var loginResult = viewModel.DoLogin(UsernameTextfield.Text, PasswordTextField.Text);
            switch (loginResult)
            {
                case LoginValidator.Success:
                    this.PushViewController<BeneficiariesViewController>();
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