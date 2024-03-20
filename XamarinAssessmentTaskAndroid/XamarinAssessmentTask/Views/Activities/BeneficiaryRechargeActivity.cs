using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using XamarinAssessmentTask.Adapters;
using XamarinAssessmentTask.Core;
using XamarinAssessmentTask.Models;
using XamarinAssessmentTask.ViewModels;

namespace XamarinAssessmentTask.Views.Activities
{
    [Activity (Label = "BeneficiaryRechargeActivity", Theme = "@style/AppTheme.NoActionBar")]			
	public class BeneficiaryRechargeActivity : BaseActivity
	{
        private BeneficiariesModel beneficiary;
        private PaymentModel payment;
        private TextView tvBeneficiaryName, tvMobile;
        private RecyclerView rvTopup;
        private BeneficiaryRechargeViewModel viewModel;
        private TopupAdapter adapter;
        private Button btnProceed;
        private ImageView ivVerified;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Layout.activity_benficiary_recharge);
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

        private void SetupToolBar()
        {
            var toolBar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            if (SupportActionBar != null)
            {
                SupportActionBar.Title = "Beneficiary Recharge";
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        private void SubscribeEvents()
        {
            if (adapter != null) {
                adapter.SelectedItem += Adapter_SelectedItem;
                adapter.DeSelectedItem += Adapter_DeSelectedItem;
            }
            if (btnProceed != null) {
                btnProceed.Click += BtnProceed_Click;
            }
        }

        private void UnSubscribeEvents()
        {
            if (adapter != null)
            {
                adapter.SelectedItem -= Adapter_SelectedItem;
                adapter.DeSelectedItem -= Adapter_DeSelectedItem;
            }
            if (btnProceed != null)
            {
                btnProceed.Click -= BtnProceed_Click;
            }
        }

        private void Adapter_DeSelectedItem(object sender, int amount)
        {
            payment.TopUpAmount = amount;
        }

        private void Adapter_SelectedItem(object sender, int amount)
        {
            payment.TopUpAmount = amount;
        }

        private void BtnProceed_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PaymentActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            payment.Username = beneficiary?.Name ?? "";
            payment.MobileNumber = beneficiary?.MobileNumber ?? "";
            payment.RechargeFee = 1;
            payment.BeneficiaryId = beneficiary.Id;
            if (payment.TopUpAmount == 0)
            {
                ShowAlert("Alert", "Please select top up amount");
                return;
            }
            var json = JsonConvert.SerializeObject(payment);
            intent.PutExtra("payment", json);
            StartActivity(intent);
        }

        private void FetchData() {
            var json = Intent.GetStringExtra("beneficiary");
            beneficiary = JsonConvert.DeserializeObject<BeneficiariesModel>(json);
            if (beneficiary != null) {
                tvBeneficiaryName.Text = beneficiary.Name ?? "";
                tvMobile.Text = beneficiary.MobileNumber ?? "";
                var isVerifiedUser = viewModel.IsVerifiedUser(beneficiary.Id);
                if (isVerifiedUser) {
                    ivVerified.Visibility = ViewStates.Visible;
                } else {
                    ivVerified.Visibility = ViewStates.Gone;
                }
            }
        }

        private void SetupViews() {
            tvBeneficiaryName = FindViewById<TextView>(Resource.Id.tv_beneficiary_name);
            tvMobile = FindViewById<TextView>(Resource.Id.tv_mobile);
            rvTopup = FindViewById<RecyclerView>(Resource.Id.rv_topup);
            btnProceed = FindViewById<Button>(Resource.Id.btn_proceed);
            ivVerified = FindViewById<ImageView>(Resource.Id.iv_verify);
            SetupToolBar();
            payment = new PaymentModel();
            viewModel = new BeneficiaryRechargeViewModel();
            adapter = new TopupAdapter(viewModel.FetchTopUpData());
            if (adapter != null)
            {
                rvTopup.SetAdapter(adapter);
            }
            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            rvTopup.SetLayoutManager(layoutManager);
        }
    }
}