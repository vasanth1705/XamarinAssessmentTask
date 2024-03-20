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
	[Register ("BeneficiaryCell")]
	partial class BeneficiaryCell
	{
		[Outlet]
		UIKit.UIView ContentView { get; set; }

		[Outlet]
		UIKit.UILabel MobileNumberLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel RechargeNowLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentView != null) {
				ContentView.Dispose ();
				ContentView = null;
			}

			if (MobileNumberLabel != null) {
				MobileNumberLabel.Dispose ();
				MobileNumberLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (RechargeNowLabel != null) {
				RechargeNowLabel.Dispose ();
				RechargeNowLabel = null;
			}
		}
	}
}
