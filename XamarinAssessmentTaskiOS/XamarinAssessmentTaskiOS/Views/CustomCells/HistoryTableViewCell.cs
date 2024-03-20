using System;
using Foundation;
using UIKit;
using XamarinAssessmentTaskiOS.Models;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	public partial class HistoryTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("HistoryTableViewCell");
		public static readonly UINib Nib;

		static HistoryTableViewCell ()
		{
			Nib = UINib.FromName ("HistoryTableViewCell", NSBundle.MainBundle);
		}

		protected HistoryTableViewCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void ConfigureCell(PaymentModel payment) {
			MainView.Layer.CornerRadius = 8;
			MainView.BackgroundColor = UIColor.Link.ColorWithAlpha(0.11f);
			this.UserNameLabel.Text = "Beneficiary Name : " + payment?.Username ?? "";
			this.MobileNumberLabel.Text = "Mobile Number : " + payment?.MobileNumber ?? "";
			this.TopUpAmountLabel.Text = "Top Up Amount : " + payment?.TopUpAmount.ToString() + " AED";
			this.RechargeFeeLabel.Text = "Recharage Fee : " + payment?.RechargeFee.ToString() + " AED";
			this.TotalAmountLabel.Text = "Total Amount : " + (payment?.TopUpAmount + payment?.RechargeFee).ToString() + " AED";
		}
    }
}