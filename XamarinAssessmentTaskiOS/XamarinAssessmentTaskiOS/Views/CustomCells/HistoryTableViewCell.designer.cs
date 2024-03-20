// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace XamarinAssessmentTaskiOS.Views.CustomCells
{
	[Register ("HistoryTableViewCell")]
	partial class HistoryTableViewCell
	{
		[Outlet]
		UIKit.UIView MainView { get; set; }

		[Outlet]
		UIKit.UILabel MobileNumberLabel { get; set; }

		[Outlet]
		UIKit.UILabel RechargeFeeLabel { get; set; }

		[Outlet]
		UIKit.UILabel TopUpAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel TotalAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MainView != null) {
				MainView.Dispose ();
				MainView = null;
			}

			if (MobileNumberLabel != null) {
				MobileNumberLabel.Dispose ();
				MobileNumberLabel = null;
			}

			if (RechargeFeeLabel != null) {
				RechargeFeeLabel.Dispose ();
				RechargeFeeLabel = null;
			}

			if (TopUpAmountLabel != null) {
				TopUpAmountLabel.Dispose ();
				TopUpAmountLabel = null;
			}

			if (TotalAmountLabel != null) {
				TotalAmountLabel.Dispose ();
				TotalAmountLabel = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}
		}
	}
}
