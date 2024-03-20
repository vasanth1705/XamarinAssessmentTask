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
	[Register ("BeneficiariesViewController")]
	partial class BeneficiariesViewController
	{
		[Outlet]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.UIView HistoryContainerView { get; set; }

		[Outlet]
		UIKit.UIView RechargeContainerView { get; set; }

		[Outlet]
		UIKit.UISegmentedControl SegmentControl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HistoryContainerView != null) {
				HistoryContainerView.Dispose ();
				HistoryContainerView = null;
			}

			if (RechargeContainerView != null) {
				RechargeContainerView.Dispose ();
				RechargeContainerView = null;
			}

			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (SegmentControl != null) {
				SegmentControl.Dispose ();
				SegmentControl = null;
			}
		}
	}
}
