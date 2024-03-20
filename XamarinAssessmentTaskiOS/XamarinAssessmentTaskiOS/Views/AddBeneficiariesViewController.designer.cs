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
	[Register ("AddBeneficiariesViewController")]
	partial class AddBeneficiariesViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UITextField CountryCodeTextField { get; set; }

		[Outlet]
		UIKit.UITextField MobileNumberTextField { get; set; }

		[Outlet]
		UIKit.UITextField NameTextField { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }

		[Outlet]
		UIKit.UISwitch VerifiedSwitch { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CountryCodeTextField != null) {
				CountryCodeTextField.Dispose ();
				CountryCodeTextField = null;
			}

			if (MobileNumberTextField != null) {
				MobileNumberTextField.Dispose ();
				MobileNumberTextField = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}

			if (VerifiedSwitch != null) {
				VerifiedSwitch.Dispose ();
				VerifiedSwitch = null;
			}

			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}
		}
	}
}
