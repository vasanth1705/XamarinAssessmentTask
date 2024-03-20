// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinAssessmentTaskiOS.Views
{
	[Register ("PaymentViewController")]
	partial class PaymentViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UILabel MobileNumberLabel { get; set; }

		[Outlet]
		UIKit.UILabel MyBalanceLabel { get; set; }

		[Outlet]
		UIKit.UIButton PaymentButton { get; set; }

		[Outlet]
		UIKit.UIView PaymentDetailBackgroundView { get; set; }

		[Outlet]
		UIKit.UILabel RechargeFeeLabel { get; set; }

		[Outlet]
		UIKit.UILabel TopUpAmountLabel { get; set; }

		[Outlet]
		UIKit.UIView TotalAmountBackgroundView { get; set; }

		[Outlet]
		UIKit.UILabel TotalAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel UsernameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MobileNumberLabel != null) {
				MobileNumberLabel.Dispose ();
				MobileNumberLabel = null;
			}

			if (MyBalanceLabel != null) {
				MyBalanceLabel.Dispose ();
				MyBalanceLabel = null;
			}

			if (PaymentButton != null) {
				PaymentButton.Dispose ();
				PaymentButton = null;
			}

			if (PaymentDetailBackgroundView != null) {
				PaymentDetailBackgroundView.Dispose ();
				PaymentDetailBackgroundView = null;
			}

			if (RechargeFeeLabel != null) {
				RechargeFeeLabel.Dispose ();
				RechargeFeeLabel = null;
			}

			if (TopUpAmountLabel != null) {
				TopUpAmountLabel.Dispose ();
				TopUpAmountLabel = null;
			}

			if (TotalAmountBackgroundView != null) {
				TotalAmountBackgroundView.Dispose ();
				TotalAmountBackgroundView = null;
			}

			if (TotalAmountLabel != null) {
				TotalAmountLabel.Dispose ();
				TotalAmountLabel = null;
			}

			if (UsernameLabel != null) {
				UsernameLabel.Dispose ();
				UsernameLabel = null;
			}

			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}
		}
	}
}
