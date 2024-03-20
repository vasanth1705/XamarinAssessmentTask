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
	[Register ("ChildBeneficiariesViewController")]
	partial class ChildBeneficiariesViewController
	{
		[Outlet]
		UIKit.UIButton AddBeneficiaryButton { get; set; }

		[Outlet]
		UIKit.UICollectionView BeneficiariesCollectionView { get; set; }

		[Outlet]
		UIKit.UILabel BeneficiaryNotFoundLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddBeneficiaryButton != null) {
				AddBeneficiaryButton.Dispose ();
				AddBeneficiaryButton = null;
			}

			if (BeneficiariesCollectionView != null) {
				BeneficiariesCollectionView.Dispose ();
				BeneficiariesCollectionView = null;
			}

			if (BeneficiaryNotFoundLabel != null) {
				BeneficiaryNotFoundLabel.Dispose ();
				BeneficiaryNotFoundLabel = null;
			}
		}
	}
}
