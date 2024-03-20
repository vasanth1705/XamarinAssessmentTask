using System;
using Foundation;
using UIKit;
using XamarinAssessmentTaskiOS.Models;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	public partial class BeneficiaryCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("BeneficiaryCell");
		public static readonly UINib Nib;

		static BeneficiaryCell ()
		{
			Nib = UINib.FromName ("BeneficiaryCell", NSBundle.MainBundle);
		}

		protected BeneficiaryCell (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void SetData(BeneficiariesModel beneficiary) {
			ContentView.Layer.CornerRadius = 8;
			RechargeNowLabel.Layer.CornerRadius = RechargeNowLabel.Bounds.Height / 2;
			RechargeNowLabel.ClipsToBounds = true;
			NameLabel.Text = beneficiary.Name ?? "";
			MobileNumberLabel.Text = beneficiary.MobileNumber ?? "";
		}
	}
}