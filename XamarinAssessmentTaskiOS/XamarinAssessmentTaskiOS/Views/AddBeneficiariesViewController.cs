using System;
using System.Threading.Tasks;
using UIKit;
using XamarinAssessmentTaskiOS.Core;
using XamarinAssessmentTaskiOS.Enums;
using XamarinAssessmentTaskiOS.Models;
using XamarinAssessmentTaskiOS.Utilities;
using XamarinAssessmentTaskiOS.ViewModels;

namespace XamarinAssessmentTaskiOS.Views
{
    public partial class AddBeneficiariesViewController : BaseViewController
	{
        private UITapGestureRecognizer tapGesture;
        private AddBeneficiaryViewModel viewModel;

        public AddBeneficiariesViewController(IntPtr handle) : base(handle)
        {
        }

        public AddBeneficiariesViewController() { }

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
            UnsubscribeEventsAndGestures();
        }

        private void SetupViews()
        {
            this.Title = "Add Beneficiary";

            if (NameTextField != null) {
                NameTextField.Delegate = new CustomTextFieldDelegate();
            }

            tapGesture = new UITapGestureRecognizer(() => View.EndEditing(true));
            viewModel = new AddBeneficiaryViewModel();
            HideActivityIndicator();
        }

        private void NameTextField_EditingChanged(object sender, EventArgs e)
        {
            if (NameTextField.Text.Length > 20)
            {
                NameTextField.Text = NameTextField.Text.Substring(0, 20);
            }
        }

        private void MobileNumberTextField_EditingChanged(object sender, EventArgs e)
        {
            if (MobileNumberTextField.Text.Length > 8)
            {
                MobileNumberTextField.Text = MobileNumberTextField.Text.Substring(0,8);
            }
        }

        private void ShowActivityIndicator()
        {
            ActivityIndicator.Hidden = false;
            ActivityIndicator.StartAnimating();
        }

        private void HideActivityIndicator()
        {
            ActivityIndicator.Hidden = false;
            ActivityIndicator.StopAnimating();
        }

        private async void SaveButton_TouchUpInside(object sender, EventArgs e)
        {
            bool isVerified = VerifiedSwitch?.On ?? false;
            var saveBeneficiaryModel = new SaveBeneficiaryModel()
            {
                UserName = NameTextField.Text,
                CountryCode = CountryCodeTextField.Text,
                MobileNumber = MobileNumberTextField.Text,
                IsVerified = isVerified
            };
            ShowActivityIndicator();
           
            var result = viewModel.SaveBeneficiaryInDB(saveBeneficiaryModel);
            if (result == SaveBeneficiaryValidator.Success)
            {
                await Task.Delay(2000);
                HideActivityIndicator();
                ShowAlert(null, "Beneficiary was added successfully", () =>
                {
                    this.NavigationController.PopViewController(true);
                });
            }
            else if (result == SaveBeneficiaryValidator.ReachedMaxBeneficiary)
            {
                await Task.Delay(2000);
                HideActivityIndicator();
                ShowAlert("Alert", "You have added maximum of 5 active top-up beneficiaries");
            }
            else if (result == SaveBeneficiaryValidator.Error)
            {
                await Task.Delay(2000);
                HideActivityIndicator();
                ShowAlert("Alert", "Please enter required details");
            }
            else if (result == SaveBeneficiaryValidator.InvalidMobileNumber)
            {
                await Task.Delay(2000);
                HideActivityIndicator();
                ShowAlert("Alert", "Invalid mobile number");
            }
            else if (result == SaveBeneficiaryValidator.MobileNumberExists)
            {
                await Task.Delay(2000);
                HideActivityIndicator();
                ShowAlert("Alert", "Mobile number already exist");
            }
        }

        private void SubscribeEventsAndGestures() {
            if (SaveButton != null)
            {
                SaveButton.TouchUpInside += SaveButton_TouchUpInside;
            }
            if (NameTextField != null)
            {
                NameTextField.EditingChanged += NameTextField_EditingChanged;
            }
            if (MobileNumberTextField != null)
            {
                MobileNumberTextField.EditingChanged += MobileNumberTextField_EditingChanged;
            }
            View.AddGestureRecognizer(tapGesture);
        }

        private void UnsubscribeEventsAndGestures() {
            if (SaveButton != null)
            {
                SaveButton.TouchUpInside -= SaveButton_TouchUpInside;
            }
            if (NameTextField != null)
            {
                NameTextField.EditingChanged -= NameTextField_EditingChanged;
            }
            if (MobileNumberTextField != null)
            {
                MobileNumberTextField.EditingChanged -= MobileNumberTextField_EditingChanged;
            }
            View.RemoveGestureRecognizer(tapGesture);
        }
    }
}