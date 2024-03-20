using System;
using System.Threading.Tasks;
using UIKit;
using XamarinAssessmentTaskiOS.Core;
using XamarinAssessmentTaskiOS.Models;
using XamarinAssessmentTaskiOS.ViewModels;

namespace XamarinAssessmentTaskiOS.Views
{
	public partial class PaymentViewController : BaseViewController
	{
        public PaymentModel payment;
        private PaymentViewModel viewModel;

        public PaymentViewController(IntPtr handle) : base(handle) { }
        
        public PaymentViewController() { }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Payment";
            SetupViews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            FetchData();
            SubscribeEvents();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            UnSubscribeEvents();
        }

        private void SetupViews() {
            PaymentDetailBackgroundView.Layer.CornerRadius = 8;
            PaymentDetailBackgroundView.BackgroundColor = UIColor.Link.ColorWithAlpha(0.11f);
            TotalAmountBackgroundView.Layer.CornerRadius = 8;
            TotalAmountBackgroundView.BackgroundColor = UIColor.SystemGreen.ColorWithAlpha(0.11f);
            viewModel = new PaymentViewModel();
            HideActivityIndicator();
        }

        private void FetchData() {
            if (payment != null)
            {
                UsernameLabel.Text = UsernameLabel.Text + payment.Username ?? "";
                MobileNumberLabel.Text = MobileNumberLabel.Text + payment.MobileNumber ?? "";
                TopUpAmountLabel.Text = TopUpAmountLabel.Text + payment.TopUpAmount.ToString() + " AED";
                RechargeFeeLabel.Text = RechargeFeeLabel.Text + payment.RechargeFee.ToString() + " AED";
                var totalAmount = (payment.TopUpAmount + payment.RechargeFee).ToString();
                TotalAmountLabel.Text = TotalAmountLabel.Text + totalAmount + " AED";
                MyBalanceLabel.Text = viewModel.FetchMyBalance().ToString() + " AED";
            }
        }

        private async void PaymentButton_TouchUpInside(object sender, EventArgs e)
        {
            ShowActivityIndicator();
            var result = viewModel.TopUpAction(payment);
            switch (result)
            {
                case Enums.TopUpProcessValidator.Success:
                    await Task.Delay(2000);
                    HideActivityIndicator();
                    MyBalanceLabel.Text = viewModel.FetchMyBalance().ToString() + " AED";
                    ShowAlert(null, "Recharge successfull", () =>
                    {
                        if (NavigationController.ViewControllers.Length >= 3)
                        {
                            var childBeneficiariesViewController = NavigationController.ViewControllers[1];
                            NavigationController.PopToViewController(childBeneficiariesViewController, true);
                        }
                    });
                    break;
                case Enums.TopUpProcessValidator.InsufficientBalance:
                    await Task.Delay(2000);
                    HideActivityIndicator();
                    ShowAlert(null, "Insufficient balance to process the top-up.");
                    break;
                case Enums.TopUpProcessValidator.MaxMultiTopUpLimit:
                    await Task.Delay(2000);
                    HideActivityIndicator();
                    var multiBeneficiaryTopUpValue = 3000;
                    ShowAlert(null, $"Your multi-beneficiary top-up limit of {multiBeneficiaryTopUpValue} AED has been reached for this current month");
                    break;
                case Enums.TopUpProcessValidator.MaxTopUpLimit:
                    await Task.Delay(2000);
                    HideActivityIndicator();
                    var value = viewModel.IsVerifiedUser(payment.BeneficiaryId) ? 500 : 100;
                    ShowAlert(null, $"This amount exceeds the maximum top-up limit of {value} AED for this user this current month.");
                    break;
                case Enums.TopUpProcessValidator.Error:
                    await Task.Delay(2000);
                    HideActivityIndicator();
                    ShowAlert(null, "Something went wrong");
                    break;
                default:
                    break;
            }
        }

        private void SubscribeEvents() {
            if (PaymentButton != null)
            {
                PaymentButton.TouchUpInside += PaymentButton_TouchUpInside;
            }
        }

        private void UnSubscribeEvents() {
            if (PaymentButton != null)
            {
                PaymentButton.TouchUpInside -= PaymentButton_TouchUpInside;
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
    }
}