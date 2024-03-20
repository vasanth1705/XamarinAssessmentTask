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
	[Register ("TopUpCollectionViewCell")]
	partial class TopUpCollectionViewCell
	{
		[Outlet]
		UIKit.UILabel AEDLabel { get; set; }

		[Outlet]
		UIKit.UILabel AmountLabel { get; set; }

		[Outlet]
		UIKit.UIView ContentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AmountLabel != null) {
				AmountLabel.Dispose ();
				AmountLabel = null;
			}

			if (AEDLabel != null) {
				AEDLabel.Dispose ();
				AEDLabel = null;
			}

			if (ContentView != null) {
				ContentView.Dispose ();
				ContentView = null;
			}
		}
	}
}
