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
	[Register ("RechargeViewController")]
	partial class RechargeViewController
	{
		[Outlet]
		UIKit.UIImageView IsVerifiedImageView { get; set; }

		[Outlet]
		UIKit.UILabel MobileNumberLabel { get; set; }

		[Outlet]
		UIKit.UIButton ProceedButton { get; set; }

		[Outlet]
		UIKit.UICollectionView TopUpCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel UserNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MobileNumberLabel != null) {
				MobileNumberLabel.Dispose ();
				MobileNumberLabel = null;
			}

			if (ProceedButton != null) {
				ProceedButton.Dispose ();
				ProceedButton = null;
			}

			if (TopUpCollectionView != null) {
				TopUpCollectionView.Dispose ();
				TopUpCollectionView = null;
			}

			if (UserNameLabel != null) {
				UserNameLabel.Dispose ();
				UserNameLabel = null;
			}

			if (IsVerifiedImageView != null) {
				IsVerifiedImageView.Dispose ();
				IsVerifiedImageView = null;
			}
		}
	}
}
