using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using XamarinAssessmentTask.Core;
using XamarinAssessmentTask.Enums;
using XamarinAssessmentTask.Models;
using XamarinAssessmentTask.ViewModels;

namespace XamarinAssessmentTask.Views.Activities
{
    [Activity (Label = "AddBeneficiaryActivity", Theme = "@style/AppTheme.NoActionBar")]			
	public class AddBeneficiaryActivity : BaseActivity
	{
        private EditText etBeneficairyName, etCountryCode, etMobileNumber;
        private Button btnSave;
        private Switch switchVerified;
        private ProgressBar progressBar;

        private AddBeneficiaryViewModel viewModel;
        private bool isVerified;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            SetContentView(Resource.Layout.activity_add_beneficiary);
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
            etBeneficairyName = FindViewById<EditText>(Resource.Id.et_beneficairy_name);
            etCountryCode = FindViewById<EditText>(Resource.Id.et_country_code);
            etMobileNumber = FindViewById<EditText>(Resource.Id.et_mobile);
            btnSave = FindViewById<Button>(Resource.Id.btn_save);
            switchVerified = FindViewById<Switch>(Resource.Id.switch_verify);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            HideProgressBar();
            viewModel = new AddBeneficiaryViewModel();
		}

        private void SubscribeEvents()
        {
            if (btnSave != null)
            {
                btnSave.Click += BtnAddBeneficiary_Click;
            }
            if (etBeneficairyName != null) {
                etBeneficairyName.TextChanged += EtBeneficairyName_TextChanged;
            }
            if (etMobileNumber != null)
            {
                etMobileNumber.TextChanged += EtMobileNumber_TextChanged;
            }
            if (switchVerified != null)
            {
                switchVerified.CheckedChange += SwitchVerified_CheckedChange;
            }
        }

        private void UnSubscribeEvents()
        {
            if (btnSave != null)
            {
                btnSave.Click -= BtnAddBeneficiary_Click;
            }
        }

        private void SetupToolBar()
        {
            var toolBar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            if (SupportActionBar != null) {
                SupportActionBar.Title = "Add Beneficiary";
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        private void EtBeneficairyName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (etBeneficairyName.Text.Length > 20)
            {
                etBeneficairyName.Text = etBeneficairyName.Text.Substring(0, 20);
                etBeneficairyName.SetSelection(etBeneficairyName.Text.Length);
            }
        }

        private void EtMobileNumber_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (etMobileNumber.Text.Length > 8)
            {
                etMobileNumber.Text = etMobileNumber.Text.Substring(0, 8);
                etMobileNumber.SetSelection(etMobileNumber.Text.Length);
            }
        }

        private void SwitchVerified_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            isVerified = e.IsChecked;
        }

        private void ShowProgressBar()
        {
            progressBar.Visibility = Android.Views.ViewStates.Visible;
        }

        private void HideProgressBar()
        {
            progressBar.Visibility = Android.Views.ViewStates.Gone;
        }

        
        private void BtnAddBeneficiary_Click(object sender, System.EventArgs e)
        {
            ShowProgressBar();
            var saveBeneficiaryModel = new SaveBeneficiaryModel()
            {
                UserName = etBeneficairyName.Text,
                CountryCode = etCountryCode.Text,
                MobileNumber = etMobileNumber.Text,
                IsVerified = isVerified
            };

            var result = viewModel.SaveBeneficiaryInDB(saveBeneficiaryModel);

            Action showAlertAction = null;

            if (result == SaveBeneficiaryValidator.Success)
            {
                showAlertAction = () =>
                {
                    HideProgressBar();
                    ShowAlert(null, "Beneficiary was added successfully", () => { Finish(); });
                };
            }
            else if (result == SaveBeneficiaryValidator.ReachedMaxBeneficiary)
            {
                showAlertAction = () =>
                {
                    HideProgressBar();
                    ShowAlert("Alert", "You have reached the maximum of 5 active top-up beneficiaries");
                };
            }
            else if (result == SaveBeneficiaryValidator.Error)
            {
                showAlertAction = () =>
                {
                    HideProgressBar();
                    ShowAlert("Alert", "Please enter the required details");
                };
            }
            else if (result == SaveBeneficiaryValidator.InvalidMobileNumber)
            {
                showAlertAction = () =>
                {
                    HideProgressBar();
                    ShowAlert("Alert", "Invalid mobile number");
                };
            }
            else if (result == SaveBeneficiaryValidator.MobileNumberExists)
            {
                showAlertAction = () =>
                {
                    HideProgressBar();
                    ShowAlert("Alert", "Mobile number already exist");
                };
            }
            ExecuteWithDelay(showAlertAction);
        }
    }
}