using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using XamarinAssessmentTask.Core;
using XamarinAssessmentTask.Models;
using XamarinAssessmentTask.Utilities;
using XamarinAssessmentTask.ViewModels;

namespace XamarinAssessmentTask.Views.Activities
{
    [Activity (Label = "PaymentActivity", Theme = "@style/AppTheme.NoActionBar")]			
	public class PaymentActivity : BaseActivity
	{
        private Button btnPayment;
        private PaymentModel payment;
        private TextView tvMyBalance, tvBeneficiaryName, tvMobile, tvTopupAmount, tvRechargeFee, tvTotalAmount;
        private PaymentViewModel viewModel;
        private ProgressBar progressBar;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Layout.activity_payment);
            SetupViews();
        }

        protected override void OnStart()
        {
            base.OnStart();
            FetchData();
            SubscribeEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            if (btnPayment != null) {
                btnPayment.Click += BtnPayment_Click;
            }
        }

        private void UnSubscribeEvents()
        {
            if (btnPayment != null)
            {
                btnPayment.Click -= BtnPayment_Click;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void SetupViews() {
            SetupToolBar();
            btnPayment = FindViewById<Button>(Resource.Id.btn_payment);
            tvMyBalance = FindViewById<TextView>(Resource.Id.tv_my_balance);
            tvBeneficiaryName = FindViewById<TextView>(Resource.Id.tv_beneficiary_name);
            tvMobile = FindViewById<TextView>(Resource.Id.tv_mobile);
            tvRechargeFee = FindViewById<TextView>(Resource.Id.tv_recharge_fee);
            tvTopupAmount = FindViewById<TextView>(Resource.Id.tv_topup_amount);
            tvTotalAmount = FindViewById<TextView>(Resource.Id.tv_total_amount);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            HideProgressBar();
            SharedPrefHelper.EnsureMyBalanceInitialized(this);
            viewModel = new PaymentViewModel();
        }

        private void SetupToolBar()
        {
            var toolBar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            if (SupportActionBar != null)
            {
                SupportActionBar.Title = "Payment";
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        private void FetchData() {
            var json = Intent.GetStringExtra("payment");
            payment = JsonConvert.DeserializeObject<PaymentModel>(json);
            if (payment != null) {
                tvMyBalance.Text = viewModel.FetchMyBalance(this).ToString() + " AED" ?? "";
                tvBeneficiaryName.Text = "Beneficiary Name : " + payment.Username ?? "";
                tvMobile.Text = "Mobile Number : " + payment.MobileNumber ?? "";
                tvRechargeFee.Text = "Recharge Fee : " + payment.RechargeFee.ToString() + " AED";
                tvTopupAmount.Text = "Top Up Amount : " + payment.TopUpAmount.ToString() + " AED";
                var totalAmount = (payment.TopUpAmount + payment.RechargeFee).ToString();
                tvTotalAmount.Text = "Total Amount : " + totalAmount + " AED";
            }
        }

        private void ShowProgressBar()
        {
            progressBar.Visibility = Android.Views.ViewStates.Visible;
        }

        private void HideProgressBar()
        {
            progressBar.Visibility = Android.Views.ViewStates.Gone;
        }

        private void BtnPayment_Click(object sender, EventArgs e)
        {
            ShowProgressBar();
            var result = viewModel.TopUpAction(payment, this);

            switch (result)
            {
                case Enums.TopUpProcessValidator.Success:
                    ExecuteWithDelay(() =>
                    {
                        tvMyBalance.Text = viewModel.FetchMyBalance(this).ToString() + " AED" ?? "";
                        HideProgressBar();
                        ShowAlert(null, "Recharge successful", () =>
                        {
                            Intent intent = new Intent(this, typeof(RechargeActivity));
                            intent.AddFlags(ActivityFlags.ClearTop);
                            StartActivity(intent);
                        });
                    });
                    break;
                case Enums.TopUpProcessValidator.InsufficientBalance:
                    ExecuteWithDelay(() =>
                    {
                        HideProgressBar();
                        ShowAlert(null, "Insufficient balance to process the top-up.");
                    });
                    break;
                case Enums.TopUpProcessValidator.MaxMultiTopUpLimit:
                    ExecuteWithDelay(() =>
                    {
                        HideProgressBar();
                        var value = 3000;
                        ShowAlert(null, $"Your multi-beneficiary top-up limit of {value} AED has been reached for this current month");
                    });
                    break;
                case Enums.TopUpProcessValidator.MaxTopUpLimit:
                    ExecuteWithDelay(() =>
                    {
                        HideProgressBar();
                        var value = viewModel.IsVerifiedUser(payment.BeneficiaryId) ? 500 : 100;
                        ShowAlert(null, $"This amount exceeds the maximum top-up limit of {value} AED for this user in the current month.");
                    });
                    break;
                case Enums.TopUpProcessValidator.Error:
                    ExecuteWithDelay(() =>
                    {
                        HideProgressBar();
                        ShowAlert(null, "Something went wrong");
                    });
                    break;
                default:
                    break;
            }
        }
    }
}