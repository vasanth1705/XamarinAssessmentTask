using System;
using CoreGraphics;
using Foundation;
using UIKit;
using XamarinAssessmentTaskiOS.Core;
using XamarinAssessmentTaskiOS.Models;
using XamarinAssessmentTaskiOS.ViewModels;
using XamarinAssessmentTaskiOS.Views.CustomCells;

namespace XamarinAssessmentTaskiOS.Views
{
	public partial class RechargeViewController : BaseViewController
	{
        private TopUpCollectionViewSource source;
        public BeneficiariesModel beneficiary;
        private PaymentModel payment;
        private RechargeViewModel viewModel;

        public RechargeViewController(IntPtr handle) : base(handle)
        {
        }

        public RechargeViewController() { }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            SetupViews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SubscribeEvents();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            UnSubscribeEvents();
        }

        private void SetupViews() {
            this.Title = "Beneficiary Recharge";
            NavigationItem.BackButtonTitle = "Back";
            payment = new PaymentModel();
            viewModel = new RechargeViewModel();
            FetchData();
            SetupCollectionView();
        }

        private void SetupCollectionView() {
            if (TopUpCollectionView != null)
            {
                TopUpCollectionView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.08f);
                TopUpCollectionView.AllowsSelection = true;
                TopUpCollectionView.AllowsMultipleSelection = false;

                var key = TopUpCollectionViewCell.Key;
                var nib = UINib.FromName(key, NSBundle.MainBundle);
                TopUpCollectionView.RegisterNibForCell(nib, key);

                var layout = new UICollectionViewFlowLayout();
                layout.ItemSize = new CGSize(100, 100);
                layout.MinimumInteritemSpacing = 10;
                layout.MinimumLineSpacing = 10;
                layout.SectionInset = new UIEdgeInsets(10, 10, 10, 10);
                layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
                TopUpCollectionView.SetCollectionViewLayout(layout, false);
                TopUpCollectionView.ShowsHorizontalScrollIndicator = false;

                source = new TopUpCollectionViewSource(viewModel.FetchTopUpData());
                TopUpCollectionView.Source = source;
            }
        }

        private void SubscribeEvents() {
            if (ProceedButton != null)
            {
                ProceedButton.TouchUpInside += ProceedButton_TouchUpInside;
            }
            if (source != null)
            {
                source.SelectedItem += Source_SelectedItem;
                source.DeSelectedItem += Source_DeSelectedItem;
            }
        }

        private void UnSubscribeEvents() {
            if (ProceedButton != null)
            {
                ProceedButton.TouchUpInside -= ProceedButton_TouchUpInside;
            }
            if (source != null)
            {
                source.SelectedItem -= Source_SelectedItem;
                source.DeSelectedItem -= Source_DeSelectedItem;
            }
        }

        private void FetchData() {
            if (beneficiary != null) {
                UserNameLabel.Text = beneficiary.Name ?? "";
                MobileNumberLabel.Text = beneficiary.MobileNumber ?? "";
                payment.BeneficiaryId = beneficiary.Id;
                var isVerifiedUser = viewModel.IsVerifiedUser(beneficiary.Id);
                IsVerifiedImageView.Hidden = !isVerifiedUser;
            }
        }

        private void ProceedButton_TouchUpInside(object sender, EventArgs e)
        {
            this.PushViewController<PaymentViewController>(vc => {
                payment.Username = beneficiary?.Name ?? "";
                payment.MobileNumber = beneficiary?.MobileNumber ?? "";
                payment.RechargeFee = 1;
                if (payment.TopUpAmount == 0)
                {
                    ShowAlert("Alert", "Please select top up amount");
                    return;
                }
                vc.payment = payment;
            });
        }

        private void Source_SelectedItem(object sender, int amount)
        {
            payment.TopUpAmount = amount;
        }

        private void Source_DeSelectedItem(object sender, int amount)
        {
            payment.TopUpAmount = amount;
        }
    }
}