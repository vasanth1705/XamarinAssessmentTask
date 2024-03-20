using System;
using Foundation;
using UIKit;
using XamarinAssessmentTaskiOS.ViewModels;
using XamarinAssessmentTaskiOS.Views.CustomCells;

namespace XamarinAssessmentTaskiOS
{
	public partial class HistoryViewController : UIViewController
	{
        private HistoryViewModel viewModel;

		public HistoryViewController (IntPtr handle) : base (handle)
		{
		}

		public HistoryViewController() { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            viewModel = new HistoryViewModel();
            SetUpTableView();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            UpdateViews();
        }

        private void SetUpTableView() {
            if (HistoryTableView != null) {
                var key = HistoryTableViewCell.Key;
                var nib = UINib.FromName(key, NSBundle.MainBundle);
                HistoryTableView.RegisterNibForCellReuse(nib, key);
                HistoryTableView.Source = new HistoryTableViewSource(viewModel.GetPaymentList());
                HistoryTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            }
        }

        private void UpdateViews()
        {
            if (viewModel.IsShowPaymentHistory())
            {
                HistoryTableView.Hidden = false;
                NoHistoryFoundLabel.Hidden = true;
            }
            else
            {
                HistoryTableView.Hidden = true;
                NoHistoryFoundLabel.Hidden = false;
            }
        }
    }
}