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
	public partial class ChildBeneficiariesViewController : BaseViewController
	{
        private BeneficiaryCollectionViewSource source;
        private ChildBeneficiaryViewModel viewModel;

        public ChildBeneficiariesViewController(IntPtr handle) : base(handle)
        {
        }

        public ChildBeneficiariesViewController() { }

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            SetupViews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            SubscribeEvents();
            UpdateViews();
            UpdatebenefieciariesData();
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            UnSubscribeEvents();
        }

        private void SetupViews()
        {
            BeneficiariesCollectionView.Hidden = true;
            viewModel = new ChildBeneficiaryViewModel();
            SetupCollectionView();
        }

        private void SetupCollectionView()
        {
            if (BeneficiariesCollectionView != null)
            {
                BeneficiariesCollectionView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.08f);
                var key = BeneficiaryCell.Key;
                var nib = UINib.FromName(key, NSBundle.MainBundle);
                BeneficiariesCollectionView.RegisterNibForCell(nib, key);

                var layout = new UICollectionViewFlowLayout();
                var screenSize = UIScreen.MainScreen.Bounds.Width;
                var itemSize = screenSize / 2.5f;
                layout.ItemSize = new CGSize(itemSize, 100);
                layout.MinimumInteritemSpacing = 10;
                layout.MinimumLineSpacing = 10;
                layout.SectionInset = new UIEdgeInsets(10, 10, 10, 10);
                layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
                BeneficiariesCollectionView.SetCollectionViewLayout(layout, false);
                BeneficiariesCollectionView.ShowsHorizontalScrollIndicator = false;

                source = new BeneficiaryCollectionViewSource(viewModel.GetAllBeneficiaries());
                BeneficiariesCollectionView.Source = source;
            }
        }

        private void UpdatebenefieciariesData() {
            source.UpdateBeneficiaries(viewModel.GetAllBeneficiaries());
            BeneficiariesCollectionView.ReloadData();
        }

        private void UpdateViews() {
            if (viewModel.IsShowBeneficiaries()) {
                BeneficiariesCollectionView.Hidden = false;
                BeneficiaryNotFoundLabel.Hidden = true;
            } else {
                BeneficiariesCollectionView.Hidden = true;
                BeneficiaryNotFoundLabel.Hidden = false;
            }
        }

        private void SubscribeEvents() {
            if (AddBeneficiaryButton != null)
            {
                AddBeneficiaryButton.TouchUpInside += AddBeneficiaryButton_TouchUpInside;
            }
            if (source != null)
            {
                source.SelectedItem += Source_SelectedItem;
            }
        }

        private void UnSubscribeEvents()
        {
            if (AddBeneficiaryButton != null)
            {
                AddBeneficiaryButton.TouchUpInside -= AddBeneficiaryButton_TouchUpInside;
            }
            if (source != null)
            {
                source.SelectedItem -= Source_SelectedItem;
            }
        }

        private void Source_SelectedItem(object sender, BeneficiariesModel beneficiary)
        {
             this.PushViewController<RechargeViewController>( vc => {
                if (beneficiary != null)
                {
                    vc.beneficiary = beneficiary;
                }
            });
        }

        private void AddBeneficiaryButton_TouchUpInside(object sender, EventArgs e)
        {
            this.PushViewController<AddBeneficiariesViewController>();
        }
    }
}